using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.Setting;

namespace Misa.Fresher.Education.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : BaseController<Attachment> 
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly HostDirection _hostDirection;
        public AttachmentsController(IAttachmentRepository attachmentRepository, HostDirection hostDirection) : base(attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
            _hostDirection = hostDirection;
        }

        [HttpGet("GetFile/{id}")]
        public async Task<ActionResult> GetFile([FromRoute] Guid id, [FromQuery] bool isDownLoad)
        {
            Attachment attachment = await _attachmentRepository.GetById(id);

            string filePath = Path.Combine(_hostDirection.SrcHost, "Attachments", attachment.Path);
            if (attachment.IsTemp)
            {
                filePath = Path.Combine(_hostDirection.SrcHost, "TempAttachments", attachment.Path);

            }
            var dataBytes = System.IO.File.ReadAllBytes(filePath);
            Stream stream = new MemoryStream(dataBytes);

            string contentType = attachment.ContentType;
            string fileName = attachment.FileName;
            if (isDownLoad)
            {
                return File(stream, contentType, fileName);
            }
            return File(stream, contentType);
        }

        [HttpPost]
        public override async Task<IActionResult> Post([FromForm] Attachment entity)
        {
            entity.AttachmentId = Guid.NewGuid();

            if (entity.Resource is not null)
            {
                string extension = Path.GetExtension(entity.Resource.FileName).ToLower();
                string fileName = $"{Path.GetFileNameWithoutExtension(entity.Resource.FileName)}-{DateTime.Now.ToString("yymmssfff")}{extension}";
                string filePath = Path.Combine(_hostDirection.SrcHost, "TempAttachments", fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await entity.Resource.CopyToAsync(fileStream);
                entity.FileName = Path.GetFileName(entity.Resource.FileName);
                entity.Path = fileName;

                // content type
                switch (extension)
                {
                    case ".jpg":
                        entity.ContentType = "image/jpeg";
                        break;
                    case ".jpeg":
                        entity.ContentType = "image/jpeg";
                        break;
                    case ".png":
                        entity.ContentType = "image/png";
                        break;
                    case ".gif":
                        entity.ContentType = "image/gif";
                        break;
                    case ".svg":
                        entity.ContentType = "image/svg";
                        break;
                    case ".ico":
                        entity.ContentType = "image/ico";
                        break;
                    case ".docx":
                        entity.ContentType = "application/msword";
                        break;
                    case ".doc":
                        entity.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".pdf":
                        entity.ContentType = "application/pdf";
                        break;
                    case ".ppt":
                        entity.ContentType = "application/vnd.ms-powerpoint";
                        break;
                    case ".pptx":
                        entity.ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                        break;
                    case ".xls":
                        entity.ContentType = "application/vnd.ms-excel";
                        break;
                    case ".xlsx":
                        entity.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    case ".zip":
                        entity.ContentType = "application/zip";
                        break;
                    case ".7z":
                        entity.ContentType = "application/x-7z-compressed";
                        break;
                    case ".rar":
                        entity.ContentType = "application/vnd.rar";
                        break;
                    case ".txt":
                        entity.ContentType = "text/plain";
                        break;
                    case ".mp4":
                        entity.ContentType = "video/mp4";
                        break;
                    case ".mp3":
                        entity.ContentType = "audio/mp3";
                        break;
                    default:
                        break;
                }
            }
            entity.Resource = null;
            Attachment attachment = await _baseRepository.Insert(entity);
            return CreatedAtAction(nameof(Get), attachment);
        }

        [HttpPost("Copy")]
        public async Task<ActionResult> Copy(List<Attachment> attachments)
        {
            foreach (Attachment attachment in attachments)
            {
                string filePath = Path.Combine(_hostDirection.SrcHost, "TempAttachments", attachment.Path);
                string newFilePath = Path.Combine(_hostDirection.SrcHost, "Attachments", attachment.Path);

                System.IO.File.Copy(filePath, newFilePath);
            }
            await _attachmentRepository.Update(attachments);

            return NoContent();
        }

    }
}
