using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientPatientManagement.Core.Model;
using ClientPatientManagement.Core.Interfaces;

namespace FrontDesk.Web.Controllers.Api.Crud
{
    public class CrudClientsController : ApiController
    {
        private readonly IRepository<Client> _clientRepository;

        public CrudClientsController(IRepository<Client> clientRepository)
        {
            this._clientRepository = clientRepository;
        }

        // GET api/values
        public IEnumerable<Client> Get()
        {
            return _clientRepository.List();
        }

        // GET api/values/5
        public Client Get(int id)
        {
            var client = _clientRepository.GetById(id);
            if(client == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return client;
        }

        // POST api/values
        public void Post([FromBody]
                         Client client)
        {
            _clientRepository.Insert(client);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]
                        Client client)
        {
            var clientToUpdate = _clientRepository.GetById(id);
            clientToUpdate.FullName = client.FullName;
            clientToUpdate.EmailAddress = client.EmailAddress;
            clientToUpdate.Salutation = client.Salutation;
            clientToUpdate.PreferredName = client.PreferredName;
            clientToUpdate.PreferredDoctorId = client.PreferredDoctorId;
            _clientRepository.Update(clientToUpdate);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }
    }
}