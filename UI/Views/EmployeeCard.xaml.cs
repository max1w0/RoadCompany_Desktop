using RoadCompany.Classes;
using RoadCompany.Models;
using RoadCompany.UI.UC;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RoadCompany.UI.Views
{
    public partial class EmployeeCard : Window
    {
        private EmployeeVm _viewModel;
        private bool _isNewEmployee;
        public EmployeeCard(EmployeeVw employee = null)
        {
            InitializeComponent();
            _isNewEmployee = employee == null;

            if (_isNewEmployee)
            {
                _viewModel = new EmployeeVm();
                _viewModel.IsEditMode = true;
                Title = "LLC Road Company - New employee";
            }
            else
            {
                _viewModel = new EmployeeVm(employee);
                Title = $"LLC Road Company - {employee.FullNameEmp}";
            }

            DataContext = _viewModel;
            LoadDepartments();
            LoadPositions();
            LoadEvents();
        }
        private void LoadDepartments()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    _viewModel.Departments = context.Department.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadPositions()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    _viewModel.Positions = context.Position.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading positions: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoadEvents()
        {
            if (_viewModel.EmployeeId > 0)
            {
                _viewModel.LoadEmployeeEvents();
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.IsEditMode = true;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.Validate())
            {
                MessageBox.Show("Please fix the errors in the form before saving.",
                    "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool result;
                if (_isNewEmployee)
                {
                    result = _viewModel.SaveNewEmployee();
                    if (result)
                    {
                        _isNewEmployee = false;
                        MessageBox.Show("Employee successfully added.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    result = _viewModel.UpdateEmployee();
                    if (result)
                    {
                        MessageBox.Show("Employee data successfully updated.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                if (result)
                {
                    _viewModel.IsEditMode = false;
                    LoadEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewEmployee)
            {
                this.Close();
            }
            else
            {
                _viewModel.CancelChanges();
                _viewModel.IsEditMode = false;
            }
        }
        private void BtnDismiss_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.HasUpcomingTrainings())
            {
                MessageBox.Show("Cannot dismiss an employee who has upcoming trainings.",
                    "Dismissal Impossible", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to dismiss this employee?\n\n" +
                                       "All future time off and vacations will be deleted.",
                                       "Confirm Dismissal",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (_viewModel.DismissEmployee())
                    {
                        MessageBox.Show("Employee successfully dismissed.", "Dismissal",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error dismissing employee: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEventDialog(_viewModel.EmployeeId);
            if (dialog.ShowDialog() == true)
            {
                if (dialog.EventData != null)
                {
                    try
                    {
                        if (_viewModel.AddEmployeeEvent(dialog.EventData))
                        {
                            LoadEvents();
                            MessageBox.Show("Event successfully added.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding event: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is EmployeeEventVw eventToDelete)
            {
                var result = MessageBox.Show("Are you sure you want to delete this event?",
                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (_viewModel.DeleteEmployeeEvent(eventToDelete.Id))
                        {
                            LoadEvents();
                            MessageBox.Show("Event successfully deleted.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting event: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void PhoneNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9\+\-\(\)\s\#]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!regex.IsMatch(textBox.Text))
                {
                    MessageBox.Show("Please enter a valid email address.",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}