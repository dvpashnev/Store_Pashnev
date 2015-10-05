using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Employee
  {
    private static int _id;
    private static string _lastName;
    private static string _firstName;
    private static string _middleName;
    private static int _departmentId;
    private static int _userId;
    private static DateTime _acceptanceDate;
    private static double _bonuses;
    private static double _sumOrders;

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

    public static int DepartmentId
    {
      get { return _departmentId; }
    }

    public static int UserId
    {
      get { return _userId; }
    }

    public static DateTime AcceptanceDate
    {
      get { return _acceptanceDate; }
    }

    public static double Bonuses
    {
      get { return _bonuses; }
    }

    public static double SumOrders
    {
      get { return _sumOrders; }
    }

    public static void SetValues(
      int id,
      string lastName,
      string firstName,
      string middleName,
      int departmentId,
      int userId,
      DateTime acceptanceDate,
      double bonuses,
      double sumOrders
      )
    {
      _id = id;
      _lastName = lastName;
      _firstName = firstName;
      _middleName = middleName;
      _departmentId = departmentId;
      _userId = userId;
      _acceptanceDate = acceptanceDate;
      _bonuses = bonuses;
      _sumOrders = sumOrders;
    }

    public static void Clear()
    {
      _id = 0;
      _lastName = "";
      _firstName = "";
      _middleName = "";
      _departmentId = 0;
      _userId = 0;
      _acceptanceDate = DateTime.Today;
      _bonuses = 0.0;
      _sumOrders = 0.0;
    }

    public static void SetId(int id)
    {
      _id = id;
    }
  }
}
