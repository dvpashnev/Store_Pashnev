using System;
using Store_WSL;

namespace Host
{
  class Program
  {
    static void Main(string[] args)
    {
      StoreServiceHost.StartService();

      Console.WriteLine("Server is started!");

      Console.ReadKey();

      StoreServiceHost.StopService();
    }
  }
}
