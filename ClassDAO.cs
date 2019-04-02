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

