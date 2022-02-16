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

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string url = txtUrl.Text;
            MessageBox.Show(url);
            if (url != "")
            {
                var response = Indeed.GetUrl(url).Result;
                MessageBox.Show(response);
                //txtResponse.Text = Indeed.GetUrl(txtUrl.Text).Result;
                MessageBox.Show("Finished !");
            }
            else
            {
                MessageBox.Show("URL cannot be empty !");
                return;
            }
        }
    }
}
