﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        public string GetGreet();
        public bool GreetMessage(GreetModel greetModel);
        public GreetModel GreetMessagebyID(int ID);
        public string greeting(UserModel userModel);
        public List<GreetModel> GetAllGreetings();
        public GreetModel EditGreeting(int ID, GreetModel greetingModel);
        public bool DeleteGreetingMessage(int ID);

    }
}
