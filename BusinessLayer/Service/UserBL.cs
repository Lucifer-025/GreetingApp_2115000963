using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Security.Cryptography;
using NLog;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRL _userRL;
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
            logger.Info("UserBL initialized.");
        }

        public bool Register(UserEntity user)
        {
            return _userRL.Register(user);
        }

        public bool UserLogin(LoginUser login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                logger.Warn("Invalid login details provided.");
                return false;
            }

            var user = GetUserByEmail(login.Email);
            if (user == null)
            {
                logger.Warn("No user found with email: {0}", login.Email);
                return false;
            }
            return CheckEmailPassword(login.Email, login.Password, user);
        }

        public UserEntity GetUserByEmail(string email)
        {
            return _userRL.GetUserByEmail(email);
        }

        public bool CheckEmailPassword(string email, string password, UserEntity user)
        {
            if (user == null || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                logger.Warn("Invalid parameters for CheckEmailPassword.");
                return false;
            }

            if (user.Email != email)
            {
                logger.Warn("Email mismatch: expected {0}, found {1}", email, user.Email);
                return false;
            }

            return VerifyPassword(password, user.Password);
        }

        public bool ForgetPassword(string email) => _userRL.ForgetPassword(email);

        public bool ResetPassword(string email, string newPassword)
        {
            return _userRL.ResetPassword(email, newPassword);
        }

        public bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            logger.Info("Starting password verification.");

            if (string.IsNullOrWhiteSpace(enteredPassword))
            {
                logger.Warn("Entered password is null or empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(storedPassword))
            {
                logger.Warn("Stored password is null or empty.");
                return false;
            }

            logger.Info("Entered Password: {0}", enteredPassword);
            logger.Info("Stored Password (Base64): {0}", storedPassword);

            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedPassword);
                logger.Info("HashBytes length: {0}", hashBytes.Length);

                if (hashBytes.Length < SaltSize + HashSize)
                {
                    logger.Warn("Invalid hash length.");
                    return false;
                }

                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);
                    for (int i = 0; i < HashSize; i++)
                    {
                        if (hashBytes[i + SaltSize] != hash[i])
                        {
                            logger.Warn("Password hashes do not match.");
                            return false;
                        }
                    }
                }

                logger.Info("Password verified successfully.");
                return true;
            }
            catch (FormatException ex)
            {
                logger.Error(ex, "Invalid Base64 format in stored password.");
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected error during password verification.");
                return false;
            }
        }

    }
}

