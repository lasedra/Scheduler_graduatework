using System.Windows;
using System.Windows.Controls;

namespace Scheduler.UserControls
{
    public partial class LessonCell : UserControl
    {
        public LessonCell()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent DeleteCellBttnClickEvent =
            EventManager.RegisterRoutedEvent(nameof(DeleteCellBttnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LessonCell));

        public event RoutedEventHandler DeleteCellBttnClick
        {
            add { AddHandler(DeleteCellBttnClickEvent, value); }
            remove { RemoveHandler(DeleteCellBttnClickEvent, value); }
        }

        private void OnDeleteCellBttn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeleteCellBttnClickEvent));
        }
    }
}
