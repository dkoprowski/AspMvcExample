using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime DateOfPublication { get; set; }

        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual User User { get; set; }
    }
}