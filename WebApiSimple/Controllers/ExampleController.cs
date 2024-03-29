
﻿using Base.Domain.Interfaces.DbApp.ServiceInterface;
using Base.Domain.Models.Example;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSimple.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ExampleController : ControllerBase
    {
        private readonly IExampleService exampleService;
        private readonly IExcelDocGeneratorExampleService excelDocGeneratorExampleService;

        public ExampleController(IExampleService exampleService, IExcelDocGeneratorExampleService excelDocGeneratorExampleService)
        {
            this.exampleService = exampleService;
            this.excelDocGeneratorExampleService = excelDocGeneratorExampleService;
        }


        [HttpGet("GetExample")]
        public async Task<IActionResult> GetExample( long idExample)
        {
            var response = await exampleService.GetExample(idExample);
            return Ok(response);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ExampleModel example)
        {
            var response = await exampleService.Create(example);
            return Ok(response);
        }


        [HttpPost("Update")]
        public async Task<IActionResult> Update( long idExample, [FromBody] ExampleModel example)
        {
            await exampleService.Update(idExample, example);
            return Ok();
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(long idExample)
        {
            await exampleService.Delete(idExample);
            return Ok();
        }

        


        [HttpGet("Pagination")]
        public  IActionResult Pagination(SieveModel sieveModel)
        {
            var pageExampleModel = exampleService.GetPagination(sieveModel);
            return Ok(pageExampleModel.ToList());
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await exampleService.GetAll();
            return Ok(response);
        }



        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile()
        {
            var response = await exampleService.DownloadFile();
            if (response.Length < 0)
            {

                var memoryStream = new MemoryStream(response);
                return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "Example" };

            }
            return Ok(response);
        }


        [HttpGet("GetExcelFile")]
        public IActionResult GetExcelFile()
        {
            var excelPackage = excelDocGeneratorExampleService.ExampleCreateExcelEPPLUS();
            if (excelPackage.Result.Length  > 0)
            {

                var memoryStream = new MemoryStream(excelPackage.Result);
                return File(excelPackage.Result, "application/octet-stream", "FileManager.xlsx");
                //return new FileStreamResult(memoryStream, "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet") { FileDownloadName = "Example.xlsx" };

            }
            return Ok(excelPackage);
        }






    }
}
