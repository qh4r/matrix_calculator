using GalaSoft.MvvmLight;

namespace MatrixCalc.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;

    using GalaSoft.MvvmLight.Command;

    using MatrixCalc.Models;

    using MatrixLibrary;

    public class MainViewModel : ViewModelBase
    {
        private MatrixModel firstMatrix;

        private MatrixModel secondMatrix;

        private MatrixModel resultMatrix;

        private string operationType;

        public MainViewModel()
        {
            FirstMatrix = new MatrixModel(new Matrix(2,2));
            SecondMatrix = new MatrixModel(new Matrix(2,2));
            ResultMatrix = null;
            OperationType = this.OperationTypes.First();
        }

        public List<string> OperationTypes { get; } = new List<string> {"+", "-", "*"};

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

        public RelayCommand PrintMatrix => new RelayCommand(
            () =>
        Debug.WriteLine(
            FirstMatrix.CellsCollection.Aggregate(string.Empty, (sum, next) => $"{sum}, {next.NumericValue().ToString()}")));

        public string OperationType
        {
            get
            {
                return operationType;
            }
            set
            {
                Debug.WriteLine(value);
                Set(ref this.operationType, value);
            }
        }

        private MatrixModel ChangeDimensions(Matrix matrix, int rowsDelta = 0, int columnsDelta = 0)
        {
            return new MatrixModel(new Matrix(Math.Max(matrix.RowsCount + rowsDelta, 1), Math.Max(matrix.ColumnsCount + columnsDelta, 1), matrix));
        }

        public RelayCommand PerformCalculationCommand => new RelayCommand(
            () =>
                {
                    ResultMatrix = PerformCalculation();
                });

        private MatrixModel PerformCalculation()
        {
            try
            {
                switch (OperationType)
                {

                    case "-":
                        return new MatrixModel(FirstMatrix.Matrix - SecondMatrix.Matrix);
                    case "*":
                        return new MatrixModel(FirstMatrix.Matrix * SecondMatrix.Matrix);
                    case "+":
                    default:
                        return new MatrixModel(FirstMatrix.Matrix + SecondMatrix.Matrix);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                return null;
            }
        }

        public RelayCommand<string> AddRow => new RelayCommand<string>(
            x =>
                {
                    if (x == "firstMatrix")
                    {
                        FirstMatrix = ChangeDimensions(FirstMatrix.Matrix, 1);
                    }
                    else
                    {
                        SecondMatrix = ChangeDimensions(SecondMatrix.Matrix, 1);
                    }
                });
        public RelayCommand<string> SubtractRow => new RelayCommand<string>(
            x =>
            {
                if (x == "firstMatrix")
                {
                    FirstMatrix = ChangeDimensions(FirstMatrix.Matrix, -1);
                }
                else
                {
                    SecondMatrix = ChangeDimensions(SecondMatrix.Matrix, -1);
                }
            });
        public RelayCommand<string> AddColumn => new RelayCommand<string>(
            x =>
            {
                if (x == "firstMatrix")
                {
                    FirstMatrix = ChangeDimensions(FirstMatrix.Matrix, 0, 1);
                }
                else
                {
                    SecondMatrix = ChangeDimensions(SecondMatrix.Matrix, 0, 1);
                }
            });
        public RelayCommand<string> SubtractColumn => new RelayCommand<string>(
            x =>
            {
                if (x == "firstMatrix")
                {
                    FirstMatrix = ChangeDimensions(FirstMatrix.Matrix, 0, -1);
                }
                else
                {
                    SecondMatrix = ChangeDimensions(SecondMatrix.Matrix, 0, -1);
                }
            });
    }
}