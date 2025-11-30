using Login.Models;
using Login.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Login.Services
{
    internal class UserManager
    {
        const int TotalAttempts = 3;
        int _attemptsRemaining = TotalAttempts;

        public int AttemptsRemaining => _attemptsRemaining;

        public bool Register(string username, string password)
        {
            if (!RegistrationRepo.Registrations.ContainsKey(username))
            {
                RegistrationRepo.Registrations.Add(username, HashPassword(password));
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _attemptsRemaining = TotalAttempts;
        }

        public bool TryLogin(Registration credentials)
        {
            _attemptsRemaining--;
            if (RegistrationRepo.Registrations.TryGetValue(credentials.UserName, out string pwd))
            {
                return HashPassword(credentials.Password) == pwd;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
