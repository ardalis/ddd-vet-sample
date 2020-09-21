using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.ConfigurationEndpoints
{
    public class Read : BaseAsyncEndpoint<string, string>
    {
        public Read()
        {
        }

        [HttpGet("api/configurations/{configurationName}")]
        [SwaggerOperation(
            Summary = "Reads a configuration",
            Description = "Reads a configuration",
            OperationId = "configurations.read",
            Tags = new[] { "configurationEndpoints" })
        ]
        public override async Task<ActionResult<string>> HandleAsync([FromRoute]string configurationName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(configurationName)) return BadRequest();

            if(configurationName == nameof(OfficeSettings.TestDate))
            {
                return Ok(new OfficeSettings().TestDate.ToString());
            }

            return NotFound();
        }
    }
}
