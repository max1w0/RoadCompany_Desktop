using RoadCompany.UI.UC;
using RoadCompany.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace RoadCompany.Classes
{
    public class EmployeeVm : INotifyPropertyChanged, IDataErrorInfo
    {
        private EmployeeVw _employee;
        private EmployeeVw _originalEmployee;
        private List<EmployeeVw> _allEmployees = new List<EmployeeVw>();

        public EmployeeVm(EmployeeVw employee = null)
        {
            if (employee == null)
            {
                _employee = new EmployeeVw();
                _originalEmployee = null;
                IsEditMode = true;
            }
            else
            {
                _employee = employee;
                _originalEmployee = new EmployeeVw
                {
                    Id = employee.Id,
                    FullNameEmp = employee.FullNameEmp,
                    BirthdayEmp = employee.BirthdayEmp,
                    PhoneEmp = employee.PhoneEmp,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    ManagerId = employee.ManagerId,
                    AssistantId = employee.AssistantId,
                    WorkPhoneEmp = employee.WorkPhoneEmp,
                    EmailEmp = employee.EmailEmp,
                    ParlorEmp = employee.ParlorEmp,
                    AdditionalInfoEmp = employee.AdditionalInfoEmp,
                    DismissalDate = employee.DismissalDate
                };
            }

            LoadAllEmployees();

            ShowPastEvents = false;
            ShowCurrentEvents = true;
            ShowFutureEvents = true;
        }

        #region Properties

        public int EmployeeId => _employee?.Id ?? 0;

        public string FullNameEmp
        {
            get => _employee.FullNameEmp;
            set
            {
                _employee.FullNameEmp = value;
                OnPropertyChanged(nameof(FullNameEmp));
            }
        }

        public DateTime? BirthdayEmp
        {
            get => _employee.BirthdayEmp;
            set
            {
                _employee.BirthdayEmp = value;
                OnPropertyChanged(nameof(BirthdayEmp));
            }
        }

        public string PhoneEmp
        {
            get => _employee.PhoneEmp;
            set
            {
                _employee.PhoneEmp = value;
                OnPropertyChanged(nameof(PhoneEmp));
            }
        }

        public int? DepartmentId
        {
            get => _employee.DepartmentId;
            set
            {
                _employee.DepartmentId = value;
                OnPropertyChanged(nameof(DepartmentId));
            }
        }

        public int? PositionId
        {
            get => _employee.PositionId;
            set
            {
                _employee.PositionId = value;
                OnPropertyChanged(nameof(PositionId));
            }
        }

        public int? ManagerId
        {
            get => _employee.ManagerId;
            set
            {
                _employee.ManagerId = value;
                OnPropertyChanged(nameof(ManagerId));
            }
        }

        public int? AssistantId
        {
            get => _employee.AssistantId;
            set
            {
                _employee.AssistantId = value;
                OnPropertyChanged(nameof(AssistantId));
            }
        }

        public string WorkPhoneEmp
        {
            get => _employee.WorkPhoneEmp;
            set
            {
                _employee.WorkPhoneEmp = value;
                OnPropertyChanged(nameof(WorkPhoneEmp));
            }
        }

        public string EmailEmp
        {
            get => _employee.EmailEmp;
            set
            {
                _employee.EmailEmp = value;
                OnPropertyChanged(nameof(EmailEmp));
            }
        }

        public string ParlorEmp
        {
            get => _employee.ParlorEmp;
            set
            {
                _employee.ParlorEmp = value;
                OnPropertyChanged(nameof(ParlorEmp));
            }
        }

        public string AdditionalInfoEmp
        {
            get => _employee.AdditionalInfoEmp;
            set
            {
                _employee.AdditionalInfoEmp = value;
                OnPropertyChanged(nameof(AdditionalInfoEmp));
            }
        }

        public DateTime? DismissalDate
        {
            get => _employee.DismissalDate;
            set
            {
                _employee.DismissalDate = value;
                OnPropertyChanged(nameof(DismissalDate));
            }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        private List<Department> _departments;
        public List<Department> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        public Department SelectedDepartment
        {
            get => Departments?.FirstOrDefault(d => d.Id == DepartmentId);
            set
            {
                DepartmentId = value?.Id;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }

        private List<Position> _positions;
        public List<Position> Positions
        {
            get => _positions;
            set
            {
                _positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }

        public Position SelectedPosition
        {
            get => Positions?.FirstOrDefault(p => p.Id == PositionId);
            set
            {
                PositionId = value?.Id;
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }

        public List<EmployeeVw> AllEmployees
        {
            get => _allEmployees;
            set
            {
                _allEmployees = value;
                OnPropertyChanged(nameof(AllEmployees));
                OnPropertyChanged(nameof(SelectedManager));
                OnPropertyChanged(nameof(SelectedAssistant));
            }
        }

        public List<EmployeeVw> DepartmentManagers => AllEmployees;

        public EmployeeVw SelectedManager
        {
            get => AllEmployees?.FirstOrDefault(m => m.Id == ManagerId);
            set
            {
                ManagerId = value?.Id;
                OnPropertyChanged(nameof(SelectedManager));
            }
        }

        public List<EmployeeVw> DepartmentAssistants => AllEmployees;

        public EmployeeVw SelectedAssistant
        {
            get => AllEmployees?.FirstOrDefault(a => a.Id == AssistantId);
            set
            {
                AssistantId = value?.Id;
                OnPropertyChanged(nameof(SelectedAssistant));
            }
        }

        #endregion

        #region Events Properties

        private List<EmployeeEventVw> _employeeEvents;
        public List<EmployeeEventVw> EmployeeEvents
        {
            get => GetFilteredEvents();
            set
            {
                _employeeEvents = value;
                OnPropertyChanged(nameof(EmployeeEvents));
                OnPropertyChanged(nameof(HasEvents));
            }
        }

        public bool HasEvents => EmployeeEvents?.Any() == true;

        private bool _showPastEvents;
        public bool ShowPastEvents
        {
            get => _showPastEvents;
            set
            {
                _showPastEvents = value;
                OnPropertyChanged(nameof(ShowPastEvents));
                OnPropertyChanged(nameof(EmployeeEvents));
            }
        }

        private bool _showCurrentEvents;
        public bool ShowCurrentEvents
        {
            get => _showCurrentEvents;
            set
            {
                _showCurrentEvents = value;
                OnPropertyChanged(nameof(ShowCurrentEvents));
                OnPropertyChanged(nameof(EmployeeEvents));
            }
        }

        private bool _showFutureEvents;
        public bool ShowFutureEvents
        {
            get => _showFutureEvents;
            set
            {
                _showFutureEvents = value;
                OnPropertyChanged(nameof(ShowFutureEvents));
                OnPropertyChanged(nameof(EmployeeEvents));
            }
        }

        #endregion

        #region Validation

        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(FullNameEmp):
                        if (string.IsNullOrWhiteSpace(FullNameEmp))
                            error = "Full name is required";
                        break;

                    case nameof(PhoneEmp):
                        if (!string.IsNullOrWhiteSpace(PhoneEmp))
                        {
                            var phoneRegex = new Regex(@"^[0-9\+\-\(\)\s\#]{1,20}$");
                            if (!phoneRegex.IsMatch(PhoneEmp))
                                error = "Invalid phone number format";
                        }
                        break;

                    case nameof(SelectedDepartment):
                        if (SelectedDepartment == null)
                            error = "Department is required";
                        break;

                    case nameof(SelectedPosition):
                        if (SelectedPosition == null)
                            error = "Position is required";
                        break;

                    case nameof(WorkPhoneEmp):
                        if (string.IsNullOrWhiteSpace(WorkPhoneEmp))
                            error = "Work phone is required";
                        else
                        {
                            var phoneRegex = new Regex(@"^[0-9\+\-\(\)\s\#]{1,20}$");
                            if (!phoneRegex.IsMatch(WorkPhoneEmp))
                                error = "Invalid work phone number format";
                        }
                        break;

                    case nameof(EmailEmp):
                        if (string.IsNullOrWhiteSpace(EmailEmp))
                            error = "Email is required";
                        else
                        {
                            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                            if (!emailRegex.IsMatch(EmailEmp))
                                error = "Invalid email format";
                        }
                        break;

                    case nameof(ParlorEmp):
                        if (string.IsNullOrWhiteSpace(ParlorEmp))
                            error = "Office/Desk is required";
                        else if (ParlorEmp.Length > 10)
                            error = "Maximum length is 10 characters";
                        break;
                }

                if (error != null)
                    _errors[columnName] = error;
                else
                    _errors.Remove(columnName);

                OnPropertyChanged(nameof(HasErrors));
                OnPropertyChanged(nameof(ErrorMessage));

                return error;
            }
        }

        public bool HasErrors => _errors.Any();

        public string ErrorMessage => HasErrors ?
            "There are errors in the form:\n" + string.Join("\n", _errors.Values) :
            string.Empty;

        public bool Validate()
        {
            var properties = new[]
            {
        nameof(FullNameEmp), nameof(PhoneEmp), nameof(SelectedDepartment),
        nameof(SelectedPosition), nameof(WorkPhoneEmp), nameof(EmailEmp),
        nameof(ParlorEmp)
    };

            foreach (var property in properties)
            {
                var error = this[property];
                if (error != null)
                    return false;
            }

            return true;
        }

        #endregion

        #region Methods

        private void LoadAllEmployees()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var query = context.Employee.AsQueryable();

                    if (EmployeeId > 0)
                    {
                        query = query.Where(e => e.Id != EmployeeId);
                    }

                    var employees = query
                        .Select(e => new EmployeeVw
                        {
                            Id = e.Id,
                            FullNameEmp = e.Person.FullName,
                            PositionId = e.JobId
                        })
                        .OrderBy(e => e.FullNameEmp)
                        .ToList();

                    AllEmployees = employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employee list: {ex.Message}");
                AllEmployees = new List<EmployeeVw>();
            }
        }

        public void LoadEmployeeEvents()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var absences = context.Absence
                        .Where(a => a.ReplacementId == EmployeeId)
                        .Select(a => new EmployeeEventVw
                        {
                            Id = a.Id,
                            EventType = a.AbsenceType.Name,
                            StartDate = a.DateStart,
                            EndDate = a.DateEnd,
                            Description = a.AbsenceType.Description,
                            Type = "Absence"
                        }).ToList();

                    var trainings = context.EventEmployee
                        .Where(ee => ee.Employee == EmployeeId)
                        .Select(ee => new EmployeeEventVw
                        {
                            Id = ee.EventId,
                            EventType = ee.Event.Name,
                            StartDate = ee.Event.StartDate,
                            EndDate = ee.Event.EndDate,
                            Description = ee.Event.Description,
                            Type = "Training"
                        }).ToList();

                    _employeeEvents = absences.Concat(trainings).ToList();
                    OnPropertyChanged(nameof(EmployeeEvents));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<EmployeeEventVw> GetFilteredEvents()
        {
            if (_employeeEvents == null) return new List<EmployeeEventVw>();

            var today = DateTime.Today;
            var filtered = _employeeEvents
                .Where(e =>
                {
                    if (e.EndDate < today && ShowPastEvents) return true;
                    if (e.StartDate <= today && e.EndDate >= today && ShowCurrentEvents) return true;
                    if (e.StartDate > today && ShowFutureEvents) return true;
                    return false;
                })
                .OrderBy(e => e.StartDate)
                .ToList();

            return filtered;
        }

        public bool SaveNewEmployee()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var person = new Person
                    {
                        FullName = FullNameEmp,
                        Birthday = BirthdayEmp,
                        Phone = PhoneEmp
                    };
                    context.Person.Add(person);
                    context.SaveChanges();

                    var employee = new Employee
                    {
                        PersonId = person.Id,
                        JobId = PositionId.Value,
                        WorkPhone = WorkPhoneEmp,
                        ParlorId = GetOrCreateParlorId(ParlorEmp),
                        Email = EmailEmp
                    };
                    context.Employee.Add(employee);
                    context.SaveChanges();

                    var empDept = new EmployeeDepartment
                    {
                        EmployeeId = employee.Id,
                        DepartmentId = DepartmentId.Value,
                        ManagerId = ManagerId,
                        AssistantId = AssistantId
                    };
                    context.EmployeeDepartment.Add(empDept);

                    _employee.Id = employee.Id;
                    _originalEmployee = new EmployeeVw
                    {
                        Id = employee.Id,
                        FullNameEmp = FullNameEmp,
                        BirthdayEmp = BirthdayEmp,
                        PhoneEmp = PhoneEmp,
                        DepartmentId = DepartmentId,
                        PositionId = PositionId,
                        ManagerId = ManagerId,
                        AssistantId = AssistantId,
                        WorkPhoneEmp = WorkPhoneEmp,
                        EmailEmp = EmailEmp,
                        ParlorEmp = ParlorEmp,
                        AdditionalInfoEmp = AdditionalInfoEmp
                    };

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool UpdateEmployee()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var employee = context.Employee.Find(_employee.Id);
                    if (employee == null) return false;

                    var person = context.Person.Find(employee.PersonId);
                    if (person == null) return false;

                    person.FullName = FullNameEmp;
                    person.Birthday = BirthdayEmp;
                    person.Phone = PhoneEmp;

                    employee.JobId = PositionId.Value;
                    employee.WorkPhone = WorkPhoneEmp;
                    employee.ParlorId = GetOrCreateParlorId(ParlorEmp);
                    employee.Email = EmailEmp;

                    var empDept = context.EmployeeDepartment
                        .FirstOrDefault(ed => ed.EmployeeId == employee.Id);

                    if (empDept != null)
                    {
                        empDept.DepartmentId = DepartmentId.Value;
                        empDept.ManagerId = ManagerId;
                        empDept.AssistantId = AssistantId;
                    }
                    else
                    {
                        empDept = new EmployeeDepartment
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = DepartmentId.Value,
                            ManagerId = ManagerId,
                            AssistantId = AssistantId
                        };
                        context.EmployeeDepartment.Add(empDept);
                    }

                    _originalEmployee = new EmployeeVw
                    {
                        Id = _employee.Id,
                        FullNameEmp = FullNameEmp,
                        BirthdayEmp = BirthdayEmp,
                        PhoneEmp = PhoneEmp,
                        DepartmentId = DepartmentId,
                        PositionId = PositionId,
                        ManagerId = ManagerId,
                        AssistantId = AssistantId,
                        WorkPhoneEmp = WorkPhoneEmp,
                        EmailEmp = EmailEmp,
                        ParlorEmp = ParlorEmp,
                        AdditionalInfoEmp = AdditionalInfoEmp
                    };

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employee: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public void CancelChanges()
        {
            if (_originalEmployee != null)
            {
                FullNameEmp = _originalEmployee.FullNameEmp;
                BirthdayEmp = _originalEmployee.BirthdayEmp;
                PhoneEmp = _originalEmployee.PhoneEmp;
                DepartmentId = _originalEmployee.DepartmentId;
                PositionId = _originalEmployee.PositionId;
                ManagerId = _originalEmployee.ManagerId;
                AssistantId = _originalEmployee.AssistantId;
                WorkPhoneEmp = _originalEmployee.WorkPhoneEmp;
                EmailEmp = _originalEmployee.EmailEmp;
                ParlorEmp = _originalEmployee.ParlorEmp;
                AdditionalInfoEmp = _originalEmployee.AdditionalInfoEmp;
            }
        }

        public bool HasUpcomingTrainings()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var today = DateTime.Today;
                    return context.EventEmployee
                        .Any(ee => ee.Employee == EmployeeId && ee.Event.StartDate > today);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DismissEmployee()
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var today = DateTime.Today;

                    var futureAbsences = context.Absence
                        .Where(a => a.ReplacementId == EmployeeId && a.DateStart > today)
                        .ToList();
                    foreach (var absence in futureAbsences)
                    {
                        context.Absence.Remove(absence);
                    }
                    context.SaveChanges();

                    var employee = context.Employee.Find(EmployeeId);
                    if (employee != null)
                    {
                        var empDept = context.EmployeeDepartment
                            .FirstOrDefault(ed => ed.EmployeeId == EmployeeId);
                    }

                    context.SaveChanges();
                    DismissalDate = today;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error dismissing employee: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool AddEmployeeEvent(EventData eventData)
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var existingEvents = context.Absence
                        .Where(a => a.ReplacementId == EmployeeId)
                        .ToList();

                    foreach (var existing in existingEvents)
                    {
                        if (eventData.StartDate <= existing.DateEnd && eventData.EndDate >= existing.DateStart)
                        {
                            if (eventData.EventType == "Absence" || eventData.EventType == "Time Off")
                            {
                                throw new Exception("Event overlaps with existing absence/time off");
                            }
                        }
                    }

                    if (eventData.EventType == "Absence" || eventData.EventType == "Time Off")
                    {
                        var isWeekend = IsWeekendDay(eventData.StartDate) || IsWeekendDay(eventData.EndDate);
                        if (isWeekend && eventData.EventType == "Time Off")
                        {
                            throw new Exception("Time off cannot be on a weekend day");
                        }

                        var absenceType = context.AbsenceType
                            .FirstOrDefault(at => at.Name == eventData.EventType);

                        if (absenceType == null)
                        {
                            absenceType = new AbsenceType
                            {
                                Name = eventData.EventType,
                                Description = eventData.Description
                            };
                            context.AbsenceType.Add(absenceType);
                            context.SaveChanges();
                        }

                        var absence = new Absence
                        {
                            DateStart = eventData.StartDate,
                            DateEnd = eventData.EndDate,
                            ReplacementId = EmployeeId,
                            AbsenceTypeId = absenceType.Id
                        };
                        context.Absence.Add(absence);
                    }
                    else if (eventData.EventType == "Training")
                    {
                        var trainingEvent = new Event
                        {
                            Name = eventData.Description,
                            TypeId = 1,
                            StartDate = eventData.StartDate,
                            EndDate = eventData.EndDate,
                            Description = eventData.Description
                        };
                        context.Event.Add(trainingEvent);
                        context.SaveChanges();

                        var eventEmployee = new EventEmployee
                        {
                            EventId = trainingEvent.Id,
                            Employee = EmployeeId,
                            IsOrganizer = false
                        };
                        context.EventEmployee.Add(eventEmployee);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding event: {ex.Message}");
            }
        }

        public bool DeleteEmployeeEvent(int eventId)
        {
            try
            {
                using (var context = new RoadEntities())
                {
                    var absence = context.Absence.Find(eventId);
                    if (absence != null)
                    {
                        context.Absence.Remove(absence);
                    }
                    else
                    {
                        var eventEmployee = context.EventEmployee
                            .FirstOrDefault(ee => ee.EventId == eventId && ee.Employee == EmployeeId);
                        if (eventEmployee != null)
                        {
                            context.EventEmployee.Remove(eventEmployee);
                        }
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting event: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private int GetOrCreateParlorId(string parlorName)
        {
            using (var context = new RoadEntities())
            {
                var parlor = context.Parlor.FirstOrDefault(p => p.ParlorName == parlorName);
                if (parlor == null)
                {
                    parlor = new Parlor
                    {
                        ParlorName = parlorName
                    };
                    context.Parlor.Add(parlor);
                    context.SaveChanges();
                }
                return parlor.Id;
            }
        }

        private bool IsWeekendDay(DateTime date)
        {
            using (var context = new RoadEntities())
            {
                var calendar = context.WorkingCalendar
                    .FirstOrDefault(wc => wc.ExceptionDate == date.Date);

                if (calendar != null)
                    return !calendar.IsWorkingDay;

                return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class EmployeeEventVw
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public string DateRange => StartDate.Date == EndDate.Date ?
            StartDate.ToString("dd.MM.yyyy") :
            $"{StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}";

        public string TypeIcon
        {
            get
            {
                switch (Type)
                {
                    case "Training":
                        return "🎓";
                    case "Vacation":
                        return "🏖️";
                    case "Absence":
                    case "Time Off":
                        return "⚠️";
                    default:
                        return "📅";
                }
            }
        }
    }

    public class EventData
    {
        public string EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
