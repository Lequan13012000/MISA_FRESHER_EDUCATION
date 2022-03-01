using Misa.Fresher.Education.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Interfaces.Repository
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
        Task<IEnumerable<Topic>> Filter(Guid GradeId, Guid SubjectId);
    }
}
