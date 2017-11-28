using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DvdStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DvdStore.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in stock")]
        [Required]
        [Range(0, 20, ErrorMessage = "The Number in stock field must be between 0 and 20.")]
        public byte? NumberInStock { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }


        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
                
        }


        public string Title
        {
            get {

                return Id == 0 ? "Add movie" : "Edit movie";
            }
        }
    }
}