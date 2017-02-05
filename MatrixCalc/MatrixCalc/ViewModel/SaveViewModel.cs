using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Views;

    using MatrixCalc.Models;
    using MatrixCalc.Services;

    using MatrixesDb;

    public class SaveViewModel : ViewModelBase
    {
        private readonly DialogService dialogService;

        private readonly IMatrixRepository matrixRepository;

        private readonly MatrixesStore matrixesStore;

        private string matrixName;

        private bool isClosed;

        private MatrixName matrxSelection;

        private bool isProcessing;

        public SaveViewModel(DialogService dialogService, IMatrixRepository matrixRepository, MatrixesStore matrixesStore)
        {
            this.dialogService = dialogService;
            this.matrixRepository = matrixRepository;
            this.matrixesStore = matrixesStore;
            matrxSelection = dialogService.GetTransportationData();
        }

        public string MatrixName
        {
            get
            {
                return matrixName;
            }
            set
            {
                Set(ref this.matrixName, value);
                this.RaisePropertyChanged(() => NameFilled);
            }
        }

        public bool NameFilled => !string.IsNullOrWhiteSpace(MatrixName);

        public RelayCommand CancelCommand => new RelayCommand(() => { IsClosed = true; });

        public RelayCommand SaveCommand => new RelayCommand(
            async () =>
                {
                    if (!IsProcessing)
                    {
                        IsProcessing = true;

                        await new TaskFactory().StartNew(
                            async () =>
                                {
                                    await
                                        this.matrixRepository.SaveMatrix(
                                            this.matrxSelection == Models.MatrixName.FirstMatrix
                                                ? this.matrixesStore.FirstMatrix.Matrix
                                                : this.matrxSelection == Models.MatrixName.SecondMatrix
                                                      ? this.matrixesStore.SecondMatrix.Matrix
                                                      : this.matrixesStore.ResultMatrix.Matrix,
                                            MatrixName);
                                }).ContinueWith(
                                    async x =>
                                        {
                                            await DispatcherHelper.RunAsync(
                                                () =>
                                                    {
                                                        IsProcessing = false;
                                                        IsClosed = true;
                                                    });
                                        });
                    }
                });

        public bool IsProcessing
        {
            get
            {
                return isProcessing;
            }
            set
            {
                Set(ref isProcessing, value);
            }
        }

        public bool IsClosed
        {
            get
            {
                return isClosed;
            }
            set
            {
                Set(ref isClosed, value);
            }
        }
    }
}
