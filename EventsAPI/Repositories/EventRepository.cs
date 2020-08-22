using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Repositories
{
    public class EventRepository : IEvent
    {
        private readonly EventsDBContext Context;
        public EventRepository(EventsDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Event> Get()
        {
            return Context.Event.Where(x => x.IsDeleted == null || x.IsDeleted == false).ToList();
        }
        public Event Get(int Id)
        {
            return Context.Event.FirstOrDefault(x => x.Id == Id && (x.IsDeleted == null || x.IsDeleted == false));
        }
        public void Add(Event Model)
        {
            Model.DateTime = DateTime.Now;
            Context.Event.Add(Model);
        }
        public void Update(Event Model, Event UpdatedModel, int Id)
        {
            //Mention the columns you want to update, we dont need to update all the fields, if yes, then you can use entity modified process to update
            Model.Title = UpdatedModel.Title;
            Model.Date = UpdatedModel.Date;
            Context.SaveChanges();
        }
        public void Delete(Event Model)
        {
            Model.IsDeleted = true;
            Context.SaveChanges();
        }
        public bool AlreadyExists(string Title, int UserId, DateTime Date)
        {
            Event Event = Context.Event.FirstOrDefault(x => x.Title == Title && x.UserId == UserId && Date.Date == x.Date);
            if (Event == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateInput(Event Model, out string Message)
        {
            // Any validation checks
            Message = null;

            if (Model.Title.Length < 5)
            {
                Message = "Title is too short, should be more than 5 charactors";
            }

            return true;
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
