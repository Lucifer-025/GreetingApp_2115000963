using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLayer.Interface;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }
        public GreetModel GreetMessagebyID(int ID)
        {
            return _greetingRL.GreetMessagebyID(ID);
        }
        public string GetGreet()
        {
            return "Hello! World";
        }
        public string greeting(UserModel userModel)
        {
            return _greetingRL.Greeting(userModel);
        }
        public bool GreetMessage(GreetModel greetModel)
        {
            return _greetingRL.GreetMessage(greetModel);
        }
        public List<GreetModel> GetAllGreetings()
        {
            var entityList = _greetingRL.GetAllGreetings();  // Calling Repository Layer
            if (entityList != null)
            {
                return entityList.Select(g => new GreetModel
                {
                    ID = g.id,
                    GreetingMessage = g.Greeting
                }).ToList();  // Converting List of Entity to List of Model
            }
            return null;
        }
    }
}
