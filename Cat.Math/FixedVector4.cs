namespace Cat.Math; 

public struct FixedVector4 {

	public static readonly FixedVector4 Zero = new FixedVector4 {
		X = fixed32.Zero,
		Y = fixed32.Zero,
		Z = fixed32.Zero,
		W = fixed32.Zero
	};
	
	public static readonly FixedVector4 One = new FixedVector4 {
		X = fixed32.One,
		Y = fixed32.One,
		Z = fixed32.One,
		W = fixed32.One
	};
	
	public static readonly FixedVector4 UnitX = new FixedVector4 {
		X = fixed32.One,
		Y = fixed32.Zero,
		Z = fixed32.Zero,
		W = fixed32.Zero
	};
	public static readonly FixedVector4 UnitY = new FixedVector4 {
		X = fixed32.Zero,
		Y = fixed32.One,
		Z = fixed32.Zero,
		W = fixed32.Zero
	};
	public static readonly FixedVector4 UnitZ = new FixedVector4 {
		X = fixed32.Zero,
		Y = fixed32.Zero,
		Z = fixed32.One,
		W = fixed32.Zero
	};
	
	public static readonly FixedVector4 UnitW = new FixedVector4 {
		X = fixed32.Zero,
		Y = fixed32.Zero,
		Z = fixed32.Zero,
		W = fixed32.One
	};

	public fixed32 X { get; set; }
	public fixed32 Y { get; set; }
	public fixed32 Z { get; set; }
	public fixed32 W { get; set; }

	public fixed32 this[int index] { 
		get => index switch {
			0 => X,
			1 => Y,
			2 => Z,
			3 => W,
			_ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
		};
		set {
			switch(index) {
				case 0:
					X = value;
					return;
				case 1:
					Y = value;
					return;
				case 2:
					Z = value;
					return;
				case 3:
					W = value;
					return;
				default:
					throw new ArgumentOutOfRangeException(nameof(index), index, null);
			}
		} 
	}
	

	public FixedVector4 Normalized {
		get {
			FixedVector4 copy = this;
			copy.Normalize();
			return copy;
		}
	}

	public FixedVector4(fixed32 x, fixed32 y, fixed32 z, fixed32 w) {
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public static FixedVector4 operator +(FixedVector4 a, FixedVector4 b) {
		return new FixedVector4 {
			X = a.X + b.X,
			Y = a.Y + b.Y,
			Z = a.Z + b.Z,
			W = a.W + b.W
		};
	}
	
	public static FixedVector4 operator -(FixedVector4 a, FixedVector4 b) {
		return new FixedVector4 {
			X = a.X - b.X,
			Y = a.Y - b.Y,
			Z = a.Z - b.Z,
			W = a.W - b.W
		};
	}
	
	public static FixedVector4 operator -(FixedVector4 a) {
		return new FixedVector4 {
			X = -a.X,
			Y = -a.Y,
			Z = -a.Z,
			W = -a.W
		};
	}
	
	public static FixedVector4 operator *(FixedVector4 a, FixedVector4 b) {
		return new FixedVector4 {
			X = a.X * b.X,
			Y = a.Y * b.Y,
			Z = a.Z * b.Z,
			W = a.W * b.W
		};
	}
	
	public static FixedVector4 operator *(fixed32 a, FixedVector4 b) {
		return new FixedVector4 {
			X = a * b.X,
			Y = a * b.Y,
			Z = a * b.Z,
			W = a * b.W
		};
	}
	
	public static FixedVector4 operator *(FixedVector4 a, fixed32 b) {
		return b * a;
	}
	
	public static FixedVector4 operator /(FixedVector4 a, FixedVector4 b) {
		return new FixedVector4 {
			X = a.X / b.X,
			Y = a.Y / b.Y,
			Z = a.Z / b.Z,
			W = a.W / b.W
		};
	}
	
	public static FixedVector4 operator /(FixedVector4 a, fixed32 b) {
		return new FixedVector4 {
			X = a.X / b,
			Y = a.Y / b,
			Z = a.Z / b,
			W = a.W / b
		};
	}

	public static fixed32 Dot(FixedVector4 a, FixedVector4 b) {
		return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
	}

	public static fixed32 Length(FixedVector4 a) {
		return (fixed32)fixed64.Sqrt(LengthSquared(a));
	}

	public static fixed64 LengthSquared(FixedVector4 a) {
		fixed64 aX = new fixed64(a.X);
		fixed64 aY = new fixed64(a.Y);
		fixed64 aZ = new fixed64(a.Z);
		fixed64 aW = new fixed64(a.W);
		
		return aX * aX + aY * aY + aZ * aZ + aW * aW;
	}

	public fixed32 Length() {
		return Length(this);
	}
	
	public fixed64 LengthSquared() {
		return LengthSquared(this);
	}

	public void Normalize() {
		fixed64 length = Length();
		
		X = (fixed32)(X / length);
		Y = (fixed32)(Y / length);
		Z = (fixed32)(Z / length);
		W = (fixed32)(W / length);
	}

	public static fixed32 Distance(FixedVector4 a, FixedVector4 b) {
		return Length(a - b);
	}

	public override string ToString() {
		return $"({X}, {Y}, {Z}, {W})";
	}
}