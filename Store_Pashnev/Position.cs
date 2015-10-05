using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Position
  {
    private static int _id;
    private static string _title;
    private static bool _products;
    private static bool _order;
    private static bool _clients;
    private static bool _reports;
    private static bool _claims;
    private static bool _management;
    private static bool _client_LN;
    private static bool _client_FN;
    private static bool _client_MN;
    private static bool _client_Sex;
    private static bool _client_BD;
    private static bool _client_Phone1;
    private static bool _client_Phone2;
    private static bool _client_Phone3;
    private static bool _client_Adress;
    private static bool _client_SO;
    private static bool _client_Discount;
    
    public static int Id
    {
      get { return _id; }
    }

    public static string Title
    {
      get { return _title; }
    }

    public static bool Products
    {
      get { return _products; }
    }

    public static bool Order
    {
      get { return _order; }
    }

    public static bool Clients
    {
      get { return _clients; }
    }

    public static bool Reports
    {
      get { return _reports; }
    }

    public static bool Claims
    {
      get { return _claims; }
    }

    public static bool Management
    {
      get { return _management; }
    }

    public static bool Client_LN
    {
      get { return _client_LN; }
    }

    public static bool Client_FN
    {
      get { return _client_FN; }
    }

    public static bool Client_MN
    {
      get { return _client_MN; }
    }

    public static bool Client_Sex
    {
      get { return _client_Sex; }
    }

    public static bool Client_BD
    {
      get { return _client_BD; }
    }

    public static bool Client_Phone1
    {
      get { return _client_Phone1; }
    }

    public static bool Client_Phone2
    {
      get { return _client_Phone2; }
    }

    public static bool Client_Phone3
    {
      get { return _client_Phone3; }
    }

    public static bool Client_Adress
    {
      get { return _client_Adress; }
    }

    public static bool Client_SO
    {
      get { return _client_SO; }
    }

    public static bool Client_Discount
    {
      get { return _client_Discount; }
    }

    public static void SetValues(
          int id,
          string title,
          bool products,
          bool order,
          bool clients,
          bool reports,
          bool claims,
          bool management,
          bool clientLn,
          bool clientFn,
          bool clientMn,
          bool clientSex,
          bool clientBd,
          bool clientPhone1,
          bool clientPhone2,
          bool clientPhone3,
          bool clientAdress,
          bool clientSo,
          bool clientDiscount
    )
    {
      _id = id;
      _title = title;
      _products = products;
      _order = order;
      _clients = clients;
      _reports = reports;
      _claims = claims;
      _management = management;
      _client_LN = clientLn;
      _client_FN = clientFn;
      _client_MN = clientMn;
      _client_Sex = clientSex;
      _client_BD = clientBd;
      _client_Phone1 = clientPhone1;
      _client_Phone2 = clientPhone2;
      _client_Phone3 = clientPhone3;
      _client_Adress = clientAdress;
      _client_SO = clientSo;
      _client_Discount = clientDiscount;
    }

    public static void Clear()
    {
      _id = 0;
      _title = "";
      _products = false;
      _order = false;
      _clients = false;
      _reports = false;
      _claims = false;
      _management = false;
      _client_LN = false;
      _client_FN = false;
      _client_MN = false;
      _client_Sex = false;
      _client_BD = false;
      _client_Phone1 = false;
      _client_Phone2 = false;
      _client_Phone3 = false;
      _client_Adress = false;
      _client_SO = false;
      _client_Discount = false;
    }

    public static void SetId(int id)
    {
      _id = id;
    }
  }
}
