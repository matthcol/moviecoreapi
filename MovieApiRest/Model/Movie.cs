using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieApiRest.Model
{
    public class Movie
    {
        public uint? Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public short? Year { get; set; }

        public short? Duration { get; set; }

        public string Synopsis { get; set; }

        [Column("poster_uri")]
        public string PosterUri { get; set; }






    }
}
