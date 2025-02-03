using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Help")]
    public class Help
    {
        public Guid HelpId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
