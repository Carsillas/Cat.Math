namespace Cat.Math.Tests;

public class Fixed32FunctionTests {
	
	[Test]
	[TestCaseSource(nameof(GenerateSqrtTestCases))]
	public void TestFixed32Sqrt(TestCase testCase) {
		fixed32 result = fixed32.Sqrt(testCase.A);
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateSqrtTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
			},
			new TestCase {
				A = new fixed32(0, 1),
				Expected = new fixed32(0, 256),
			},
			new TestCase {
				A = new fixed32(0, 2),
				Expected = new fixed32(0, 362),
			},
			new TestCase {
				A = new fixed32(0, 3),
				Expected = new fixed32(0, 442),
			},
			new TestCase {
				A = new fixed32(0, 4),
				Expected = new fixed32(0, 512),
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				Expected = new fixed32(11, 5933),
			},
			new TestCase {
				A = new fixed32(15863, 57842),
				Expected = new fixed32(125, 62383),
			},

		};
	}

	
	[Test]
	[TestCaseSource(nameof(GenerateAbsTestCases))]
	public void TestFixed32Abs(TestCase testCase) {
		fixed32 result = fixed32.Abs(testCase.A);
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateAbsTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 456),
				Expected = new fixed32(123, 456),
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 456),
				Expected = new fixed32(122, 65080),
			},

			new TestCase {
				A = -new fixed32(123, 456),
				Expected = new fixed32(123, 456),
			},

			new TestCase {
				A = -new fixed32(0, 456),
				Expected = new fixed32(0, 456),
			},

		};
	}
	
	
	[Test]
	[TestCaseSource(nameof(GenerateTruncateTestCases))]
	public void TestFixed32Truncate(TestCase testCase) {
		fixed32 result = fixed32.Truncate(testCase.A);
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateTruncateTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 456),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				Expected = new fixed32(-123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 456),
				Expected = new fixed32(-122, 0),
			},

			new TestCase {
				A = -new fixed32(123, 456),
				Expected = -new fixed32(123, 0),
			},

			new TestCase {
				A = -new fixed32(0, 456),
				Expected = new fixed32(0, 0),
			},

		};
	}


	[Test]
	[TestCaseSource(nameof(GenerateCeilingTestCases))]
	public void TestFixed32Ceiling(TestCase testCase) {
		fixed32 result = fixed32.Ceiling(testCase.A);
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateCeilingTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 456),
				Expected = new fixed32(124, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				Expected = new fixed32(-123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 456),
				Expected = new fixed32(-122, 0),
			},

			new TestCase {
				A = -new fixed32(123, 456),
				Expected = -new fixed32(123, 0),
			},

			new TestCase {
				A = -new fixed32(0, 456),
				Expected = new fixed32(0, 0),
			},

		};
	}

	
	[Test]
	[TestCaseSource(nameof(GenerateFloorTestCases))]
	public void TestFixed32Floor(TestCase testCase) {
		fixed32 result = fixed32.Floor(testCase.A);
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}

	private static IEnumerable<TestCase> GenerateFloorTestCases() {
		return new TestCase[] {
			 
			new TestCase {
				A = new fixed32(0, 0),
				Expected = new fixed32(0, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 0),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(123, 456),
				Expected = new fixed32(123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 0),
				Expected = new fixed32(-123, 0),
			},
			
			new TestCase {
				A = new fixed32(-123, 456),
				Expected = new fixed32(-123, 0),
			},

			new TestCase {
				A = -new fixed32(123, 456),
				Expected = -new fixed32(124, 0),
			},

			new TestCase {
				A = -new fixed32(0, 456),
				Expected = new fixed32(-1, 0),
			},

		};
	}


	public readonly struct TestCase {
		public fixed32 A { get; init; } 
		public fixed32 Expected { get; init; } 
	}	
		
}