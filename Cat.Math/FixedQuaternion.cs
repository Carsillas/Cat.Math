
namespace Cat.Math;

public record struct FixedQuaternion {
	public fixed32 W { get; set; }
	public FixedVector3 V { get; set; }
	
	public fixed32 X => V.X;
	public fixed32 Y => V.Y;
	public fixed32 Z => V.Z;
	
	public static FixedQuaternion Identity => new FixedQuaternion(fixed32.One, FixedVector3.Zero);

	public fixed32 Length => fixed32.Sqrt(LengthSquared);
	public fixed32 LengthSquared => W * W + X * X + Y * Y + Z * Z;

	private FixedQuaternion Normalized {
		get {
			fixed32 length = Length;
			return new FixedQuaternion(W / length, V / length);
		}
	}

	private FixedQuaternion(fixed32 w, fixed32 x, fixed32 y, fixed32 z) : this(w, new FixedVector3(x, y, z)) { }
	
	private FixedQuaternion(fixed32 w, FixedVector3 v) {
		W = w;
		V = v;
	}

	public static FixedQuaternion operator *(FixedQuaternion a, FixedQuaternion b) {
		fixed32 newW = a.W * b.W - FixedVector3.Dot(a.V, b.V);
		FixedVector3 newV = a.W * b.V + b.W * a.V + FixedVector3.Cross(a.V, b.V);
		
		return new FixedQuaternion(newW, newV.X, newV.Y, newV.Z);
	}

	public FixedQuaternion Conjugate() {
		return new FixedQuaternion(W, -V);
	}

	public void Normalize() {
		fixed32 length = Length;

		if (length == fixed32.Zero) {
			this = Identity;
			return;
		}

		W /= length;
		V /= length;
	}
	
	public FixedQuaternion Inverse() {
		fixed32 lengthSquared = LengthSquared;
		FixedQuaternion conjugate = Conjugate();
		return new FixedQuaternion(conjugate.W / lengthSquared, conjugate.V / lengthSquared);
	}

	public static FixedVector3 operator*(FixedVector3 a, FixedQuaternion b) {
		FixedQuaternion operand = new FixedQuaternion(fixed32.Zero, a.X, a.Y, a.Z);
		FixedQuaternion inverse = b.Inverse();
	
		FixedQuaternion rotatedQuaternion = b * operand * inverse;
	
		return rotatedQuaternion.V;
	}

	public static FixedQuaternion FromAxisAngle(FixedVector3 axis, fixed32 angle) {
		axis.Normalize();

		fixed32 sinValue = fixed32.Sin(angle / fixed32.Two);

		FixedVector3 v = axis * sinValue;
		fixed32 w = fixed32.Cos(angle / fixed32.Two);

		return new FixedQuaternion(w, v).Normalized;
	}

	public override string ToString() {
		return $"({W}, {X}, {Y}, {Z})";
	}
	
}
