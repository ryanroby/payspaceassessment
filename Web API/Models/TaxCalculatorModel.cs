using System;
using System.Collections.Generic;
using System.Data;

namespace Web_API.Models
{
    public class TaxCalculatorModel
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string Salary { get; set; }
        public DateTime DateCalculated { get; set; }
        public string CalculationType { get; set; }
        public decimal Tax { get; set; }

        public static TaxCalculatorModel TaxCalculation(TaxCalculatorModel data)
        {
            switch (data.PostalCode)
            {
                case "7441":
                    data.Tax = ProgressiveCalculation(data);
                    break;
                case "A100":
                    data.Tax = FlatValueCalculation(data);
                    break;
                case "7000":
                    data.Tax = FlatRateCalculation(data);
                    break;
                case "1000":
                    data.Tax = ProgressiveCalculation(data);
                    break;
                default:
                    data.Tax = 0;
                    break;
            }

            if (data.Tax > 0)
            {
                data.DateCalculated = DateTime.Now;
                Library.TaxCalculatorLibrary.StoreTaxCalculation(data);
            }
            return data;
        }

        public static decimal ProgressiveCalculation(TaxCalculatorModel data)
        {
            decimal salary = Convert.ToDecimal(data.Salary);

            if (salary <= 8350)
            {
                data.Tax = salary * (decimal)0.1;
            }
            else if (salary >= 8351 && salary <= 33950)
            {
                data.Tax = salary * (decimal)0.15;
            }
            else if (salary >= 33951 && salary <= 82250)
            {
                data.Tax = salary * (decimal)0.25;
            }
            else if (salary >= 82251 && salary <= 171550)
            {
                data.Tax = salary * (decimal)0.28;
            }
            else if (salary >= 171551 && salary <= 372950)
            {
                data.Tax = salary * (decimal)0.33;
            }
            else
            {
                data.Tax = salary * (decimal)0.35;
            }

            data.CalculationType = "Progressive Calculation";

            return data.Tax;
        }

        public static decimal FlatValueCalculation(TaxCalculatorModel data)
        {
            decimal salary = Convert.ToDecimal(data.Salary);

            decimal annualincome = salary * 12;
            if (annualincome < 200000)
            {
                data.Tax = salary * (decimal)0.05;
            }
            else
            {
                data.Tax = 10000;
            }

            data.CalculationType = "Flat Value Calculation";

            return data.Tax;
        }

        public static decimal FlatRateCalculation(TaxCalculatorModel data)
        {
            decimal salary = Convert.ToDecimal(data.Salary);

            data.Tax = salary * (decimal)0.175;

            data.CalculationType = "Flat Rate Calculation";

            return data.Tax;
        }

        public static List<TaxCalculatorModel> ListTaxCalculations()
        {
            return Library.TaxCalculatorLibrary.ListTaxCalculations();
        }

        public static bool CheckNegativeValues(int value)
        {
            if (Convert.ToInt32(value) < 1)
            {
                return false;
            }

            return true;
        }

        public static bool CheckNumericValues(string value)
        {
            bool res = false;

            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                {
                    res = false;
                }
                else
                {
                    res = true;
                }
            }

            return res;
        }
    }
}