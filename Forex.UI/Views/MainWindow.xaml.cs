using System.Windows;
using Forex.UI.ViewModels;

namespace Forex.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}