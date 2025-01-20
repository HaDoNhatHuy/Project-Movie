using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie
{
    public interface IMeta
    {
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
    }
}
