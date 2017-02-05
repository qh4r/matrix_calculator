using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MatrixCalc.ViewModel
{
    using MatrixCalc.Services;

    using MatrixesDb;

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {          
            SimpleIoc.Default.Register<MatrixRepository>();
            SimpleIoc.Default.Register<IMatrixRepository>(() => SimpleIoc.Default.GetInstance<MatrixRepository>());
            SimpleIoc.Default.Register<DialogService>();
            SimpleIoc.Default.Register<MatrixesStore>();
            SimpleIoc.Default.Register<SaveViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

        public SaveViewModel Save => SimpleIoc.Default.GetInstanceWithoutCaching<SaveViewModel>();
    }
}