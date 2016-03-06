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
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public bool Products { get; set; }

    [DataMember]
    public bool Order { get; set; }

    [DataMember]
    public bool Clients { get; set; }

    [DataMember]
    public bool Reports { get; set; }

    [DataMember]
    public bool Management { get; set; }

    [DataMember]
    public bool Client_LN { get; set; }

    [DataMember]
    public bool Client_FN { get; set; }

    [DataMember]
    public bool Client_MN { get; set; }

    [DataMember]
    public bool Client_Sex { get; set; }

    [DataMember]
    public bool Client_BD { get; set; }

    [DataMember]
    public bool Client_Phone1 { get; set; }

    [DataMember]
    public bool Client_Phone2 { get; set; }

    [DataMember]
    public bool Client_Phone3 { get; set; }

    [DataMember]
    public bool Client_Adress { get; set; }

    [DataMember]
    public bool Client_SO { get; set; }

    [DataMember]
    public bool Client_Discount { get; set; }

    [DataMember]
    public bool Client_FirmName { get; set; }

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
      Id = id;
      Title = title;
      Products = products;
      Order = order;
      Clients = clients;
      Reports = reports;
      Management = management;
      Client_LN = clientLn;
      Client_FN = clientFn;
      Client_MN = clientMn;
      Client_Sex = clientSex;
      Client_BD = clientBd;
      Client_Phone1 = clientPhone1;
      Client_Phone2 = clientPhone2;
      Client_Phone3 = clientPhone3;
      Client_Adress = clientAdress;
      Client_SO = clientSo;
      Client_Discount = clientDiscount;
      Client_FirmName = clientFirmName;
    }

    public void Clear()
    {
      Id = 0;
      Title = "";
      Products = false;
      Order = false;
      Clients = false;
      Reports = false;
      Management = false;
      Client_LN = false;
      Client_FN = false;
      Client_MN = false;
      Client_Sex = false;
      Client_BD = false;
      Client_Phone1 = false;
      Client_Phone2 = false;
      Client_Phone3 = false;
      Client_Adress = false;
      Client_SO = false;
      Client_Discount = false;
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
