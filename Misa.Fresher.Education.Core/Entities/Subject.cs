using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Subject
    {
        [BsonId]
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } 
        public string Image { get; set; }
    }
}
