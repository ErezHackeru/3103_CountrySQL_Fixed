using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW310319_CountryDataBase
{
    interface IClassDAO
    {
        object GetCountyAndItsCapitalCityName(int countryId);
        object GetCountyAndItsCapitalCityDetails(int countryId);
        object GetCountyAndItsCapitalCityName(string countryName);
        object GetCountyAndItsCapitalCityDetails(string countryName);

        List<object> AllCountriesFromFirstLetter(string FirstLetter);
    }
}
