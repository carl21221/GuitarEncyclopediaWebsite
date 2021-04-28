using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Models
{
    public class GuitarComparisonViewModel
    {
        public GuitarViewModel Guitar1 { get; set; }
        public GuitarViewModel Guitar2 { get; set; }

        public GuitarComparisonViewModel()
        {

        }

        public GuitarComparisonViewModel(GuitarViewModel g1, GuitarViewModel g2)
        {
            Guitar1 = g1;
            Guitar2 = g2;
        }

        public bool IsInComparison(GuitarViewModel g)
        {
            if(g != null )
            {
                if (Guitar1 != null && g.Guitar.Id == Guitar1.Guitar.Id) return true;
                else if (Guitar2 != null && g.Guitar.Id == Guitar2.Guitar.Id) return true;
            }
            return false;
        }

        public bool IsInComparison(int id)
        {
            if (Guitar1 != null && id == Guitar1.Guitar.Id) return true;
            else if (Guitar2 != null && id == Guitar2.Guitar.Id) return true;
            return false;
        }

        public bool IsEmpty()
        {
            if (Guitar1 == null && Guitar2 == null) return true;
            else return false;
        }
        public bool IsFull()
        {
            if (Guitar1 != null && Guitar2 != null) return true;
            else return false;
        }

        public int Count()
        {
            if (IsFull()) return 2;
            else if (IsEmpty()) return 0;
            else return 1;
        }
    }


}
