using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class ExercisePage
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<Exercise> Data { get; set; } = new List<Exercise>();
    }
}
