using Scheduler.Models;
using System;

namespace Scheduler.Services
{
    public static class CurrentUser
    {
        public static Guid EmployeeId { get; set; }

        public static bool WorkingStatus { get; set; }

        public static string Name { get; set; } = null!;

        public static bool Role { get; set; }

        public static string Login { get; set; } = null!;

        public static string Password { get; set; } = null!;

        public static string PhoneNumber { get; set; } = null!;

        public static string? EMail { get; set; }

        public static bool TelegramConfirmed { get; set; }

        public static bool SetCurrentUser(Employee loggingEmployee)
        {
            EmployeeId = loggingEmployee.EmployeeId;
            Name = loggingEmployee.Name;
            Role = loggingEmployee.Role;
            Login = loggingEmployee.Login;
            Password = loggingEmployee.Password;
            TelegramConfirmed = loggingEmployee.TelegramConfirmed;
            PhoneNumber = loggingEmployee.PhoneNumber;
            EMail = loggingEmployee.EMail is not null ? loggingEmployee.EMail : "Почта не указана";

            return true;
        }

        public static string GetRoleString()
        {
            return Role ? "менеджер учебного процесса" : "преподаватель";
        }
    }
}
