namespace Cat.Math.Tests; 

public class Fixed32TrigonometryTests {

	[Test]
	public void VerifySinValues() {
		for (fixed32 value = -fixed32.TwoPi; value <= fixed32.TwoPi; value += fixed32.Epsilon) {

			fixed32 sin = fixed32.Sin(value);
			float actual = MathF.Sin((float)value);
			
			//Console.Write($"({(float)value}, {(float)sin}), ");
			//Console.Write($"({(float)value}, {actual}), ");
			
			Assert.That((float)sin, Is.EqualTo(actual).Within(0.00005), value.ToString());
			
		}
	}
	
	[Test]
	public void VerifyCosValues() {
		for (fixed32 value = -fixed32.TwoPi; value <= fixed32.TwoPi; value += fixed32.Epsilon) {

			fixed32 cos = fixed32.Cos(value);
			float actual = MathF.Cos((float)value);
			
			//Console.Write($"({(float)value}, {(float)sin}), ");
			//Console.Write($"({(float)value}, {actual}), ");
			
			Assert.That((float)cos, Is.EqualTo(actual).Within(0.00005), value.ToString());
			
		}
	}

	[Test]
	[Ignore("Manually ran")]
	public void TestGenerateLookupTable() {
		
		for (int i = 0; i < 1440; i += 12) {
			for (int j = 0; j < 12; j++) {
				float value = MathF.Sin(2 * MathF.PI * (i + j) / 1440.0f);
		
				float frac = MathF.Abs(value) - MathF.Floor(MathF.Abs(value));
				string negativeSign = value < 0 ? "-" : string.Empty;
		
				Console.Write($"{negativeSign}new fixed32({(short)value}, {(ushort)(frac * 65536)}), ");
				//Console.Write($"{value}f, ");
				
			}
			Console.WriteLine();
		}
	}
	
}