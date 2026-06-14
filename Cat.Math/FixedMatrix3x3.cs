namespace Cat.Math;

public record struct FixedMatrix3x3 {
	private FixedVector3 _row0;
	private FixedVector3 _row1;
	private FixedVector3 _row2;
	
	public FixedVector3 Row0 {
		get => _row0;
		set => _row0 = value;
	}

	public FixedVector3 Row1 {
		get => _row1;
		set => _row1 = value;
	}

	public FixedVector3 Row2 {
		get => _row2;
		set => _row2 = value;
	}


	public FixedVector3 Column0 {
		get => new(_row0.X, _row1.X, _row2.X);
		set {
			_row0.X = value.X;
			_row1.X = value.Y;
			_row2.X = value.Z;
		}
	}

	public FixedVector3 Column1 {
		get => new(_row0.Y, _row1.Y, _row2.Y);
		set {
			_row0.Y = value.X;
			_row1.Y = value.Y;
			_row2.Y = value.Z;
		}
	}

	public FixedVector3 Column2 {
		get => new(_row0.Z, _row1.Z, _row2.Z);
		set {
			_row0.Z = value.X;
			_row1.Z = value.Y;
			_row2.Z = value.Z;
		}
	}

	public fixed32 this[int column, int row] {
		get {
			switch (row) {
				case 0:
					return _row0[column];
				case 1:
					return _row1[column];
				case 2:
					return _row2[column];
				default:
					throw new ArgumentOutOfRangeException(nameof(row), row, null);
			}
		}
		set {
			switch (row) {
				case 0:
					_row0[column] = value;
					break;
				case 1:
					_row1[column] = value;
					break;
				case 2:
					_row2[column] = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(row), row, null);
			}
		}
	}
	
	public static FixedMatrix3x3 Identity => new FixedMatrix3x3 {
		Row0 = FixedVector3.UnitX,
		Row1 = FixedVector3.UnitY,
		Row2 = FixedVector3.UnitZ
	};

	public FixedMatrix3x3(FixedVector3 row0, FixedVector3 row1, FixedVector3 row2) {
		Row0 = row0;
		Row1 = row1;
		Row2 = row2;
	}

	public fixed64 Determinant() {
		fixed64 row0X = Row0.X, row0Y = Row0.Y, row0Z = Row0.Z;
		fixed64 row1X = Row1.X, row1Y = Row1.Y, row1Z = Row1.Z;
		fixed64 row2X = Row2.X, row2Y = Row2.Y, row2Z = Row2.Z;

		return Determinant(
			row0X, row0Y, row0Z,
			row1X, row1Y, row1Z,
			row2X, row2Y, row2Z);
	}

	private static fixed64 Determinant(
		fixed64 row0X, fixed64 row0Y, fixed64 row0Z,
		fixed64 row1X, fixed64 row1Y, fixed64 row1Z,
		fixed64 row2X, fixed64 row2Y, fixed64 row2Z) {
		return row0X * (row1Y * row2Z - row1Z * row2Y)
		       - row0Y * (row1X * row2Z - row1Z * row2X)
		       + row0Z * (row1X * row2Y - row1Y * row2X);
	}

	public FixedMatrix3x3 Inverse() {
		// TODO remove copied implementation

		// Original implementation can be found here:
		// https://github.com/niswegmann/small-matrix-inverse/blob/6eac02b84ad06870692abaf828638a391548502c/invert3x3_c.h
		fixed64 row0X = Row0.X, row0Y = Row0.Y, row0Z = Row0.Z;
		fixed64 row1X = Row1.X, row1Y = Row1.Y, row1Z = Row1.Z;
		fixed64 row2X = Row2.X, row2Y = Row2.Y, row2Z = Row2.Z;

		// Compute the elements needed to calculate the determinant
		// so that we can throw without writing anything to the out parameter.
		fixed64 invRow0X = (row1Y * row2Z) - (row1Z * row2Y);
		fixed64 invRow1X = (-row1X * row2Z) + (row1Z * row2X);
		fixed64 invRow2X = (row1X * row2Y) - (row1Y * row2X);

		fixed64 det = (row0X * invRow0X) + (row0Y * invRow1X) + (row0Z * invRow2X);

		if (det == fixed64.Zero) {
			throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
		}

		FixedMatrix3x3 result = default;

		// Compute adjoint:
		result._row0.X = (fixed32)(invRow0X / det);
		result._row0.Y = (fixed32)(((-row0Y * row2Z) + (row0Z * row2Y)) / det);
		result._row0.Z = (fixed32)(((row0Y * row1Z) - (row0Z * row1Y)) / det);
		result._row1.X = (fixed32)(invRow1X / det);
		result._row1.Y = (fixed32)(((row0X * row2Z) - (row0Z * row2X)) / det);
		result._row1.Z = (fixed32)(((-row0X * row1Z) + (row0Z * row1X)) / det);
		result._row2.X = (fixed32)(invRow2X / det);
		result._row2.Y = (fixed32)(((-row0X * row2Y) + (row0Y * row2X)) / det);
		result._row2.Z = (fixed32)(((row0X * row1Y) - (row0Y * row1X)) / det);

		return result;
	}

	public static FixedMatrix3x3 operator *(FixedMatrix3x3 a, FixedMatrix3x3 b) {
		return new FixedMatrix3x3 {
			Row0 = new FixedVector3(FixedVector3.Dot(a.Row0, b.Column0), FixedVector3.Dot(a.Row0, b.Column1),
				FixedVector3.Dot(a.Row0, b.Column2)),
			Row1 = new FixedVector3(FixedVector3.Dot(a.Row1, b.Column0), FixedVector3.Dot(a.Row1, b.Column1),
				FixedVector3.Dot(a.Row1, b.Column2)),
			Row2 = new FixedVector3(FixedVector3.Dot(a.Row2, b.Column0), FixedVector3.Dot(a.Row2, b.Column1),
				FixedVector3.Dot(a.Row2, b.Column2)),
		};
	}

	public static FixedVector3 operator *(FixedMatrix3x3 a, FixedVector3 b) {
		return new FixedVector3 {
			X = FixedVector3.Dot(a.Row0, b),
			Y = FixedVector3.Dot(a.Row1, b),
			Z = FixedVector3.Dot(a.Row2, b)
		};
	}

	public FixedMatrix3x3 Transposed() {
		return new FixedMatrix3x3(Column0, Column1, Column2);
	}

	public void Transpose() {
		this = Transposed();
	}

	public static FixedMatrix3x3 operator *(fixed32 a, FixedMatrix3x3 b) {
		return new FixedMatrix3x3 {
			Row0 = b.Row0 * a,
			Row1 = b.Row1 * a,
			Row2 = b.Row2 * a,
		};
	}

	public static FixedMatrix3x3 operator *(FixedMatrix3x3 a, fixed32 b) {
		return new FixedMatrix3x3 {
			Row0 = a.Row0 * b,
			Row1 = a.Row1 * b,
			Row2 = a.Row2 * b,
		};
	}

	public static FixedMatrix3x3 operator /(FixedMatrix3x3 a, fixed32 b) {
		return new FixedMatrix3x3 {
			Row0 = a.Row0 / b,
			Row1 = a.Row1 / b,
			Row2 = a.Row2 / b,
		};
	}

	public override string ToString() {
		return $"[{Row0}, {Row1}, {Row2}]";
	}
}
