using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BuisnessLayer.Interface
{
    public interface IGreetingBL
    {
        public string GetGreet();
        public bool GreetMessage(GreetModel greetModel);
        public GreetModel GreetMessagebyID(int ID);
        public string greeting(UserModel userModel);

    }
}
