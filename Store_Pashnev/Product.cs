using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Product
  {
    private static int _id;
    private static string _title;
    private static double _price;
    private static int _departmentId;
    private static int _produserId;
    private static int _quantity;
    private static bool _criticalQ;

    public static void SetValues(
      int id, 
      string title,
      double price,
      int departmentId,
      int produserId,
      int quantity,
      bool criticalQ)
    {
      _id = id;
    _title = title;
    _price = price;
    _departmentId = departmentId;
    _produserId = produserId;
    _quantity = quantity;
    _criticalQ = criticalQ;
    }

    public static void Clear()
    {
      _id = 0;
      _title = "";
      _price = 0.0;
      _departmentId = 0;
      _produserId = 0;
      _quantity = 0;
      _criticalQ = false;
    }


    public static int Id
    {
      get { return _id; }
    }

    public static string Title
    {
      get { return _title; }
    }

    public static double Price
    {
      get { return _price; }
    }

    public static int DepartmentId
    {
      get { return _departmentId; }
    }

    public static int ProduserId
    {
      get { return _produserId; }
    }

    public static int Quantity
    {
      get { return _quantity; }
    }

    public static bool CriticalQ
    {
      get { return _criticalQ; }
    }

    public static void SetId(int id)
    {
      _id = id;
    }
  }
}
