using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DopeZoo.Models
{
    public class SaleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImagePath { get; set; }

        public string AuthorId { get; set; }

    }
}