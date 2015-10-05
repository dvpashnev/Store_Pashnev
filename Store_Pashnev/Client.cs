using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Client
  {
    private static int _id;
    private static string _lastName;
    private static string _firstName;
    private static string _middleName;
    private static string _sex;
    private static DateTime _birthDay = DateTime.Today;
    private static string _phone1;
    private static string _phone2;
    private static string _phone3;
    private static string _adress;
    private static double _sumOrders;
    private static int _discount;

    public static int Id
    {
      get { return _id; }
    }

    public static string LastName
    {
      get { return _lastName; }
    }

    public static string FirstName
    {
      get { return _firstName; }
    }

    public static string MiddleName
    {
      get { return _middleName; }
    }

    public static string Sex
    {
      get { return _sex; }
    }

    public static DateTime BirthDay
    {
      get { return _birthDay; }
    }

    public static string Phone1
    {
      get { return _phone1; }
    }

    public static string Phone2
    {
      get { return _phone2; }
    }

    public static string Phone3
    {
      get { return _phone3; }
    }

    public static string Adress
    {
      get { return _adress; }
    }

    public static double SumOrders
    {
      get { return _sumOrders; }
    }

    public static int Discount
    {
      get { return _discount; }
    }

    public static void SetId(int id)
    {
      _id = id;
    }

    public static void SetValues(
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
      int discount
      )
    {
      _id = id;
      _lastName = lastName;
      _firstName = firstName;
      _middleName = middleName;
      _sex = sex;
      _birthDay = birthDay;
      _phone1 = phone1;
      _phone2 = phone2;
      _phone3 = phone3;
      _adress = adress;
      _sumOrders = sumOrders;
      _discount = discount;
    }

    public static void Clear()
    {
      _id = 0;
      _lastName = "";
      _firstName = "";
      _middleName = "";
      _sex = "";
      _birthDay = DateTime.Today;
      _phone1 = "";
      _phone2 = "";
      _phone3 = "";
      _adress = "";
      _sumOrders = 0.0;
      _discount = 0;
    }
  }
}
