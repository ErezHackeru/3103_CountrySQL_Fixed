using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW310319_CountryDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            IClassDAO classDAO = new ClassDAO();
            try
            {
                var result1 = classDAO.GetCountyAndItsCapitalCityDetails(1);
                var result2 = classDAO.GetCountyAndItsCapitalCityName(4);
                var result3 = classDAO.GetCountyAndItsCapitalCityDetails("Austria");
                var result4 = classDAO.GetCountyAndItsCapitalCityName("Israel");

                var result5 = classDAO.AllCountriesFromFirstLetter("I");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine();
            }
            finally
            {
                ClassDAO.Close();
            }
        }
    }
}
