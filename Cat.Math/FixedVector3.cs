namespace Cat.Math; 

public struct FixedVector3 {

	public static readonly FixedVector3 Zero = new FixedVector3 {
		X = fixed32.Zero,
		Y = fixed32.Zero,
		Z = fixed32.Zero
	};
	
	public static readonly FixedVector3 One = new FixedVector3 {
		X = fixed32.One,
		Y = fixed32.One,
		Z = fixed32.One
	};
	
	public static readonly FixedVector3 UnitX = new FixedVector3 {
		X = fixed32.One,
		Y = fixed32.Zero,
		Z = fixed32.Zero
	};
	public static readonly FixedVector3 UnitY = new FixedVector3 {
		X = fixed32.Zero,
		Y = fixed32.One,
		Z = fixed32.Zero
	};
	public static readonly FixedVector3 UnitZ = new FixedVector3 {
		X = fixed32.Zero,
		Y = fixed32.Zero,
		Z = fixed32.One
	};

	public fixed32 X { get; set; }
	public fixed32 Y { get; set; }
	public fixed32 Z { get; set; }

	
	public fixed32 this[int index] { 
		get => index switch {
			0 => X,
			1 => Y,
			2 => Z,
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
				default:
					throw new ArgumentOutOfRangeException(nameof(index), index, null);
			}
		} 
	}


	
	public FixedVector3 Normalized {
		get {
			FixedVector3 copy = this;
			copy.Normalize();
			return copy;
		}
	}

	public FixedVector3(fixed32 x, fixed32 y, fixed32 z) {
		X = x;
		Y = y;
		Z = z;
	}

	public static FixedVector3 operator +(FixedVector3 a, FixedVector3 b) {
		return new FixedVector3 {
			X = a.X + b.X,
			Y = a.Y + b.Y,
			Z = a.Z + b.Z
		};
	}
	
	public static FixedVector3 operator -(FixedVector3 a, FixedVector3 b) {
		return new FixedVector3 {
			X = a.X - b.X,
			Y = a.Y - b.Y,
			Z = a.Z - b.Z
		};
	}
	
	public static FixedVector3 operator -(FixedVector3 a) {
		return new FixedVector3 {
			X = -a.X,
			Y = -a.Y,
			Z = -a.Z
		};
	}
	
	public static FixedVector3 operator *(FixedVector3 a, FixedVector3 b) {
		return new FixedVector3 {
			X = a.X * b.X,
			Y = a.Y * b.Y,
			Z = a.Z * b.Z
		};
	}
	
	public static FixedVector3 operator *(fixed32 a, FixedVector3 b) {
		return new FixedVector3 {
			X = a * b.X,
			Y = a * b.Y,
			Z = a * b.Z
		};
	}
	
	public static FixedVector3 operator *(FixedVector3 a, fixed32 b) {
		return b * a;
	}
	
	public static FixedVector3 operator /(FixedVector3 a, FixedVector3 b) {
		return new FixedVector3 {
			X = a.X / b.X,
			Y = a.Y / b.Y,
			Z = a.Z / b.Z
		};
	}
	
	public static FixedVector3 operator /(FixedVector3 a, fixed32 b) {
		return new FixedVector3 {
			X = a.X / b,
			Y = a.Y / b,
			Z = a.Z / b
		};
	}

	public static fixed32 Dot(FixedVector3 a, FixedVector3 b) {
		return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
	}
	
	public static FixedVector3 Cross(FixedVector3 a, FixedVector3 b) {
		fixed32 x = a.Y * b.Z - a.Z * b.Y;
		fixed32 y = -(a.X * b.Z - a.Z * b.X);
		fixed32 z = a.X * b.Y - a.Y * b.X;

		return new FixedVector3(x, y, z);
	}
	
	public static fixed32 Length(FixedVector3 a) {
		return (fixed32)fixed64.Sqrt(LengthSquared(a));
	}

	public static fixed64 LengthSquared(FixedVector3 a) {

		fixed64 aX = new fixed64(a.X);
		fixed64 aY = new fixed64(a.Y);
		fixed64 aZ = new fixed64(a.Z);
		
		return aX * aX + aY * aY + aZ * aZ;
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
	}

	public static fixed32 Distance(FixedVector3 a, FixedVector3 b) {
		return Length(a - b);
	}

	public override string ToString() {
		return $"({X}, {Y}, {Z})";
	}
}