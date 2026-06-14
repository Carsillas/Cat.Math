namespace Cat.Math;

// ReSharper disable once InconsistentNaming
public readonly record struct fixed64 {
	private const long Scale = uint.MaxValue + 1L;

	private readonly long _internalValue;

	public static readonly fixed64 Zero = new fixed64(0, 0);
	public static readonly fixed64 Epsilon = new fixed64(0, 1);
	public static readonly fixed64 Half = new fixed64(0, 2_147_483_648);
	public static readonly fixed64 One = new fixed64(1, 0);
	public static readonly fixed64 Two = new fixed64(2, 0);
	public static readonly fixed64 HalfPi = new fixed64(1, 2_451_551_556);
	public static readonly fixed64 Pi = new fixed64(3, 608_135_817);
	public static readonly fixed64 TwoPi = new fixed64(6, 1_216_271_633);
	public static readonly fixed64 MinValue = new fixed64(int.MinValue, 0);
	public static readonly fixed64 MaxValue = new fixed64(int.MaxValue, uint.MaxValue);

	public int Integral => unchecked((int)(_internalValue >>> 32));
	public uint Fractional => (uint)(_internalValue & 0xffffffff);

	public fixed64(int integral, uint fractional) {
		_internalValue = ((long)integral << 32) | fractional;
	}

	public fixed64(fixed32 value) {
		_internalValue = ((long)value.Integral << 32) | ((long)value.Fractional << 16);
	}

	private fixed64(long value) { 
		_internalValue = value;
	}
	
	public static fixed64 FromQuotient(int numerator, int denominator) {
		return new fixed64(numerator, 0) / new fixed64(denominator, 0);
	}

	public static fixed64 operator +(fixed64 a, fixed64 b) {
		return new fixed64(a._internalValue + b._internalValue);
	}

	public static fixed64 operator -(fixed64 a, fixed64 b) {
		return new fixed64(a._internalValue - b._internalValue);
	}

	public static fixed64 operator -(fixed64 a) {
		return a * new fixed64(-1, 0);
	}

	public static fixed64 operator *(fixed64 a, fixed64 b) {
		Int128 product = (Int128)a._internalValue * b._internalValue;
		product /= Scale;
		return new fixed64((long)product);
	}

	public static fixed64 operator /(fixed64 a, fixed64 b) {
		Int128 quotient = (Int128)a._internalValue * Scale / b._internalValue;
		return new fixed64((long)quotient);
	}

	public static fixed64 Abs(fixed64 a) {
		return new fixed64(a._internalValue < 0 ? -a._internalValue : a._internalValue);
	}

	public static fixed64 Sqrt(fixed64 a) {
		if (a < Zero) {
			throw new ArgumentOutOfRangeException(nameof(a));
		}

		if (a == Zero) {
			return Zero;
		}
		
		if (a == Epsilon) {
			return fixed32.Epsilon;
		}

		fixed64 x = a / Two;
		fixed64 xSquared = x * x;
		fixed64 error = Abs(xSquared - a);

		while (true) {
			fixed64 nextGuess = x / Two + a / (Two * x);
			fixed64 nextGuessSquared = nextGuess * nextGuess;
			fixed64 nextError = Abs(nextGuessSquared - a);

			if (Abs(nextGuess - x) <= Epsilon) {
				return error < nextError ? x : nextGuess;
			}

			x = nextGuess;
			error = nextError;
		}
	}

	public static bool operator <(fixed64 a, fixed64 b) {
		return a._internalValue < b._internalValue;
	}

	public static bool operator <=(fixed64 a, fixed64 b) {
		return a._internalValue <= b._internalValue;
	}

	public static bool operator >(fixed64 a, fixed64 b) {
		return a._internalValue > b._internalValue;
	}

	public static bool operator >=(fixed64 a, fixed64 b) {
		return a._internalValue >= b._internalValue;
	}

	public static explicit operator fixed32(fixed64 a) {
		return new fixed32((short)a.Integral, (ushort)(a.Fractional >>> 16));
	}

	public static implicit operator fixed64(fixed32 a) {
		return new fixed64(a.Integral, (uint)a.Fractional << 16);
	}

	public override string ToString() {
		return $"{Integral}+{Fractional}/4.294b";
	}
}