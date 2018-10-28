using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Forex.Service.Model;
using Forex.Service.Services;

namespace Forex.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusMessage;

        public MainViewModel()
        {
            this.Quotes = new ObservableCollection<Quote>();
            this.RefreshQuotesCommand = new DelegateCommand((o) => this.LoadQuotes());
            this.StatusMessage = "Ready. Press Refresh to get quotes.";
        }

        public ObservableCollection<Quote> Quotes { get; }

        public ICommand RefreshQuotesCommand { get; }

        public string StatusMessage
        {
            get => this.statusMessage;
            set
            {
                if (this.statusMessage != value)
                {
                    this.statusMessage = value;
                    this.OnPropertyChanged(nameof(this.StatusMessage));
                }
            }
        }

        private async void LoadQuotes()
        {
            try
            {
                IForexService forexService = new ForexService(new ForexServiceConfiguration());
                var currencyPairs = new[] { "CHFUSD", "USDCHF", "CHFEUR", "EURCHF" };

                this.StatusMessage = $"{DateTime.Now:s} Getting quotes...";
                var quotes = (await forexService.GetQuotes(currencyPairs)).ToList();

                this.Quotes.Clear();
                foreach (var quote in quotes)
                {
                    this.Quotes.Add(quote);
                }

                this.StatusMessage = $"{DateTime.Now:s} Successfully got quotes";
            }
            catch (Exception ex)
            {
                this.StatusMessage = ex.Message;
            }
        }
    }
}