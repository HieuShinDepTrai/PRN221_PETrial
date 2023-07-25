using Q1.DataAccess;
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

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Prn221TrialContext context;

        public MainWindow()
        {
            InitializeComponent();
            context = new Prn221TrialContext();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            listEmp.ItemsSource = context.Employees.ToList();
        }

        private void listEmp_Change(object sender, EventArgs e)
        {
            var employee = listEmp.SelectedItem as Employee;
            if (employee != null)
            {
                if (employee.Gender == "Male")
                {
                    Male.IsChecked = true;
                }
                else
                {
                    Female.IsChecked = true;
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Id.Text = "";
            Name.Text = "";
            Male.IsChecked = true;
            DoB.Text = "";
            Phone.Text = "";
            Idnumber.Text = "";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee()
            {
                Name = Name.Text,
                Gender = Male.IsChecked == true ? "Male" : "Female",
                Dob = DateTime.Parse(DoB.Text),
                Phone = Phone.Text,
                Idnumber = Idnumber.Text
            };

            context.Employees.Add(employee);
            context.SaveChanges();
            MessageBox.Show("Add Employee Successful!", "Add Employee");
            LoadEmployees();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Id.Text))
            {
                var employee = context.Employees.SingleOrDefault(x => x.Id == int.Parse(Id.Text));
                if (employee != null)
                {
                    employee.Name = Name.Text;
                    employee.Gender = Male.IsChecked == true ? "Male" : "Female";
                    employee.Dob = DateTime.Parse(DoB.Text);
                    employee.Phone = Phone.Text;
                    employee.Idnumber = Idnumber.Text;
                    context.Employees.Update(employee);
                    context.SaveChanges();
                    MessageBox.Show("Update successfully", "Update Employee");
                    LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Employee Not Found", "Edit Employee");
                }
            } else
            {
                MessageBox.Show("You must to choose an employee to edit", "Edit Employee");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(Id.Text))
            {
                var employee = context.Employees.SingleOrDefault(x => x.Id == int.Parse(Id.Text));
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    MessageBox.Show("Delete successfully", "Delete Employee");
                    LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Employee Not Found", "Delete Employee");
                }
            }
            else
            {
                MessageBox.Show("You must to choose an employee to delete", "Edit Employee");
            }
        }
    }
}
