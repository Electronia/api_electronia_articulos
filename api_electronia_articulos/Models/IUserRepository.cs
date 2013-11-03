using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_electronia_articulos.Models
{
    interface IUserRepository
    {
        
        User Get(int id);
        bool Get(string email);
        bool Get(string id, string token);
        User Add(User item);
        void Remove(int id);
        User Update(int id,User item);
    }
}