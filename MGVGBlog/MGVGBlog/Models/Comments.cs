using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MGVGBlog.Models
{
    public class Comments
    {
        public Comments()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int PostId { get; set; }

        public DateTime Date { get; set; }

        public int Points { get; set; }

        public virtual Post Post { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}