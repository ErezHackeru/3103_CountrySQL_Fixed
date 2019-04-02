using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW310319_CountryDataBase
{
    class CapitalCity
    {
        public int Id { get; set; }
        public string CName { get; set; }
        public int NumCitizens { get; set; }
        public int CountryId { get; set; }

        public static bool operator ==(CapitalCity c1, CapitalCity c2)
        {
            if ((c1 == null) && (c2 == null))
                return true;
            if ((c1 == null) || (c2 == null))
                return false;

            return (c1.Id == c2.Id);
        }
        public static bool operator !=(CapitalCity c1, CapitalCity c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            CapitalCity c = obj as CapitalCity;
            if (c == null)
                return false;

            return this.Id == c.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }


        public override string ToString()
        {
            return $"CapitalCity Id {Id}, Name {CName}, NumCitizens {NumCitizens}, Country Id {CountryId}";
        }
    }
}
