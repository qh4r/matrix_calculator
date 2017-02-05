namespace MatrixesDb.Migrations.Types
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using MatrixLibrary;

    internal class MatrixEntity
    {
        public MatrixEntity()
        {
            
        }

        public MatrixEntity(IMatrix matrix, string name)
        {
            this.Name = name;
            this.Rows = matrix.RowsCount;
            this.Columns = matrix.ColumnsCount;
            this.Content = matrix.Aggregate(string.Empty, (sum, item) => $"{sum} {item}").Trim();            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public string Content { get; set; }
    }
}