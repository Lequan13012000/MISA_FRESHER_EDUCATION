using Misa.Fresher.Education.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Interfaces.Repository
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        /// <summary>
        /// Filter exercises.
        /// CreatedBy: LEQUAN(28/02/2022)
        /// </summary>
        /// <param name="pageSize">page size</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="searchText">search text</param>
        /// <param name="gradeId">grade id</param>
        /// <param name="subjectId">subject id</param>
        /// <param name="topicId">topic id</param>
        /// <returns>filted exercises and total records, total pages</returns>
        Task<Tuple<IEnumerable<Exercise>, int, int>> Filter(int pageSize, int pageIndex, string? searchText, Guid? gradeId, Guid? subjectId, Guid? topicId);

        //Task Update(Guid exerciseId, Exercise exercise, Exercise oldExercise);
    }
}
