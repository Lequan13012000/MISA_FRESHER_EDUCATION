using Misa.Fresher.Education.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Interfaces.Service
{
    public interface IExerciseService
    {
        Task<Exercise> Filter(int pageSize, int pageIndex, string? searchText, Guid? gradeId, Guid? subjectId, Guid? topicId);
    }
}
