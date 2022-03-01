using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Infrastructure.Repository;

namespace Misa.Fresher.Education.Api.Controllers
{
    public class SubjectsController : BaseController<Subject>
    {
        public SubjectsController(IBaseRepository<Subject> baseRepository) : base(baseRepository)
        {
        }
    }
}
