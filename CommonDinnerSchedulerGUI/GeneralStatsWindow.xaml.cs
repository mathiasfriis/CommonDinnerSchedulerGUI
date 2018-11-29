using CommonDinnerSchedulerGUI.Business_Logic;
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
    /// Interaction logic for GeneralStatsWindow.xaml
    /// </summary>
    public partial class GeneralStatsWindow : Window
    {
        public GeneralStatsWindow(List<ddPerson> people)
        {
            InitializeComponent();
            dgPeople.ItemsSource = people;
        }
    }
}
