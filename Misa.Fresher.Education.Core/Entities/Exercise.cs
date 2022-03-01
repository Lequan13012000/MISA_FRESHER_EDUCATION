using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Misa.Fresher.Education.Core.Entities
{
    public class Exercise
    {
        [BsonId]
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public Guid? GradeId { get; set; }
        public string? GradeName { get; set; }
        public Guid? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public List<Question> Questions { get; set; } = new();
        [BsonIgnore]
        public string? TextQuestions { get; set; }
        public string? Avatar  { get; set; }
        [BsonIgnore]
        public IFormFile? AvatarFile { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }
    }
}
