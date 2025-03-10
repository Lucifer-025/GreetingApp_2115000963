﻿using RepositoryLayer.Interface;
using System;
using ModelLayer.Model;
using NLog;
using Microsoft.EntityFrameworkCore;
using  RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GreetContext _context;
        public GreetingRL(GreetContext context)
        {
            _context = context;
        }
      
        public bool GreetMessage(GreetModel greetModel)
        {
            if (_context.GreetMessages.Any(greet => greet.Greeting == greetModel.GreetingMessage))
            {
                return false;
            }
            var greetingEntity = new GreetEntity
            {
                Greeting = greetModel.GreetingMessage,
            };
            _context.GreetMessages.Add(greetingEntity);
            _context.SaveChanges();
            return true;
        }
       

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
        public GreetModel GreetMessagebyID(int ID)
        {
            var entity = _context.GreetMessages.FirstOrDefault(g => g.id == ID);
            if (entity != null)
            {
                return new GreetModel()
                {
                    ID = entity.id,
                    GreetingMessage = entity.Greeting
                };
            }
            return null;
        }
        public List<GreetEntity> GetAllGreetings()
        {
            return _context.GreetMessages.ToList();  // Fetching All Data from Database
        }
        public GreetEntity EditGreeting(int ID, GreetModel greetingModel)
        {
            var entity = _context.GreetMessages.FirstOrDefault(g => g.id == ID);
            if (entity != null)
            {
                entity.Greeting = greetingModel.GreetingMessage;
                _context.GreetMessages.Update(entity);
                _context.SaveChanges();
                return entity; // Returning the updated Entity
            }
            return null; // If greeting message not found
        }
        public bool DeleteGreetingMessage(int ID)
        {
            var entity = _context.GreetMessages.FirstOrDefault(g => g.id == ID);
            if (entity != null)
            {
                _context.GreetMessages.Remove(entity);
                _context.SaveChanges();
                return true; // Successfully Deleted
            }
            return false; // Not Found
        }
    }
}