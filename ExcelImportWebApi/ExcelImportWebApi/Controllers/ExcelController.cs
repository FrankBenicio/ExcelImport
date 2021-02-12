using Histories.ProductHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExcelImportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Post([FromServices] ListProducts listProducts, [FromServices] ReadExcelSpreadsheet readExcelSpreadsheet, [FromForm] IFormFile file)
        {
            try
            {
                await readExcelSpreadsheet.Run(file);

                var list = await listProducts.Run();


                var response = new
                {
                    success = true,
                    products = list

                };

                return Ok(response);
            }
            catch (System.Exception e)
            {

                var response = new
                {
                    success = false,
                    message = e.Message

                };

                return BadRequest(response);
            }
        }
    }
}
