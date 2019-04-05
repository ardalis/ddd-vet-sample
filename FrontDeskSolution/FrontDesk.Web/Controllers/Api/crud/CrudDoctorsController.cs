using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientPatientManagement.Core.Model;
using ClientPatientManagement.Core.Interfaces;

namespace FrontDesk.Web.Controllers.Api.Crud
{
    public class CrudDoctorsController : ApiController
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public CrudDoctorsController(IRepository<Doctor> doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        // GET api/values
        public IEnumerable<Doctor> Get()
        {
            return _doctorRepository.List();
        }

        // GET api/values/5
        public Doctor Get(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if(doctor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return doctor;
        }

        // POST api/values
        public void Post([FromBody]
                         Doctor doctor)
        {
            _doctorRepository.Insert(doctor);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]
                        Doctor doctor)
        {
            var doctorToUpdate = _doctorRepository.GetById(id);
            doctorToUpdate.Name = doctor.Name;
            _doctorRepository.Update(doctorToUpdate);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _doctorRepository.Delete(id);
        }
    }
}