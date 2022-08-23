namespace SmartFamily.Services;

public interface IAnalytics
{
    void SendEvent(string eventName);
}