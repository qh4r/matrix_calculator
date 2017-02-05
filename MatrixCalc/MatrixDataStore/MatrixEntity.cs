namespace MatrixDataStore
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MatrixEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public string Content { get; set; }
    }
}