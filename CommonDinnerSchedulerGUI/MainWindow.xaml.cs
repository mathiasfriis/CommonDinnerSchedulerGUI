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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommonDinnerSchedulerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Schedule schedule;
        public MainWindow()
        {
            schedule = new Schedule();
            InitializeComponent();

            LV_weekdays.ItemsSource = schedule.dinnerDays;

            LV_participants.ItemsSource = schedule.participants;
        }

        private void btnAddWeekday_Click(object sender, RoutedEventArgs e)
        {
            AddWeekdayDialogBox addWeekdayDialog = new AddWeekdayDialogBox();
            if(addWeekdayDialog.ShowDialog()==true)
            {
                //MessageBox.Show(addWeekdayDialog.dayOfWeek.ToString() + " - StartDate:" + addWeekdayDialog.startDate.ToShortDateString() + " - EndDate: " + addWeekdayDialog.endDate.ToShortDateString());
                if(schedule.addDayToSchedule(addWeekdayDialog.dayOfWeek, addWeekdayDialog.startDate, addWeekdayDialog.endDate)==false)
                {
                    MessageBox.Show("Day was already added to schedule.");
                }
            }
        }

        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            AddParticipantDialogBox addParticipantDialogBox = new AddParticipantDialogBox();
            if(addParticipantDialogBox.ShowDialog()==true)
            {
                string name = addParticipantDialogBox.nameResult;
                if(schedule.participants.Contains(name))
                {
                    MessageBox.Show("Participant " + name + " was already on list.");
                }
                else
                {
                    schedule.participants.Add(name);
                }
            }
        }
    }
}
