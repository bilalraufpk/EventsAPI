using EventsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Repositories
{
    interface IEvent
    {
        IEnumerable<Event> Get();
        Event Get(int Id);
        void Add(Event Model);
        void Update(Event Model, Event UpdatedModel, int Id);
        void Delete(Event Event);
        bool AlreadyExists(string Title, int CreatedBy, DateTime Date);
        bool ValidateInput(Event Model, out string Message);
        void Save();
    }
}
