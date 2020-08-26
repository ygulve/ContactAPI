using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ContactAPI.Repository;
using System.Net;
using System;
using log4net;

namespace ContactAPI.Controllers
{

    public class ContactController : ApiController
    {
        private readonly IDataRepository<Contact> _dataRepository;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ContactController));


        public ContactController(IDataRepository<Contact> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            Log.Info("Inside Get");
            IEnumerable<Contact> Contacts = _dataRepository.GetAll();
            return Contacts;
        }

        [HttpGet]
        public HttpResponseMessage Get(long Id)
        {
            Contact contact = _dataRepository.Get(Id);

            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The Contact data couldn't be found.");

            }
            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Contact is null");
            }

            try
            {
                _dataRepository.Add(contact);
            }
            catch (Exception ex)
            {
                Log.Error("Error in post", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while adding the data.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri] long id, [FromBody] Contact contact)
        {
            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Contact is null");
            }

            Contact ContactToUpdate = _dataRepository.Get(id);
            if (ContactToUpdate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The Contact data couldn't be found.");
            }

            try
            {
                _dataRepository.Update(ContactToUpdate, contact);
            }
            catch (Exception ex)
            {
                Log.Error("Error in put", ex);
            }           

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] long id)
        {
            Contact contact = _dataRepository.Get(id);
            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The Contact data couldn't be found.");
            }
            try
            {
                _dataRepository.Delete(contact);
            }
            catch (Exception ex)
            {
                Log.Error("Error in delete", ex);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
