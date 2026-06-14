namespace Cat.Math.Tests;

public class FixedMatrix4x4Tests {

	[Test]
	public void TestFixedMatrix4x4VectorMultiplicationUsesRows() {
		FixedMatrix4x4 matrix = new FixedMatrix4x4(
			new FixedVector4(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0), new fixed32(4, 0)),
			new FixedVector4(new fixed32(5, 0), new fixed32(6, 0), new fixed32(7, 0), new fixed32(8, 0)),
			new FixedVector4(new fixed32(9, 0), new fixed32(10, 0), new fixed32(11, 0), new fixed32(12, 0)),
			new FixedVector4(new fixed32(13, 0), new fixed32(14, 0), new fixed32(15, 0), new fixed32(16, 0))
		);
		FixedVector4 vector = new FixedVector4(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0), new fixed32(4, 0));

		FixedVector4 result = matrix * vector;

		Assert.That(result, Is.EqualTo(new FixedVector4(new fixed32(30, 0), new fixed32(70, 0), new fixed32(110, 0), new fixed32(150, 0))));
	}

	[Test]
	public void TestFixedMatrix4x4Determinant() {
		FixedMatrix4x4 matrix = new FixedMatrix4x4(
			new FixedVector4(fixed32.One, new fixed32(2, 0), fixed32.Zero, fixed32.Zero),
			new FixedVector4(fixed32.Zero, fixed32.One, new fixed32(3, 0), fixed32.Zero),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.One, new fixed32(4, 0)),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.Zero, fixed32.One)
		);

		Assert.That(matrix.Determinant(), Is.EqualTo(fixed64.One));
	}

	[Test]
	public void TestFixedMatrix4x4Inverse() {
		FixedMatrix4x4 matrix = new FixedMatrix4x4(
			new FixedVector4(fixed32.One, new fixed32(2, 0), fixed32.Zero, fixed32.Zero),
			new FixedVector4(fixed32.Zero, fixed32.One, new fixed32(3, 0), fixed32.Zero),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.One, new fixed32(4, 0)),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.Zero, fixed32.One)
		);

		FixedMatrix4x4 inverse = matrix.Inverse();

		Assert.That(inverse, Is.EqualTo(new FixedMatrix4x4(
			new FixedVector4(fixed32.One, -new fixed32(2, 0), new fixed32(6, 0), -new fixed32(24, 0)),
			new FixedVector4(fixed32.Zero, fixed32.One, -new fixed32(3, 0), new fixed32(12, 0)),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.One, -new fixed32(4, 0)),
			new FixedVector4(fixed32.Zero, fixed32.Zero, fixed32.Zero, fixed32.One)
		)));
		Assert.That(matrix * inverse, Is.EqualTo(FixedMatrix4x4.Identity));
	}

	[Test]
	public void TestFixedMatrix4x4Transpose() {
		FixedMatrix4x4 matrix = new FixedMatrix4x4(
			new FixedVector4(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0), new fixed32(4, 0)),
			new FixedVector4(new fixed32(5, 0), new fixed32(6, 0), new fixed32(7, 0), new fixed32(8, 0)),
			new FixedVector4(new fixed32(9, 0), new fixed32(10, 0), new fixed32(11, 0), new fixed32(12, 0)),
			new FixedVector4(new fixed32(13, 0), new fixed32(14, 0), new fixed32(15, 0), new fixed32(16, 0))
		);

		FixedMatrix4x4 transposed = matrix.Transposed();

		Assert.That(transposed, Is.EqualTo(new FixedMatrix4x4(
			new FixedVector4(new fixed32(1, 0), new fixed32(5, 0), new fixed32(9, 0), new fixed32(13, 0)),
			new FixedVector4(new fixed32(2, 0), new fixed32(6, 0), new fixed32(10, 0), new fixed32(14, 0)),
			new FixedVector4(new fixed32(3, 0), new fixed32(7, 0), new fixed32(11, 0), new fixed32(15, 0)),
			new FixedVector4(new fixed32(4, 0), new fixed32(8, 0), new fixed32(12, 0), new fixed32(16, 0))
		)));
	}

	[Test]
	public void TestFixedMatrix4x4ScalarDivision() {
		FixedMatrix4x4 matrix = new FixedMatrix4x4(
			new FixedVector4(new fixed32(2, 0), new fixed32(4, 0), new fixed32(6, 0), new fixed32(8, 0)),
			new FixedVector4(new fixed32(10, 0), new fixed32(12, 0), new fixed32(14, 0), new fixed32(16, 0)),
			new FixedVector4(new fixed32(18, 0), new fixed32(20, 0), new fixed32(22, 0), new fixed32(24, 0)),
			new FixedVector4(new fixed32(26, 0), new fixed32(28, 0), new fixed32(30, 0), new fixed32(32, 0))
		);

		FixedMatrix4x4 result = matrix / fixed32.Two;

		Assert.That(result, Is.EqualTo(new FixedMatrix4x4(
			new FixedVector4(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0), new fixed32(4, 0)),
			new FixedVector4(new fixed32(5, 0), new fixed32(6, 0), new fixed32(7, 0), new fixed32(8, 0)),
			new FixedVector4(new fixed32(9, 0), new fixed32(10, 0), new fixed32(11, 0), new fixed32(12, 0)),
			new FixedVector4(new fixed32(13, 0), new fixed32(14, 0), new fixed32(15, 0), new fixed32(16, 0))
		)));
	}
}
