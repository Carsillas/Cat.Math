namespace Cat.Math.Tests; 

public class Fixed64ConversionTests {


	[Test]
	public void TestFixed32ToFixed64Conversion() {

		fixed64 value = new fixed64(new fixed32(57, 32768));
		fixed64 expected = new fixed64(57, 2147483648);

		Assert.That(value, Is.EqualTo(expected));
		

		value = -new fixed64(new fixed32(57, 32768));
		expected = -new fixed64(57, 2147483648);

		Assert.That(value, Is.EqualTo(expected));
	}
	
	
	[Test]
	public void TestFixedVector3Normalize() {

		FixedVector3 vector = new FixedVector3(fixed32.Zero, fixed32.Zero, -new fixed32(0, 13));
		FixedVector3 expected = new FixedVector3(fixed32.Zero, fixed32.Zero, -new fixed32(1, 0));
		FixedVector3 normalized = vector.Normalized;
		
		Assert.That(normalized, Is.EqualTo(expected));
	}
	
	
}