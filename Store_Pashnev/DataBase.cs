using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  public class DataBase
  {
    // извлекаю строку подключения из файла web.config
    public string _connectString = ConfigurationManager.ConnectionStrings["Store"].ConnectionString;

    // создание объекта DataSet
    public DataSet _ds;// ссылка на нетипизированный набор DataSet

    public SqlDataAdapter _adapterClaim;
    public SqlDataAdapter _adapterClient;
    public SqlDataAdapter _adapterDepartment;
    public SqlDataAdapter _adapterEmployee;
    public SqlDataAdapter _adapterOrder;
    public SqlDataAdapter _adapterPosition;
    public SqlDataAdapter _adapterProduct;
    public SqlDataAdapter _adapterProducts_in_Order;
    public SqlDataAdapter _adapterProducer;
    public SqlDataAdapter _adapterUser;

    public Dictionary<string, bool> Rights;

    public DataBase()
    {
      _ds = new DataSet();// Создаем множественный набор данных 

      // заполняю множественный набор данных из БД Connection
      using (SqlConnection connect = new SqlConnection(_connectString))// создание объекта подключения
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
        //_adapterProducts_in_Order = new SqlDataAdapter("select OrderId, Title, Price, Products_in_Order.Quantity, PriceWithDiscount, [Sum] from Products_in_Order join Product on Products_in_Order.ProductId = Product.Id", _connectString);

        _adapterProducer = new SqlDataAdapter("Select * From Producer", _connectString);
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
        build = new SqlCommandBuilder(_adapterProducer);
        _adapterProducer.Fill(_ds, "Producer");
        build = new SqlCommandBuilder(_adapterUser);
        _adapterUser.Fill(_ds, "User");
      }
    }

    public DataBase(int userID)
    {

      _ds = new DataSet();// Создаем множественный набор данных 
      
      using (SqlConnection connect = new SqlConnection(_connectString))// создание объекта подключения
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

        Rights = new Dictionary<string,bool>();
        using (SqlDataReader sdr = command.ExecuteReader())
        {
          sdr.Read();
          for(int i=2;i<sdr.FieldCount;i++)
          {
            Rights.Add(sdr.GetName(i), Convert.ToInt32(sdr.GetValue(i))==1);
          }
        }

        //загружать, если нужен доступ к этим модулям - добавить поля в Position
        SqlCommandBuilder build;
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
          fields.Remove(fields.Length - 2, 1);

          _adapterClient = new SqlDataAdapter("Select "
            + fields
            +" From Client", _connectString);
          build = new SqlCommandBuilder(_adapterClient);
          _adapterClient.Fill(_ds, "Client");

        }

          _adapterClaim = new SqlDataAdapter("Select * From Claim", _connectString);
          build = new SqlCommandBuilder(_adapterClaim);
          _adapterClaim.Fill(_ds, "Claim");

          _adapterDepartment = new SqlDataAdapter("Select * From Department", _connectString);
          build = new SqlCommandBuilder(_adapterDepartment);
          _adapterDepartment.Fill(_ds, "Department");

          _adapterEmployee = new SqlDataAdapter("Select * From Employee", _connectString);
          build = new SqlCommandBuilder(_adapterEmployee);
          _adapterEmployee.Fill(_ds, "Employee");

          _adapterPosition = new SqlDataAdapter("Select * From Position", _connectString);
          build = new SqlCommandBuilder(_adapterPosition);
          _adapterPosition.Fill(_ds, "Position"); 

          _adapterUser = new SqlDataAdapter("Select * From [User]", _connectString);
          build = new SqlCommandBuilder(_adapterUser);
          _adapterUser.Fill(_ds, "User");

          _adapterOrder = new SqlDataAdapter("Select * From [Order]", _connectString);
          build = new SqlCommandBuilder(_adapterOrder);
          _adapterOrder.Fill(_ds, "Order");

          _adapterProducts_in_Order = new SqlDataAdapter("Select * From Products_in_Order", _connectString);
          build = new SqlCommandBuilder(_adapterProducts_in_Order);
          _adapterProducts_in_Order.Fill(_ds, "Products_in_Order");

          _adapterProduct = new SqlDataAdapter("Select * From Product", _connectString);
          build = new SqlCommandBuilder(_adapterProduct);
          _adapterProduct.Fill(_ds, "Product");

          _adapterProducer = new SqlDataAdapter("Select * From Produser", _connectString);
          build = new SqlCommandBuilder(_adapterProducer);
          _adapterProducer.Fill(_ds, "Produser");
      }
    }


  }
}
