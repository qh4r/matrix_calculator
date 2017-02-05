using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Services
{
    using GalaSoft.MvvmLight;

    using MatrixCalc.Models;

    using MatrixLibrary;

    public class MatrixesStore : ViewModelBase
    {
        private MatrixModel secondMatrix;

        private MatrixModel firstMatrix;

        private MatrixModel resultMatrix;

        public MatrixesStore()
        {
            FirstMatrix = new MatrixModel(new Matrix(2, 2));
            SecondMatrix = new MatrixModel(new Matrix(2, 2));
            ResultMatrix = null;
        }

        public MatrixModel SecondMatrix
        {
            get
            {
                return secondMatrix;
            }
            set
            {
                Set(ref secondMatrix, value);
            }
        }

        public MatrixModel FirstMatrix
        {
            get
            {
                return firstMatrix;
            }
            set
            {
                Set(ref firstMatrix, value);
            }
        }

        public MatrixModel ResultMatrix
        {
            get
            {
                return resultMatrix;
            }
            set
            {
                Set(ref resultMatrix, value);
                this.RaisePropertyChanged(() => ResultPresent);
            }
        }

        public bool ResultPresent => ResultMatrix != null;
    }
}
