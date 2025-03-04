using RepositoryLayer.Interface;
using System;
using ModelLayer.Model;
using NLog;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public string Greeting(UserModel userModel)
        {
            string greetingMessage = string.Empty;

            if (!string.IsNullOrEmpty(userModel.FirstName) && !string.IsNullOrEmpty(userModel.LastName))
            {
                greetingMessage = $"Welcome {userModel.FirstName} {userModel.LastName}";
            }
            else if (!string.IsNullOrEmpty(userModel.FirstName))
            {
                greetingMessage = $"Welcome {userModel.FirstName}";
            }
            else if (!string.IsNullOrEmpty(userModel.LastName))
            {
                greetingMessage = $"Welcome {userModel.LastName}";
            }
            else
            {
                greetingMessage = "Hello World";
            }

            _logger.Info($"Greeting is Generated: {greetingMessage}");
            return greetingMessage;
        }
    }
}