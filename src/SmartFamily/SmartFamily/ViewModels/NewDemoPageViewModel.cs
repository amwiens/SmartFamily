using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Configuration;

using SmartFamily.Services;

using System.Windows.Input;

namespace SmartFamily.ViewModels;

public class NewDemoPageViewModel : ObservableObject
{
    private readonly IConfiguration _configuration;
    private readonly IMessageWriter _messageWriter;

    private string _message;

    public NewDemoPageViewModel(IConfiguration configuration,
        IMessageWriter messageWriter)
    {
        _configuration = configuration;
        _messageWriter = messageWriter;

        ChangeValuesCommand = new Command(InvokeChangeValuesCommand);

        UpdateMessageLabel();
    }

    public ICommand ChangeValuesCommand { get; }

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    private void InvokeChangeValuesCommand()
    {
        UpdatePreferencesWithRandom();

        UpdateMessageLabel();
    }

    private void UpdateMessageLabel()
    {
        var preferenceDemo = _configuration.GetValue<string>("PreferenceDemo");
        var preferenceDemoInt = _configuration.GetValue<int>("PreferenceDemoInt");

        int keyOneValue = _configuration.GetValue<int>("KeyOne");
        bool keyTwoValue = _configuration.GetValue<bool>("KeyTwo");
        string keyThreeNestedValue = _configuration.GetValue<string>("KeyThree:Message");
        string inMemoryKey1 = _configuration.GetValue<string>("InMemoryKey1");

        _messageWriter.Write($"KeyOne: {keyOneValue}");
        _messageWriter.Write($"KeyTwo: {keyTwoValue}");
        _messageWriter.Write($"KeyThree:Message: {keyThreeNestedValue}");

        _messageWriter.Write($"InMemoryKey1: {inMemoryKey1}");

        _messageWriter.Write($"PreferenceDemo: {preferenceDemo}");
        _messageWriter.Write($"PreferenceDemoInt: {preferenceDemoInt}");

        Message = $"Values from Json:{Environment.NewLine}KeyOne: {keyOneValue}{Environment.NewLine}KeyTwo: {keyTwoValue}{Environment.NewLine}KeyThree:Message: {keyThreeNestedValue}{Environment.NewLine}"
            + $"{Environment.NewLine}Values from In Memory:{Environment.NewLine}InMemoryKey1: {inMemoryKey1}{Environment.NewLine}"
            + $"{Environment.NewLine}Values from MAUI Essentials Preferences:{Environment.NewLine}PreferenceDemo: {preferenceDemo}{Environment.NewLine}PreferenceDemoInt: {preferenceDemoInt}";
    }

    private void UpdatePreferencesWithRandom()
    {
        var rand = new Random().Next(0, 100);

        _configuration["PreferenceDemo"] = $"Hello / {rand}";
        _configuration["PreferenceDemoInt"] = rand.ToString();
    }
}