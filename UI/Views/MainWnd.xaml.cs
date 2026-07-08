using RoadCompany.Classes;
using RoadCompany.UI.Pages;
using System.Windows;

namespace RoadCompany.UI.Views
{
    public partial class MainWnd : Window
    {
        public MainWnd()
        {
            InitializeComponent();
            Frames.MainFrame = MainFrm;
            Frames.MainFrame.Navigate(new OrganizationStructurePg());
            this.Show();
        }
    }
}
