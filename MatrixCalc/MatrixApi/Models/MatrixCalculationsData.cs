using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixApi.Models
{
    public class MatrixCalculationsData
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public IEnumerable<double> Content { get; set; }
    }
}