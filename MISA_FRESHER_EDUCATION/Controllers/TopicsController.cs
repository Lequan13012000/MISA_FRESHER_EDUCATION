using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;

namespace Misa.Fresher.Education.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : BaseController<Topic>
    {
        private readonly ITopicRepository _topicRepository;

        public TopicsController(ITopicRepository topicRepository) : base(topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet("Filter")]
        public async Task<IEnumerable<Topic>> Filter([FromQuery] Guid gradeId, [FromQuery] Guid subjectId)
        {
            return await _topicRepository.Filter(gradeId, subjectId);
        }
    }
}
