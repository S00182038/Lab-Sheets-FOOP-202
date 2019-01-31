using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Sheet_1
{
   public class Album 
    {
        public string  AlbumName { get; set; }
        public int ReleasedYear { get; set; }
        public int YearsSinceReleased
        {
            get
            {
                return Years(ReleasedYear);
            }
        }
        public double Sales { get; set; }

       //override method to display albums
        public override string ToString() => string.Format(format: $"{AlbumName,-2}{ReleasedYear,12}  ({YearsSinceReleased}) {Sales,24:C}");
        //calculate years since release
        public int Years(int relasedYear)
        {           
            DateTime YearReleased = new DateTime(relasedYear, 1, 1, 0, 0, 0);
            DateTime today = DateTime.Now;
            TimeSpan ts = today -YearReleased;
            int Years = ts.Days/365;
            return Years;
        }
    }
}
