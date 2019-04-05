using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PatientHistory.Domain.Entities;
using PatientHistory.Domain.ValueObjects;
using Repository;

namespace WebAPI.Controllers
{
    public class PatientNotesController : ApiController
    {
      private readonly IPatientRepository _repo;

      public PatientNotesController(IPatientRepository repo)
      {
        _repo = repo;
      }

      public PatientNotesController()
      {
        
      }

      // GET api/patientnotes/Sam/Flynn
      public IEnumerable<PatientResultItem> GetPatientList(string first, string last)
      {
        return _repo.Find(first,last);
      }
       

       // GET api/patientnotes/5
        public PatientInfo GetPatientInfoWithHistory(int id)
        {
          var pi= _repo.Find(id);
          return pi;
        }

        // POST api/patientnotes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/patientnotes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/patientnotes/5
        public void Delete(int id)
        {
        }
    }
}
