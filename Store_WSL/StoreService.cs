using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Store_WSL
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, 
    InstanceContextMode = InstanceContextMode.Single)]
  public class StoreService : IStoreService
  {
    DataBase _db;

    public int GetUserId(string n, string p)
    {
      int id;

      using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["Store"].ConnectionString)
        ) // создание объекта подключения
      {
        connect.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connect;
        SqlParameter nick = new SqlParameter();
        nick.ParameterName = "@Nick";
        nick.Direction = ParameterDirection.Input;
        nick.Value = n;
        command.Parameters.Add(nick);

        SqlParameter password = new SqlParameter();
        password.ParameterName = "@Password";
        password.Direction = ParameterDirection.Input;
        password.Value = p;
        command.Parameters.Add(password);

        command.CommandText = "select Id from [User] where Nick = @Nick";

        if (Convert.ToInt32(command.ExecuteScalar()) == 0)
        {
          return 0;
        }

        command.CommandText = "select Id from [User] where Nick = @Nick and Password = @Password";

        id = Convert.ToInt32(command.ExecuteScalar());

        if (id == 0)
        {
          return -1;
        }
      }

      return id;
    }

    public DataBase GetDatabaseAccess(int userId)
    {
      _db = new DataBase(userId);
      return _db;
    }

    public int GetCurIdentity(string tableName)
    {
      int curIdentity;
      using (SqlConnection connect = new SqlConnection(_db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('" + tableName + "')");
        curIdentity = Convert.ToInt32(command.ExecuteScalar());
      }
      return curIdentity;
    }

    public DataBase SetProduct(Product product)
    {
      DataRow row = _db._ds.Tables["Product"].Rows.Find(product.Id);
      if (row == null)
      {
        row = _db._ds.Tables["Product"].NewRow();
        row["Id"] = product.Id;
        row["Title"] = product.Title;
        row["PurchasePrice"] = product.PurchasePrice;
        row["Markup"] = product.Markup;
        row["Price"] = product.Price;
        row["DepartmentId"] = product.DepartmentId;
        row["ProduserId"] = product.ProduserId;
        row["Quantity"] = product.Quantity;
        row["CriticalQ"] = product.CriticalQ;

        _db._ds.Tables["Product"].Rows.Add(row);
      }
      else
      {
        row["Id"] = product.Id;
        row["Title"] = product.Title;
        row["PurchasePrice"] = product.PurchasePrice;
        row["Markup"] = product.Markup;
        row["Price"] = product.Price;
        row["DepartmentId"] = product.DepartmentId;
        row["ProduserId"] = product.ProduserId;
        row["Quantity"] = product.Quantity;
        row["CriticalQ"] = product.CriticalQ;
      }

      _db._adapterProduct.Update(_db._ds.Tables["Product"]);

      return _db;
    }

    public DataBase DelProduct(Product product)
    {
      _db._ds.Tables["Product"].Rows.Find(product.Id).Delete();
      _db._adapterProduct.Update(_db._ds.Tables["Product"]);
      return _db;
    }

    public DataBase SetPosition(Position position)
    {
      DataRow row = _db._ds.Tables["Position"].Rows.Find(position.Id);

      if (row == null)
      {
        row = _db._ds.Tables["Position"].NewRow();

        row["Id"] = position.Id;
        row["Title"] = position.Title;
        row["Products"] = position.Products;
        row["Order"] = position.Order;
        row["Clients"] = position.Clients;
        row["Reports"] = position.Reports;
        row["Management"] = position.Management;
        row["Client_LN"] = position.Client_LN;
        row["Client_FN"] = position.Client_FN;
        row["Client_MN"] = position.Client_MN;
        row["Client_Sex"] = position.Client_Sex;
        row["Client_BD"] = position.Client_BD;
        row["Client_Phone1"] = position.Client_Phone1;
        row["Client_Phone2"] = position.Client_Phone2;
        row["Client_Phone3"] = position.Client_Phone3;
        row["Client_Adress"] = position.Client_Adress;
        row["Client_SO"] = position.Client_SO;
        row["Client_Discount"] = position.Client_Discount;

        _db._ds.Tables["Position"].Rows.Add(row);
      }
      else
      {
        row["Id"] = position.Id;
        row["Title"] = position.Title;
        row["Products"] = position.Products;
        row["Order"] = position.Order;
        row["Clients"] = position.Clients;
        row["Reports"] = position.Reports;
        row["Management"] = position.Management;
        row["Client_LN"] = position.Client_LN;
        row["Client_FN"] = position.Client_FN;
        row["Client_MN"] = position.Client_MN;
        row["Client_Sex"] = position.Client_Sex;
        row["Client_BD"] = position.Client_BD;
        row["Client_Phone1"] = position.Client_Phone1;
        row["Client_Phone2"] = position.Client_Phone2;
        row["Client_Phone3"] = position.Client_Phone3;
        row["Client_Adress"] = position.Client_Adress;
        row["Client_SO"] = position.Client_SO;
        row["Client_Discount"] = position.Client_Discount;
      }

      _db._adapterPosition.Update(_db._ds.Tables["Position"]);

      return _db;
    }

    public DataBase SetDepartment(Department department)
    {
      DataRow row = _db._ds.Tables["Department"].Rows.Find(department.Id);

      if (row == null)
      {
        row = _db._ds.Tables["Department"].NewRow();

        row["Id"] = department.Id;
        row["Name"] = department.Name;

        _db._ds.Tables["Department"].Rows.Add(row);
      }
      else
      {
        row["Id"] = department.Id;
        row["Name"] = department.Name;
      }

      _db._adapterDepartment.Update(_db._ds.Tables["Department"]);

      return _db;
    }

    public DataBase SetEmployee(Employee employee)
    {
      DataRow row = _db._ds.Tables["Employee"].Rows.Find(employee.Id);

      if (row == null)
      {
        row = _db._ds.Tables["Employee"].NewRow();

        row["Id"] = employee.Id;
        row["FirstName"] = employee.FirstName;
        row["LastName"] = employee.LastName;
        row["MiddleName"] = employee.MiddleName;
        row["AcceptanceDate"] = employee.AcceptanceDate;
        row["Bonuses"] = employee.Bonuses;
        row["DepartmentId"] = employee.DepartmentId;
        row["SumOrders"] = employee.SumOrders;
        row["UserId"] = employee.UserId;

        _db._ds.Tables["Employee"].Rows.Add(row);
      }
      else
      {
        row["Id"] = employee.Id;
        row["FirstName"] = employee.FirstName;
        row["LastName"] = employee.LastName;
        row["MiddleName"] = employee.MiddleName;
        row["AcceptanceDate"] = employee.AcceptanceDate;
        row["Bonuses"] = employee.Bonuses;
        row["DepartmentId"] = employee.DepartmentId;
        row["SumOrders"] = employee.SumOrders;
        row["UserId"] = employee.UserId;
      }

      _db._adapterEmployee.Update(_db._ds.Tables["Employee"]);

      return _db;
    }

    public DataBase SetUser(User user)
    {
      DataRow row = _db._ds.Tables["User"].Rows.Find(user.Id);

      if (row == null)
      {
        row = _db._ds.Tables["User"].NewRow();

        row["Id"] = user.Id;
        row["Nick"] = user.Nick;
        row["Password"] = user.Password;
        row["PositionId"] = user.PositionId;

        _db._ds.Tables["User"].Rows.Add(row);
      }
      else
      {
        row["Id"] = user.Id;
        row["Nick"] = user.Nick;
        row["Password"] = user.Password;
        row["PositionId"] = user.PositionId;
      }

      _db._adapterUser.Update(_db._ds.Tables["User"]);

      return _db;
    }

    public DataBase SetClient(Client client)
    {
      DataRow row = _db._ds.Tables["Client"].Rows.Find(client.Id);

      if (row == null)
      {
        row = _db._ds.Tables["Client"].NewRow();

        row["Id"] = client.Id;
        row["Adress"] = client.Adress;
        row["BirthDay"] = client.BirthDay;
        row["Discount"] = client.Discount;
        row["FirstName"] = client.FirstName;
        row["LastName"] = client.LastName;
        row["MiddleName"] = client.MiddleName;
        row["Phone1"] = client.Phone1;
        row["Phone2"] = client.Phone2;
        row["Phone3"] = client.Phone3;
        row["Sex"] = client.Sex;
        row["SumOrders"] = client.SumOrders;
        row["FirmName"] = client.FirmName;

        _db._ds.Tables["Client"].Rows.Add(row);
      }
      else
      {
        row["Id"] = client.Id;
        row["Adress"] = client.Adress;
        row["BirthDay"] = client.BirthDay;
        row["Discount"] = client.Discount;
        row["FirstName"] = client.FirstName;
        row["LastName"] = client.LastName;
        row["MiddleName"] = client.MiddleName;
        row["Phone1"] = client.Phone1;
        row["Phone2"] = client.Phone2;
        row["Phone3"] = client.Phone3;
        row["Sex"] = client.Sex;
        row["SumOrders"] = client.SumOrders;
        row["FirmName"] = client.FirmName;
      }

      _db._adapterClient.Update(_db._ds.Tables["Client"]);

      return _db;
    }

    public DataBase SetOrder(Order order)
    {
      DataRow row = _db._ds.Tables["Order"].Rows.Find(order.Id);

      if (row == null)
      {
        row = _db._ds.Tables["Order"].NewRow();

        row["Id"] = order.Id;
        row["Category"] = order.Category;
        row["ClientId"] = order.ClientId;
        row["DeliveryAdress"] = order.DeliveryAdress;
        row["DeliveryDate"] = order.DeliveryDate;
        row["EmployeeId"] = order.EmployeeId;
        row["OrderDate"] = order.OrderDate;
        row["Sum"] = order.Sum;

        _db._ds.Tables["Order"].Rows.Add(row);

        _db._adapterOrder.Update(_db._ds.Tables["Order"]);

        foreach (var pair in order.Products)
        {
          DataRow dr = _db._ds.Tables["Products_in_Order"].NewRow();
          dr["Id"] = pair.Value.Id;
          dr["OrderId"] = pair.Value.OrderId;
          dr["ProductId"] = pair.Value.ProductId;
          dr["Quantity"] = pair.Value.Quantity;
          dr["FinalPrice"] = pair.Value.FinalPrice;
          dr["Sum"] = pair.Value.Sum;

          _db._ds.Tables["Products_in_Order"].Rows.Add(dr);

          _db._adapterProducts_in_Order.Update(_db._ds.Tables["Products_in_Order"]);
        }
      }
      else
      {
        row["Id"] = order.Id;
        row["Category"] = order.Category;
        row["ClientId"] = order.ClientId;
        row["DeliveryAdress"] = order.DeliveryAdress;
        row["DeliveryDate"] = order.DeliveryDate;
        row["EmployeeId"] = order.EmployeeId;
        row["OrderDate"] = order.OrderDate;
        row["Sum"] = order.Sum;

        _db._adapterOrder.Update(_db._ds.Tables["Order"]);

        DataRow[] pios = _db._ds.Tables["Products_in_Order"].Select("OrderId = " + order.Id).ToArray();

        foreach (DataRow r in pios)
        {
          r.Delete();
        }

        _db._adapterProducts_in_Order.Update(_db._ds.Tables["Products_in_Order"]);

        foreach (var pair in order.Products)
        {
          DataRow dr = _db._ds.Tables["Products_in_Order"].Rows.Find(pair.Value.Id);
          dr["Id"] = pair.Value.Id;
          dr["OrderId"] = pair.Value.OrderId;
          dr["ProductId"] = pair.Value.ProductId;
          dr["Quantity"] = pair.Value.Quantity;
          dr["FinalPrice"] = pair.Value.FinalPrice;
          dr["Sum"] = pair.Value.Sum;

          _db._ds.Tables["Products_in_Order"].Rows.Add(dr);
        }

        _db._adapterProducts_in_Order.Update(_db._ds.Tables["Products_in_Order"]);
      }

      _db._adapterProduct.Fill(_db._ds.Tables["Product"]);
      _db._adapterEmployee.Fill(_db._ds.Tables["Employee"]);
      _db._adapterClient.Fill(_db._ds.Tables["Client"]);

      return _db;
    }

    public DataBase RemoveOrder(Order order)
    {
      DataRow[] rows = _db._ds.Tables["Products_in_Order"].Select("OrderId = " + order.Id).ToArray();

      foreach (DataRow row in rows)
      {
        row.Delete();
      }

      _db._adapterProducts_in_Order.Update(_db._ds.Tables["Products_in_Order"]);

      DataRow rowInOrder = _db._ds.Tables["Order"].Rows.Find(order.Id);

      rowInOrder.Delete();



      _db._adapterOrder.Update(_db._ds.Tables["Order"]);

      return _db;
    }
  }
}
