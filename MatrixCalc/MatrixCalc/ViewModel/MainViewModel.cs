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
    using MatrixCalc.Services;

    using MatrixLibrary;

    public class MainViewModel : ViewModelBase
    {
        private readonly DialogService dialogService;

        private MatrixModel resultMatrix;

        private string operationType;

        public MainViewModel(MatrixesStore matrixesStore, DialogService dialogService)
        {
            this.dialogService = dialogService;
            this.MatrixesStore = matrixesStore;
            ResultMatrix = null;
            OperationType = this.OperationTypes.First();
        }

        public MatrixesStore MatrixesStore { get; }

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

        public RelayCommand<MatrixName> AddRow => new RelayCommand<MatrixName>(
            x =>
                {
                    if (x == MatrixName.FirstMatrix)
                    {
                        MatrixesStore.FirstMatrix = ChangeDimensions(MatrixesStore.FirstMatrix.Matrix, 1);
                    }
                    else
                    {
                        MatrixesStore.SecondMatrix = ChangeDimensions(MatrixesStore.SecondMatrix.Matrix, 1);
                    }
                });
        public RelayCommand<MatrixName> SubtractRow => new RelayCommand<MatrixName>(
            x =>
            {
                if (x == MatrixName.FirstMatrix)
                {
                    MatrixesStore.FirstMatrix = ChangeDimensions(MatrixesStore.FirstMatrix.Matrix, -1);
                }
                else
                {
                    MatrixesStore.SecondMatrix = ChangeDimensions(MatrixesStore.SecondMatrix.Matrix, -1);
                }
            });
        public RelayCommand<MatrixName> AddColumn => new RelayCommand<MatrixName>(
            x =>
            {
                if (x == MatrixName.FirstMatrix)
                {
                    MatrixesStore.FirstMatrix = ChangeDimensions(MatrixesStore.FirstMatrix.Matrix, 0, 1);
                }
                else
                {
                    MatrixesStore.SecondMatrix = ChangeDimensions(MatrixesStore.SecondMatrix.Matrix, 0, 1);
                }
            });

        public RelayCommand<MatrixName> SubtractColumn => new RelayCommand<MatrixName>(
            x =>
            {
                if (x == MatrixName.FirstMatrix)
                {
                    MatrixesStore.FirstMatrix = ChangeDimensions(MatrixesStore.FirstMatrix.Matrix, 0, -1);
                }
                else
                {
                    MatrixesStore.SecondMatrix = ChangeDimensions(MatrixesStore.SecondMatrix.Matrix, 0, -1);
                }
            });

        public RelayCommand<MatrixName> SaveMatrix => new RelayCommand<MatrixName>(
            x =>
                {
                    dialogService.OpenSaveWindow(x);
                });

        public RelayCommand<MatrixName> LoadMatrix => new RelayCommand<MatrixName>(x => { });

        private MatrixModel PerformCalculation()
        {
            try
            {
                switch (OperationType)
                {

                    case "-":
                        return new MatrixModel(MatrixesStore.FirstMatrix.Matrix - MatrixesStore.SecondMatrix.Matrix);
                    case "*":
                        return new MatrixModel(MatrixesStore.FirstMatrix.Matrix * MatrixesStore.SecondMatrix.Matrix);
                    case "+":
                    default:
                        return new MatrixModel(MatrixesStore.FirstMatrix.Matrix + MatrixesStore.SecondMatrix.Matrix);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                return null;
            }
        }
    }
}