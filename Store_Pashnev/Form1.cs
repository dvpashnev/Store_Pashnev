using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Store_Pashnev
{
  public partial class Form1 : Form
  {
    DataBase db;

    private BindingSource bsProducts;
    private BindingSource bsClients;

    private DataView dvProductsInOrder;

    private int widthBeforeResize;
    private int heightBeforeResize;

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      //for Products

      EnterForm ef = new EnterForm();
      ef.ShowDialog();

      if (EnterData.UserID == 0)
      {
        Close();
      }
      else
      {
        db = new DataBase(EnterData.UserID);

        if (!db.Rights["Products"])
        {
          tabControlMain.TabPages["Products"].Dispose();
        }
        if (!db.Rights["Order"])
        {
          tabControlMain.TabPages["Order"].Dispose();
        }
        if (!db.Rights["Clients"])
        {
          tabControlMain.TabPages["Clients"].Dispose();
        }
        if (!db.Rights["Claims"])
        {
          tabControlMain.TabPages["Claims"].Dispose();
        }
        if (!db.Rights["Reports"])
        {
          tabControlMain.TabPages["Reports"].Dispose();
        }
        if (!db.Rights["Management"])
        {
          tabControlMain.TabPages.RemoveByKey("Management");
        }

        bsProducts = new BindingSource { DataSource = db._ds.Tables["Product"] };
        dataGridViewProducts.DataSource = bsProducts;
        bindingNavigatorProducts.BindingSource = bsProducts;

        ProductsBinding();

        foreach (DataColumn col in db._ds.Tables["Product"].Columns)
        {
          string item = "";

          switch (col.ColumnName)
          {
            //case "Id":
            //  item = "Номер (Id)";
            //  break;
            case "Title":
              item = "Наименование";
              break;
            //case "Price":
            //  item = "Цена";
            //  break;
            //case "DepartmentId":
            //  item = "Номер отдела";
            //  break;
            //case "ProduserId":
            //  item = "Номер поставщика";
            //  break;
            //case "Quantity":
            //  item = "Количество";
            //  break;
            //case "CriticalQ":
            //  item = "Критическое количество";
            //  break;
          }

          if (item != "")
            comboBoxCriteriaFilterProduct.Items.Add(item);
        }

        // for Order

        using (SqlConnection connect = new SqlConnection(db._connectString))
        {
          SqlCommand command = new SqlCommand();
          connect.Open();
          command.Connection = connect;
          command.CommandText = String.Format("select ident_current('Order')");
          OrderCurrent.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
        }

        textBoxOrderId.Text = OrderCurrent.Id.ToString();

        //comboBoxEmpl.DataSource = db._ds.Tables["Employee"];
        //comboBoxEmpl.DisplayMember = "LastName";
        //comboBoxEmpl.ValueMember = "Id";

        string[] empls = (from r in db._ds.Tables["Employee"].AsEnumerable()
                          select (string)r["LastName"] + " " + r["FirstName"] + " " + r["MiddleName"]).ToArray();
        comboBoxEmpl.Items.Clear();
        comboBoxEmpl.Items.AddRange(empls);

        //for Clients

        bsClients = new BindingSource { DataSource = db._ds.Tables["Client"] };
        dataGridViewClients.DataSource = bsClients;
        bindingNavigatorClients.BindingSource = bsClients;

        if (!db.Rights["Client_LN"])
        {
          labelLN.Hide();
          textBoxLN.Hide();
        }
        if (!db.Rights["Client_FN"])
        {
          labelFN.Hide();
          textBoxFN.Hide();
        }
        if (!db.Rights["Client_MN"])
        {
          labelMN.Hide();
          textBoxMN.Hide();
        }
        if (!db.Rights["Client_Sex"])
        {
          labelSex.Hide();
          radioButtonClientMale.Hide();
          radioButtonClientFemale.Hide();
        }
        if (!db.Rights["Client_BD"])
        {
          labelBD.Hide();
          dateTimePickerClientBD.Hide();
        }
        if (!db.Rights["Client_Phone1"])
        {
          labelPhone1.Hide();
          textBoxPhone1.Hide();
        }
        if (!db.Rights["Client_Phone2"])
        {
          labelPhone2.Hide();
          textBoxPhone2.Hide();
        }
        if (!db.Rights["Client_Phone3"])
        {
          labelPhone3.Hide();
          textBoxPhone3.Hide();
        }
        if (!db.Rights["Client_Adress"])
        {
          labelAdress.Hide();
          textBoxAdress.Hide();
        }
        if (!db.Rights["Client_SO"])
        {
          labelSO.Hide();
          textBoxSO.Hide();
        }
        if (!db.Rights["Client_Discount"])
        {
          labelDiscount.Hide();
          textBoxDiscount.Hide();
        }

        ClientsBinding();

        foreach (DataColumn col in db._ds.Tables["Client"].Columns)
        {
          string item = "";
          switch (col.ColumnName)
          {
            case "LastName":
              item = "Фамилия";
              break;
            case "FirstName":
              item = "Имя";
              break;
            case "MiddleName":
              item = "Отчество";
              break;
            case "Sex":
              item = "Пол";
              break;
            case "Phone1":
              item = "Телефон";
              break;
            case "Adress":
              item = "Адрес";
              break;
          }
          if (item != "")
            cbCritToFindClient.Items.Add(item);
        }

        //for Management

        dataGridViewRoles.DataSource = db._ds.Tables["Position"];

        dataGridViewUsers.DataSource = db._ds.Tables["User"];
      }
    }

    void ClientsBinding()
    {
      if (textBoxClientID.DataBindings.Count != 0)
      {
        textBoxClientID.DataBindings.Clear();
        textBoxLN.DataBindings.Clear();
        textBoxFN.DataBindings.Clear();
        textBoxMN.DataBindings.Clear();
        dateTimePickerClientBD.DataBindings.Clear();
        textBoxPhone1.DataBindings.Clear();
        textBoxPhone2.DataBindings.Clear();
        textBoxPhone3.DataBindings.Clear();
        textBoxAdress.DataBindings.Clear();
        textBoxSO.DataBindings.Clear();
        textBoxDiscount.DataBindings.Clear();
      }

      textBoxClientID.DataBindings.Add("Text", bsClients, "Id");

      if (db.Rights["Client_LN"])
      {
        textBoxLN.DataBindings.Add("Text", bsClients, "LastName");
      }
      if (db.Rights["Client_FN"])
      {
        textBoxFN.DataBindings.Add("Text", bsClients, "FirstName");
      }
      if (db.Rights["Client_MN"])
      {
        textBoxMN.DataBindings.Add("Text", bsClients, "MiddleName");
      }
      if (db.Rights["Client_BD"])
      {
        dateTimePickerClientBD.DataBindings.Add("Value", bsClients, "BirthDay");
      }
      if (db.Rights["Client_Phone1"])
      {
        textBoxPhone1.DataBindings.Add("Text", bsClients, "Phone1");
      }
      if (db.Rights["Client_Phone2"])
      {
        textBoxPhone2.DataBindings.Add("Text", bsClients, "Phone2");
      }
      if (db.Rights["Client_Phone3"])
      {
        textBoxPhone3.DataBindings.Add("Text", bsClients, "Phone3");
      }
      if (db.Rights["Client_Adress"])
      {
        textBoxAdress.DataBindings.Add("Text", bsClients, "Adress");
      }
      if (db.Rights["Client_SO"])
      {
        textBoxSO.DataBindings.Add("Text", bsClients, "SumOrders");
      }
      if (db.Rights["Client_Discount"])
      {
        textBoxDiscount.DataBindings.Add("Text", bsClients, "Discount");
      }

      bsClients.PositionChanged += bsClients_PositionChanged;
    }

    void ProductsBinding()
    {
      if (textBoxID.DataBindings.Count != 0)
      {
        textBoxID.DataBindings.Clear();
        textBoxTitle.DataBindings.Clear();
        textBoxPrice.DataBindings.Clear();
        textBoxDepartment.DataBindings.Clear();
        textBoxProducer.DataBindings.Clear();
        textBoxQuantity.DataBindings.Clear();
      }

      textBoxID.DataBindings.Add("Text", bsProducts, "Id");
      textBoxTitle.DataBindings.Add("Text", bsProducts, "Title");
      textBoxPrice.DataBindings.Add("Text", bsProducts, "Price");
      textBoxDepartment.DataBindings.Add("Text", bsProducts, "DepartmentId");
      textBoxProducer.DataBindings.Add("Text", bsProducts, "ProduserId");
      textBoxQuantity.DataBindings.Add("Text", bsProducts, "Quantity");

      bsProducts.PositionChanged += bsProducts_PositionChanged;
    }

    void bsProducts_PositionChanged(object sender, EventArgs e)
    {

    }

    void bsClients_PositionChanged(object sender, EventArgs e)
    {
      if (db.Rights["Client_Sex"])
      {
        string sex = db._ds.Tables["Client"].Rows[bsClients.Position]["Sex"].ToString();

        radioButtonClientMale.Checked = sex == "мужской";
        radioButtonClientFemale.Checked = sex == "женский";
      }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (EnterData.UserID == 0)
      {
        return;
      }

      if (MessageBox.Show(@"Подтверждаете выход?",
        @"Подтверждение",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning) == DialogResult.No)
      {
        e.Cancel = true;
      }
    }

    private void button13_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Position')");
        Position.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      PositionForm pf = new PositionForm(Position.Id);
      pf.ShowDialog();

      if (Position.Id != 0)
      {
        DataRow newRow = db._ds.Tables["Position"].NewRow();
        newRow["Id"] = Position.Id;
        newRow["Title"] = Position.Title;
        newRow["Products"] = Position.Products;
        newRow["Order"] = Position.Order;
        newRow["Clients"] = Position.Clients;
        newRow["Claims"] = Position.Claims;
        newRow["Reports"] = Position.Reports;
        newRow["Management"] = Position.Management;
        newRow["Client_LN"] = Position.Client_LN;
        newRow["Client_FN"] = Position.Client_FN;
        newRow["Client_MN"] = Position.Client_MN;
        newRow["Client_Sex"] = Position.Client_Sex;
        newRow["Client_BD"] = Position.Client_BD;
        newRow["Client_Phone1"] = Position.Client_Phone1;
        newRow["Client_Phone2"] = Position.Client_Phone2;
        newRow["Client_Phone3"] = Position.Client_Phone3;
        newRow["Client_Adress"] = Position.Client_Adress;
        newRow["Client_SO"] = Position.Client_SO;
        newRow["Client_Discount"] = Position.Client_Discount;

        db._ds.Tables["Position"].Rows.Add(newRow);

        db._adapterPosition.Update(db._ds.Tables["Position"]);

        Position.Clear();
      }
    }

    private void button22_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('User')");
        User.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      UserForm uf = new UserForm(User.Id, db._ds.Tables["Position"], db._ds.Tables["User"]);
      uf.ShowDialog();

      if (User.Id != 0)
      {
        DataRow newRow = db._ds.Tables["User"].NewRow();
        newRow["Id"] = User.Id;
        newRow["PositionId"] = User.PositionId;
        newRow["Nick"] = User.Nick;
        newRow["Password"] = User.Password;
        db._ds.Tables["User"].Rows.Add(newRow);

        db._adapterUser.Update(db._ds.Tables["User"]);

        User.Clear();
      }

    }

    private void tabControlMain_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (e.TabPage.Name == "Management")
      {
        treeViewUpdate();
      }

      if (e.TabPage.Name == "Order")
      {
      }

      if (e.TabPage.Name == "Clients")
      {
        bsClients_PositionChanged(bindingNavigatorClients, new EventArgs());
      }
    }

    private void treeViewUpdate()
    {
      treeViewDepEmpl.BeginUpdate();
      treeViewDepEmpl.Nodes.Clear();
      foreach (DataRow row in db._ds.Tables["Department"].Rows)
      {
        treeViewDepEmpl.Nodes.Add(new TreeNode(row["Name"].ToString()));

        var emplRows = from r in db._ds.Tables["Employee"].AsEnumerable() where (int)r["DepartmentId"] == (int)row["Id"] select r;

        foreach (DataRow empl in emplRows)
        {
          string nextNodeName = empl["LastName"] + " " + empl["FirstName"] + " " + empl["MiddleName"];
          TreeNode tn = new TreeNode(nextNodeName);
          TreeNode tn0 = treeViewDepEmpl.Nodes[db._ds.Tables["Department"].Rows.IndexOf(row)];
          tn0.Nodes.Add(tn);
        }
      }
      treeViewDepEmpl.EndUpdate();
    }

    private void newDepToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Department')");
        Department.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      DepartmentForm df = new DepartmentForm(Department.Id);
      df.ShowDialog();

      if (Department.Id != 0)
      {
        DataRow newRow = db._ds.Tables["Department"].NewRow();
        newRow["Id"] = Department.Id;
        newRow["Name"] = Department.Name;
        db._ds.Tables["Department"].Rows.Add(newRow);

        db._adapterDepartment.Update(db._ds.Tables["Department"]);

        treeViewDepEmpl.BeginUpdate();
        treeViewDepEmpl.Nodes.Add(new TreeNode(Department.Name));
        treeViewDepEmpl.EndUpdate();

        Department.Clear();
      }
    }

    private void newEmpToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      int depNode;
      if (treeViewDepEmpl.Nodes.Count != 0)
      {
        if (treeViewDepEmpl.SelectedNode != null)
        {
          depNode = treeViewDepEmpl.SelectedNode.Index;
        }
        else
        {
          MessageBox.Show(@"Нужно выделить левой кнопкой отдел, в котором будете создавать работника!");
          treeViewDepEmpl.Focus();
          return;
        }
      }
      else
      {
        depNode = 0;
      }

      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Employee')");
        Employee.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      EmployeeForm ef = new EmployeeForm(
        Employee.Id,
        depNode,
        db._ds.Tables["Department"],
        db._ds.Tables["Position"],
        db._ds.Tables["User"],
        db._connectString
        );
      ef.ShowDialog();

      if (Employee.Id != 0)
      {
        DataRow newRowUser = db._ds.Tables["User"].NewRow();
        newRowUser["Id"] = User.Id;
        newRowUser["PositionId"] = User.PositionId;
        newRowUser["Nick"] = User.Nick;
        newRowUser["Password"] = User.Password;
        db._ds.Tables["User"].Rows.Add(newRowUser);

        db._adapterUser.Update(db._ds.Tables["User"]);

        User.Clear();

        DataRow newRow = db._ds.Tables["Employee"].NewRow();
        newRow["Id"] = Employee.Id;
        newRow["LastName"] = Employee.LastName;
        newRow["FirstName"] = Employee.FirstName;
        newRow["MiddleName"] = Employee.MiddleName;
        newRow["DepartmentId"] = Employee.DepartmentId;
        newRow["UserId"] = Employee.UserId;
        newRow["AcceptanceDate"] = Employee.AcceptanceDate;
        newRow["SumOrders"] = Employee.SumOrders;
        newRow["Bonuses"] = Employee.Bonuses;

        db._ds.Tables["Employee"].Rows.Add(newRow);

        db._adapterEmployee.Update(db._ds.Tables["Employee"]);

        treeViewUpdate();

        Employee.Clear();
      }
    }

    private void dataGridViewProducts_DoubleClick(object sender, EventArgs e)
    {
      int productID = Convert.ToInt32(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex].ItemArray[0]);

      Product.SetValues(productID,
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Title"].ToString(),
        Convert.ToDouble(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Price"]),
        Convert.ToInt32(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["DepartmentId"]),
        Convert.ToInt32(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["ProduserId"]),
        Convert.ToInt32(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Quantity"]),
        Convert.ToBoolean(db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["CriticalQ"]));

      ProductForm pf = new ProductForm(productID, db._ds.Tables["Department"], db._ds.Tables["Produser"]);
      pf.ShowDialog();

      if (Product.Id != 0)
      {
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Title"] = Product.Title;
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Price"] = Product.Price;
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["DepartmentId"] = Product.DepartmentId;
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["ProduserId"] = Product.ProduserId;
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["Quantity"] = Product.Quantity;
        db._ds.Tables["Product"].Rows[dataGridViewProducts.CurrentCell.RowIndex]["CriticalQ"] = Product.CriticalQ;

        db._adapterProduct.Update(db._ds.Tables["Product"]);

        Product.Clear();
      }
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      DataView dvFilter = new DataView(db._ds.Tables["Product"]);
      dvFilter.RowFilter = db._ds.Tables["Product"].Columns[dataGridViewProducts.CurrentCell.ColumnIndex].ColumnName +
        " = '" + dataGridViewProducts.CurrentCell.Value + "'";
      dataGridViewProducts.DataSource = dvFilter;

    }

    private void resetFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dataGridViewProducts.DataSource = db._ds.Tables["Product"];
    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      DataView dvFilter = new DataView(db._ds.Tables["Client"]);
      dvFilter.RowFilter = db._ds.Tables["Client"].Columns[dataGridViewClients.CurrentCell.ColumnIndex].ColumnName +
        " = '" + dataGridViewClients.CurrentCell.Value + "'";
      dataGridViewClients.DataSource = dvFilter;
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      dataGridViewClients.DataSource = db._ds.Tables["Client"];
    }

    private void dataGridViewClients_DoubleClick(object sender, EventArgs e)
    {
      int clientID = Convert.ToInt32(db._ds.Tables["Client"].Rows[dataGridViewProducts.CurrentCell.RowIndex].ItemArray[0]);

      Client.SetValues(clientID,
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["LastName"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["FirstName"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["MiddleName"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Sex"].ToString(),
        Convert.ToDateTime(db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["BirthDay"]),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone1"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone2"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone3"].ToString(),
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Adress"].ToString(),
        Convert.ToDouble(db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["SumOrders"]),
        Convert.ToInt32(db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Discount"])
        );

      ClientForm pf = new ClientForm();
      pf.ShowDialog();

      if (Client.Id != 0)
      {
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["LastName"] = Client.LastName;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["FirstName"] = Client.FirstName;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["MiddleName"] = Client.MiddleName;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Sex"] = Client.Sex;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["BirthDay"] = Client.BirthDay;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone1"] = Client.Phone1;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone2"] = Client.Phone2;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone3"] = Client.Phone3;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Adress"] = Client.Adress;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["SumOrders"] = Client.SumOrders;
        db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Discount"] = Client.Discount;

        db._adapterClient.Update(db._ds.Tables["Client"]);

        Client.Clear();
      }
    }

    private void button15_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Client')");
        Client.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      ClientForm cf = new ClientForm();
      cf.ShowDialog();

      if (Client.Id != 0)
      {
        DataRow newRow = db._ds.Tables["Client"].NewRow();
        newRow["Id"] = Client.Id;
        newRow["LastName"] = Client.LastName;
        newRow["FirstName"] = Client.FirstName;
        newRow["MiddleName"] = Client.MiddleName;
        newRow["Sex"] = Client.Sex;
        newRow["BirthDay"] = Client.BirthDay;
        newRow["Phone1"] = Client.Phone1;
        newRow["Phone2"] = Client.Phone2;
        newRow["Phone3"] = Client.Phone3;
        newRow["Adress"] = Client.Adress;
        newRow["SumOrders"] = Client.SumOrders;
        newRow["Discount"] = Client.Discount;

        db._ds.Tables["Client"].Rows.Add(newRow);

        db._adapterClient.Update(db._ds.Tables["Client"]);

        Client.Clear();
      }
    }

    private void newProductToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Product')");
        Product.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      ProductForm pf = new ProductForm(Product.Id, db._ds.Tables["Department"], db._ds.Tables["Produser"]);
      pf.ShowDialog();

      if (Product.Id != 0)
      {
        DataRow newRow = db._ds.Tables["Product"].NewRow();
        newRow["Id"] = Product.Id;
        newRow["Title"] = Product.Title;
        newRow["Price"] = Product.Price;
        newRow["DepartmentId"] = Product.DepartmentId;
        newRow["ProduserId"] = Product.ProduserId;
        newRow["Quantity"] = Product.Quantity;
        newRow["CriticalQ"] = Product.CriticalQ;

        db._ds.Tables["Product"].Rows.Add(newRow);

        db._adapterProduct.Update(db._ds.Tables["Product"]);

        Product.Clear();
      }
    }

    private void cbCritToFindClient_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox cb = sender as ComboBox;
      tbLiterasToFindClient.Enabled = (cb.SelectedIndex != -1);
    }

    private void tbLiterasToFindClient_TextChanged(object sender, EventArgs e)
    {
      string fieldName = "";
      switch (cbCritToFindClient.SelectedItem.ToString())
      {
        case "Фамилия":
          fieldName = "LastName";
          break;
        case "Имя":
          fieldName = "FirstName";
          break;
        case "Отчество":
          fieldName = "MiddleName";
          break;
        case "Пол":
          fieldName = "Sex";
          break;
        case "Телефон":
          fieldName = "Phone1";
          break;
        case "Адрес":
          fieldName = "Adress";
          break;
      }
      DataView dvFilter = new DataView(db._ds.Tables["Client"]);

      if (fieldName == "Телефон")
      {
        dvFilter.RowFilter = "Phone1 like '%" + tbLiterasToFindClient.Text
    + "%' or Phone2 like '%" + tbLiterasToFindClient.Text
    + "%' or Phone3 like '%" + tbLiterasToFindClient.Text + "%'";
      }
      else
      {
        dvFilter.RowFilter = fieldName + " like '%" + tbLiterasToFindClient.Text + "%'";
      }

      bsClients = new BindingSource { DataSource = dvFilter };
      dataGridViewClients.DataSource = bsClients;
      bindingNavigatorClients.BindingSource = bsClients;

      ClientsBinding();
    }

    private void buttonClearCrit_Click(object sender, EventArgs e)
    {
      tbLiterasToFindClient.Text = "";
      cbCritToFindClient.SelectedIndex = -1;
      bsClients = new BindingSource { DataSource = db._ds.Tables["Client"] };
      dataGridViewClients.DataSource = bsClients;
      bindingNavigatorClients.BindingSource = bsClients;

      ClientsBinding();
    }

    private void comboBoxCriteriaFilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
      textBoxStringToFind.Enabled = ((sender as ComboBox).SelectedIndex != -1);
    }

    private void textBoxStringToFind_TextChanged(object sender, EventArgs e)
    {
      string fieldName = "";
      switch (comboBoxCriteriaFilterProduct.SelectedItem.ToString())
      {
        case "Номер (Id)":
          fieldName = "Id";
          break;
        case "Наименование":
          fieldName = "Title";
          break;
        case "Цена":
          fieldName = "Price";
          break;
        case "Номер отдела":
          fieldName = "DepartmentId";
          break;
        case "Номер поставщика":
          fieldName = "ProduserId";
          break;
        case "Количество":
          fieldName = "Quantity";
          break;
        case "Критическое количество":
          fieldName = "CriticalQ";
          break;
      }

      DataView dvFilter = new DataView(db._ds.Tables["Product"]);


      dvFilter.RowFilter = fieldName + " like '%" + textBoxStringToFind.Text + "%'";

      bsProducts = new BindingSource { DataSource = dvFilter };
      dataGridViewProducts.DataSource = bsProducts;
      bindingNavigatorProducts.BindingSource = bsProducts;

      ProductsBinding();

    }

    private void button2_Click(object sender, EventArgs e)
    {
      textBoxStringToFind.Text = "";
      comboBoxCriteriaFilterProduct.SelectedIndex = -1;

      bsProducts = new BindingSource { DataSource = db._ds.Tables["Product"] };
      dataGridViewClients.DataSource = bsProducts;
      bindingNavigatorClients.BindingSource = bsProducts;

      ProductsBinding();
    }

    private void button23_Click(object sender, EventArgs e)
    {
      ClientFindForm cff = new ClientFindForm(db._ds.Tables["Client"], db.Rights);
      cff.ShowDialog();

      if (Client.Id != 0)
      {
        foreach (DataGridViewRow row in dataGridViewClients.Rows)
        {
          if (Convert.ToInt32(row.Cells[0].Value) == Client.Id)
          {
            bsClients.Position = dataGridViewClients.Rows.IndexOf(row);
            break;
          }
        }
        Client.SetId(0);
      }
    }

    private void button20_Click(object sender, EventArgs e)
    {
      ClientFindForm cff = new ClientFindForm(db._ds.Tables["Client"], db.Rights);
      cff.ShowDialog();

      if (Client.Id != 0)
      {
        textBoxClient.Text = Client.Id.ToString();
        Client.SetId(0);
      }
    }

    private void Form1_ResizeBegin(object sender, EventArgs e)
    {
      widthBeforeResize = Size.Width;
      heightBeforeResize = Size.Height;
    }

    private void Form1_ResizeEnd(object sender, EventArgs e)
    {
      //for Products
      dataGridViewProducts.Size = new Size(
        dataGridViewProducts.Size.Width + (Size.Width - widthBeforeResize),
        dataGridViewProducts.Size.Height + (Size.Height - heightBeforeResize)
        );

      button19.Location = new Point(button19.Location.X, button19.Location.Y + (Size.Height - heightBeforeResize));
      button8.Location = new Point(button8.Location.X, button8.Location.Y + (Size.Height - heightBeforeResize));
      groupBoxFind.Location = new Point(groupBoxFind.Location.X, groupBoxFind.Location.Y + (Size.Height - heightBeforeResize));

      //for Clients
      dataGridViewClients.Size = new Size(
        dataGridViewClients.Size.Width + (Size.Width - widthBeforeResize),
        dataGridViewClients.Size.Height + (Size.Height - heightBeforeResize)
        );

      button15.Location = new Point(button15.Location.X, button15.Location.Y + (Size.Height - heightBeforeResize));
      button21.Location = new Point(button21.Location.X, button21.Location.Y + (Size.Height - heightBeforeResize));
      button23.Location = new Point(button23.Location.X, button23.Location.Y + (Size.Height - heightBeforeResize));
      groupBox14.Location = new Point(groupBox14.Location.X, groupBox14.Location.Y + (Size.Height - heightBeforeResize));
    }

    private void buttonBindToOrder_Click(object sender, EventArgs e)
    {
      textBoxClient.Text = textBoxClientID.Text;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (dataGridViewProducts.SelectedRows.Count == 0)
      {
        MessageBox.Show(@"Нажмите в голове строки, чтобы выделить товар");
      }
      else
      {
        addProductsInOrder();
        tabControlMain.SelectTab("Order");
      }
    }

    void addProductsInOrder()
    {
      int alreadyAdded = 0;
      int discount = textBoxClient.Text == "" ? 0 :
        (from row in db._ds.Tables["Client"].AsEnumerable()
         where row.Field<int>("Id") == Convert.ToInt32(textBoxClient.Text)
         select row.Field<int>("Discount")).First();
      foreach (DataGridViewRow r in dataGridViewProducts.SelectedRows)
      {
        if (!OrderCurrent.Products.ContainsKey(Convert.ToInt32(r.Cells["Id"].Value)))
        {
          OrderCurrent.Products.Add(Convert.ToInt32(r.Cells["Id"].Value), 1);

          int index = dataGridViewProdsInOrder.Rows.Add();
          dataGridViewProdsInOrder.Rows[index].Cells["Id"].Value = r.Cells["Id"].Value;
          dataGridViewProdsInOrder.Rows[index].Cells["Title"].Value = r.Cells["Title"].Value;
          dataGridViewProdsInOrder.Rows[index].Cells["Price"].Value = r.Cells["Price"].Value;
          dataGridViewProdsInOrder.Rows[index].Cells["Quantity"].Value = 1;
          dataGridViewProdsInOrder.Rows[index].Cells["PriceWithDiscount"].Value = Convert.ToDouble(dataGridViewProdsInOrder.Rows[index].Cells["Price"].Value) -
              (Convert.ToDouble(dataGridViewProdsInOrder.Rows[index].Cells["Price"].Value) / 100 * discount);
          dataGridViewProdsInOrder.Rows[index].Cells["Sum"].Value = Convert.ToDouble(r.Cells["Price"].Value) * Convert.ToInt32(dataGridViewProdsInOrder.Rows[index].Cells["Quantity"].Value);
        }
        else
        {
          alreadyAdded++;
        }
      }

      sumRefresh();

      if (alreadyAdded > 0)
      {
        MessageBox.Show(@"Некоторые товары уже были добавлены. Изменяйте их количество, а не добавляйте заново");
      }
    }

    void discountRefresh()
    {
      int discount = textBoxClient.Text == "" ? 0 : (from r in db._ds.Tables["Client"].AsEnumerable()
                                                     where r.Field<int>("Id") == Convert.ToInt32(textBoxClient.Text)
                                                     select r.Field<int>("Discount")).First();
      for (int i = 0; i < dataGridViewProdsInOrder.Rows.Count; i++)
      {
        dataGridViewProdsInOrder.Rows[i].Cells["PriceWithDiscount"].Value =
          Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Price"].Value) -
          (Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Price"].Value) / 100 * discount);
      }
    }

    void sumRefresh()
    {
      double sum = 0.0;
      for (int i = 0; i < dataGridViewProdsInOrder.Rows.Count; i++)
      {
        dataGridViewProdsInOrder.Rows[i].Cells["Sum"].Value =
          Convert.ToInt32(dataGridViewProdsInOrder.Rows[i].Cells["Quantity"].Value) *
          Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["PriceWithDiscount"].Value);
        sum += Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Sum"].Value);
      }
      textBoxAllSum.Text = sum.ToString();
    }

    private void checkBoxDefaultAdress_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxDefaultAdress.Checked)
      {
        if (textBoxClient.Text == "")
        {
          checkBoxDefaultAdress.Checked = false;
          MessageBox.Show(@"Сначала выберите клиента!");
          button20.Select();
        }
        else
        {
          string adress = (from a in db._ds.Tables["Client"].AsEnumerable()
                           where a.Field<int>("Id") == Convert.ToInt32(textBoxClient.Text)
                           select a["Adress"]).First().ToString();
          textBoxDeliverAdress.Text = adress;
        }
      }
      else
      {
        textBoxDeliverAdress.Text = "";
      }
    }

    private void numericUpDownProdQuantity_ValueChanged(object sender, EventArgs e)
    {
      if (dataGridViewProdsInOrder.CurrentRow != null)
      {
        dataGridViewProdsInOrder.CurrentRow.Cells["Quantity"].Value = numericUpDownProdQuantity.Value;
        sumRefresh();
      }
      else
      {
        MessageBox.Show(@"Выделите строку перед изменением количества");
      }
    }

    private void buttonAddProductToOrder_Click(object sender, EventArgs e)
    {
      tabControlMain.SelectTab("Products");
    }

    private void buttonConfirmOrder_Click(object sender, EventArgs e)
    {
      int orderN;
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Order')");
        orderN = Convert.ToInt32(command.ExecuteScalar()) + 1;
      }

      if (OrderCurrent.Id == orderN)
      {
        DataRow newRow = db._ds.Tables["Order"].NewRow();
        newRow["Id"] = textBoxOrderId.Text;
        newRow["Category"] = comboBoxOrderCat.SelectedItem.ToString();

        int eid = (from r in db._ds.Tables["Employee"].AsEnumerable()
                   where (r.Field<string>("LastName") + " " + r.Field<string>("FirstName") + " " + r.Field<string>("MiddleName")) == comboBoxEmpl.SelectedItem.ToString()
                   select r.Field<int>("Id")).First();

        newRow["EmployeeId"] = eid;

        newRow["ClientId"] = textBoxClient.Text;
        newRow["OrderDate"] = dateTimePickerOrderDate.Value.ToShortDateString();
        newRow["DeliveryDate"] = dateTimePickerOrderDateDeliver.Value.ToShortDateString();
        newRow["DeliveryAdress"] = textBoxDeliverAdress.Text;
        newRow["Sum"] = textBoxAllSum.Text;

        db._ds.Tables["Order"].Rows.Add(newRow);

        db._adapterOrder.Update(db._ds.Tables["Order"]);

        int nextP_in_O;
        using (SqlConnection connect = new SqlConnection(db._connectString))
        {
          SqlCommand command = new SqlCommand();
          connect.Open();
          command.Connection = connect;
          command.CommandText = String.Format("select ident_current('Order')");
          nextP_in_O = Convert.ToInt32(command.ExecuteScalar()) + 1;
        }

        foreach (DataGridViewRow row in dataGridViewProdsInOrder.Rows)
        {
          DataRow newProdRow = db._ds.Tables["Products_in_Order"].NewRow();
          newProdRow["Id"] = nextP_in_O++;
          newProdRow["OrderId"] = textBoxOrderId.Text;
          newProdRow["ProductId"] = row.Cells["Id"].Value;
          newProdRow["Quantity"] = row.Cells["Quantity"].Value;
          newProdRow["PriceWithDiscount"] = row.Cells["PriceWithDiscount"].Value;
          newProdRow["Sum"] = row.Cells["Sum"].Value;

          db._ds.Tables["Products_in_Order"].Rows.Add(newProdRow);

          db._adapterProducts_in_Order.Update(db._ds.Tables["Products_in_Order"]);
        }

        for (int i = 0; i < db._ds.Tables["Employee"].Rows.Count; i++)
        {
          if (Convert.ToInt32(db._ds.Tables["Employee"].Rows[i]["Id"]) == eid)
          {
            double SO = Convert.ToDouble(db._ds.Tables["Employee"].Rows[i]["SumOrders"]);
            SO += Convert.ToDouble(textBoxAllSum.Text);
            db._ds.Tables["Employee"].Rows[i]["SumOrders"] = SO;

            db._adapterEmployee.Update(db._ds.Tables["Employee"]);

            break;
          }
        }

        clearOrder();
      }
      else
      {
        DataRow[] rows = (from row in db._ds.Tables["Products_in_Order"].AsEnumerable()
                         where row.Field<int>("OrderId") == OrderCurrent.Id
                         select row).ToArray();

        foreach (DataRow row in rows)
        {
          row.Delete();
        }

        db._adapterProducts_in_Order.Update(db._ds.Tables["Products_in_Order"]);

        for (int i = 0; i < db._ds.Tables["Order"].Rows.Count; i++)
        {
          if (Convert.ToInt32(db._ds.Tables["Order"].Rows[i]["Id"]) == OrderCurrent.Id)
          {
            db._ds.Tables["Order"].Rows[i]["Category"] = comboBoxOrderCat.SelectedItem.ToString();

            int eid = (from r in db._ds.Tables["Employee"].AsEnumerable()
                       where (r.Field<string>("LastName") + " " + r.Field<string>("FirstName") + " " + r.Field<string>("MiddleName")) == comboBoxEmpl.SelectedItem.ToString()
                       select r.Field<int>("Id")).First();

            db._ds.Tables["Order"].Rows[i]["EmployeeId"] = eid;

            db._ds.Tables["Order"].Rows[i]["ClientId"] = textBoxClient.Text;
            db._ds.Tables["Order"].Rows[i]["OrderDate"] = dateTimePickerOrderDate.Value.ToShortDateString();
            db._ds.Tables["Order"].Rows[i]["DeliveryDate"] = dateTimePickerOrderDateDeliver.Value.ToShortDateString();
            db._ds.Tables["Order"].Rows[i]["DeliveryAdress"] = textBoxDeliverAdress.Text;
            db._ds.Tables["Order"].Rows[i]["Sum"] = textBoxAllSum.Text;

            db._adapterProducts_in_Order.Update(db._ds.Tables["Products_in_Order"]);

            int nextP_in_O;
            using (SqlConnection connect = new SqlConnection(db._connectString))
            {
              SqlCommand command = new SqlCommand();
              connect.Open();
              command.Connection = connect;
              command.CommandText = String.Format("select ident_current('Order')");
              nextP_in_O = Convert.ToInt32(command.ExecuteScalar()) + 1;
            }

            foreach (DataGridViewRow row in dataGridViewProdsInOrder.Rows)
            {
              DataRow newProdRow = db._ds.Tables["Products_in_Order"].NewRow();
              newProdRow["Id"] = nextP_in_O++;
              newProdRow["OrderId"] = textBoxOrderId.Text;
              newProdRow["ProductId"] = row.Cells["Id"].Value;
              newProdRow["Quantity"] = row.Cells["Quantity"].Value;
              newProdRow["PriceWithDiscount"] = row.Cells["PriceWithDiscount"].Value;
              newProdRow["Sum"] = row.Cells["Sum"].Value;

              db._ds.Tables["Products_in_Order"].Rows.Add(newProdRow);

              db._adapterProducts_in_Order.Update(db._ds.Tables["Products_in_Order"]);
            }

            for (int j = 0; j < db._ds.Tables["Employee"].Rows.Count; j++)
            {
              if (Convert.ToInt32(db._ds.Tables["Employee"].Rows[j]["Id"]) == eid)
              {
                double SO = Convert.ToDouble(db._ds.Tables["Employee"].Rows[j]["SumOrders"]);
                SO += Convert.ToDouble(textBoxAllSum.Text);
                db._ds.Tables["Employee"].Rows[j]["SumOrders"] = SO;

                db._adapterEmployee.Update(db._ds.Tables["Employee"]);

                break;
              }
            }

            clearOrder();
            break;
          }
        }
      }
    }

    private void textBoxClient_TextChanged(object sender, EventArgs e)
    {
      discountRefresh();
      sumRefresh();
    }

    private void dataGridViewProdsInOrder_SelectionChanged(object sender, EventArgs e)
    {
      if (dataGridViewProdsInOrder.CurrentRow != null)
      {
        if (dataGridViewProdsInOrder.CurrentRow.Cells["Quantity"].Value != null)
        {
          numericUpDownProdQuantity.Value = Convert.ToInt32(dataGridViewProdsInOrder.CurrentRow.Cells["Quantity"].Value);
        }
      }
    }

    private void buttonClearOrder_Click(object sender, EventArgs e)
    {
      DialogResult dr = MessageBox.Show(@"Хотите ли Вы сохранить текущий заказ?",
        @"Подтверждение",
        MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Warning);
      if (dr == DialogResult.No)
      {
        clearOrder();
      }
      else if (dr == DialogResult.Yes)
      {
        buttonConfirmOrder_Click(buttonConfirmOrder, new EventArgs());
      }
    }

    void clearOrder()
    {
      using (SqlConnection connect = new SqlConnection(db._connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('Order')");
        OrderCurrent.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      textBoxOrderId.Text = OrderCurrent.Id.ToString();

      comboBoxOrderCat.SelectedIndex = -1;
      comboBoxOrderCat.Text = @"Выберите";

      comboBoxEmpl.SelectedIndex = -1;
      comboBoxEmpl.Text = @"Выберите";

      dateTimePickerOrderDate.Value = DateTime.Today;
      textBoxClient.Text = "";

      dateTimePickerOrderDateDeliver.Value = DateTime.Today;

      checkBoxDefaultAdress.Checked = false;

      textBoxDeliverAdress.Text = "";
      dataGridViewProdsInOrder.Rows.Clear();

      textBoxAllSum.Text = "";
    }

    private void buttonDelProdFromOrder_Click(object sender, EventArgs e)
    {
      if (dataGridViewProdsInOrder.CurrentRow != null)
      {
        OrderCurrent.Products.Remove(Convert.ToInt32(dataGridViewProdsInOrder.Rows[dataGridViewProdsInOrder.CurrentRow.Index].Cells["Id"].Value));
        dataGridViewProdsInOrder.Rows.RemoveAt(dataGridViewProdsInOrder.CurrentRow.Index);
      }
      else
      {
        MessageBox.Show(@"Не выделена ни одна строка");
      }
    }

    private void button24_Click(object sender, EventArgs e)
    {
      dataGridViewProducts.SelectAll();
      addProductsInOrder();
      dataGridViewProducts.ClearSelection();
      tabControlMain.SelectTab("Order");
    }

    private void button17_Click(object sender, EventArgs e)
    {
      if (dataGridViewRoles.CurrentRow != null)
      {
        Position.SetValues(
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Id"].Value),
          dataGridViewRoles.CurrentRow.Cells["Title"].Value.ToString(),
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Products"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Order"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Clients"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Claims"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Reports"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Management"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_LN"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_FN"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_MN"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Sex"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_BD"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Phone1"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Phone2"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Phone3"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Adress"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_SO"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Discount"].Value) == 1
          );

        PositionForm pf = new PositionForm(Position.Id);
        pf.ShowDialog();

        if (Position.Id != 0)
        {
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Id"] = Position.Id;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Title"] = Position.Title;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Products"] = Position.Products;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Order"] = Position.Order;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Clients"] = Position.Clients;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Claims"] = Position.Claims;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Reports"] = Position.Reports;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Management"] = Position.Management;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_LN"] = Position.Client_LN;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_FN"] = Position.Client_FN;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_MN"] = Position.Client_MN;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Sex"] = Position.Client_Sex;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_BD"] = Position.Client_BD;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Phone1"] = Position.Client_Phone1;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Phone2"] = Position.Client_Phone2;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Phone3"] = Position.Client_Phone3;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Adress"] = Position.Client_Adress;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_SO"] = Position.Client_SO;
          db._ds.Tables["Position"].Rows[dataGridViewRoles.CurrentRow.Index]["Client_Discount"] = Position.Client_Discount;

          db._adapterPosition.Update(db._ds.Tables["Position"]);

          Position.Clear();
        }
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (dataGridViewRoles.CurrentRow != null)
      {
        if (MessageBox.Show(@"Вы уверены?", @"Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
          db._ds.Tables["Position"].Rows.RemoveAt(dataGridViewRoles.CurrentRow.Index);
        }
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (dataGridViewUsers.CurrentRow != null)
      {
        if (MessageBox.Show(@"Вы уверены?", @"Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
          db._ds.Tables["User"].Rows.RemoveAt(dataGridViewUsers.CurrentRow.Index);

          db._adapterUser.Update(db._ds.Tables["User"]);
        }
      }
    }

    private void button18_Click(object sender, EventArgs e)
    {
      if (dataGridViewUsers.CurrentRow != null)
      {
        User.SetValues(
          Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["Id"].Value),
          Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["PositionId"].Value),
          dataGridViewUsers.CurrentRow.Cells["Nick"].Value.ToString(),
          dataGridViewUsers.CurrentRow.Cells["Password"].Value.ToString()
          );

        UserForm pf = new UserForm(User.Id, db._ds.Tables["Position"], db._ds.Tables["User"]);
        pf.ShowDialog();

        if (User.Id != 0)
        {
          db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Id"] = User.Id;
          db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["PositionId"] = User.PositionId;
          db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Nick"] = User.Nick;
          db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Password"] = User.Password;

          db._adapterUser.Update(db._ds.Tables["User"]);

          User.Clear();
        }
      }
      else
      {
        MessageBox.Show(@"Выделите строку с пользователем!");
      }
    }

    private void toolStripMenuItem5_Click(object sender, EventArgs e)
    {
      if (treeViewDepEmpl.SelectedNode != null)
      {
        for (int i = 0; i < db._ds.Tables["Employee"].Rows.Count; i++)
        {
          string empName = db._ds.Tables["Employee"].Rows[i]["LastName"] +
            " " + db._ds.Tables["Employee"].Rows[i]["FirstName"] +
            " " + db._ds.Tables["Employee"].Rows[i]["MiddleName"];
          if (empName == treeViewDepEmpl.SelectedNode.Text)
          {
            Employee.SetValues(
              Convert.ToInt32(db._ds.Tables["Employee"].Rows[i]["Id"]),
              db._ds.Tables["Employee"].Rows[i]["LastName"].ToString(),
              db._ds.Tables["Employee"].Rows[i]["FirstName"].ToString(),
              db._ds.Tables["Employee"].Rows[i]["MiddleName"].ToString(),
              Convert.ToInt32(db._ds.Tables["Employee"].Rows[i]["DepartmentId"]),
              Convert.ToInt32(db._ds.Tables["Employee"].Rows[i]["UserId"]),
              Convert.ToDateTime(db._ds.Tables["Employee"].Rows[i]["AcceptanceDate"]),
              Convert.ToDouble(db._ds.Tables["Employee"].Rows[i]["Bonuses"]),
              Convert.ToDouble(db._ds.Tables["Employee"].Rows[i]["SumOrders"])
              );

            EmployeeForm ef = new EmployeeForm(
              Employee.Id,
              Employee.DepartmentId,
              db._ds.Tables["Department"],
              db._ds.Tables["Position"],
              db._ds.Tables["User"],
              db._connectString
              );
            ef.ShowDialog();

            if (Employee.Id != 0)
            {
              db._ds.Tables["Employee"].Rows[i]["Id"] = Employee.Id;
              db._ds.Tables["Employee"].Rows[i]["LastName"] = Employee.LastName;
              db._ds.Tables["Employee"].Rows[i]["FirstName"] = Employee.FirstName;
              db._ds.Tables["Employee"].Rows[i]["MiddleName"] = Employee.MiddleName;
              db._ds.Tables["Employee"].Rows[i]["DepartmentId"] = Employee.DepartmentId;
              db._ds.Tables["Employee"].Rows[i]["UserId"] = Employee.UserId;
              db._ds.Tables["Employee"].Rows[i]["AcceptanceDate"] = Employee.AcceptanceDate;
              db._ds.Tables["Employee"].Rows[i]["SumOrders"] = Employee.SumOrders;
              db._ds.Tables["Employee"].Rows[i]["Bonuses"] = Employee.Bonuses;

              db._adapterEmployee.Update(db._ds.Tables["Employee"]);
              treeViewUpdate();
              Employee.Clear();
            }
            break;
          }
        }
      }
    }

    private void toolStripMenuItem6_Click(object sender, EventArgs e)
    {
      if (treeViewDepEmpl.SelectedNode != null)
      {
        for (int i = 0; i < db._ds.Tables["Department"].Rows.Count; i++)
        {
          string depName = db._ds.Tables["Department"].Rows[i]["Name"].ToString();
          if (depName == treeViewDepEmpl.SelectedNode.Text)
          {
            Department.SetValues(
              Convert.ToInt32(db._ds.Tables["Department"].Rows[i]["Id"]),
              db._ds.Tables["Department"].Rows[i]["Name"].ToString());

            DepartmentForm df = new DepartmentForm(Department.Id);
            df.ShowDialog();

            if (Department.Id != 0)
            {
              db._ds.Tables["Department"].Rows[i]["Name"] = Department.Name;

              db._adapterDepartment.Update(db._ds.Tables["Department"]);
            }

            treeViewDepEmpl.BeginUpdate();
            treeViewDepEmpl.SelectedNode.Text = Department.Name;
            treeViewDepEmpl.EndUpdate();

          }
        }
      }
    }

    private void toolStripMenuItem7_Click(object sender, EventArgs e)
    {


    }

    private void buttonFindOrder_Click(object sender, EventArgs e)
    {
      OrderFindForm off = new OrderFindForm(
        db._ds.Tables["Order"],
        db._ds.Tables["Employee"],
        db._ds.Tables["Products_in_Order"]
        );
      off.ShowDialog();

      textBoxOrderId.Text = OrderCurrent.Id.ToString();

      string eid = (from r in db._ds.Tables["Employee"].AsEnumerable()
                    where r.Field<int>("Id") == OrderCurrent.EmployeeId
                    select (r.Field<string>("LastName") + " " + r.Field<string>("FirstName") + " " + r.Field<string>("MiddleName"))).First();

      comboBoxEmpl.SelectedItem = eid;

      comboBoxOrderCat.SelectedItem = OrderCurrent.Category;
      dateTimePickerOrderDate.Value = OrderCurrent.OrderDate;
      textBoxClient.Text = OrderCurrent.ClientId.ToString();
      dateTimePickerOrderDateDeliver.Value = OrderCurrent.DeliveryDate;
      textBoxDeliverAdress.Text = OrderCurrent.DeliveryAdress;

      var products = from row in db._ds.Tables["Products_in_Order"].AsEnumerable()
                     where row.Field<int>("OrderId") == OrderCurrent.Id
                     select row;
      foreach (var product in products)
      {
        int index = dataGridViewProdsInOrder.Rows.Add();
        dataGridViewProdsInOrder.Rows[index].Cells["Id"].Value = product["ProductId"];

        string prodName = (from row in db._ds.Tables["Product"].AsEnumerable()
                           where row.Field<int>("Id") == Convert.ToInt32(product["ProductId"])
                           select row.Field<string>("Title")).First();

        dataGridViewProdsInOrder.Rows[index].Cells["Title"].Value = prodName;
        double prodPrice = Convert.ToDouble((from row in db._ds.Tables["Product"].AsEnumerable() where row.Field<int>("Id") == Convert.ToInt32(product["ProductId"]) select row["Price"]).First());

        dataGridViewProdsInOrder.Rows[index].Cells["Price"].Value = prodPrice;
        dataGridViewProdsInOrder.Rows[index].Cells["Quantity"].Value = product["Quantity"];
        dataGridViewProdsInOrder.Rows[index].Cells["PriceWithDiscount"].Value = product["PriceWithDiscount"];
        dataGridViewProdsInOrder.Rows[index].Cells["Sum"].Value = product["Sum"];

      }
      sumRefresh();

    }

    private void button5_Click(object sender, EventArgs e)
    {
      DataRow[] rows = (from row in db._ds.Tables["Products_in_Order"].AsEnumerable()
                        where row.Field<int>("OrderId") == OrderCurrent.Id
                        select row).ToArray();

      foreach (DataRow row in rows)
      {
        row.Delete();
      }

      db._adapterProducts_in_Order.Update(db._ds.Tables["Products_in_Order"]);

      DataRow rowInOrder = (from row in db._ds.Tables["Order"].AsEnumerable()
                        where row.Field<int>("Id") == OrderCurrent.Id
                        select row).First();

      rowInOrder.Delete();

      db._adapterOrder.Update(db._ds, "Order");

      clearOrder();

    }
  }
}