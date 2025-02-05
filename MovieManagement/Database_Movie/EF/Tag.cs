﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
