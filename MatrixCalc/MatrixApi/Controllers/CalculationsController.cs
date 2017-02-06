using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MatrixApi.Models;
using MatrixLibrary;

namespace MatrixApi.Controllers
{
    public class CalculationsController : ApiController
    {
        private readonly object _wronFormatMessage = new
        {
            message =
                "Error provide 2 matrixes, correct format example: " +
                "\"Matrix1\": { \"Rows\": 2, \"Columns\": 2, \"Content\":" +
                " [2,3,1,51] }, \"Matrix2\": {\"Rows\": 2, \"Columns\": 2" +
                ", \"Content\": [8,1,4,2]}"
        };

        [HttpPost]
        public object Add(MatrixPair matrixes)
        {
            return ProcessRequest(matrixes, (matrix1, matrix2) => matrix1 + matrix2);
        }

        [HttpPost]
        public object Subtract(MatrixPair matrixes)
        {
            return ProcessRequest(matrixes, (matrix1, matrix2) => matrix1 - matrix2);
        }

        [HttpPost]
        public object Multiply(MatrixPair matrixes)
        {
            return ProcessRequest(matrixes, (matrix1, matrix2) => matrix1 * matrix2);
        }

        private object ProcessRequest(MatrixPair matrixes, Func<Matrix, Matrix, Matrix> action)
        {
            if (ValidateMatrixes(matrixes))
            {
                var mat1 = new Matrix(matrixes.Matrix1.Rows, matrixes.Matrix1.Columns, matrixes.Matrix1.Content);
                var mat2 = new Matrix(matrixes.Matrix2.Rows, matrixes.Matrix2.Columns, matrixes.Matrix2.Content);
                try
                {
                    var result = action(mat1, mat2);
                    return new MatrixCalculationsData
                    {
                        Rows = result.RowsCount,
                        Columns = result.ColumnsCount,
                        Content = result.Select(x => x)
                    };
                }
                catch (Exception e)
                {
                    return new
                    {
                        message = e.Message
                    };
                }
            }
            return _wronFormatMessage;
        }


        private bool ValidateMatrixes(MatrixPair matrixes)
        {
            return matrixes != null && ValidateMatrix(matrixes.Matrix1) && ValidateMatrix(matrixes.Matrix2);
        }

        private bool ValidateMatrix(MatrixCalculationsData matrix)
        {
            return matrix?.Content != null && matrix.Content.Count() == matrix.Rows * matrix.Columns;
        }
    }
}