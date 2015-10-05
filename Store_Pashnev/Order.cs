using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Order
  {
    private static int _id;
    private static string _category;
    private static int _employeeId;
    private static int _clientId;
    private static DateTime _orderDate;
    private static DateTime _deliveryDate;
    private static string _deliveryAdress;
    private static double _sum;
    private static Dictionary<int, int> _products;

    public static int Id
    {
      get { return _id; }
    }

    public static int EmployeeId
    {
      get { return _employeeId; }
    }

    public static int ClientId
    {
      get { return _clientId; }
    }

    public static DateTime OrderDate
    {
      get { return _orderDate; }
    }

    public static DateTime DeliveryDate
    {
      get { return _deliveryDate; }
    }

    public static string DeliveryAdress
    {
      get { return _deliveryAdress; }
    }

    public static double Sum
    {
      get { return _sum; }
    }

    public static Dictionary<int, int> Products
    {
      get { return _products; }
    }

    public static string Category
    {
      get { return _category; }
    }

    public static void SetValues(
      int id,
      string category,
      int employeeId,
      int clientId,
      DateTime orderDate,
      DateTime deliveryDate,
      string deliveryAdress,
      double sum,
      Dictionary<int, int> products
      )
    {
      _id = id;
      _category = category;
      _employeeId = employeeId;
      _clientId = clientId;
      _orderDate = orderDate;
      _deliveryDate = deliveryDate;
      _deliveryAdress = deliveryAdress;
      _sum = sum;
      _products = products;
    }

    public static void Clear()
    {
      _id = 0;
      _category = "";
      _employeeId = 0;
      _clientId = 0;
      _orderDate = DateTime.Today;
      _deliveryDate = DateTime.Today;
      _deliveryAdress = "";
      _sum = 0.0;
      _products = null;
    }
  }
}
