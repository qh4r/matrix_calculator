using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
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
                    await this.matrixRepository.SaveMatrix(this.matrxSelection == Models.MatrixName.FirstMatrix 
                        ? this.matrixesStore.FirstMatrix.Matrix
                        : this.matrixesStore.SecondMatrix.Matrix,
                        MatrixName);
                    IsClosed = true;
                });

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
