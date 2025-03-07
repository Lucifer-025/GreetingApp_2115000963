using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public string Greeting(UserModel userModel);
        public bool GreetMessage(GreetModel greetModel);
        public List<GreetEntity> GetAllGreetings();
        public GreetModel GreetMessagebyID(int ID);
        public GreetEntity EditGreeting(int ID, GreetModel greetingModel);
    }
}
