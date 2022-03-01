using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.MisaAttribute;
using Misa.Fresher.Education.Infrastructure.Repository;

namespace Misa.Fresher.Education.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase
    {
        protected readonly IBaseRepository<MISAEntity> _baseRepository;

        public BaseController(IBaseRepository<MISAEntity> baseRepository) =>
            _baseRepository = baseRepository;

        [HttpGet]
        public async Task<List<MISAEntity>> Get() =>
            await _baseRepository.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<MISAEntity>> Get(Guid id)
        {
            var entity = await _baseRepository.GetById(id);

            if (entity is null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(MISAEntity entity)
        {

            MISAEntity result = await _baseRepository.Insert(entity);
            
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, MISAEntity entity)
        {
            var exitedEntity = await _baseRepository.GetById(id);

            if (exitedEntity is null)
            {
                return NotFound();
            }
            var props = typeof(MISAEntity).GetProperties();
            foreach (var prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(PrimaryKey)))
                {
                    prop.SetValue(entity, prop.GetValue(exitedEntity));
                    break;
                }
            }
            await _baseRepository.Update(entity, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exitedEntity = await _baseRepository.GetById(id);

            if (exitedEntity is null)
            {
                return NotFound();
            }
            await _baseRepository.Delete(id);

            return NoContent();
        }
    }
}
