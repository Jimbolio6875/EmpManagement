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
            // change to dark theme and load datagrid data
            InitializeComponent();
            UpdateSkin(SkinType.Dark);
            GetData();
        }

        public void UpdateSkin(SkinType skin)
        {
            // a bunch of stuff from HandyControl package to change theme and look of user controls
            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            Application.Current.MainWindow?.OnApplyTemplate();
        }

        // instantiate new EmpDBEntity
        EmpDBEntities1 objContext = new EmpDBEntities1();
        private void GetData()
        {
            // load item source of datagrid with list of emps from empDBEntity
            EmployeeDataGrid.ItemsSource = objContext.Employees.ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // stores selected row of datagrid as new Employee
            // Removes stored Employee from Emp DB table and saves changes
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


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // new Employee instantiated
            // adds textbox info to Employee properties
            Employee objEmpToAdd = new Employee();
            objEmpToAdd.ID = int.Parse(IDTextBox.Text);
            objEmpToAdd.FirstName = FNTextBox.Text;
            objEmpToAdd.LastName = LNTextBox.Text;

            // adds new Employee to Emp DB table and saves changes
            objContext.Employees.Add(objEmpToAdd);
            objContext.SaveChanges();

            IDTextBox.Clear();
            FNTextBox.Clear();
            LNTextBox.Clear();

            // immediately adds new employee to datagrid view so you can view change without restarting program
            // apparently not a good method, but it works for now
            EmployeeDataGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            EmployeeDataGrid.ItemsSource = objContext.Employees.ToList(); ;
        }
    }
}
