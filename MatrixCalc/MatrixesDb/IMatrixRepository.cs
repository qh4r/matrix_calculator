using System.Collections.Generic;
using System.Threading.Tasks;
using MatrixesDb.Migrations.Types;
using MatrixLibrary;

namespace MatrixesDb
{
    public interface IMatrixRepository
    {
        Task<bool> DeleteMatrix(long id);
        Matrix GetMatrixDetails(long id);
        IEnumerable<MatrixMeta> GetMatrixesList();
        Task<MatrixMeta> SaveMatrix(IMatrix matrix, string name);
    }
}