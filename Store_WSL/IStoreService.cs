using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Store_WSL
{
  [ServiceContract(CallbackContract = typeof(IStoreServiceCallback))]
  public interface IStoreService
  {
    [OperationContract]
    int GetUserId(string nick, string password);

    [OperationContract]
    DataBase GetDatabaseAccess(int userId);

    [OperationContract]
    int GetCurIdentity(string tableName);

    [OperationContract]
    DataBase SetPosition(Position position);

    [OperationContract]
    DataBase SetProduct(Product product);

    [OperationContract]
    DataBase DelProduct(Product product);

    [OperationContract]
    DataBase SetDepartment(Department department);

    [OperationContract]
    DataBase SetEmployee(Employee employee);

    [OperationContract]
    DataBase SetUser(User user);
    
    [OperationContract]
    DataBase SetClient(Client client);

    [OperationContract]
    DataBase SetOrder(Order order);

    [OperationContract]
    DataBase RemoveOrder(Order order);
  }

}
