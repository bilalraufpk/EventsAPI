using EventsAPI.Models;
using OrderCombo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly EventsDBContext Context;
        public OrderRepository(EventsDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Order> Get()
        {
            return Context.Order.ToList();
        }
        public Order Get(int Id)
        {
            return Context.Order.FirstOrDefault(x => x.Id == Id);
        }
        public void Add(Order Model)
        {
            Model.DateTime = DateTime.Now;
            Context.Order.Add(Model);
        }
        public void Update(Order Model, Order UpdatedModel, int Id)
        {
            //Mention the columns you want to update, we dont need to update all the fields, if yes, then you can use entity modified process to update
            Model.Amount = UpdatedModel.Amount;
            Model.DateTime = UpdatedModel.DateTime;
            Context.SaveChanges();
        }
        public void Delete(int Id)
        {
            Order Model = Context.Order.Find(Id);
            Context.Order.Remove(Model);
        }
        public bool AlreadyExists(int EventId, int UserId)
        {
            Order Order = Context.Order.FirstOrDefault(x => x.EventId == EventId && x.UserId == UserId);
            if (Order == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
