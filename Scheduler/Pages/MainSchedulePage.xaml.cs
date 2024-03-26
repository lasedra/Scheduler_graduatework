using Microsoft.EntityFrameworkCore;
using Scheduler.Services;
using System.Linq;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    /* 
      TODO: Сделать миграции
            Базовые справочные данные:
            - Студ. группа типа "Не указана"
            - аккаунт админа
            - основное и сокращённое расписания
    */
    /* TODO: Подумать над выводом PIVOT schedule */
    /* TODO: Дизайн норм сделать */
    /* TODO: Использовать регулярки в регистрации и т.п.*/
    /* TODO: Функционал:
                1) Оповещения МУП-а о внесении изменений в расписание
                2) Логгирование*/

    public partial class MainSchedulePage : Page
    {

        public MainSchedulePage()
        {
            InitializeComponent();
            ScheduleController schController = new ScheduleController();
            MondayGrid.ItemsSource = schController.MondayTab;
            TuesdayGrid.ItemsSource= schController.TuesdayTab;
            WednesdayGrid.ItemsSource = schController.WednesdayTab;
            ThursdayGrid.ItemsSource = schController.ThursdayTab;
            FridayGrid.ItemsSource = schController.FridayTab;
        }
    }
}
