using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieApiRest.Model
{
    [Table("movies")]
    public class Movie
    {
        [Column("id")]
        public uint? Id { get; set; }

        [Column("title")]
        [Required]
        public string Title { get; set; }

        [Column("year")]
        [Required]
        public short? Year { get; set; }

        [Column("duration")]
        public short? Duration { get; set; }

        [Column("synopsis")]
        public string Synopsis { get; set; }

        [Column("poster_uri")]
        public string PosterUri { get; set; }






    }
}
