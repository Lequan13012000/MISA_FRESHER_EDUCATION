using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Misa.Fresher.Education.Core.Setting;
using Misa.Fresher.Education.Core.Interfaces.Service;

namespace Misa.Fresher.Education.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : BaseController<Exercise>
    {
        private readonly HostDirection _hostDirection;
        private readonly IExerciseRepository _exerciseRepository;

        public ExercisesController(IExerciseRepository exerciseRepository, HostDirection hostDirection) : base(exerciseRepository)
        {
            _hostDirection = hostDirection;
            _exerciseRepository = exerciseRepository;

        }
        [HttpPost]
        public override async Task<IActionResult> Post([FromForm]Exercise entity)
        {
            // convert questions => list
            List<Question>? questions = new();
            if (entity.TextQuestions is not null)
            {
                questions = JsonConvert.DeserializeObject<List<Question>>(entity.TextQuestions);
            }

            entity.Questions = questions;

            if(entity.AvatarFile is not null)
            {
                IFormFile avatarFile = entity.AvatarFile;
                string imageName = $"{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(avatarFile.FileName)}";
                string avatarPath = Path.Combine(_hostDirection.SrcHost, "Image", "ExerciseAvatars", imageName);
                using var fileStream = new FileStream(avatarPath, FileMode.Create);
                await avatarFile.CopyToAsync(fileStream);
                entity.Avatar = $"https://localhost:7051/{Path.Combine("Image", "ExerciseAvatars", imageName)}";
            }

            Exercise result = await _baseRepository.Insert(entity);

            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] Exercise entity)
        {
            var exited = await _baseRepository.GetById(id);

            if (exited is null)
            {
                return NotFound();
            }
            entity.ExerciseId = exited.ExerciseId;
            // convert questions => list
            List<Question>? questions = new();
            if (entity.TextQuestions is not null)
            {
                questions = JsonConvert.DeserializeObject<List<Question>>(entity.TextQuestions);
            }

            entity.Questions = questions;

            if (entity.AvatarFile is not null)
            {
                IFormFile avatarFile = entity.AvatarFile;
                string imageName = $"{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(avatarFile.FileName)}";
                string avatarPath = Path.Combine(_hostDirection.SrcHost, "Image", "ExerciseAvatars", imageName);
                using var fileStream = new FileStream(avatarPath, FileMode.Create);
                await avatarFile.CopyToAsync(fileStream);
                entity.Avatar = $"https://localhost:7051/{Path.Combine("Image", "ExerciseAvatars", imageName)}";
            }

            Exercise result = await _baseRepository.Update(entity,id);
 

            return CreatedAtAction(nameof(Get), result);
        }

        [HttpGet("Filter")]
        public async Task<ExercisePage> Filter(int pageSize, int pageIndex, string? searchText, Guid? gradeId, Guid? subjectId, Guid? topicId)
        {
            Tuple<IEnumerable<Exercise>, int, int> result = await _exerciseRepository.Filter(pageSize, pageIndex, searchText, gradeId, subjectId, topicId);
            ExercisePage page = new()
            {
                Data = result.Item1,
                TotalRecords = result.Item2,
                TotalPages = result.Item3,
            };
            return page;
        }
    }
}
