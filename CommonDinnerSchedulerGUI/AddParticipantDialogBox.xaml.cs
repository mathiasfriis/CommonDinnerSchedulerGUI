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
    /// Interaction logic for AddParticipantDialogBox.xaml
    /// </summary>
    public partial class AddParticipantDialogBox : Window
    {
        public string nameResult;
        public AddParticipantDialogBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(tbName.Text.Equals(""))
            {
                MessageBox.Show("Please enter a valid name.");
            }
            else
            {
                nameResult = tbName.Text;
                this.DialogResult = true;
            }
            
        }
    }
}
