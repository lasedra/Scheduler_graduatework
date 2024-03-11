using Scheduler.Models;
using System;

namespace Scheduler
{
    public static class CurrentUser
    {
        public static Guid EmployeeId { get; set; }

        public static string Name { get; set; } = null!;

        public static bool Role { get; set; }

        public static string Login { get; set; } = null!;

        public static string Password { get; set; } = null!;

        public static string TelegramId { get; set; } = null!;

        public static string PhoneNumber { get; set; } = null!;

        public static string EMail { get; set; } = null!;

        public static void SetCurrentUser(Employee loggingEmployee)
        {
            if (loggingEmployee != null)
            {
                EmployeeId = loggingEmployee.EmployeeId;
                Name = loggingEmployee.Name;
                Role = loggingEmployee.Role;
                Login = loggingEmployee.Login;
                Password = loggingEmployee.Password;
                TelegramId = loggingEmployee.TelegramId;
                PhoneNumber = loggingEmployee.PhoneNumber;
                EMail = loggingEmployee.EMail is not null ? loggingEmployee.EMail : "почта не указана";
            }
            else
                throw new ArgumentNullException(nameof(loggingEmployee));
        }
    }
}
