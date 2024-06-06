using Scheduler.Models;
using System;

namespace Scheduler.Services
{
    // TODO: Можно заменить статическим экземпляром Employee
    //public static class CurrentUser
    //{
    //    public static Guid EmployeeId { get; set; }

    //    public static bool WorkingStatus { get; set; }

    //    public static bool IsTelegramConfirmed { get; set; }

    //    public static bool Role { get; set; }

    //    public static string Name { get; set; } = null!;

    //    public static string Login { get; set; } = null!;

    //    public static string Password { get; set; } = null!;

    //    public static string PhoneNumber { get; set; } = null!;

    //    public static string? EMail { get; set; }

    //    public static bool SetCurrentUser(Employee loggingEmployee)
    //    {
    //        EmployeeId = SchedulerDbContext.SchedulerDbContext.CurrentUser.EmployeeId;
    //        WorkingStatus = SchedulerDbContext.SchedulerDbContext.CurrentUser.WorkingStatus;
    //        IsTelegramConfirmed = SchedulerDbContext.SchedulerDbContext.CurrentUser.IsTelegramConfirmed;
    //        Role = SchedulerDbContext.SchedulerDbContext.CurrentUser.Role;
    //        Name = SchedulerDbContext.SchedulerDbContext.CurrentUser.Name;
    //        Login = SchedulerDbContext.SchedulerDbContext.CurrentUser.Login;
    //        Password = SchedulerDbContext.SchedulerDbContext.CurrentUser.Password;
    //        PhoneNumber = SchedulerDbContext.SchedulerDbContext.CurrentUser.PhoneNumber;
    //        EMail = string.IsNullOrEmpty(SchedulerDbContext.SchedulerDbContext.CurrentUser.EMail) ? SchedulerDbContext.SchedulerDbContext.CurrentUser.EMail : null;

    //        return true;
    //    }

    //    public static string GetRoleString()
    //    { 
    //        return Role ? "Менеджер" : "Преподаватель"; 
    //    }

    //    public static Employee ToEmployee()
    //    {
    //        return new Employee()
    //        {
    //            EmployeeId = EmployeeId,
    //            WorkingStatus = WorkingStatus,
    //            IsTelegramConfirmed = IsTelegramConfirmed,
    //            Role = Role,
    //            Name = Name,
    //            Login = Login,
    //            Password = Password,
    //            PhoneNumber = PhoneNumber,
    //            EMail = string.IsNullOrEmpty(EMail) ? EMail : null
    //        };
    //    }
    //}
}
