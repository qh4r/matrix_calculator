using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Models
{
    using System.Collections.ObjectModel;
    using System.Globalization;

    using GalaSoft.MvvmLight;

    using MatrixLibrary;

    public class MatrixModel : ViewModelBase
    {
        private ObservableCollection<CellModel> cellsCollection;

        private int rows;

        private int columns;

        public MatrixModel(MatrixLibrary.Matrix matrix)
        {
            this.CellsCollection = new ObservableCollection<CellModel>(matrix.Select(x => new CellModel(x)));
            this.Rows = matrix.RowsCount;
            this.Columns = matrix.ColumnsCount;
        }

        public Matrix Matrix => new Matrix(this.rows,this.columns, this.cellsCollection.Select(x => x.NumericValue()));

        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                Set(ref columns, value);
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                Set(ref rows, value);
            }
        }

        public ObservableCollection<CellModel> CellsCollection
        {
            get
            {
                return cellsCollection;
            }
            set
            {
                Set(ref cellsCollection, value);
            }
        }
    }

    public class CellModel : ViewModelBase
    {
        private string valueString;

        public CellModel(double value)
        {
            this.valueString = value.ToString(CultureInfo.InvariantCulture);
        }

        public string ValueString
        {
            get
            {
                return this.valueString;
            }
            set
            {
                Set(ref this.valueString, value);
            }
        }

        public double NumericValue()
        {
            double output;
            return double.TryParse(
                valueString.Replace(",", "."),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out output)
                       ? output
                       : 0;
        }
    }
}

