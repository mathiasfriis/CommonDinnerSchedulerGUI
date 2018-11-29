using CommonDinnerScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CommonDinnerSchedulerGUI
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        public ScheduleWindow(DinnerDay dd)
        {
            InitializeComponent();

            tbWeekday.Text = dd.dayOfWeekString;
            dgDates.ItemsSource = dd.specificDates;

            foreach(var participant in dd.Participants)
            {
                var column = new DataGridTextColumn
                {
                    Header = participant
                };
                dgDates.Columns.Add(column);
            }
        }
    }
}
