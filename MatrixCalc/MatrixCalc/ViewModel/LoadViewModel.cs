using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.ViewModel
{
    using System.Collections.ObjectModel;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Threading;

    using MatrixCalc.Models;
    using MatrixCalc.Services;

    using MatrixesDb;
    using MatrixesDb.Migrations.Types;

    public class LoadViewModel : ViewModelBase
    {
        private DialogService dialogService;

        private IMatrixRepository matrixRepository;

        private MatrixesStore matrixesStore;

        private MatrixName matrxSelection;

        private bool isClosed;

        private ObservableCollection<MatrixMeta> matrixesList;

        private MatrixMeta selectedItem;

        public LoadViewModel(DialogService dialogService, IMatrixRepository matrixRepository, MatrixesStore matrixesStore)
        {
            this.dialogService = dialogService;
            this.matrixRepository = matrixRepository;
            this.matrixesStore = matrixesStore;
            matrxSelection = dialogService.GetTransportationData();
        }

        public RelayCommand CloseCommand => new RelayCommand(() => { IsClosed = true; });
        public RelayCommand LoadCommand => new RelayCommand(
            () =>
                {
                    Task.Run(
                        async () =>
                        {
                            var result = matrixRepository.GetMatrixDetails(SelectedItem.Id);
                            await DispatcherHelper.RunAsync(
                                () =>
                                    {
                                        if (result == null) return;
                                        if (this.matrxSelection == MatrixName.FirstMatrix)
                                        {
                                            this.matrixesStore.FirstMatrix = new MatrixModel(result);
                                        }
                                        else
                                        {
                                            this.matrixesStore.SecondMatrix = new MatrixModel(result);
                                        }
                                    });
                        });                   
                });
        public RelayCommand DeleteCommand => new RelayCommand(
            () =>
                {
                    Task.Run(
                        async () =>
                        {
                            var result = await matrixRepository.DeleteMatrix(SelectedItem.Id);
                            await DispatcherHelper.RunAsync(
                                () =>
                                {
                                    if (result)
                                    {
                                        MatrixesList.Remove(SelectedItem);
                                        SelectedItem = null;
                                    }
                                });
                        });
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

        public RelayCommand OnLoaded => new RelayCommand(
            () =>
                {
                    Task.Run(
                        async () =>
                            {
                                var result = this.matrixRepository.GetMatrixesList();
                                await DispatcherHelper.RunAsync(
                                    () =>
                                        {
                                            MatrixesList = new ObservableCollection<MatrixMeta>(result);
                                        });
                            });

                });

        public ObservableCollection<MatrixMeta> MatrixesList
        {
            get
            {
                return matrixesList;
            }
            set
            {
                Set(ref matrixesList, value);
                this.RaisePropertyChanged(() => MatrixesLoaded);
            }
        }

        public bool MatrixesLoaded => MatrixesList != null;

        public MatrixMeta SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                Set(ref this.selectedItem, value);
                this.RaisePropertyChanged(() => ItemIsSelected);
            }
        }

        public bool ItemIsSelected => SelectedItem != null;
    }
}
