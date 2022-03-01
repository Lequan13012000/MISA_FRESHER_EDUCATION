using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Topic
    {
        [BsonId]
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }
    }
}
