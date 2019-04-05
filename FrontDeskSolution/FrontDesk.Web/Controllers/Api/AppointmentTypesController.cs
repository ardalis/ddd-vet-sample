using System.Collections.Generic;
using System.Web.Http;
using AppointmentScheduling.Data;
using FrontDesk.Web.Models;
using System.Linq;

namespace FrontDesk.Web.Controllers.Api
{
    public class AppointmentTypesController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        // GET api/values
        public IEnumerable<AppointmentTypeViewModel> Get()
        {
            return db.AppointmentTypes.Select(a => new AppointmentTypeViewModel()
            {
                AppointmentTypeId = a.Id,
                Name=a.Name,
                Code=a.Code,
                Duration=a.Duration
            })
            .OrderBy(a => a.Name);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]
                         string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]
                        string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}