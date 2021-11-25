using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PrintFromDatabase
{
    public class PlayerDAL
    {
        private string _connectionString;
        public PlayerDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<Player> GetList()
        {
            var listCountryModel = new List<Player>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM zawodnicy", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listCountryModel.Add(new Player
                        {
                            id_zawodnika = Convert.ToInt32(rdr[0]),
                            id_trenera = Convert.ToInt32(rdr[1]),
                            imie = Convert.ToString(rdr[2]),
                            nazwisko = Convert.ToString(rdr[3]),
                            kraj = Convert.ToString(rdr[4]),
                            data_ur = Convert.ToDateTime(rdr[5]),
                            wzrost = Convert.ToInt32(rdr[6]),
                            waga = Convert.ToDouble(rdr[7])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCountryModel;
        }
    }
}
