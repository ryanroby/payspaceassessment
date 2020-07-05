using System.Web.Http;
using Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Web_API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/calculations")]
    [ApiController]
    public class TaxCalculatorController : Controller
    {
        [System.Web.Http.HttpPost]
        public TaxCalculatorModel TaxCalculation(TaxCalculatorModel data)
        {
            TaxCalculatorModel response = TaxCalculatorModel.TaxCalculation(data);
            return response;
        }
    }
}