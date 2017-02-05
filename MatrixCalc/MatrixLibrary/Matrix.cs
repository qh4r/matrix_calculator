using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    using System.Collections;

    public class Matrix : IMatrix
    {
        private readonly double[,] matrix;

        public Matrix(int rowsCount, int columnsCount)
        {
            this.RowsCount = rowsCount;
            this.ColumnsCount = columnsCount;
            this.matrix = new double[rowsCount, columnsCount];
        }

        public Matrix(int rowsCount, int columnsCount, IEnumerable<double> values) : this(rowsCount, columnsCount)
        {
            var valuesEnumerator = values.GetEnumerator();
            for (var i = 0; i < this.RowsCount; i++)
            {
                for (var j = 0; j < this.ColumnsCount; j++)
                {
                    var next = valuesEnumerator.MoveNext();
                    this.matrix[i, j] = next ? valuesEnumerator.Current : 0;
                }
            }
        }

        public Matrix(int rowsCount, int columnsCount, Matrix matrix) : this(rowsCount, columnsCount)
        {
            for (var i = 0; i < this.RowsCount; i++)
            {
                for (var j = 0; j < this.ColumnsCount; j++)
                {
                    this.matrix[i, j] = (i < matrix.RowsCount && j < matrix.ColumnsCount) ? matrix[i, j] : 0;
                }
            }
        }

        public int RowsCount { get; }

        public int ColumnsCount { get; }

        public double this[int iRow, int iCol]
        {
            get { return this.matrix[iRow, iCol]; }
            set { this.matrix[iRow, iCol] = value; }
        }

        public void SetColumn(int columnNumber, IEnumerable<double> elements)
        {
            var newValues = elements as double[] ?? elements.ToArray();
            if (this.RowsCount != newValues.Count())
            {
                throw new Exception("Wrong number of items");
            }
            if (columnNumber >= this.ColumnsCount)
            {
                throw new Exception("Columns number exceeds existing columns");
            }
            for (int i = 0; i < this.RowsCount; i++)
            {
                this.matrix[i, columnNumber] = newValues[i];
            }
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount) throw new Exception("Matrix dimensions must match!");
            var r = new Matrix(matrix1.RowsCount, matrix1.ColumnsCount);
            for (var i = 0; i < r.RowsCount; i++)
                for (var j = 0; j < r.ColumnsCount; j++)
                    r[i, j] = matrix1[i, j] + matrix2[i, j];
            return r;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount) throw new Exception("Matrix dimensions must match!");
            var r = new Matrix(matrix1.RowsCount, matrix1.ColumnsCount);
            for (var i = 0; i < r.RowsCount; i++)
                for (var j = 0; j < r.ColumnsCount; j++)
                    r[i, j] = matrix1[i, j] - matrix2[i, j];
            return r;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix2.RowsCount == matrix1.ColumnsCount)
            {
                return PerformCalculations(matrix1, matrix2);
            }
            else
            {
                throw new Exception("Matrixes can not be multiplied - number of columns in 1st must be equal to number of rows in 2nd");
            }
        }

        private static Matrix PerformCalculations(Matrix matrix1, Matrix matrix2)
        {
            var result = new Matrix(matrix1.RowsCount, matrix2.ColumnsCount);
            for (var i = 0; i < matrix1.RowsCount; i++)
            {
                for (var j = 0; j < matrix2.ColumnsCount; j++)
                {
                    double tmp = 0;
                    for (var k = 0; k < matrix2.RowsCount; k++)
                    {
                        tmp += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = tmp;
                }
            }
            return result;
        }

        public void SetRow(int rowNumber, IEnumerable<double> elements)
        {
            var newValues = elements as double[] ?? elements.ToArray();
            if (this.ColumnsCount != newValues.Count())
            {
                throw new Exception("Wrong number of items");
            }
            if (rowNumber >= this.RowsCount)
            {
                throw new Exception("Columns number exceeds existing columns");
            }
            for (var i = 0; i < this.ColumnsCount; i++)
            {
                this.matrix[rowNumber, i] = newValues[i];
            }
        }

        public IEnumerator<double> GetEnumerator() => this.matrix.Cast<double>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public double GetSum() => this.Aggregate(0.0, (sum, elem) => sum + elem);
    }
}
