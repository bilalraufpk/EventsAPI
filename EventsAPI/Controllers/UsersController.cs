using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using EventsAPI.Handlers;
using System.Runtime.InteropServices.WindowsRuntime;

namespace EventsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser UserRepository;
        EventsDBContext Context = new EventsDBContext();

        public UsersController()
        {
            this.UserRepository = new UserRepository(Context);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserRepository.Get();
        }

        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            //Allow accesss to Admin and Client(only his details)
            string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!TokenHandler.IsUserAllowed(Id, Token)) { return Unauthorized(); }
            User User = UserRepository.Get(Id);
            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post(User Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be saved");
                }

                if (UserRepository.AlreadyExists(Model.Email))
                {
                    return BadRequest("Record already exists");
                }

                string Message;
                if (!UserRepository.ValidateInput(Model, out Message))
                {
                    return BadRequest(Message);
                }

                UserRepository.Add(Model);
                UserRepository.Save();

                return Ok("Record Saved");
            }
            catch (Exception) { return BadRequest("Record can not be saved"); }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, User Model)
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

                //Allow accesss to Admin and Client(only his details)
                string Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!TokenHandler.IsUserAllowed(Id, Token)) { return Unauthorized(); }

                string Message;
                if (!UserRepository.ValidateInput(Model, out Message))
                {
                    return BadRequest(Message);
                }

                UserRepository.Update(Model, Id);
                UserRepository.Save();

                return Ok("Record Updated");
            }
            catch (Exception) { return BadRequest("Record can not be updated, please check your input data"); }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                User User = UserRepository.Get(Id);
                if (User == null)
                {
                    return NotFound();
                }

                UserRepository.Delete(Id);
                UserRepository.Save();

                return Ok("Record Deleted");
            }
            catch (Exception) { return BadRequest("Record can not be deleted"); }
        }
    }
}
