using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientPatientManagement.Core.Model;
using ClientPatientManagement.Core.Interfaces;

namespace FrontDesk.Web.Controllers.Api.Crud
{
    public class CrudRoomsController : ApiController
    {
        private readonly IRepository<Room> _roomRepository;

        public CrudRoomsController(IRepository<Room> roomRepository)
        {
            this._roomRepository = roomRepository;
        }

        // GET api/values
        public IEnumerable<Room> Get()
        {
            return _roomRepository.List();
        }

        // GET api/values/5
        public Room Get(int id)
        {
            var room = _roomRepository.GetById(id);
            if(room == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return room;
        }

        // POST api/values
        public void Post([FromBody]
                         Room room)
        {
            _roomRepository.Insert(room);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]
                        Room room)
        {
            var roomToUpdate = _roomRepository.GetById(id);
            roomToUpdate.Name = room.Name;
            _roomRepository.Update(roomToUpdate);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }
    }
}