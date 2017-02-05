using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixesDb
{
    using System.Globalization;

    using MatrixDataStore;

    using MatrixesDb.Migrations.Types;

    using MatrixLibrary;

    public class MatrixRepository : IMatrixRepository
    {
        private MatrixesContext context;

        public MatrixRepository()
        {
            this.context = new MatrixesContext();
        }

        public async Task<MatrixMeta> SaveMatrix(IMatrix matrix, string name)
        {
            var result = this.context.Matrixes.Add(new MatrixEntity(matrix, name));
            await this.context.SaveChangesAsync();
            return new MatrixMeta(result.Id, result.Name);
        }

        public IEnumerable<MatrixMeta> GetMatrixesList()
        {
            return this.context.Matrixes.Select(x => new MatrixMeta(x.Id, x.Name)).ToList();
        }

        public Matrix GetMatrixDetails(long id)
        {
            var result = this.context.Matrixes.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return new Matrix(result.Rows, result.Columns, result.Content.Split(' ').Select(
                    x =>
                    {
                        double output;
                        return double.TryParse(
                            x.Replace(",", "."),
                            NumberStyles.Float,
                            CultureInfo.InvariantCulture,
                            out output) ? output : 0;
                    }));
            }
            return new Matrix(1, 1);
        }

        public async Task<bool> DeleteMatrix(long id)
        {
            var matrix = this.context.Matrixes.FirstOrDefault(x => x.Id == id);
            this.context.Matrixes.Remove(matrix);
            var result = await this.context.SaveChangesAsync();
            return result != 0;
        }
    }
}
