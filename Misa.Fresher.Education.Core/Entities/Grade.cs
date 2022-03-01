using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Grade
    {
        [BsonId]
        public Guid GradeId { get; set; }   
        public string GradeName { get; set;}
    }
}
