using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using EventsAPI.Handlers;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEvent EventRepository;
        private IUser UserRepository;
        
        public EventsController()
        {
            this.EventRepository = new EventRepository(new EventsDBContext());
            this.UserRepository = new UserRepository(new EventsDBContext());
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return EventRepository.Get();
        }

        [AllowAnonymous]
        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            Event Event = EventRepository.Get(Id);
            if (Event == null)
            {
                return NotFound();
            }

            return Ok(Event);
        }
                
        [HttpPost]
        public ActionResult Post(Event Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be saved");
                }

                if (EventRepository.AlreadyExists(Model.Title, Model.UserId, Model.Date))
                {
                    return BadRequest("Record already exists");
                }

                string Message;
                if (!EventRepository.ValidateInput(Model, out Message))
                {
                    return BadRequest(Message);
                }

                User User = UserRepository.Get(Model.UserId);
                if (User == null)
                {
                    return BadRequest("Invalid CreatedBy. User id does not exists.");
                }

                EventRepository.Add(Model);
                EventRepository.Save();

                return Ok("Record Saved");
            }
            catch (Exception) { return BadRequest("Record can not be saved"); }
        }
                
        [HttpPut("{Id}")]
        public ActionResult Put(int Id, Event Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be updated");
                }

                if (Id != Model.Id)
                {
                    return BadRequest("Invalid Data");
                }
                
                string Message;
                if (!EventRepository.ValidateInput(Model, out Message))
                {
                    return BadRequest(Message);
                }

                User User = UserRepository.Get(Model.UserId);
                if (User == null)
                {
                    return BadRequest("Invalid Owner. User id does not exists.");
                }

                //Owner / Admin allowed to update event
                string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!TokenHandler.IsUserAllowed(Model.UserId, Token)) { return Unauthorized(); }

                Event Event = EventRepository.Get(Id);
                if (Event == null)
                {
                    return BadRequest("Event not found.");
                }
                                                
                EventRepository.Update(Event, Model, Id);

                return Ok("Record Updated");
            }
            catch (Exception) { return BadRequest("Record can not be updated, please check your input data"); }
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                Event Event = EventRepository.Get(Id);
                if (Event == null)
                {
                    return NotFound();
                }

                //Owner / Admin allowed to delete event
                string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!TokenHandler.IsUserAllowed(Event.UserId, Token)) { return Unauthorized(); }
                EventRepository.Delete(Event);

                return Ok("Record Deleted");
            }
            catch (Exception) { return BadRequest("Record can not be deleted"); }
        }
    }
}
