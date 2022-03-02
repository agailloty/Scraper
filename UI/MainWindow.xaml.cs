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
using Robot;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private async void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string query = txtQuery.Text;
            string location = txtLocation.Text;
            int nbPages = 1;
            Int32.TryParse(txtNbPages.Text, out nbPages);

            //MessageBox.Show(url);
            if (query != "")
            {

                var scraper = new Indeed(query, nbPages, location);
                var allJobs = await scraper.GetJobsAsync();
                string jobTitles = "";
                foreach (var job in allJobs)
                {
                    jobTitles += job.Title;
                }

                txtResponse.Text = Environment.NewLine + jobTitles;

            }
            else
            {
                MessageBox.Show("Query cannot be empty !");
            }
        }
    }
}
