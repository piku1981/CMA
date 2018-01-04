namespace MovieApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Director { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(60)]
        public string Actor { get; set; }
        public int Rate { get; set; }

        [Required]
        [StringLength(10)]
        public string Genre { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

    }
}