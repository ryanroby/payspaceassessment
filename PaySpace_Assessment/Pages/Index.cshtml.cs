using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace_Assessment.Pages
{
    public class IndexModel : PageModel
    {
        public List<TaxCalculationModel> taxCalculationsList { get; set; }
        [Microsoft.AspNetCore.Mvc.BindProperty]
        public string PostalCode { get; set; }
        [Microsoft.AspNetCore.Mvc.BindProperty]
        public string Salary { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            var newcalc = Calculate(PostalCode, Salary);
            OnGet();
        }

        public void OnGet()
        {
            List<TaxCalculationModel> type = new List<TaxCalculationModel>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (var _response = httpClient.GetAsync("http://localhost:58300/api/calculations/history").Result)
                {
                    Task<string> apiResponse = _response.Content.ReadAsStringAsync();
                    taxCalculationsList = JsonConvert.DeserializeAnonymousType(apiResponse.Result, type);
                }
            }
        }

        public static TaxCalculationModel Calculate(string postalcode, string salary)
        {
            TaxCalculationModel newcalculation = new TaxCalculationModel();
            TaxCalculationModel data = new TaxCalculationModel()
            {
                PostalCode = postalcode,
                Salary = salary
            };

            var json = JsonConvert.SerializeObject(data);
            var _data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            var result = httpClient.PostAsync("http://localhost:58300/api/calculations", _data).Result;
            Task<string> apiResponse = result.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeAnonymousType(apiResponse.Result, newcalculation);

            return res;
        }
    }

    public class TaxCalculationModel
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string Salary { get; set; }
        public DateTime DateCalculated { get; set; }
        public string CalculationType { get; set; }
        public decimal Tax { get; set; }
    }
}