using System.Globalization;
using Cat.Math.Lookups;

namespace Cat.Math;

// ReSharper disable once InconsistentNaming
public readonly record struct fixed32 {

	private const int Scale = ushort.MaxValue + 1;
	
	private readonly int _internalValue;

	public static readonly fixed32 Zero = new fixed32(0, 0);
	public static readonly fixed32 Epsilon = new fixed32(0, 1);
	public static readonly fixed32 Half = new fixed32(0, 32768);
	public static readonly fixed32 One = new fixed32(1, 0);
	public static readonly fixed32 Two = new fixed32(2, 0);
	public static readonly fixed32 Ten = new fixed32(10, 0);
	public static readonly fixed32 HalfPi = new fixed32(1, 37408);
	public static readonly fixed32 Pi = new fixed32(3, 9279);
	public static readonly fixed32 TwoPi = new fixed32(6, 18559);
	public static readonly fixed32 MinValue = new fixed32(short.MinValue, 0);
	public static readonly fixed32 MaxValue = new fixed32(short.MaxValue, ushort.MaxValue);

	public short Integral => unchecked((short)(_internalValue >>> 16));
	public ushort Fractional => (ushort)(_internalValue & 0xffff);
	public float Float => (float)this;
	
	public fixed32(short integral, ushort fractional) {
		_internalValue = (integral << 16) | fractional;
	}
	
	private fixed32(int value) {
		_internalValue = value;
	}

	public static fixed32 FromQuotient(short numerator, short denominator) {
		return new fixed32(numerator, 0) / new fixed32(denominator, 0);
	}

	public static fixed32 operator +(fixed32 a, fixed32 b) {
		return new fixed32(a._internalValue + b._internalValue);
	}
	
	public static fixed32 operator -(fixed32 a, fixed32 b) {
		return new fixed32(a._internalValue - b._internalValue);
	}
	
	public static fixed32 operator -(fixed32 a) {
		return a * new fixed32(-1, 0);
	}
	
	public static fixed32 operator *(fixed32 a, fixed32 b) {
		long product = (long)a._internalValue * b._internalValue;
		product /= Scale;
		return new fixed32((int)product);
	}
	
	public static fixed32 operator /(fixed32 a, fixed32 b) {
		long quotient = (long)a._internalValue * Scale / b._internalValue;
		return new fixed32((int)quotient);
	}
	
	public static fixed32 operator %(fixed32 a, fixed32 b) {
		fixed32 quotient = Truncate(a / b);
		fixed32 product = quotient * b;
		fixed32 modulo = a - product;

		return a < Zero && modulo < Zero || a >= Zero && modulo >= Zero ? modulo : -modulo;
	}
	
	public static fixed32 Abs(fixed32 a) {
		return new fixed32(a._internalValue < 0 ? -a._internalValue : a._internalValue);
	}
	
	public static fixed32 Frac(fixed32 a) {
		int newInternalValue = (a._internalValue & 0xffff);
		return new fixed32(newInternalValue);
	}
	
	public static fixed32 Truncate(fixed32 a) {
		int newInternalValue = (int)(a._internalValue & 0xffff0000);
		fixed32 newValue = new fixed32(newInternalValue);
		if (a < Zero && a != newValue) {
			newValue += One;
		}

		return newValue;
	}
	
	public static fixed32 Floor(fixed32 a) {
		int newInternalValue = (int)(a._internalValue & 0xffff0000);
		return new fixed32(newInternalValue);
	}
	
	public static fixed32 Ceiling(fixed32 a) {
		int newInternalValue = (int)(a._internalValue & 0xffff0000);
		return newInternalValue == a._internalValue ? a : new fixed32(newInternalValue) + One;
	}

	public static fixed32 Sqrt(fixed32 a) {
		
		if (a < Zero) {
			throw new ArgumentOutOfRangeException(nameof(a));
		}

		if (a == Zero) {
			return Zero;
		}

		if (a == Epsilon) {
			return new fixed32(0, byte.MaxValue + 1);
		}

		fixed32 x = a / Two;
		fixed32 xSquared = x * x;
		fixed32 error = Abs(xSquared - a);
		
		while (true) {
			fixed32 nextGuess = x
			 / Two + a / (Two * x);
			fixed32 nextGuessSquared = nextGuess * nextGuess;
			fixed32 nextError = Abs(nextGuessSquared - a);
		
			if (Abs(nextGuess - x) <= Epsilon) {
				return error < nextError ? x : nextGuess;
			}

			x = nextGuess;
			error = nextError;
		}
	}

	public static fixed32 Lerp(fixed32 a, fixed32 b, fixed32 t) {
		return a + (b - a) * t;
	}

	public static fixed32 Sin(fixed32 a) {
		fixed32 angle = a % TwoPi;
		angle = angle < Zero ? angle + TwoPi : angle;
		fixed32 lookupTableLength = new fixed32((short)SinLookup.FixedTable.Length, 0);
		
		fixed32 index = (lookupTableLength * angle / TwoPi);
		int index1 = index.Integral;
		int index2 = (index1 + 1) % SinLookup.FixedTable.Length;
		
		return Lerp(SinLookup.FixedTable[index1], SinLookup.FixedTable[index2], fixed32.Frac(index));
	}
	
	public static fixed32 Cos(fixed32 a) {
		return Sin(a + HalfPi);
	}

	public static fixed32 Max(fixed32 a, fixed32 b) {
		return a > b ? a : b;
	}
	
	public static fixed32 Min(fixed32 a, fixed32 b) {
		return a < b ? a : b;
	}
	
	public static bool operator <(fixed32 a, fixed32 b) {
		return a._internalValue < b._internalValue;
	}
	
	public static bool operator <=(fixed32 a, fixed32 b) {
		return a._internalValue <= b._internalValue;
	}
	
	public static bool operator >(fixed32 a, fixed32 b) {
		return a._internalValue > b._internalValue;
	}	
	
	public static bool operator >=(fixed32 a, fixed32 b) {
		return a._internalValue >= b._internalValue;
	}
	
	public static explicit operator float(fixed32 value) {
		return value._internalValue / (float)Scale;
	}

	public override string ToString() {
		return $"{Integral}+{Fractional}/65536";
	}
}
