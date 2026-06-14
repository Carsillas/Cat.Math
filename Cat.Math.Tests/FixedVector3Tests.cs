namespace Cat.Math.Tests; 

public class FixedVector3Tests {

	
	[Test]
	public void TestFixedVector3Normalize() {
		FixedVector3 one = FixedVector3.One;
		FixedVector3 oneNormalizedProperty = one.Normalized;
		FixedVector3 oneNormalized =
			new FixedVector3(new fixed32(0, 37837), new fixed32(0, 37837), new fixed32(0, 37837));

		one.Normalize();
		
		Assert.That(one, Is.EqualTo(oneNormalized));
		Assert.That(oneNormalizedProperty, Is.EqualTo(oneNormalized));
	}

	[Test]
	public void TestFixedVector3Length() {
		
		FixedVector3 value = new FixedVector3(fixed32.Epsilon, fixed32.Zero, fixed32.Zero);
		
		Assert.That(value.Length(), Is.EqualTo(fixed32.Epsilon));
		
	}
	
	[Test]
	[TestCaseSource(nameof(GenerateDotProductTestCases))]
	public void TestFixedVector3DotProduct(DotProductTestCase testCase) {

		fixed32 result = FixedVector3.Dot(testCase.A, testCase.B);
		
		Assert.That(result, Is.EqualTo(testCase.Expected));

	}
	
	private static IEnumerable<DotProductTestCase> GenerateDotProductTestCases() {
		return new DotProductTestCase[] {
			 
			// Orthogonal
			new DotProductTestCase {
				A = FixedVector3.UnitX,
				B = FixedVector3.UnitY,
				Expected = new fixed32(0, 0)
			},
			
			// 45 degree
			new DotProductTestCase {
				A = FixedVector3.UnitX,
				B = new FixedVector3(new fixed32(0, 46341), new fixed32(0, 46341), fixed32.Zero),
				Expected = new fixed32(0, 46341)
			},

		};
	}
	
	
	[Test]
	[TestCaseSource(nameof(GenerateCrossProductTestCases))]
	public void TestFixedVector3CrossProduct(CrossProductTestCase testCase) {
		FixedVector3 result = FixedVector3.Cross(testCase.A, testCase.B);
		
		Assert.That(result, Is.EqualTo(testCase.Expected));
	}
	
	private static IEnumerable<CrossProductTestCase> GenerateCrossProductTestCases() {
		return new CrossProductTestCase[] {
			 
			new CrossProductTestCase {
				A = FixedVector3.UnitX,
				B = FixedVector3.UnitY,
				Expected = FixedVector3.UnitZ
			},

			new CrossProductTestCase {
				A = new FixedVector3(fixed32.Zero, fixed32.Two, fixed32.One),
				B = new FixedVector3(new fixed32(3, 0), -fixed32.One, fixed32.Zero),
				Expected = new FixedVector3(fixed32.One, new fixed32(3, 0), -new fixed32(6, 0)),
			},
		};
	}

	public readonly struct DotProductTestCase {
		public FixedVector3 A { get; init; } 
		public FixedVector3 B { get; init; } 
		public fixed32 Expected { get; init; } 
	}

	public readonly struct CrossProductTestCase {
		public FixedVector3 A { get; init; } 
		public FixedVector3 B { get; init; } 
		public FixedVector3 Expected { get; init; } 
	}

}