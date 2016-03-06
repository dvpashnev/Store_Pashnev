using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class Position
  {
    private int _id;
    private string _title;
    private bool _products;
    private bool _order;
    private bool _clients;
    private bool _reports;
    private bool _management;
    private bool _client_LN;
    private bool _client_FN;
    private bool _client_MN;
    private bool _client_Sex;
    private bool _client_BD;
    private bool _client_Phone1;
    private bool _client_Phone2;
    private bool _client_Phone3;
    private bool _client_Adress;
    private bool _client_SO;
    private bool _client_Discount;
    private bool _client_FirmName;

    [DataMember]
    public int Id
    {
      get { return _id; }
    }

    [DataMember]
    public string Title
    {
      get { return _title; }
    }

    [DataMember]
    public bool Products
    {
      get { return _products; }
    }

    [DataMember]
    public bool Order
    {
      get { return _order; }
    }

    [DataMember]
    public bool Clients
    {
      get { return _clients; }
    }

    [DataMember]
    public bool Reports
    {
      get { return _reports; }
    }

    [DataMember]
    public bool Management
    {
      get { return _management; }
    }

    [DataMember]
    public bool Client_LN
    {
      get { return _client_LN; }
    }

    [DataMember]
    public bool Client_FN
    {
      get { return _client_FN; }
    }

    [DataMember]
    public bool Client_MN
    {
      get { return _client_MN; }
    }

    [DataMember]
    public bool Client_Sex
    {
      get { return _client_Sex; }
    }

    [DataMember]
    public bool Client_BD
    {
      get { return _client_BD; }
    }

    [DataMember]
    public bool Client_Phone1
    {
      get { return _client_Phone1; }
    }

    [DataMember]
    public bool Client_Phone2
    {
      get { return _client_Phone2; }
    }

    [DataMember]
    public bool Client_Phone3
    {
      get { return _client_Phone3; }
    }

    [DataMember]
    public bool Client_Adress
    {
      get { return _client_Adress; }
    }

    [DataMember]
    public bool Client_SO
    {
      get { return _client_SO; }
    }

    [DataMember]
    public bool Client_Discount
    {
      get { return _client_Discount; }
    }

    [DataMember]
    public bool Client_FirmName
    {
      get { return _client_FirmName; }
    }
    public void SetValues(
          int id,
          string title,
          bool products,
          bool order,
          bool clients,
          bool reports,
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
          bool clientDiscount,
          bool clientFirmName
    )
    {
      _id = id;
      _title = title;
      _products = products;
      _order = order;
      _clients = clients;
      _reports = reports;
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
      _client_FirmName = clientFirmName;
    }

    public void Clear()
    {
      _id = 0;
      _title = "";
      _products = false;
      _order = false;
      _clients = false;
      _reports = false;
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

    public void SetId(int id)
    {
      _id = id;
    }
  }
}
