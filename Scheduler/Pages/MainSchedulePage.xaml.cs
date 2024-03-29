using Microsoft.EntityFrameworkCore;
using Scheduler.Services;
using System.Linq;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    /* 
      TODO: Миграции
            Базовые справочные данные:
            - Студ. группа типа "Не указана"
            - аккаунт админа
            - основное и сокращённое расписания
    */
    /* TODO: Перемещение во времени по расписанию */
    /* TODO: Доделать навигацию */
    /* TODO: Использовать хеширование */
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
