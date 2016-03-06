using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class DataBase
  {
    [DataMember] public string _connectString = ConfigurationManager.ConnectionStrings["Store"].ConnectionString;

    [DataMember] public DataSet _ds; // ссылка на нетипизированный набор DataSet

    public SqlDataAdapter _adapterClaim;
    public SqlDataAdapter _adapterClient;
    public SqlDataAdapter _adapterDepartment;
    public SqlDataAdapter _adapterEmployee;
    public SqlDataAdapter _adapterOrder;
    public SqlDataAdapter _adapterPosition;
    public SqlDataAdapter _adapterProduct;
    public SqlDataAdapter _adapterProducts_in_Order;
    public SqlDataAdapter _adapterUser;

    [DataMember] public Dictionary<string, bool> Rights;

    public DataBase()
    {
      _ds = new DataSet(); // Создаем множественный набор данных 

      // заполняю множественный набор данных из БД Connection
      using (SqlConnection connect = new SqlConnection(_connectString)) // создание объекта подключения
      {
        // объявление адаптеров и передача адаптерам текста команды Select и подключения
        _adapterClaim = new SqlDataAdapter("Select * From Claim", _connectString);
        _adapterClient = new SqlDataAdapter("Select * From Client", _connectString);
        _adapterDepartment = new SqlDataAdapter("Select * From Department", _connectString);
        _adapterEmployee = new SqlDataAdapter("Select * From Employee", _connectString);
        _adapterOrder = new SqlDataAdapter("Select * From Order", _connectString);
        _adapterPosition = new SqlDataAdapter("Select * From Position", _connectString);
        _adapterProduct = new SqlDataAdapter("Select * From Product", _connectString);
        _adapterProducts_in_Order = new SqlDataAdapter("select * from Products_in_Order", _connectString);

        _adapterUser = new SqlDataAdapter("Select * From User", _connectString);

        SqlCommandBuilder build = new SqlCommandBuilder(_adapterClaim);
        _adapterClaim.Fill(_ds, "Claim");
        build = new SqlCommandBuilder(_adapterClient);
        _adapterClient.Fill(_ds, "Client");
        build = new SqlCommandBuilder(_adapterDepartment);
        _adapterDepartment.Fill(_ds, "Department");
        build = new SqlCommandBuilder(_adapterEmployee);
        _adapterEmployee.Fill(_ds, "Employee");
        build = new SqlCommandBuilder(_adapterOrder);
        _adapterOrder.Fill(_ds, "Order");
        build = new SqlCommandBuilder(_adapterPosition);
        _adapterPosition.Fill(_ds, "Position");
        build = new SqlCommandBuilder(_adapterProduct);
        _adapterProduct.Fill(_ds, "Product");
        build = new SqlCommandBuilder(_adapterProducts_in_Order);
        _adapterProducts_in_Order.Fill(_ds, "Products_in_Order");
        build = new SqlCommandBuilder(_adapterUser);
        _adapterUser.Fill(_ds, "User");
      }
    }

    public DataBase(int userID)
    {

      _ds = new DataSet(); // Создаем множественный набор данных 

      using (SqlConnection connect = new SqlConnection(_connectString)) // создание объекта подключения
      {
        connect.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connect;
        SqlParameter UID = new SqlParameter();
        UID.Direction = ParameterDirection.Input;
        UID.ParameterName = "@UID";
        UID.Value = userID;
        command.Parameters.Add(UID);
        command.CommandText = String.Format("select PositionId from [User] where Id = @UID");
        int PosID = Convert.ToInt32(command.ExecuteScalar());

        command.Parameters.Clear();

        SqlParameter PositionID = new SqlParameter();
        PositionID.Direction = ParameterDirection.Input;
        PositionID.ParameterName = "@PosID";
        PositionID.Value = PosID;
        command.Parameters.Add(PositionID);
        //++++++++++++Добавить поля из Position в комманду!!!!!!!!!!!!!!!
        command.CommandText = String.Format("select * from Position where Id = @PosID");

        Rights = new Dictionary<string, bool>();
        using (SqlDataReader sdr = command.ExecuteReader())
        {
          sdr.Read();
          for (int i = 2; i < sdr.FieldCount; i++)
          {
            Rights.Add(sdr.GetName(i), Convert.ToInt32(sdr.GetValue(i)) == 1);
          }
        }

        //загружать, если нужен доступ к этим модулям - добавить поля в Position
        SqlCommandBuilder build;
        DataColumn[] key = new DataColumn[1];
        if (Rights["Clients"])
        {
          StringBuilder fields = new StringBuilder();
          fields.Append("Id, ");

          if (Rights["Client_LN"]) fields.Append("LastName, ");
          if (Rights["Client_FN"]) fields.Append("FirstName, ");
          if (Rights["Client_MN"]) fields.Append("MiddleName, ");
          if (Rights["Client_Sex"]) fields.Append("Sex, ");
          if (Rights["Client_BD"]) fields.Append("BirthDay, ");
          if (Rights["Client_Phone1"]) fields.Append("Phone1, ");
          if (Rights["Client_Phone2"]) fields.Append("Phone2, ");
          if (Rights["Client_Phone3"]) fields.Append("Phone3, ");
          if (Rights["Client_Adress"]) fields.Append("Adress, ");
          if (Rights["Client_SO"]) fields.Append("SumOrders, ");
          if (Rights["Client_Discount"]) fields.Append("Discount, ");
          if (Rights["Client_FirmName"]) fields.Append("FirmName, ");
          fields.Remove(fields.Length - 2, 1);//последним м.б. любое поле, запятую удалить

          _adapterClient = new SqlDataAdapter("Select "
                                              + fields
                                              + " From Client", _connectString);
          build = new SqlCommandBuilder(_adapterClient);
          _adapterClient.Fill(_ds, "Client");
          key[0] = _ds.Tables["Client"].Columns["Id"];
          _ds.Tables["Client"].PrimaryKey = key;
        }

        _adapterClaim = new SqlDataAdapter("Select * From Claim", _connectString);
        build = new SqlCommandBuilder(_adapterClaim);
        _adapterClaim.Fill(_ds, "Claim");
        key[0] = _ds.Tables["Claim"].Columns["Id"];
        _ds.Tables["Claim"].PrimaryKey = key;

        _adapterDepartment = new SqlDataAdapter("Select * From Department", _connectString);
        build = new SqlCommandBuilder(_adapterDepartment);
        _adapterDepartment.Fill(_ds, "Department");
        key[0] = _ds.Tables["Department"].Columns["Id"];
        _ds.Tables["Department"].PrimaryKey = key;

        _adapterEmployee = new SqlDataAdapter("Select * From Employee", _connectString);
        build = new SqlCommandBuilder(_adapterEmployee);
        _adapterEmployee.Fill(_ds, "Employee");
        key[0] = _ds.Tables["Employee"].Columns["Id"];
        _ds.Tables["Employee"].PrimaryKey = key;

        _adapterPosition = new SqlDataAdapter("Select * From Position", _connectString);
        build = new SqlCommandBuilder(_adapterPosition);
        _adapterPosition.Fill(_ds, "Position");
        key[0] = _ds.Tables["Position"].Columns["Id"];
        _ds.Tables["Position"].PrimaryKey = key;

        _adapterUser = new SqlDataAdapter("Select * From [User]", _connectString);
        build = new SqlCommandBuilder(_adapterUser);
        _adapterUser.Fill(_ds, "User");
        key[0] = _ds.Tables["User"].Columns["Id"];
        _ds.Tables["User"].PrimaryKey = key;

        _adapterOrder = new SqlDataAdapter("Select * From [Order]", _connectString);
        build = new SqlCommandBuilder(_adapterOrder);
        _adapterOrder.Fill(_ds, "Order");
        key[0] = _ds.Tables["Order"].Columns["Id"];
        _ds.Tables["Order"].PrimaryKey = key;

        _adapterProducts_in_Order = new SqlDataAdapter("Select * From Products_in_Order", _connectString);
        build = new SqlCommandBuilder(_adapterProducts_in_Order);
        _adapterProducts_in_Order.Fill(_ds, "Products_in_Order");
        //key[0] = _ds.Tables["Products_in_Order"].Columns["Id"];
        //_ds.Tables["Products_in_Order"].PrimaryKey = key;

        _adapterProduct = new SqlDataAdapter("Select * From Product", _connectString);
        build = new SqlCommandBuilder(_adapterProduct);
        _adapterProduct.Fill(_ds, "Product");
        key[0] = _ds.Tables["Product"].Columns["Id"];
        _ds.Tables["Product"].PrimaryKey = key;
      }
    }
  }
}
