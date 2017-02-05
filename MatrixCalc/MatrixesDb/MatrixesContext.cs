using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixDataStore
{
    using System.Data.Entity;

    using MatrixesDb.Migrations.Types;

    internal class MatrixesContext : DbContext
    {
        public  DbSet<MatrixEntity> Matrixes { get; set; }
    }
}
