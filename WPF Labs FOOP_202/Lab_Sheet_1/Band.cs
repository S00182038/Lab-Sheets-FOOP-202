using System;

namespace Lab_Sheet_1
{
    public abstract class Band : IComparable
    {
        public string BandName { get; set; }
        public int YearFormed { get; set; }
        public string Members { get; set; }
        public Album[] Albums { get; set; }

        public int CompareTo(object obj)
        {
            Band that = obj as Band;
            return BandName.CompareTo(that.BandName);
        }
        //override method
        public override string ToString()
        {
            return string.Format($"{BandName}");
        }
        //method to display the member details in the text block
        public string DisplayBandDetails()
        {
            return $"{"Year Formed :"}\t\n{YearFormed}\n{"Members :"}\t\n{Members}";
        }
    }
}
