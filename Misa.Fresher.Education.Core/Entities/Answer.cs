using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Entities
{
    public class Answer
    {
        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("incorrect")]
        public bool? Incorrect { get; set; }
    }
}