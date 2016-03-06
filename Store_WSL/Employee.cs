using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class Employee
  {
    [DataMember]
    public int Id { get ; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string MiddleName { get; set; }

    [DataMember]
    public int DepartmentId { get; set; }

    [DataMember]
    public int UserId { get; set; }

    [DataMember]
    public DateTime AcceptanceDate { get; set; }

    [DataMember]
    public double Bonuses { get; set; }

    [DataMember]
    public double SumOrders { get; set; }

    public void SetValues(
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
      Id = id;
      LastName = lastName;
      FirstName = firstName;
      MiddleName = middleName;
      DepartmentId = departmentId;
      UserId = userId;
      AcceptanceDate = acceptanceDate;
      Bonuses = bonuses;
      SumOrders = sumOrders;
    }

    public void Clear()
    {
      Id = 0;
      LastName = "";
      FirstName = "";
      MiddleName = "";
      DepartmentId = 0;
      UserId = 0;
      AcceptanceDate = DateTime.Today;
      Bonuses = 0.0;
      SumOrders = 0.0;
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
