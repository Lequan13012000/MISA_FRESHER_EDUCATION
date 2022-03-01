using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Question
    {
        [JsonProperty("type")]
        public int? Type { get; set; }
        [JsonProperty("content")]
        public string? Content { get; set; }
        [JsonProperty("answers")]
        public List<Answer>? Answers { get; set; } = new List<Answer>();

        [JsonProperty("attachments")]
        public List<Attachment>? Attachments { get; set; } = new();

    }
}
























