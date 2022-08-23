using System.Diagnostics;

namespace SmartFamily.Services;

public class AnalyticsDebug : IAnalytics
{
    public void SendEvent(string eventName)
        => Debug.WriteLine(eventName);
}