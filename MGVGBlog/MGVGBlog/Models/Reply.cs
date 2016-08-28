using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MGVGBlog.Models
{
    public class Reply
    {
        public Reply()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int CommentId { get; set; }

        public DateTime Date { get; set; }

        public int Points { get; set; }

        public int PostId { get; set; } 

        public virtual Comments Comments { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}