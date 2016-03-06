using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class Client
  {
    [DataMember]
    public int Id {get ; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string MiddleName { get; set; }

    [DataMember]
    public string Sex { get; set; }

    [DataMember]
    public DateTime BirthDay { get; set; }

    [DataMember]
    public string Phone1 { get; set; }

    [DataMember]
    public string Phone2 { get; set; }

    [DataMember]
    public string Phone3 { get; set; }

    [DataMember]
    public string Adress { get; set; }

    [DataMember]
    public double SumOrders { get; set; }

    [DataMember]
    public int Discount { get; set; }

    [DataMember]
    public string FirmName { get; set; }

    public void SetId(int id)
    {
      Id = id;
    }

    public void SetValues(
      int id,
      string lastName,
      string firstName,
      string middleName,
      string sex,
      DateTime birthDay,
      string phone1,
      string phone2,
      string phone3,
      string adress,
      double sumOrders,
      int discount,
      string firmName
      )
    {
      Id = id;
      LastName = lastName;
      FirstName = firstName;
      MiddleName = middleName;
      Sex = sex;
      BirthDay = birthDay;
      Phone1 = phone1;
      Phone2 = phone2;
      Phone3 = phone3;
      Adress = adress;
      SumOrders = sumOrders;
      Discount = discount;
      FirmName = firmName;
    }

    public void Clear()
    {
      Id = 0;
      LastName = String.Empty;
      FirstName = String.Empty;
      MiddleName = String.Empty;
      Sex = String.Empty;
      BirthDay = DateTime.Today;
      Phone1 = String.Empty;
      Phone2 = String.Empty;
      Phone3 = String.Empty;
      Adress = String.Empty;
      SumOrders = 0.0;
      Discount = 0;
      FirmName = String.Empty;
    }
  }
}
