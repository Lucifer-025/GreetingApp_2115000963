﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Security.Cryptography;
using System.Text;
using RepositoryLayer.Context;
using NLog;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserContext context;
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public UserRL(UserContext context)
        {
            this.context = context;
        }
        public bool Register(UserEntity user)
        {
            var isUserExist = context.Users.Any(u => u.Email == user.Email);
            if (isUserExist) return false;

            user.Password = HashPassword(user.Password);
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }


        public UserEntity GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                logger.Warn("GetUserByEmail called with null or empty email.");
                return null;
            }

            var user = context.Users.AsNoTracking().FirstOrDefault(user => user.Email == email);

            if (user == null)
            {
                logger.Warn("No user found with email: {0}", email);
            }

            return user;
        }

        public bool ForgetPassword(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                context.SaveChanges();
                return true; // Return true if password reset is successful
            }
            return false; // Return false if user not found
        }
        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}