using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Attachment
    {
        [BsonId]
        [JsonProperty("attachmentId")]
        public Guid AttachmentId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; } = "";

        [JsonProperty("contentType")]
        public string ContentType { get; set; } = "";

        [JsonProperty("path")]
        public string Path { get; set; } = "";

        [JsonProperty("isTemp")]
        public bool IsTemp { get; set; } = true;

        [BsonIgnore]
        public IFormFile? Resource { get; set; }

        
    }
}
