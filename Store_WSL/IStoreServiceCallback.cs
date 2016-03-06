using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Store_WSL
{
  public interface IStoreServiceCallback
  {
    [OperationContract(IsOneWay = true)]
    void DbRenew();
  }
}
