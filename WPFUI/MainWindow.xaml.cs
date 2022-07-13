using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
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
            UpdateSkin(SkinType.Dark);
            GetData();
        }

        public void UpdateSkin(SkinType skin)
        {
            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            Application.Current.MainWindow?.OnApplyTemplate();
        }

        EmployeeDBEntities objContext = new EmployeeDBEntities();
        private void GetData()
        {
            EmployeeDataGrid.ItemsSource = objContext.Employees.ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Employee objEmpToEdit = EmployeeDataGrid.SelectedItem as Employee;
            if (objEmpToEdit == null)
            {
                MessageBox.Show("Cannot delete the blank Entry");
            }
            else
            {
                objContext.Employees.Remove(objEmpToEdit);
                objContext.SaveChanges();
                MessageBox.Show("Record Deleted..");
            }
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee objEmpToEdit = EmployeeDataGrid.SelectedItem as Employee;
        }
    }
}
