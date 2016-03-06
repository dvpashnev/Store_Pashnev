using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Store_WSL
{
  [DataContract]
  public class Order
  {
    private Dictionary<int, ProductInOrder> _products = new Dictionary<int, ProductInOrder>();

    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public int EmployeeId { get; set; }

    [DataMember]
    public int ClientId { get; set; }

    [DataMember]
    public DateTime OrderDate { get; set; }

    [DataMember]
    public DateTime DeliveryDate { get; set; }

    [DataMember]
    public string DeliveryAdress { get; set; }

    [DataMember]
    public double Sum { get; set; }

    [DataMember]
    public Dictionary<int, ProductInOrder> Products {
      get { return _products; }
      set { _products = value; } }
    
    [DataMember]
    public string Category { get; set; }

    public void SetValues(
      int id,
      string category,
      int employeeId,
      int clientId,
      DateTime orderDate,
      DateTime deliveryDate,
      string deliveryAdress,
      double sum,
      Dictionary<int, ProductInOrder> products
      )
    {
      Id = id;
      Category = category;
      EmployeeId = employeeId;
      ClientId = clientId;
      OrderDate = orderDate;
      DeliveryDate = deliveryDate;
      DeliveryAdress = deliveryAdress;
      Sum = sum;
      Products = products;
    }

    public void Clear()
    {
      Id = 0;
      Category = "";
      EmployeeId = 0;
      ClientId = 0;
      OrderDate = DateTime.Today;
      DeliveryDate = DateTime.Today;
      DeliveryAdress = "";
      Sum = 0.0;
      Products = new Dictionary<int, ProductInOrder>();
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
