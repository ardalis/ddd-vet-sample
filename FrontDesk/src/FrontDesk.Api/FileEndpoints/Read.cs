using Ardalis.ApiEndpoints;
using BlazorShared.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.FileEndpoints
{
    public class Read : BaseAsyncEndpoint<FileItem, FileItem>
    {
        public Read()
        {
        }

        [HttpGet("api/files")]
        [SwaggerOperation(
            Summary = "Reads a file",
            Description = "Reads a file",
            OperationId = "files.read",
            Tags = new[] { "FileEndpoints" })
        ]
        public override async Task<ActionResult<FileItem>> HandleAsync(FileItem request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.FileName)) return BadRequest();            

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"images/Patients", request.FileName);
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            byte[] fileArray = System.IO.File.ReadAllBytes(fullPath);
            if (fileArray.Length <= 0) return BadRequest();

            string fileDataBase64 = Convert.ToBase64String(fileArray);            
            var respons = new FileItem()
            {
                DataBase64 = fileDataBase64,
                FileName = request.FileName
            };

            return Ok(respons);
        }
    }
}
