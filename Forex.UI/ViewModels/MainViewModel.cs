using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Forex.Service.Model;
using Forex.Service.Services;
using Forex.Service.Services.Exceptions;

namespace Forex.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IForexService forexService;
        private string statusMessage;

        public MainViewModel(IForexService forexService)
        {
            this.forexService = forexService;

            this.Quotes = new ObservableCollection<QuoteDto>();
            this.RefreshQuotesCommand = new DelegateCommand((o) => this.LoadQuotes());
            this.StatusMessage = "Ready. Press Refresh to get quotes.";
        }

        public ObservableCollection<QuoteDto> Quotes { get; }

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
                var currencyPairs = new[] { "EUR_CHF", "CHF_EUR", };

                this.StatusMessage = $"{DateTime.Now:s} Getting quotes...";
                var quotes = (await this.forexService.GetQuotesAsync(currencyPairs)).ToList();

                this.Quotes.Clear();
                foreach (var quote in quotes)
                {
                    this.Quotes.Add(quote);
                }

                this.StatusMessage = $"{DateTime.Now:s} Successfully got quotes";
            }
            catch (QuoteRequestException qrex)
            {
                this.StatusMessage = $"Error status {qrex.Status}: {qrex.Message}";
            }
            catch (Exception ex)
            {
                this.StatusMessage = ex.Message;
            }
        }
    }
}