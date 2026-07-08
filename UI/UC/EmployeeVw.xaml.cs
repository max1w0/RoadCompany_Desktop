using System;
using System.Windows;
using System.Windows.Controls;

namespace RoadCompany.UI.UC
{
    public partial class EmployeeVw : UserControl
    {
        public int Id { get; set; }
        public string FullNameEmp { get; set; }
        public string StructureEmp { get; set; }
        public string PositionEmp { get; set; }
        public string PhoneEmp { get; set; }
        public string ParlorEmp { get; set; }
        public string EmailEmp { get; set; }
        public DateTime? BirthdayEmp { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public int? ManagerId { get; set; }
        public int? AssistantId { get; set; }
        public string WorkPhoneEmp { get; set; }
        public string AdditionalInfoEmp { get; set; }
        public DateTime? DismissalDate { get; set; }
        public EmployeeVw()
        {
            InitializeComponent();
        }
        public EmployeeVw(int id, string fullName, string structureName, string positionName, string workPhone, string parlorName, string email)
        {
            InitializeComponent();
            Id = id;
            FullNameEmp = fullName;
            StructureEmp = structureName;
            PositionEmp = positionName;
            PhoneEmp = workPhone;
            ParlorEmp = parlorName;
            EmailEmp = email;

            fullNameEmp.Text = fullName;
            structureEmp.Text = structureName;
            positionEmp.Text = positionName;
            phoneEmp.Text = workPhone;
            parlorEmp.Text = parlorName;
            emailEmp.Text = email;
        }
        private void EmployeeCardBtn_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Click;

        public EmployeeVw(int id, string fullName, DateTime? birthday, string phone,
                         int? departmentId, int? positionId, int? managerId, int? assistantId,
                         string workPhone, string email, string parlor, string additionalInfo,
                         DateTime? dismissalDate = null)
        {
            InitializeComponent();
            Id = id;
            FullNameEmp = fullName;
            BirthdayEmp = birthday;
            PhoneEmp = phone;
            DepartmentId = departmentId;
            PositionId = positionId;
            ManagerId = managerId;
            AssistantId = assistantId;
            WorkPhoneEmp = workPhone;
            EmailEmp = email;
            ParlorEmp = parlor;
            AdditionalInfoEmp = additionalInfo;
            DismissalDate = dismissalDate;

            fullNameEmp.Text = fullName;
            phoneEmp.Text = workPhone;
            emailEmp.Text = email;
            parlorEmp.Text = parlor;
        }
        public void UpdateDisplayText(string structureName = null, string positionName = null)
        {
            if (!string.IsNullOrEmpty(structureName))
                StructureEmp = structureName;

            if (!string.IsNullOrEmpty(positionName))
                PositionEmp = positionName;

            structureEmp.Text = StructureEmp;
            positionEmp.Text = PositionEmp;
        }
    }
}
