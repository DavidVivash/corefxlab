// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

using System.Runtime.InteropServices;

namespace System.Numerics.Matrices
{
    /// <summary>
    /// Represents a matrix of double precision floating-point values defined by its number of columns and rows
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4x4: IEquatable<Matrix4x4>, IMatrix
    {
        public const int ColumnCount = 4;
        public const int RowCount = 4;

        static Matrix4x4()
        {
            Zero = new Matrix4x4(0);
            Identity = new Matrix4x4(1, 0, 0, 0, 
                                     0, 1, 0, 0, 
                                     0, 0, 1, 0, 
                                     0, 0, 0, 1);
        }

        
        /// <summary>
        /// Constant Matrix4x4 with value intialized to the identity of a 4 x 4 matrix
        /// </summary>
        public static readonly Matrix4x4 Identity;
        /// <summary>
        /// Gets the smallest value used to determine equality
        /// </summary>
        public double Epsilon { get { return MatrixHelper.Epsilon; } }

        /// <summary>
        /// Constant Matrix4x4 with all values initialized to zero
        /// </summary>
        public static readonly Matrix4x4 Zero;

        /// <summary>
        /// Initializes a Matrix4x4 with all of it values specifically set
        /// </summary>
        /// <param name="m11">The column 1, row 1 value</param>
        /// <param name="m21">The column 2, row 1 value</param>
        /// <param name="m31">The column 3, row 1 value</param>
        /// <param name="m41">The column 4, row 1 value</param>
        /// <param name="m12">The column 1, row 2 value</param>
        /// <param name="m22">The column 2, row 2 value</param>
        /// <param name="m32">The column 3, row 2 value</param>
        /// <param name="m42">The column 4, row 2 value</param>
        /// <param name="m13">The column 1, row 3 value</param>
        /// <param name="m23">The column 2, row 3 value</param>
        /// <param name="m33">The column 3, row 3 value</param>
        /// <param name="m43">The column 4, row 3 value</param>
        /// <param name="m14">The column 1, row 4 value</param>
        /// <param name="m24">The column 2, row 4 value</param>
        /// <param name="m34">The column 3, row 4 value</param>
        /// <param name="m44">The column 4, row 4 value</param>
        public Matrix4x4(double m11, double m21, double m31, double m41, 
                         double m12, double m22, double m32, double m42, 
                         double m13, double m23, double m33, double m43, 
                         double m14, double m24, double m34, double m44)
        {
            M11 = m11; M21 = m21; M31 = m31; M41 = m41; 
            M12 = m12; M22 = m22; M32 = m32; M42 = m42; 
            M13 = m13; M23 = m23; M33 = m33; M43 = m43; 
            M14 = m14; M24 = m24; M34 = m34; M44 = m44; 
        }

        /// <summary>
        /// Initialized a Matrix4x4 with all values set to the same value
        /// </summary>
        /// <param name="value">The value to set all values to</param>
        public Matrix4x4(double value)
        {
            M11 = M21 = M31 = M41 = 
            M12 = M22 = M32 = M42 = 
            M13 = M23 = M33 = M43 = 
            M14 = M24 = M34 = M44 = value;
        }

        public double M11;
        public double M21;
        public double M31;
        public double M41;
        public double M12;
        public double M22;
        public double M32;
        public double M42;
        public double M13;
        public double M23;
        public double M33;
        public double M43;
        public double M14;
        public double M24;
        public double M34;
        public double M44;

        public unsafe double this[int col, int row]
        {
            get
            {
                if (col < 0 || col >= ColumnCount)
                    throw new ArgumentOutOfRangeException("col", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", ColumnCount, col));
                if (row < 0 || row >= RowCount)
                    throw new ArgumentOutOfRangeException("row", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", RowCount, row));

                fixed (Matrix4x4* p = &this)
                {
                    double* d = (double*)p;
                    return d[row * ColumnCount + col];
                }
            }
            set
            {
                if (col < 0 || col >= ColumnCount)
                    throw new ArgumentOutOfRangeException("col", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", ColumnCount, col));
                if (row < 0 || row >= RowCount)
                    throw new ArgumentOutOfRangeException("row", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", RowCount, row));

                fixed (Matrix4x4* p = &this)
                {
                    double* d = (double*)p;
                    d[row * ColumnCount + col] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of columns in the matrix
        /// </summary>
        public int Columns { get { return ColumnCount; } }
        /// <summary>
        /// Get the number of rows in the matrix
        /// </summary>
        public int Rows { get { return RowCount; } }

        /// <summary>
        /// Gets a new Matrix1x4 containing the values of column 1
        /// </summary>
        public Matrix1x4 Column1 { get { return new Matrix1x4(M11, M12, M13, M14); } }
        /// <summary>
        /// Gets a new Matrix1x4 containing the values of column 2
        /// </summary>
        public Matrix1x4 Column2 { get { return new Matrix1x4(M21, M22, M23, M24); } }
        /// <summary>
        /// Gets a new Matrix1x4 containing the values of column 3
        /// </summary>
        public Matrix1x4 Column3 { get { return new Matrix1x4(M31, M32, M33, M34); } }
        /// <summary>
        /// Gets a new Matrix1x4 containing the values of column 4
        /// </summary>
        public Matrix1x4 Column4 { get { return new Matrix1x4(M41, M42, M43, M44); } }
        /// <summary>
        /// Gets a new Matrix4x1 containing the values of column 1
        /// </summary>
        public Matrix4x1 Row1 { get { return new Matrix4x1(M11, M21, M31, M41); } }
        /// <summary>
        /// Gets a new Matrix4x1 containing the values of column 2
        /// </summary>
        public Matrix4x1 Row2 { get { return new Matrix4x1(M12, M22, M32, M42); } }
        /// <summary>
        /// Gets a new Matrix4x1 containing the values of column 3
        /// </summary>
        public Matrix4x1 Row3 { get { return new Matrix4x1(M13, M23, M33, M43); } }
        /// <summary>
        /// Gets a new Matrix4x1 containing the values of column 4
        /// </summary>
        public Matrix4x1 Row4 { get { return new Matrix4x1(M14, M24, M34, M44); } }

        public override bool Equals(object obj)
        {
            if (obj is Matrix4x4)
                return this == (Matrix4x4)obj;

            return false;
        }

        public bool Equals(Matrix4x4 other)
        {
            return this == other;
        }

        public unsafe override int GetHashCode()
        {
            fixed (Matrix4x4* p = &this)
            {
                int* x = (int*)p;
                unchecked
                {
                    return 0xFFE1
                         + 7 * ((((x[00] ^ x[01]) << 0) + ((x[02] ^ x[03]) << 1) + ((x[04] ^ x[05]) << 2) + ((x[06] ^ x[07]) << 3)) << 0)
                         + 7 * ((((x[04] ^ x[05]) << 0) + ((x[06] ^ x[07]) << 1) + ((x[08] ^ x[09]) << 2) + ((x[10] ^ x[11]) << 3)) << 1)
                         + 7 * ((((x[08] ^ x[09]) << 0) + ((x[10] ^ x[11]) << 1) + ((x[12] ^ x[13]) << 2) + ((x[14] ^ x[15]) << 3)) << 2)
                         + 7 * ((((x[12] ^ x[13]) << 0) + ((x[14] ^ x[15]) << 1) + ((x[16] ^ x[17]) << 2) + ((x[18] ^ x[19]) << 3)) << 3);
                }
            }
        }

        public override string ToString()
        {
            return "Matrix4x4: "
                 + String.Format("{{|{0:00}|{1:00}|{2:00}|{3:00}|}}", M11, M21, M31, M41)
                 + String.Format("{{|{0:00}|{1:00}|{2:00}|{3:00}|}}", M12, M22, M32, M42)
                 + String.Format("{{|{0:00}|{1:00}|{2:00}|{3:00}|}}", M13, M23, M33, M43)
                 + String.Format("{{|{0:00}|{1:00}|{2:00}|{3:00}|}}", M14, M24, M34, M44); 
        }

        /// <summary>
        /// Creates and returns a transposed matrix
        /// </summary>
        /// <returns>Matrix with transposed values</returns>
        public Matrix4x4 Transpose()
        {
            return new Matrix4x4(M11, M12, M13, M14, 
                                 M21, M22, M23, M24, 
                                 M31, M32, M33, M34, 
                                 M41, M42, M43, M44);
        }

        public static bool operator ==(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            return MatrixHelper.AreEqual(matrix1.M11, matrix2.M11)
                && MatrixHelper.AreEqual(matrix1.M21, matrix2.M21)
                && MatrixHelper.AreEqual(matrix1.M31, matrix2.M31)
                && MatrixHelper.AreEqual(matrix1.M41, matrix2.M41)
                && MatrixHelper.AreEqual(matrix1.M12, matrix2.M12)
                && MatrixHelper.AreEqual(matrix1.M22, matrix2.M22)
                && MatrixHelper.AreEqual(matrix1.M32, matrix2.M32)
                && MatrixHelper.AreEqual(matrix1.M42, matrix2.M42)
                && MatrixHelper.AreEqual(matrix1.M13, matrix2.M13)
                && MatrixHelper.AreEqual(matrix1.M23, matrix2.M23)
                && MatrixHelper.AreEqual(matrix1.M33, matrix2.M33)
                && MatrixHelper.AreEqual(matrix1.M43, matrix2.M43)
                && MatrixHelper.AreEqual(matrix1.M14, matrix2.M14)
                && MatrixHelper.AreEqual(matrix1.M24, matrix2.M24)
                && MatrixHelper.AreEqual(matrix1.M34, matrix2.M34)
                && MatrixHelper.AreEqual(matrix1.M44, matrix2.M44);
        }

        public static bool operator !=(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            return MatrixHelper.NotEqual(matrix1.M11, matrix2.M11)
                || MatrixHelper.NotEqual(matrix1.M21, matrix2.M21)
                || MatrixHelper.NotEqual(matrix1.M31, matrix2.M31)
                || MatrixHelper.NotEqual(matrix1.M41, matrix2.M41)
                || MatrixHelper.NotEqual(matrix1.M12, matrix2.M12)
                || MatrixHelper.NotEqual(matrix1.M22, matrix2.M22)
                || MatrixHelper.NotEqual(matrix1.M32, matrix2.M32)
                || MatrixHelper.NotEqual(matrix1.M42, matrix2.M42)
                || MatrixHelper.NotEqual(matrix1.M13, matrix2.M13)
                || MatrixHelper.NotEqual(matrix1.M23, matrix2.M23)
                || MatrixHelper.NotEqual(matrix1.M33, matrix2.M33)
                || MatrixHelper.NotEqual(matrix1.M43, matrix2.M43)
                || MatrixHelper.NotEqual(matrix1.M14, matrix2.M14)
                || MatrixHelper.NotEqual(matrix1.M24, matrix2.M24)
                || MatrixHelper.NotEqual(matrix1.M34, matrix2.M34)
                || MatrixHelper.NotEqual(matrix1.M44, matrix2.M44);
        }

        public static Matrix4x4 operator +(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            double m11 = matrix1.M11 + matrix2.M11;
            double m21 = matrix1.M21 + matrix2.M21;
            double m31 = matrix1.M31 + matrix2.M31;
            double m41 = matrix1.M41 + matrix2.M41;
            double m12 = matrix1.M12 + matrix2.M12;
            double m22 = matrix1.M22 + matrix2.M22;
            double m32 = matrix1.M32 + matrix2.M32;
            double m42 = matrix1.M42 + matrix2.M42;
            double m13 = matrix1.M13 + matrix2.M13;
            double m23 = matrix1.M23 + matrix2.M23;
            double m33 = matrix1.M33 + matrix2.M33;
            double m43 = matrix1.M43 + matrix2.M43;
            double m14 = matrix1.M14 + matrix2.M14;
            double m24 = matrix1.M24 + matrix2.M24;
            double m34 = matrix1.M34 + matrix2.M34;
            double m44 = matrix1.M44 + matrix2.M44;

            return new Matrix4x4(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43, 
                                 m14, m24, m34, m44);
        }

        public static Matrix4x4 operator -(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            double m11 = matrix1.M11 - matrix2.M11;
            double m21 = matrix1.M21 - matrix2.M21;
            double m31 = matrix1.M31 - matrix2.M31;
            double m41 = matrix1.M41 - matrix2.M41;
            double m12 = matrix1.M12 - matrix2.M12;
            double m22 = matrix1.M22 - matrix2.M22;
            double m32 = matrix1.M32 - matrix2.M32;
            double m42 = matrix1.M42 - matrix2.M42;
            double m13 = matrix1.M13 - matrix2.M13;
            double m23 = matrix1.M23 - matrix2.M23;
            double m33 = matrix1.M33 - matrix2.M33;
            double m43 = matrix1.M43 - matrix2.M43;
            double m14 = matrix1.M14 - matrix2.M14;
            double m24 = matrix1.M24 - matrix2.M24;
            double m34 = matrix1.M34 - matrix2.M34;
            double m44 = matrix1.M44 - matrix2.M44;

            return new Matrix4x4(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43, 
                                 m14, m24, m34, m44);
        }

        public static Matrix4x4 operator *(Matrix4x4 matrix, double scalar)
        {
            double m11 = matrix.M11 * scalar;
            double m21 = matrix.M21 * scalar;
            double m31 = matrix.M31 * scalar;
            double m41 = matrix.M41 * scalar;
            double m12 = matrix.M12 * scalar;
            double m22 = matrix.M22 * scalar;
            double m32 = matrix.M32 * scalar;
            double m42 = matrix.M42 * scalar;
            double m13 = matrix.M13 * scalar;
            double m23 = matrix.M23 * scalar;
            double m33 = matrix.M33 * scalar;
            double m43 = matrix.M43 * scalar;
            double m14 = matrix.M14 * scalar;
            double m24 = matrix.M24 * scalar;
            double m34 = matrix.M34 * scalar;
            double m44 = matrix.M44 * scalar;

            return new Matrix4x4(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43, 
                                 m14, m24, m34, m44);
        }

        public static Matrix4x4 operator *(double scalar, Matrix4x4 matrix)
        {
            double m11 = scalar * matrix.M11;
            double m21 = scalar * matrix.M21;
            double m31 = scalar * matrix.M31;
            double m41 = scalar * matrix.M41;
            double m12 = scalar * matrix.M12;
            double m22 = scalar * matrix.M22;
            double m32 = scalar * matrix.M32;
            double m42 = scalar * matrix.M42;
            double m13 = scalar * matrix.M13;
            double m23 = scalar * matrix.M23;
            double m33 = scalar * matrix.M33;
            double m43 = scalar * matrix.M43;
            double m14 = scalar * matrix.M14;
            double m24 = scalar * matrix.M24;
            double m34 = scalar * matrix.M34;
            double m44 = scalar * matrix.M44;

            return new Matrix4x4(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43, 
                                 m14, m24, m34, m44);
        }

        public static Matrix1x4 operator *(Matrix4x4 matrix1, Matrix1x4 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11 + matrix1.M21 * matrix2.M12 + matrix1.M31 * matrix2.M13 + matrix1.M41 * matrix2.M14;
            double m12 = matrix1.M12 * matrix2.M11 + matrix1.M22 * matrix2.M12 + matrix1.M32 * matrix2.M13 + matrix1.M42 * matrix2.M14;
            double m13 = matrix1.M13 * matrix2.M11 + matrix1.M23 * matrix2.M12 + matrix1.M33 * matrix2.M13 + matrix1.M43 * matrix2.M14;
            double m14 = matrix1.M14 * matrix2.M11 + matrix1.M24 * matrix2.M12 + matrix1.M34 * matrix2.M13 + matrix1.M44 * matrix2.M14;

            return new Matrix1x4(m11, 
                                 m12, 
                                 m13, 
                                 m14);
        }
        public static Matrix2x4 operator *(Matrix4x4 matrix1, Matrix2x4 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11 + matrix1.M21 * matrix2.M12 + matrix1.M31 * matrix2.M13 + matrix1.M41 * matrix2.M14;
            double m21 = matrix1.M11 * matrix2.M21 + matrix1.M21 * matrix2.M22 + matrix1.M31 * matrix2.M23 + matrix1.M41 * matrix2.M24;
            double m12 = matrix1.M12 * matrix2.M11 + matrix1.M22 * matrix2.M12 + matrix1.M32 * matrix2.M13 + matrix1.M42 * matrix2.M14;
            double m22 = matrix1.M12 * matrix2.M21 + matrix1.M22 * matrix2.M22 + matrix1.M32 * matrix2.M23 + matrix1.M42 * matrix2.M24;
            double m13 = matrix1.M13 * matrix2.M11 + matrix1.M23 * matrix2.M12 + matrix1.M33 * matrix2.M13 + matrix1.M43 * matrix2.M14;
            double m23 = matrix1.M13 * matrix2.M21 + matrix1.M23 * matrix2.M22 + matrix1.M33 * matrix2.M23 + matrix1.M43 * matrix2.M24;
            double m14 = matrix1.M14 * matrix2.M11 + matrix1.M24 * matrix2.M12 + matrix1.M34 * matrix2.M13 + matrix1.M44 * matrix2.M14;
            double m24 = matrix1.M14 * matrix2.M21 + matrix1.M24 * matrix2.M22 + matrix1.M34 * matrix2.M23 + matrix1.M44 * matrix2.M24;

            return new Matrix2x4(m11, m21, 
                                 m12, m22, 
                                 m13, m23, 
                                 m14, m24);
        }
        public static Matrix3x4 operator *(Matrix4x4 matrix1, Matrix3x4 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11 + matrix1.M21 * matrix2.M12 + matrix1.M31 * matrix2.M13 + matrix1.M41 * matrix2.M14;
            double m21 = matrix1.M11 * matrix2.M21 + matrix1.M21 * matrix2.M22 + matrix1.M31 * matrix2.M23 + matrix1.M41 * matrix2.M24;
            double m31 = matrix1.M11 * matrix2.M31 + matrix1.M21 * matrix2.M32 + matrix1.M31 * matrix2.M33 + matrix1.M41 * matrix2.M34;
            double m12 = matrix1.M12 * matrix2.M11 + matrix1.M22 * matrix2.M12 + matrix1.M32 * matrix2.M13 + matrix1.M42 * matrix2.M14;
            double m22 = matrix1.M12 * matrix2.M21 + matrix1.M22 * matrix2.M22 + matrix1.M32 * matrix2.M23 + matrix1.M42 * matrix2.M24;
            double m32 = matrix1.M12 * matrix2.M31 + matrix1.M22 * matrix2.M32 + matrix1.M32 * matrix2.M33 + matrix1.M42 * matrix2.M34;
            double m13 = matrix1.M13 * matrix2.M11 + matrix1.M23 * matrix2.M12 + matrix1.M33 * matrix2.M13 + matrix1.M43 * matrix2.M14;
            double m23 = matrix1.M13 * matrix2.M21 + matrix1.M23 * matrix2.M22 + matrix1.M33 * matrix2.M23 + matrix1.M43 * matrix2.M24;
            double m33 = matrix1.M13 * matrix2.M31 + matrix1.M23 * matrix2.M32 + matrix1.M33 * matrix2.M33 + matrix1.M43 * matrix2.M34;
            double m14 = matrix1.M14 * matrix2.M11 + matrix1.M24 * matrix2.M12 + matrix1.M34 * matrix2.M13 + matrix1.M44 * matrix2.M14;
            double m24 = matrix1.M14 * matrix2.M21 + matrix1.M24 * matrix2.M22 + matrix1.M34 * matrix2.M23 + matrix1.M44 * matrix2.M24;
            double m34 = matrix1.M14 * matrix2.M31 + matrix1.M24 * matrix2.M32 + matrix1.M34 * matrix2.M33 + matrix1.M44 * matrix2.M34;

            return new Matrix3x4(m11, m21, m31, 
                                 m12, m22, m32, 
                                 m13, m23, m33, 
                                 m14, m24, m34);
        }
        public static Matrix4x4 operator *(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11 + matrix1.M21 * matrix2.M12 + matrix1.M31 * matrix2.M13 + matrix1.M41 * matrix2.M14;
            double m21 = matrix1.M11 * matrix2.M21 + matrix1.M21 * matrix2.M22 + matrix1.M31 * matrix2.M23 + matrix1.M41 * matrix2.M24;
            double m31 = matrix1.M11 * matrix2.M31 + matrix1.M21 * matrix2.M32 + matrix1.M31 * matrix2.M33 + matrix1.M41 * matrix2.M34;
            double m41 = matrix1.M11 * matrix2.M41 + matrix1.M21 * matrix2.M42 + matrix1.M31 * matrix2.M43 + matrix1.M41 * matrix2.M44;
            double m12 = matrix1.M12 * matrix2.M11 + matrix1.M22 * matrix2.M12 + matrix1.M32 * matrix2.M13 + matrix1.M42 * matrix2.M14;
            double m22 = matrix1.M12 * matrix2.M21 + matrix1.M22 * matrix2.M22 + matrix1.M32 * matrix2.M23 + matrix1.M42 * matrix2.M24;
            double m32 = matrix1.M12 * matrix2.M31 + matrix1.M22 * matrix2.M32 + matrix1.M32 * matrix2.M33 + matrix1.M42 * matrix2.M34;
            double m42 = matrix1.M12 * matrix2.M41 + matrix1.M22 * matrix2.M42 + matrix1.M32 * matrix2.M43 + matrix1.M42 * matrix2.M44;
            double m13 = matrix1.M13 * matrix2.M11 + matrix1.M23 * matrix2.M12 + matrix1.M33 * matrix2.M13 + matrix1.M43 * matrix2.M14;
            double m23 = matrix1.M13 * matrix2.M21 + matrix1.M23 * matrix2.M22 + matrix1.M33 * matrix2.M23 + matrix1.M43 * matrix2.M24;
            double m33 = matrix1.M13 * matrix2.M31 + matrix1.M23 * matrix2.M32 + matrix1.M33 * matrix2.M33 + matrix1.M43 * matrix2.M34;
            double m43 = matrix1.M13 * matrix2.M41 + matrix1.M23 * matrix2.M42 + matrix1.M33 * matrix2.M43 + matrix1.M43 * matrix2.M44;
            double m14 = matrix1.M14 * matrix2.M11 + matrix1.M24 * matrix2.M12 + matrix1.M34 * matrix2.M13 + matrix1.M44 * matrix2.M14;
            double m24 = matrix1.M14 * matrix2.M21 + matrix1.M24 * matrix2.M22 + matrix1.M34 * matrix2.M23 + matrix1.M44 * matrix2.M24;
            double m34 = matrix1.M14 * matrix2.M31 + matrix1.M24 * matrix2.M32 + matrix1.M34 * matrix2.M33 + matrix1.M44 * matrix2.M34;
            double m44 = matrix1.M14 * matrix2.M41 + matrix1.M24 * matrix2.M42 + matrix1.M34 * matrix2.M43 + matrix1.M44 * matrix2.M44;

            return new Matrix4x4(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43, 
                                 m14, m24, m34, m44);
        }
    }
}
