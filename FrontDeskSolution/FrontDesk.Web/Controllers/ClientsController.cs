using System.Collections.Generic;
using System.Web.Http;
using FrontDesk.Web.Models;

namespace FrontDesk.Web.Controllers
{
    public class ClientsController : ApiController
    {
        // GET api/values
        public IEnumerable<ClientViewModel> Get()
        {
            return new ClientViewModel[]
            {
                new ClientViewModel()
                {
                    Name = "Steve Smith",
                    Patients = new List<PatientViewModel>()
                    {
                        new PatientViewModel()
                        {
                            Name = "Darwin"
                        },
                        new PatientViewModel()
                        {
                            Name = "Rumor"
                        }
                    }
                },
                new ClientViewModel() { Name = "Julie" }
            };
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