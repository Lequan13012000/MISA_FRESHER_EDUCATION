using Misa.Fresher.Education.Core.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using Misa.Fresher.Education.Core.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Misa.Fresher.Education.Core.Interfaces.Repository;

namespace Misa.Fresher.Education.Infrastructure.Repository
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<Exercise> _exerciseCollection;
        private readonly IMongoCollection<Attachment> _attachmentCollection;
        public ExerciseRepository(EducationDatabaseSettings educationDatabaseSettings) : base(educationDatabaseSettings)
        {
            _client = new MongoClient(educationDatabaseSettings.ConnectionString);
            IMongoDatabase database = _client.GetDatabase(educationDatabaseSettings.DatabaseName);
            _exerciseCollection = database.GetCollection<Exercise>(nameof(Exercise));
            _attachmentCollection = database.GetCollection<Attachment>(nameof(Attachment));
        }

        public async Task<Tuple<IEnumerable<Exercise>, int, int>> Filter(int pageSize, int pageIndex, string? searchText, Guid? gradeId, Guid? subjectId, Guid? topicId)
        {
            FilterDefinition<Exercise> filter = Builders<Exercise>.Filter.Empty;

            // filter exercise name
            if (!string.IsNullOrEmpty(searchText))
            {
                FilterDefinition<Exercise> filterExerciseName = Builders<Exercise>.Filter.Regex(exercise => exercise.ExerciseName, new BsonRegularExpression($".*{searchText}.*", "i"));
                filter &= filterExerciseName;
            }

            // filter grade
            if (gradeId is not null)
            {
                FilterDefinition<Exercise> filterGrade = Builders<Exercise>.Filter.Eq(exercise => exercise.GradeId, gradeId);
                filter &= filterGrade;
            }

            // filter subject
            if (subjectId is not null)
            {
                FilterDefinition<Exercise> filterSubject = Builders<Exercise>.Filter.Eq(exercise => exercise.SubjectId, subjectId);
                filter &= filterSubject;
            }

            // filter topic
            if (topicId is not null)
            {
                FilterDefinition<Exercise> filterTopic = Builders<Exercise>.Filter.AnyEq("TopicIds", topicId);
                filter &= filterTopic;
            }


            // total record facet
            var countFacet = AggregateFacet.Create("totalRecords", PipelineDefinition<Exercise, AggregateCountResult>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Count<Exercise>()
            }));

            SortDefinition<Exercise> sortDefinition = Builders<Exercise>.Sort.Descending(exercise => exercise.CreatedDate);

            // exercises facet
            var dataFacet = AggregateFacet.Create("exercises", PipelineDefinition<Exercise, Exercise>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Sort(sortDefinition),
                PipelineStageDefinitionBuilder.Skip<Exercise>((pageIndex - 1) * pageSize),
                PipelineStageDefinitionBuilder.Limit<Exercise>(pageSize)
            }));

            var aggregate = await _baseCollection.Aggregate().Match(filter)
                .As<Exercise>()
                .Facet(countFacet, dataFacet).ToListAsync();

            // get total records
            long? totalRecords = aggregate.First().Facets
                .First(x => x.Name == "totalRecords")
                .Output<AggregateCountResult>()
                .FirstOrDefault()?.Count;

            // get exercises
            var exercises = aggregate.First()
                .Facets.First(x => x.Name == "exercises")
                .Output<Exercise>();

            // if total records is null => return empty
            if (totalRecords is null)
            {
                return new Tuple<IEnumerable<Exercise>, int, int>(new List<Exercise>(), 0, 0);
            }

            // calculate total pages
            int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

            return new Tuple<IEnumerable<Exercise>, int, int>(exercises, (int)totalRecords, totalPage);

        }
    }
}
