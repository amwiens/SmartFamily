using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Logging;

using SmartFamily.Services;
using SmartFamily.Views;

using System.Windows.Input;

namespace SmartFamily.ViewModels;

public class MainPageViewModel : ObservableObject
{
    private readonly IEnumerable<IAnalytics> _analyticsProver;
    private readonly IService _service;

    private readonly IMessageWriter _messageWriter;
    private readonly ILogger<MainPageViewModel> _viewModelLogger;

    private readonly IServiceProvider _serviceProvider;

    int _count;

    public MainPageViewModel(IService service,
        IEnumerable<IAnalytics> analyticsProver,
        IMessageWriter messageWriter,
        ILogger<MainPageViewModel> viewModelLogger,
        IServiceProvider serviceProvider)
    {
        _service = service;
        _analyticsProver = analyticsProver;
        _viewModelLogger = viewModelLogger;
        _serviceProvider = serviceProvider;
        _messageWriter = messageWriter;

        ClickCommand = new Command(() => Count++);
        GoToOtherPageCommand = new Command(InvokeGoToOtherPageCommand);
    }

    public ICommand ClickCommand { get; }

    public ICommand GoToOtherPageCommand { get; }

    public int Count
    {
        get => _count;
        set
        {
            if (SetProperty(ref _count, value))
            {
                OnPropertyChanged(nameof(ClickedCount));

                _service.OnButtonTapped();

                foreach (var logger in _analyticsProver)
                    logger.SendEvent("Button Tapped");

                _messageWriter.Write(ClickedCount);

                _viewModelLogger.LogInformation(ClickedCount);
            }
        }
    }

    public string ClickedCount
        => Count switch
        {
            0 => "Click me",
            1 => $"Clicked {Count} time",
            _ => $"Clicked {Count} times"
        };

    private void InvokeGoToOtherPageCommand()
    {
        var page = _serviceProvider.GetRequiredService<NewDemoPage>();
        App.Current.MainPage.Navigation.PushAsync(page);
    }
}