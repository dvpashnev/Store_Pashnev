using System.ServiceModel;

namespace Store_WSL
{
  public class StoreServiceHost
  {
    public static ServiceHost myServiceHost = null;
    public static void StartService()
    {
      myServiceHost =
      new ServiceHost(typeof(StoreService));

      myServiceHost.Open();
    }

    public static void StopService()
    {
      if (myServiceHost.State != CommunicationState.Closed)
        myServiceHost.Close();
    }
  }
}
