using System.Collections.Generic;

namespace MatrixApi.Models
{
    public class MatrixCreateData
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public IEnumerable<double> Content { get; set; }
    }
}