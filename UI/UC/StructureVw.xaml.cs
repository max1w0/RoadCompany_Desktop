using System;
using System.Windows;
using System.Windows.Controls;

namespace RoadCompany.UI.UC
{
    public partial class StructureVw : UserControl
    {
        public int structuralId { get; set; }
        public string structuralName { get; set; }
        public StructureVw()
        {
            InitializeComponent();
        }
        public StructureVw(string Name, int StructuralId)
        {
            InitializeComponent();
            StructuralName.Content = Name;
            structuralId = StructuralId;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Click;
    }
}
