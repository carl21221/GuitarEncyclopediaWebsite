using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string URL { get; set; }
        public string GuitarID { get; set; }

        public Image()
        {
            this.URL = "";
            this.GuitarID = "";
        }
        public Image(string newURL, string GuitarID)
        {
            this.URL = newURL;
            this.GuitarID = GuitarID;
        }
    }
}
