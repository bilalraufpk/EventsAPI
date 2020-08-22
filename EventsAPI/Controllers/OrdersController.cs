using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsAPI.Handlers;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderCombo.Repositories;

namespace EventsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder OrderRepository;
        private IEvent EventRepository;

        public OrdersController()
        {
            this.OrderRepository = new OrderRepository(new EventsDBContext());
            this.EventRepository = new EventRepository(new EventsDBContext());
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return OrderRepository.Get();
        }

        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            Order Order = OrderRepository.Get(Id);
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        [HttpPost]
        public ActionResult Post(Order Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be saved");
                }

                if (OrderRepository.AlreadyExists(Model.EventId, Model.UserId))
                {
                    return BadRequest("Record already exists");
                }
                                
                OrderRepository.Add(Model);
                OrderRepository.Save();

                return Ok("Record Saved");
            }
            catch (Exception ex) { return BadRequest("Record can not be saved"); }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, Order Model)
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

                Order Order = OrderRepository.Get(Id);
                if (Order == null)
                {
                    return BadRequest("Order not found.");
                }

                Event Event = EventRepository.Get(Order.EventId);
                if (Event == null)
                {
                    return BadRequest("Event not found.");
                }

                //Event Owner / Admin allowed to update order
                string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!TokenHandler.IsUserAllowed(Event.UserId, Token)) { return Unauthorized(); }

                OrderRepository.Update(Order, Model, Id);

                return Ok("Record Updated");
            }
            catch (Exception) { return BadRequest("Record can not be updated, please check your input data"); }
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                Order Order = OrderRepository.Get(Id);
                if (Order == null)
                {
                    return NotFound();
                }

                Event Event = EventRepository.Get(Id);
                if (Event == null)
                {
                    return BadRequest("Event not found.");
                }

                //Event Owner / Admin allowed to update order
                string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!TokenHandler.IsUserAllowed(Event.UserId, Token)) { return Unauthorized(); }

                OrderRepository.Delete(Id);
                OrderRepository.Save();

                return Ok("Record Deleted");
            }
            catch (Exception) { return BadRequest("Record can not be deleted"); }
        }
    }
}
