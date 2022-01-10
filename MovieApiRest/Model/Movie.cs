using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WineApiRest.Model
{
    public class Movie
    {
        public uint? Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Variety { get; set; }

       [JsonConverter(typeof(JsonStringEnumConverter))]
        public WineColor? Color { get; set; }

        public short? Year { get; set; }

        [JsonIgnore]
        public float? AlcoholRate { get; set; }

    }
}
