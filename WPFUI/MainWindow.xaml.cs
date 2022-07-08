using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GetData()

        {

            string connString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;

            string cmdString = string.Empty;

            using (SqlConnection con = new SqlConnection(connString))

            {

                cmdString = "SELECT ID, FirstName, LastName FROM Employee";

                SqlCommand sqlCmd = new SqlCommand(cmdString, con);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);

                DataTable dt = new DataTable("Employee");

                sda.Fill(dt);

                EmployeeDataGrid.ItemsSource = dt.DefaultView;

            }

        }
    }
}
