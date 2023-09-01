using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;
using WebsiteTinTuc03.Common.Req;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountrySvc countrySvc;

        public CountryController(CountrySvc countryService)
        {
            countrySvc = countryService;
        }

        [HttpGet("{countryName}")]
        public async Task<ActionResult<List<Country>>> Get(string countryName)
        {
            try
            {
                var countries = await countrySvc.GetCountryInfo(countryName);
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
