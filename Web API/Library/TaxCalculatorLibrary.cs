using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Models;

namespace Web_API.Library
{
    public class TaxCalculatorLibrary
    {
        public static void StoreTaxCalculation(Models.TaxCalculatorModel data)
        {
            using (SqlConnection conn = Connections.LocalConnection())
            {
                conn.Open();
                string Query = "INSERT INTO TaxCalculations " +
                    "(" +
                        "PostalCode, " +
                        "Salary, " +
                        "DateCalculated, " +
                        "Tax, " +
                        "CalculationType" +
                    ") VALUES (" +
                        "@PostalCode, " +
                        "@Salary, " +
                        "@DateCalculated, " +
                        "@Tax, " +
                        "@CalculationType" +
                    ")";

                SqlDataReader reader = null;
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                    cmd.Parameters.AddWithValue("@Salary", data.Salary);
                    cmd.Parameters.AddWithValue("@DateCalculated", data.DateCalculated);
                    cmd.Parameters.AddWithValue("@Tax", data.Tax);
                    cmd.Parameters.AddWithValue("@CalculationType", data.CalculationType);

                    reader = cmd.ExecuteReader();
                }
            }
        }

        public static List<TaxCalculatorModel> ListTaxCalculations()
        {
            using (SqlConnection conn = Connections.LocalConnection())
            {
                conn.Open();
                string Query = "SELECT * FROM TaxCalculations ORDER BY DateCalculated";

                SqlDataReader reader = null;
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<TaxCalculatorModel> results = new List<TaxCalculatorModel>();
                        while (reader.Read())
                        {
                            TaxCalculatorModel item = new TaxCalculatorModel()
                            {
                                Id = reader["Id"] is DBNull ? default : (int)reader["Id"],
                                PostalCode = reader["PostalCode"] is DBNull ? default : (string)reader["PostalCode"],
                                Salary = reader["Salary"] is DBNull ? default : (string)reader["Salary"],
                                DateCalculated = reader["DateCalculated"] is DBNull ? default : (DateTime)reader["DateCalculated"],
                                Tax = reader["Tax"] is DBNull ? default : (decimal)reader["Tax"],
                                CalculationType = reader["CalculationType"] is DBNull ? default : (string)reader["CalculationType"],
                            };

                            results.Add(item);
                        }
                        return results;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
