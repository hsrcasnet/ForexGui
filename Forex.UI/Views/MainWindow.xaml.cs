using System.Windows;
using Forex.Service.Services;
using Forex.UI.ViewModels;

namespace Forex.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            IForexServiceConfiguration forexServiceConfiguration = new ForexServiceConfiguration();
            IForexService forexService = new ForexService(forexServiceConfiguration);
            this.DataContext = new MainViewModel(forexService);
        }
    }
}