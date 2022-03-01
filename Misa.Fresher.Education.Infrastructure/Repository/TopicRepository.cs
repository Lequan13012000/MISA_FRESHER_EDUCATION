using Microsoft.Extensions.Options;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.Setting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Infrastructure.Repository
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(EducationDatabaseSettings educationDatabaseSettings) : base(educationDatabaseSettings)
        {
        }

        public async Task<IEnumerable<Topic>> Filter(Guid GradeId, Guid SubjectId)
        {
            FilterDefinition<Topic> Gradefilter = Builders<Topic>.Filter.Eq(topic => topic.GradeId, GradeId);
            FilterDefinition<Topic> Subjectfilter = Builders<Topic>.Filter.Eq(topic => topic.SubjectId, SubjectId);
            return await _baseCollection.Find(Gradefilter & Subjectfilter).ToListAsync();
        }
    }
}
