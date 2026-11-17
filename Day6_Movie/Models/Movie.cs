using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Day6_Movie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre { get; set; }
    }
}