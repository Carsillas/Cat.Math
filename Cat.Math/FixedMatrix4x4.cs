namespace Cat.Math;

public record struct FixedMatrix4x4 {

	private FixedVector4 _row0;
	private FixedVector4 _row1;
	private FixedVector4 _row2;
	private FixedVector4 _row3;
	
	public FixedVector4 Row0 { get => _row0; set => _row0 = value; }
	public FixedVector4 Row1 { get => _row1; set => _row1 = value; }
	public FixedVector4 Row2 { get => _row2; set => _row2 = value; }
	public FixedVector4 Row3 { get => _row3; set => _row3 = value; }

	public FixedVector4 Column0 {
		get => new(_row0.X, _row1.X, _row2.X, _row3.X);
		set {
			_row0.X = value.X;
			_row1.X = value.Y;
			_row2.X = value.Z;
			_row3.X = value.W;
		}
	}

	public FixedVector4 Column1 {
		get => new(_row0.Y, _row1.Y, _row2.Y, _row3.Y);
		set {
			_row0.Y = value.X;
			_row1.Y = value.Y;
			_row2.Y = value.Z;
			_row3.Y = value.W;
		}
	}

	public FixedVector4 Column2 {
		get => new(_row0.Z, _row1.Z, _row2.Z, _row3.Z);
		set {
			_row0.Z = value.X;
			_row1.Z = value.Y;
			_row2.Z = value.Z;
			_row3.Z = value.W;
		}
	}

	public FixedVector4 Column3 {
		get => new(_row0.W, _row1.W, _row2.W, _row3.W);
		set {
			_row0.W = value.X;
			_row1.W = value.Y;
			_row2.W = value.Z;
			_row3.W = value.W;
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
				case 3:
					return _row3[column];
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
				case 3:
					_row3[column] = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(row), row, null);
			}
		}
	}
	
	public static FixedMatrix4x4 Identity => new FixedMatrix4x4 {
		Row0 = FixedVector4.UnitX,
		Row1 = FixedVector4.UnitY,
		Row2 = FixedVector4.UnitZ,
		Row3 = FixedVector4.UnitW,
	};

	public FixedMatrix4x4(FixedVector4 row0, FixedVector4 row1, FixedVector4 row2, FixedVector4 row3) {
		Row0 = row0;
		Row1 = row1;
		Row2 = row2;
		Row3 = row3;
	}

	public fixed64 Determinant() {
		fixed64 det = fixed64.Zero;

		for (int column = 0; column < 4; column++) {
			fixed64 cofactor = MinorDeterminant(0, column);
			det += column % 2 == 0 ? this[column, 0] * cofactor : -this[column, 0] * cofactor;
		}

		return det;
	}

	public FixedMatrix4x4 Inverse() {
		fixed64 det = Determinant();

		if (det == fixed64.Zero) {
			throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
		}

		FixedMatrix4x4 result = default;

		for (int row = 0; row < 4; row++) {
			for (int column = 0; column < 4; column++) {
				fixed64 cofactor = MinorDeterminant(row, column);
				if ((row + column) % 2 != 0) {
					cofactor = -cofactor;
				}

				result[row, column] = (fixed32)(cofactor / det);
			}
		}

		return result;
	}

	public static FixedMatrix4x4 operator *(FixedMatrix4x4 a, FixedMatrix4x4 b) {
		return new FixedMatrix4x4 {
			Row0 = new FixedVector4(FixedVector4.Dot(a.Row0, b.Column0), FixedVector4.Dot(a.Row0, b.Column1), FixedVector4.Dot(a.Row0, b.Column2), FixedVector4.Dot(a.Row0, b.Column3)),
			Row1 = new FixedVector4(FixedVector4.Dot(a.Row1, b.Column0), FixedVector4.Dot(a.Row1, b.Column1), FixedVector4.Dot(a.Row1, b.Column2), FixedVector4.Dot(a.Row1, b.Column3)),
			Row2 = new FixedVector4(FixedVector4.Dot(a.Row2, b.Column0), FixedVector4.Dot(a.Row2, b.Column1), FixedVector4.Dot(a.Row2, b.Column2), FixedVector4.Dot(a.Row2, b.Column3)),
			Row3 = new FixedVector4(FixedVector4.Dot(a.Row3, b.Column0), FixedVector4.Dot(a.Row3, b.Column1), FixedVector4.Dot(a.Row3, b.Column2), FixedVector4.Dot(a.Row3, b.Column3)),
		};
	} 

	public static FixedVector4 operator *(FixedMatrix4x4 a, FixedVector4 b) {
		return new FixedVector4 {
			X = FixedVector4.Dot(a.Row0, b),
			Y = FixedVector4.Dot(a.Row1, b),
			Z = FixedVector4.Dot(a.Row2, b),
			W = FixedVector4.Dot(a.Row3, b)
		};
	}

	public static FixedMatrix4x4 operator *(fixed32 a, FixedMatrix4x4 b) {
		return new FixedMatrix4x4 {
			Row0 = b.Row0 * a,
			Row1 = b.Row1 * a,
			Row2 = b.Row2 * a,
			Row3 = b.Row3 * a,
		};
	}

	public static FixedMatrix4x4 operator *(FixedMatrix4x4 a, fixed32 b) {
		return new FixedMatrix4x4 {
			Row0 = a.Row0 * b,
			Row1 = a.Row1 * b,
			Row2 = a.Row2 * b,
			Row3 = a.Row3 * b,
		};
	}

	public static FixedMatrix4x4 operator /(FixedMatrix4x4 a, fixed32 b) {
		return new FixedMatrix4x4 {
			Row0 = a.Row0 / b,
			Row1 = a.Row1 / b,
			Row2 = a.Row2 / b,
			Row3 = a.Row3 / b,
		};
	}

	public FixedMatrix4x4 Transposed() {
		return new FixedMatrix4x4(Column0, Column1, Column2, Column3);
	}

	public void Transpose() {
		this = Transposed();
	}

	private fixed64 MinorDeterminant(int excludedRow, int excludedColumn) {
		fixed64[] values = new fixed64[9];
		int index = 0;

		for (int row = 0; row < 4; row++) {
			if (row == excludedRow) {
				continue;
			}

			for (int column = 0; column < 4; column++) {
				if (column == excludedColumn) {
					continue;
				}

				values[index++] = this[column, row];
			}
		}

		return Determinant3x3(
			values[0], values[1], values[2],
			values[3], values[4], values[5],
			values[6], values[7], values[8]);
	}

	private static fixed64 Determinant3x3(
		fixed64 row0X, fixed64 row0Y, fixed64 row0Z,
		fixed64 row1X, fixed64 row1Y, fixed64 row1Z,
		fixed64 row2X, fixed64 row2Y, fixed64 row2Z) {
		return row0X * (row1Y * row2Z - row1Z * row2Y)
		       - row0Y * (row1X * row2Z - row1Z * row2X)
		       + row0Z * (row1X * row2Y - row1Y * row2X);
	}

	public override string ToString() {
		return $"[{Row0}, {Row1}, {Row2}, {Row3}]";
	}
}
