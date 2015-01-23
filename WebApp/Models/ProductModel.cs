using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        
        public string Path { get; set; }    //path to the location on server where a game is stored

        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<CommentModel> Comments { get; set; }
    }
}