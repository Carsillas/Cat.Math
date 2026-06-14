namespace Cat.Math.Tests; 

public class FixedMatrix3x3Tests {

	private static readonly fixed32 TwoFifths = fixed32.FromQuotient(2, 5);
	
	[Test]
	public void TestFixedMatrix3x3Inverse() {

		fixed32 radius = new fixed32(5, 0);

		FixedMatrix3x3 matrix = fixed32.FromQuotient(4, 1) * new FixedMatrix3x3(
			new FixedVector3(TwoFifths * radius * radius, fixed32.Zero, fixed32.Zero),
			new FixedVector3(fixed32.Zero, TwoFifths * radius * radius, fixed32.Zero),
			new FixedVector3(fixed32.Zero, fixed32.Zero, TwoFifths * radius * radius)
		);

		FixedMatrix3x3 inverse = matrix.Inverse();

		FixedMatrix3x3 original = inverse.Inverse();
		
		AssertMatrixApproximately(original, matrix, 0.02f);
		
	}

	[Test]
	public void TestFixedMatrix3x3VectorMultiplicationUsesRows() {
		FixedMatrix3x3 matrix = new FixedMatrix3x3(
			new FixedVector3(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0)),
			new FixedVector3(new fixed32(4, 0), new fixed32(5, 0), new fixed32(6, 0)),
			new FixedVector3(new fixed32(7, 0), new fixed32(8, 0), new fixed32(9, 0))
		);
		FixedVector3 vector = new FixedVector3(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0));

		FixedVector3 result = matrix * vector;

		Assert.That(result, Is.EqualTo(new FixedVector3(new fixed32(14, 0), new fixed32(32, 0), new fixed32(50, 0))));
	}

	[Test]
	public void TestFixedMatrix3x3Determinant() {
		FixedMatrix3x3 matrix = new FixedMatrix3x3(
			new FixedVector3(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0)),
			new FixedVector3(fixed32.Zero, new fixed32(4, 0), new fixed32(5, 0)),
			new FixedVector3(fixed32.One, fixed32.Zero, new fixed32(6, 0))
		);

		Assert.That(matrix.Determinant(), Is.EqualTo(new fixed64(22, 0)));
	}

	[Test]
	public void TestFixedMatrix3x3Transpose() {
		FixedMatrix3x3 matrix = new FixedMatrix3x3(
			new FixedVector3(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0)),
			new FixedVector3(new fixed32(4, 0), new fixed32(5, 0), new fixed32(6, 0)),
			new FixedVector3(new fixed32(7, 0), new fixed32(8, 0), new fixed32(9, 0))
		);

		FixedMatrix3x3 transposed = matrix.Transposed();

		Assert.That(transposed, Is.EqualTo(new FixedMatrix3x3(
			new FixedVector3(new fixed32(1, 0), new fixed32(4, 0), new fixed32(7, 0)),
			new FixedVector3(new fixed32(2, 0), new fixed32(5, 0), new fixed32(8, 0)),
			new FixedVector3(new fixed32(3, 0), new fixed32(6, 0), new fixed32(9, 0))
		)));
	}

	[Test]
	public void TestFixedMatrix3x3ScalarDivision() {
		FixedMatrix3x3 matrix = new FixedMatrix3x3(
			new FixedVector3(new fixed32(2, 0), new fixed32(4, 0), new fixed32(6, 0)),
			new FixedVector3(new fixed32(8, 0), new fixed32(10, 0), new fixed32(12, 0)),
			new FixedVector3(new fixed32(14, 0), new fixed32(16, 0), new fixed32(18, 0))
		);

		FixedMatrix3x3 result = matrix / fixed32.Two;

		Assert.That(result, Is.EqualTo(new FixedMatrix3x3(
			new FixedVector3(new fixed32(1, 0), new fixed32(2, 0), new fixed32(3, 0)),
			new FixedVector3(new fixed32(4, 0), new fixed32(5, 0), new fixed32(6, 0)),
			new FixedVector3(new fixed32(7, 0), new fixed32(8, 0), new fixed32(9, 0))
		)));
	}

	private static void AssertMatrixApproximately(FixedMatrix3x3 actual, FixedMatrix3x3 expected, float tolerance) {
		for (int row = 0; row < 3; row++) {
			for (int column = 0; column < 3; column++) {
				Assert.That((float)actual[column, row], Is.EqualTo((float)expected[column, row]).Within(tolerance));
			}
		}
	}

}
