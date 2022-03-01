using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;

namespace Misa.Fresher.Education.Api.Controllers
{
    public class GradesController : BaseController<Grade>
    {
        public GradesController(IBaseRepository<Grade> baseRepository) : base(baseRepository)
        {
        }
    }
}
