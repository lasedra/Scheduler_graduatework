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
    /* TODO: Использовать хеширование */
    /* TODO: Пофиксить скрытие верхней панели */
    /* TODO: Сделать "Запомнить меня" */
    /* TODO: Функционал:
                1) Оповещения МУП-а о внесении изменений в расписание
                2) Логгирование*/

    public partial class MainSchedulePage : Page
    {

        public MainSchedulePage()
        {
            InitializeComponent();
            ScheduleController schController = new ScheduleController();
            MondayTab.ScheduleDataGrid.ItemsSource = schController.MondayTab;
        }
    }
}
