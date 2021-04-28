using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Models
{
    public class GuitarViewModel
    {
        public Guitar Guitar { get; set; }
        public IEnumerable<Image> Images { get; set; }

        public GuitarViewModel()
        {
        }

        public GuitarViewModel(Guitar guitar, IEnumerable<Image> images)
        {
            this.Guitar = guitar;
            this.Images = images;
        }
    }

}
