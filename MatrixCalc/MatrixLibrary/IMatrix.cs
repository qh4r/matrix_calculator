using System.Collections.Generic;

namespace MatrixLibrary
{
    public interface IMatrix : IEnumerable<double>
    {
        double this[int iRow, int iCol] { get; set; }

        int ColumnsCount { get; }
        int RowsCount { get; }

        double GetSum();
        void SetColumn(int columnNumber, IEnumerable<double> elements);
        void SetRow(int rowNumber, IEnumerable<double> elements);
    }
}