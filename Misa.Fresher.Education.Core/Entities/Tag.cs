using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Tag
    {
       public Guid TagId { get; set; }
        public string TagName { get; set; } 
        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }


    }
}
