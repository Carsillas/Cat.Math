namespace Cat.Math.Tests;

public class Fixed32ArithmeticTests {
	
	[Test]
	[TestCaseSource(nameof(GenerateAdditionTestCases))]
	public void TestFixed32Addition(TestCase testCase) {
		fixed32 result = testCase.A + testCase.B;
		float floatResult = (float)result;
		Assert.That(result, Is.EqualTo(testCase.Expected));
		Assert.That(floatResult, Is.EqualTo(testCase.ExpectedFloat).Within(0.0001));
	}


	private static IEnumerable<TestCase> GenerateAdditionTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				B = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
				ExpectedFloat = 0.0f
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				B = new fixed32(0, 30000),
				Expected = new fixed32(123, 30000),
				ExpectedFloat = 123.4577706f
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				B = new fixed32(0, 30000),
				Expected = new fixed32(-123, 30000),
				ExpectedFloat = -122.5422293f
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				B = new fixed32(-123, 0),
				Expected = new fixed32(-246, 0),
				ExpectedFloat = -246.0f
			},
			
			new TestCase {
				A = new fixed32(-123, 32768),
				B = new fixed32(-123, 32768),
				Expected = new fixed32(-245, 0),
				ExpectedFloat = -245.0f
			}
			
		};
	}

	[Test]
	[TestCaseSource(nameof(GenerateTestCases))]
	public void TestFixed32Multiplication(TestCase testCase) {
		fixed32 result = testCase.A * testCase.B;
		float floatResult = (float)result;
		Assert.That(floatResult, Is.EqualTo(testCase.ExpectedFloat).Within(0.0001));
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				B = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
				ExpectedFloat = 0.0f
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				B = new fixed32(0, 30000),
				Expected = new fixed32(56, 19984),
				ExpectedFloat = 56.3049316f
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				B = new fixed32(0, 30000),
				Expected = new fixed32(-57, 45552),
				ExpectedFloat = -56.3049316f
			},
			
		};
	}

	[Test]
	[TestCaseSource(nameof(GenerateDivisionTestCases))]
	public void TestFixed32Division(TestCase testCase) {
		fixed32 result = testCase.A / testCase.B;
		float floatResult = (float)result;
		Assert.That(floatResult, Is.EqualTo(testCase.ExpectedFloat).Within(0.0001));
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateDivisionTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				B = new fixed32(1, 0),
				Expected = new fixed32(0, 0),
				ExpectedFloat = 0.0f
			},
			
			new TestCase {
				A = new fixed32(56, 19984),
				B = new fixed32(0, 30000),
				Expected = new fixed32(123, 0),
				ExpectedFloat = 123.0f
			},
			
			new TestCase {
				A = new fixed32(-57, 65536 - 19984),
				B = new fixed32(0, 30000),
				Expected = new fixed32(-123, 0),
				ExpectedFloat = -123.0f
			},
			
			new TestCase {
				A = new fixed32(-56, 19984),
				B = new fixed32(0, 30000),
				Expected = new fixed32(-122, 21776),
				ExpectedFloat = -121.6677333f
			},
			
		};
	}
	
	[Test]
	[TestCaseSource(nameof(GenerateModuloTestCases))]
	public void TestFixed32Modulo(TestCase testCase) {
		fixed32 result = testCase.A % testCase.B;
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}
	
	private static IEnumerable<TestCase> GenerateModuloTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = -new fixed32(0, 6),
				B = fixed32.TwoPi,
				Expected = -new fixed32(0, 6)
			},
			
			new TestCase {
				A = new fixed32(5, 19984),
				B = fixed32.TwoPi,
				Expected = new fixed32(5, 19984)
			},
			
			new TestCase {
				A = -new fixed32(5, 19984),
				B = fixed32.TwoPi,
				Expected = -new fixed32(5, 19984)
			},
			
			new TestCase {
				A = new fixed32(1, 234),
				B = new fixed32(0, 32768),
				Expected = new fixed32(0, 234)
			},
			
			new TestCase {
				A = -new fixed32(1, 234),
				B = new fixed32(0, 32768),
				Expected = -new fixed32(0, 234)
			},
			
			new TestCase {
				A = new fixed32(1, 19984),
				B = new fixed32(0, 32768),
				Expected = new fixed32(0, 19984)
			},
			
		};
	}

	public readonly struct TestCase {
		public fixed32 A { get; init; } 
		public fixed32 B { get; init; } 
		public fixed32 Expected { get; init; } 
		public float ExpectedFloat { get; init; }
	}	
		
}