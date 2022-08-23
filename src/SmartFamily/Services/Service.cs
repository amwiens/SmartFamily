using System.Diagnostics;

namespace SmartFamily.Services;

public class Service : IService
{
    public void OnButtonTapped()
    {
        Debug.WriteLine("OnButtonTapped");
    }
}