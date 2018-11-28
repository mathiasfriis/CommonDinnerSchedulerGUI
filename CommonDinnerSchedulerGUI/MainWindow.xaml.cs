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

            miSignUpFor.ItemsSource = schedule.dinnerDays;
        }

        private void btnAddWeekday_Click(object sender, RoutedEventArgs e)
        {
            AddWeekdayDialogBox addWeekdayDialog = new AddWeekdayDialogBox();
            if (addWeekdayDialog.ShowDialog() == true)
            {
                //MessageBox.Show(addWeekdayDialog.dayOfWeek.ToString() + " - StartDate:" + addWeekdayDialog.startDate.ToShortDateString() + " - EndDate: " + addWeekdayDialog.endDate.ToShortDateString());
                if (schedule.addDayToSchedule(addWeekdayDialog.dayOfWeek, addWeekdayDialog.startDate, addWeekdayDialog.endDate) == false)
                {
                    MessageBox.Show("Day was already added to schedule.");
                }
            }

            //Select the newly added
            LV_weekdays.SelectedIndex = schedule.dinnerDays.Count-1;
        }

        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            AddParticipantDialogBox addParticipantDialogBox = new AddParticipantDialogBox();
            if (addParticipantDialogBox.ShowDialog() == true)
            {
                string name = addParticipantDialogBox.nameResult;
                if (schedule.participants.Contains(name))
                {
                    MessageBox.Show("Participant " + name + " was already on list.");
                }
                else
                {
                    schedule.participants.Add(name);
                }
            }
        }

        private void LV_weekdays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DinnerDay day = (DinnerDay)LV_weekdays.SelectedItem;
            tbWeekday.Text = day.dayOfWeekString;
            tbStartDate.Text = day.startDate.ToShortDateString();
            tbEndDate.Text = day.endDate.ToShortDateString();
            tbParticipants.Text = day.Participants.Count.ToString();
            LV_specificWeekdayParticipants.ItemsSource = day.Participants;

        }

        private void LV_participants_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string participantName = (sender as ListView).SelectedItem.ToString();
            MessageBox.Show(participantName);
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string dayString = (sender as TextBlock).Text;
                string nameString = LV_participants.SelectedItem.ToString();

                DinnerDay dd = schedule.dinnerDays.First(x => x.dayOfWeekString.Equals(dayString));
                
                if(schedule.signPersonUpForDay(nameString,dd)==true) //Return value indicates whether the person was on the list already
                {
                    MessageBox.Show("Person was already signed up for this day.");
                }

                tbParticipants.Text = dd.Participants.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Please add participants to list before signing anyone up.");
            }

            
        }

        private void cmSignOffSpecificWeekday_Click(object sender, RoutedEventArgs e)
        {
            DinnerDay dd = (DinnerDay)LV_weekdays.SelectedItem;
            string nameString = (string)LV_specificWeekdayParticipants.SelectedItem;
            schedule.signPersonOffDay(nameString, dd);

        }

       
        //hack to make sure that items in listviews are being selected on right click
        private void LV_participants_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
