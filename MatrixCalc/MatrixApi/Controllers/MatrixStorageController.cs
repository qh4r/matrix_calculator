using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MatrixApi.Models;
using MatrixesDb;
using MatrixesDb.Migrations.Types;
using MatrixLibrary;

namespace MatrixApi.Controllers
{
    public class MatrixStorageController : ApiController
    {
        private readonly HttpError wrongFormatMessage =
           new HttpError("Error provide 2 matrixes, correct format example: " +
                         "{\"Name\": \"Example\", \"Rows\": 2, \"Columns\": 2, \"Content\":" +
                         " [2,3,1,51] }");

        private readonly IMatrixRepository matrixRepository;

        public MatrixStorageController(IMatrixRepository matrixRepository)
        {
            this.matrixRepository = matrixRepository;
        }

        public object Get()
        {
            try
            {
                return matrixRepository.GetMatrixesList();
            }
            catch (Exception e)
            {
                return new HttpError(e.Message);
            }
        }

        public object Get(long id)
        {
            try
            {
                return matrixRepository.GetMatrixDetails(id);
            }
            catch (Exception e)
            {
                return new HttpError(e.Message);
            }
        }

        public async Task<object> Delete(long id)
        {
            try
            {
                return (await matrixRepository.DeleteMatrix(id)) ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                return new HttpError(e.Message);
            }
        }

        public async Task<object> Post(MatrixCreateData matrix)
        {
            if (!ValidateMatrix(matrix))
            {
                return wrongFormatMessage;
            }
            try
            {
                return await matrixRepository.SaveMatrix(new Matrix(matrix.Rows, matrix.Columns, matrix.Content), matrix.Name);
            }
            catch (Exception e)
            {
                return new HttpError(e.Message);
            }
        }

        private bool ValidateMatrix(MatrixCreateData matrix)
        {
            return matrix?.Content != null && !string.IsNullOrWhiteSpace(matrix.Name) && matrix.Content.Count() == matrix.Rows * matrix.Columns;
        }
    }
}