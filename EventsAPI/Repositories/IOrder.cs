using EventsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCombo.Repositories
{
    interface IOrder
    {
        IEnumerable<Order> Get();
        Order Get(int Id);
        void Add(Order Model);
        void Update(Order Model, Order UpdatedModel, int Id);
        void Delete(int Id);
        bool AlreadyExists(int EventId, int UserId);
        void Save();
    }
}
