using EventsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsAPI.Repositories
{
    interface IUser
    {
        IEnumerable<User> Get();
        User Get(int Id);
        void Add(User Model);
        void Update(User Model, int Id);
        void Delete(int Id);
        bool AlreadyExists(string Email);
        bool ValidateInput(User Model, out string Message);
        User Authorize(Login Model);
        void Save();
    }
}
