using System.Web.Http;
using Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Web_API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/calculations/history")]
    [ApiController]
    public class TaxCalculatorHistoryController : Controller
    {
        [System.Web.Http.HttpGet]
        public List<TaxCalculatorModel> GetTaxCalculations()
        {
            List<TaxCalculatorModel> response = TaxCalculatorModel.ListTaxCalculations();
            return response;
        }
    }
}