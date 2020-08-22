using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Repositories
{
    public class UserRepository : IUser
    {
        private readonly EventsDBContext Context;
        public UserRepository(EventsDBContext context)
        {
            Context = context;
        }

        public IEnumerable<User> Get()
        {
            return Context.User.ToList();
        }
        public User Get(int Id)
        {
            return Context.User.FirstOrDefault(x => x.Id == Id);
        }
        public void Add(User Model)
        {
            Model.DateTime = DateTime.Now;
            Model.Role = "Client";
            Context.User.Add(Model);
        }
        public void Update(User Model, int Id)
        {
            Context.Entry(Model).State = EntityState.Modified;
        }
        public void Delete(int Id)
        {
            User Model = Context.User.Find(Id);
            Context.User.Remove(Model);
        }
        public bool AlreadyExists(string Email)
        {
            User User = Context.User.FirstOrDefault(x => x.Email == Email);
            if (User == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateInput(User Model, out string Message)
        {
            // Any validation checks
            Message = null;
            try
            {
                var Address = new System.Net.Mail.MailAddress(Model.Email).Address;
            }
            catch (Exception) 
            {
                Message = "Invalid Email";
                return false;
            }

            return true;
        }
        public User Authorize(Login Model)
        {
            return Context.User.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
