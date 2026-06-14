namespace Cat.Math.Tests; 

public class FixedQuaternionTests {

	
	[Test]
	public void TestRotateVector() {

		FixedQuaternion rotation = FixedQuaternion.FromAxisAngle(FixedVector3.UnitY, fixed32.HalfPi);

		FixedVector3 result = FixedVector3.UnitX * rotation;
		
		// Failing due to precision issue, uncertain if I should be seeing it, probably.
		Assert.That(result, Is.EqualTo(-FixedVector3.UnitZ));

	}

	[Test]
	public void TestMultiplyQuaternions() {

		FixedQuaternion rotation = FixedQuaternion.FromAxisAngle(FixedVector3.UnitX, fixed32.One);

		FixedQuaternion result = rotation * rotation;
		FixedQuaternion expected = FixedQuaternion.FromAxisAngle(FixedVector3.UnitX, fixed32.Two);
		
		AssertQuaternionApproximately(result, expected);

	}

	private static void AssertQuaternionApproximately(FixedQuaternion actual, FixedQuaternion expected) {
		Assert.That((float)actual.W, Is.EqualTo((float)expected.W).Within(0.00005));
		Assert.That((float)actual.X, Is.EqualTo((float)expected.X).Within(0.00005));
		Assert.That((float)actual.Y, Is.EqualTo((float)expected.Y).Within(0.00005));
		Assert.That((float)actual.Z, Is.EqualTo((float)expected.Z).Within(0.00005));
	}


}
