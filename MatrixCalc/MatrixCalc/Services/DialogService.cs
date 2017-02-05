using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Services
{
    using System.Runtime.InteropServices.WindowsRuntime;

    using MatrixCalc.Models;

    public class DialogService
    {
        private object transportationDataLock = new object();

        private MatrixName transportationData;

        public DialogService()
        {
        }

        private void SetTransportationData(MatrixName data)
        {
            lock (this.transportationDataLock)
            {
                this.transportationData = data;
            }
        }

        public MatrixName GetTransportationData()
        {
            lock (this.transportationDataLock)
            {
                var temp = this.transportationData;
                return temp;
            }
        }

        public void OpenSaveWindow(MatrixName matrixName)
        {
            SetTransportationData(matrixName);
            var dialog = new SaveMatrixWindow();
            dialog.ShowDialog();
        }
    }
}
