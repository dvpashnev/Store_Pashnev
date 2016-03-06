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

    [OperationContract(IsOneWay = true)]
    void SetPosition(Position position);

    [OperationContract(IsOneWay = true)]
    void SetProduct(Product product);

    [OperationContract(IsOneWay = true)]
    void DelProduct(Product product);

    [OperationContract(IsOneWay = true)]
    void SetDepartment(Department department);

    [OperationContract(IsOneWay = true)]
    void SetEmployee(Employee employee);

    [OperationContract(IsOneWay = true)]
    void SetUser(User user);

    [OperationContract(IsOneWay = true)]
    void SetClient(Client client);

    [OperationContract(IsOneWay = true)]
    void SetOrder(Order order);

    [OperationContract(IsOneWay = true)]
    void RemoveOrder(Order order);
  }

}
