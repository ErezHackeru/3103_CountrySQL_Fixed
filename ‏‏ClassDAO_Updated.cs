using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW310319_CountryDataBase
{
    class ClassDAO : IClassDAO
    {
        static SQLiteConnection connection;
        public static string dbName = @"D:\HackerU\HW3103_Country.db";

        static ClassDAO()
        {
            connection = new SQLiteConnection($"Data Source = {dbName}; Version=3;");
            connection.Open();
        }
        public static void Close()
        {
            connection.Close();
        }
        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Country", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        countries.Add(CurrentCountry);
                    }
                }
            }

            return countries;
        }
        public List<CapitalCity> GetAllCities()
        {
            List<CapitalCity> cities = new List<CapitalCity>();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM CoupitalCity", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        cities.Add(CurrentCity);
                    }
                }
            }
            return cities;
        }

        public List<object> AllCountriesFromFirstLetter(string FirstLetter)
        {            
            List<object> list = new List<object>();
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.NAME LIKE '{FirstLetter}%'", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["COUNTRY_ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["CAPITALCITY_ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        var result = new
                        {
                            CurrentCountry.Id,
                            CurrentCountry.Name,
                            CurrentCity.CName                            
                        };

                        list.Add(result);                        
                    }
                }
            }
            return list;
        }
        public List<object> AllCountriesFromFirstLetterLINQ(string FirstLetter)
        {
            List<Country> countries = GetAllCountries();
            List<CapitalCity> cities = GetAllCities();
            //SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.NAME LIKE '{FirstLetter}%'"
            var result =
                from country in countries
                join city in cities
                on country.Id equals city.CountryId
                //into countryGroup
                where country.Name.StartsWith(FirstLetter)
                select new { CountryId = country.Id, CountryName = country.Name, CityName = city.CName };

            var ListResult = result.ToList();

            return null;// ListResult;
        }
        public object GetCountyAndItsCapitalCityDetails(int countryId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.ID == {countryId}", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["COUNTRY_ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["CAPITALCITY_ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        var result = new
                        {
                            CountryId = CurrentCountry.Id,
                            CountryName = CurrentCountry.Name,
                            CityName = CurrentCity.CName,
                            CityNumCities = CurrentCity.NumCitizens,
                            CityCountryId = CurrentCity.CountryId
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public object GetCountyAndItsCapitalCityDetailsLINQ(int countryId)
        {
            List<Country> countries = GetAllCountries();
            List<CapitalCity> cities = GetAllCities();
            //SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.ID == {countryId}
            var result = 
                from country in countries
                join city in cities
                on country.Id equals city.CountryId 
                //into countryGroup
                where country.Id == countryId
                select new { CountryId = country.Id, CountryName = country.Name, CityName = city.CName }; //, CityName = city.CName

            // (2) C# format
            //List<Employee> result = employees.Where(e => e.Age > 20 && e.Age < 26).ToList();

            var resulrt2 = countries
                .Join(cities,
                country => country.Id,
                city => city.CountryId, 
                (country, city) => new
                {
                    CountryId = country.Id,
                    CountryName = country.Name,
                    CityName = city.CName
                }).Where(CId => CId.CountryId == countryId).LastOrDefault();

            return result.LastOrDefault();
        }

        public object GetCountyAndItsCapitalCityDetails(string countryName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.NAME == '{countryName}'", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["COUNTRY_ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["CAPITALCITY_ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        var result = new
                        {
                            CurrentCountry.Id,
                            CurrentCountry.Name,
                            CurrentCity.CName,
                            CurrentCity.NumCitizens,
                            CurrentCity.CountryId
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public object GetCountyAndItsCapitalCityName(int countryId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.ID == {countryId}", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["COUNTRY_ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["CAPITALCITY_ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        var result = new
                        {
                            CurrentCountry.Id,
                            CurrentCountry.Name,
                            CurrentCity.CName
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public object GetCountyAndItsCapitalCityName(string countryName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM COUNTRY JOIN CoupitalCity ON COUNTRY.CAPITALCITY_ID == CoupitalCity.ID WHERE COUNTRY.NAME == '{countryName}'", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            Id = (int)reader["COUNTRY_ID"],
                            Name = (string)reader["NAME"],
                            Size_km = (int)reader["SIZE_KM"],
                            Birth_Year = (int)reader["BIRTH_YEAR"],
                            CapitalCityId = (int)reader["CAPITALCITY_ID"]
                        };

                        CapitalCity CurrentCity = new CapitalCity
                        {
                            Id = (int)reader["CAPITALCITY_ID"],
                            CName = (string)reader["CNAME"],
                            NumCitizens = (int)reader["NUMCITIZENS"],
                            CountryId = (int)reader["COUNTRY_ID"]
                        };

                        var result = new
                        {
                            CurrentCountry.Id,
                            CurrentCountry.Name,
                            CurrentCity.CName
                        };
                        return result;
                    }
                }
            }
            return null;
        }
    }
}

