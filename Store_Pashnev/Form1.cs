using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Store_Pashnev.ServiceReference1;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Store_WSL;
using IStoreServiceCallback = Store_Pashnev.ServiceReference1.IStoreServiceCallback;

namespace Store_Pashnev
{
  [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Reentrant)]
  public partial class Form1 : Form, IStoreServiceCallback
  {
    private StoreServiceClient client;
    private AutoResetEvent _waitForResponse;

    private DataBase _db;

    private User _user;

    private Employee _employee;

    private Order _order = new Order();

    private BindingSource bsProducts;
    private BindingSource bsClients;

    private DataView dvProductsInOrder;

    private int widthBeforeResize;
    private int heightBeforeResize;

    private List<GistogramDrawer.PairForGraphic> dataForGraphic;
    private List<GistogramDrawer.Pair2ForGraphic> data2ForGraphic;
    private List<string> marksNames;
    private int curGraphic = -1;

    #region form's methods

    public Form1()
    {
      InitializeComponent();

      _waitForResponse = new AutoResetEvent(false);

      InstanceContext callbackInstance = new InstanceContext(this);
      client = new StoreServiceClient(callbackInstance);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // Enter, authentification and authorization

      EnterForm ef = new EnterForm(client);

      ef.ShowDialog();
      
      _user = ef.user;

      if (_user.Id == 0)
      {
        Close();
      }
      else
      {
        _db = client.GetDatabaseAccess(_user.Id);//Getting database

        //Hiding data in relation with user rights and making data bindings
        if (!_db.Rights["Products"])
        {
          tabControlMain.TabPages["Products"].Dispose();
        }
        if (!_db.Rights["Order"])
        {
          tabControlMain.TabPages["Order"].Dispose();
        }
        if (!_db.Rights["Clients"])
        {
          tabControlMain.TabPages["Clients"].Dispose();
        }
        if (!_db.Rights["Reports"])
        {
          tabControlMain.TabPages["Reports"].Dispose();
        }
        if (!_db.Rights["Management"])
        {
          tabControlMain.TabPages.RemoveByKey("Management");
        }
        var emplRow = _db._ds.Tables["Employee"].AsEnumerable().FirstOrDefault(r => r.Field<int>("UserId") == _user.Id);

        if (emplRow != null)
        {
          _employee = new Employee
          {
            Id = emplRow.Field<int>("Id"),
            LastName = emplRow.Field<string>("LastName"),
            FirstName = emplRow.Field<string>("FirstName"),
            MiddleName = emplRow.Field<string>("MiddleName"),
            DepartmentId = emplRow.Field<int>("DepartmentId"),
            UserId = emplRow.Field<int>("UserId"),
            AcceptanceDate = emplRow.Field<DateTime>("AcceptanceDate"),
            Bonuses = emplRow.Field<double>("Bonuses"),
            SumOrders = emplRow.Field<double>("SumOrders")
          };
         
        }

        //for Products TabPage++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        bsProducts = new BindingSource { DataSource = _db._ds.Tables["Product"] };
        dataGridViewProducts.DataSource = bsProducts;
        dataGridViewProducts.Columns[0].Visible = false;
        bindingNavigatorProducts.BindingSource = bsProducts;

        ProductsBinding();

        foreach (DataColumn col in _db._ds.Tables["Product"].Columns)
        {
          string item = "";

          switch (col.ColumnName)
          {
            case "Id":
              item = "Номер (Id)";
              break;
            case "Title":
              item = "Наименование";
              break;
            case "Price":
              item = "Цена (+-25%)";
              break;
            case "DepartmentId":
              item = "Отдел";
              break;
            case "ProduserId":
              item = "Поставщик";
              break;
            case "Quantity":
              item = "Количество (+-25%)";
              break;
            case "CriticalQ":
              item = "Критическое количество (0-нет, 1-да)";
              break;
          }

          if (item != "")
            comboBoxCriteriaFilterProduct.Items.Add(item);
        }

        // for Order TabPage+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        textBoxOrderId.Text = (client.GetCurIdentity("Order") + 1).ToString();

        comboBoxEmpl.Items.Clear();

        if (_employee != null)
        {
          comboBoxEmpl.Items.Add(_employee.LastName + " " + _employee.FirstName + " " + _employee.MiddleName); 
          comboBoxEmpl.SelectedIndex = 0;
        }
        else
        {
          string[] empls = (from r in _db._ds.Tables["Employee"].AsEnumerable()
            select (string) r["LastName"] + " " + r["FirstName"] + " " + r["MiddleName"]).ToArray();
          comboBoxEmpl.Items.AddRange(empls);
        }
        
        //for Clients TabPage+++++++++++++++++++++++++++++++++++++++++++++++++++++++

        bsClients = new BindingSource { DataSource = _db._ds.Tables["Client"] };
        dataGridViewClients.DataSource = bsClients;
        dataGridViewClients.Columns[0].Visible = false;
        bindingNavigatorClients.BindingSource = bsClients;

        if (!_db.Rights["Client_LN"])
        {
          labelLN.Hide();
          textBoxLN.Hide();
        }
        if (!_db.Rights["Client_FN"])
        {
          labelFN.Hide();
          textBoxFN.Hide();
        }
        if (!_db.Rights["Client_MN"])
        {
          labelMN.Hide();
          textBoxMN.Hide();
        }
        if (!_db.Rights["Client_Sex"])
        {
          labelSex.Hide();
          radioButtonClientMale.Hide();
          radioButtonClientFemale.Hide();
        }
        if (!_db.Rights["Client_BD"])
        {
          labelBD.Hide();
          dateTimePickerClientBD.Hide();
        }
        if (!_db.Rights["Client_Phone1"])
        {
          labelPhone1.Hide();
          textBoxPhone1.Hide();
        }
        if (!_db.Rights["Client_Phone2"])
        {
          labelPhone2.Hide();
          textBoxPhone2.Hide();
        }
        if (!_db.Rights["Client_Phone3"])
        {
          labelPhone3.Hide();
          textBoxPhone3.Hide();
        }
        if (!_db.Rights["Client_Adress"])
        {
          labelAdress.Hide();
          textBoxAdress.Hide();
        }
        if (!_db.Rights["Client_SO"])
        {
          labelSO.Hide();
          textBoxSO.Hide();
        }
        if (!_db.Rights["Client_Discount"])
        {
          labelDiscount.Hide();
          textBoxDiscount.Hide();
        }
        if (!_db.Rights["Client_FirmName"])
        {
          labelFirmName.Hide();
          textBoxFirmName.Hide();
        }

        ClientsBinding();

        foreach (DataColumn col in _db._ds.Tables["Client"].Columns)
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

        //for Report

        DepsCB.DataSource = _db._ds.Tables["Department"];
        DepsCB.DisplayMember = "Name";
        DepsCB.ValueMember = "Id";

        //for Management
        
        dataGridViewRoles.DataSource = _db._ds.Tables["Position"];

        dataGridViewUsers.DataSource = _db._ds.Tables["User"];
      }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_user.Id == 0)
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
      foreach (DataRow row in _db._ds.Tables["Department"].Rows)
      {
        treeViewDepEmpl.Nodes.Add(new TreeNode(row["Name"].ToString()));

        var emplRows = from r in _db._ds.Tables["Employee"].AsEnumerable() where (int)r["DepartmentId"] == (int)row["Id"] select r;

        foreach (DataRow empl in emplRows)
        {
          string nextNodeName = empl["LastName"] + " " + empl["FirstName"] + " " + empl["MiddleName"];
          TreeNode tn = new TreeNode(nextNodeName);
          tn.Name = empl["Id"].ToString();
          TreeNode tn0 = treeViewDepEmpl.Nodes[_db._ds.Tables["Department"].Rows.IndexOf(row)];
          tn0.Nodes.Add(tn);
        }
      }
      treeViewDepEmpl.EndUpdate();
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

      NewProductBtn.Location = new Point(NewProductBtn.Location.X, NewProductBtn.Location.Y + (Size.Height - heightBeforeResize));
      EditProductBtn.Location = new Point(EditProductBtn.Location.X, EditProductBtn.Location.Y + (Size.Height - heightBeforeResize));
      groupBoxFind.Location = new Point(groupBoxFind.Location.X, groupBoxFind.Location.Y + (Size.Height - heightBeforeResize));

      //for Clients
      dataGridViewClients.Size = new Size(
        dataGridViewClients.Size.Width + (Size.Width - widthBeforeResize),
        dataGridViewClients.Size.Height + (Size.Height - heightBeforeResize)
        );

      NewClientBtn.Location = new Point(NewClientBtn.Location.X, NewClientBtn.Location.Y + (Size.Height - heightBeforeResize));
      button21.Location = new Point(button21.Location.X, button21.Location.Y + (Size.Height - heightBeforeResize));
      ClientFindBtn.Location = new Point(ClientFindBtn.Location.X, ClientFindBtn.Location.Y + (Size.Height - heightBeforeResize));
      groupBox14.Location = new Point(groupBox14.Location.X, groupBox14.Location.Y + (Size.Height - heightBeforeResize));
    }

    #endregion


    #region bindings

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
        textBoxFirmName.DataBindings.Clear();
      }

      textBoxClientID.DataBindings.Add("Text", bsClients, "Id");

      if (_db.Rights["Client_LN"])
      {
        textBoxLN.DataBindings.Add("Text", bsClients, "LastName");
      }
      if (_db.Rights["Client_FN"])
      {
        textBoxFN.DataBindings.Add("Text", bsClients, "FirstName");
      }
      if (_db.Rights["Client_MN"])
      {
        textBoxMN.DataBindings.Add("Text", bsClients, "MiddleName");
      }
      if (_db.Rights["Client_BD"])
      {
        dateTimePickerClientBD.DataBindings.Add("Value", bsClients, "BirthDay");
      }
      if (_db.Rights["Client_Phone1"])
      {
        textBoxPhone1.DataBindings.Add("Text", bsClients, "Phone1");
      }
      if (_db.Rights["Client_Phone2"])
      {
        textBoxPhone2.DataBindings.Add("Text", bsClients, "Phone2");
      }
      if (_db.Rights["Client_Phone3"])
      {
        textBoxPhone3.DataBindings.Add("Text", bsClients, "Phone3");
      }
      if (_db.Rights["Client_Adress"])
      {
        textBoxAdress.DataBindings.Add("Text", bsClients, "Adress");
      }
      if (_db.Rights["Client_SO"])
      {
        textBoxSO.DataBindings.Add("Text", bsClients, "SumOrders");
      }
      if (_db.Rights["Client_Discount"])
      {
        textBoxDiscount.DataBindings.Add("Text", bsClients, "Discount");
      }
      if (_db.Rights["Client_FirmName"])
      {
        textBoxFirmName.DataBindings.Add("Text", bsClients, "FirmName");
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
        textBoxQuantity.DataBindings.Clear();
      }

      textBoxID.DataBindings.Add("Text", bsProducts, "Id");
      textBoxTitle.DataBindings.Add("Text", bsProducts, "Title");
      textBoxPrice.DataBindings.Add("Text", bsProducts, "Price");
      textBoxQuantity.DataBindings.Add("Text", bsProducts, "Quantity");

      if (bsProducts.Position >= 0)
      {
        string produser = _db._ds.Tables["Client"].Rows.Find((int)_db._ds.Tables["Product"].Rows[bsProducts.Position]["ProduserId"])["FirmName"].ToString();
        textBoxProducer.Text = produser;

        string dep = _db._ds.Tables["Department"].Rows.Find((int)_db._ds.Tables["Product"].Rows[bsProducts.Position]["DepartmentId"])["Name"].ToString();
        textBoxDepartment.Text = dep;
      }

      bsProducts.PositionChanged += bsProducts_PositionChanged;
    }

    #endregion


    #region Product tabpage methods

    private void AddSelectedProdsInOrder_Click(object sender, EventArgs e)
    {
      if (dataGridViewProducts.SelectedRows.Count == 0)
      {
        MessageBox.Show(@"Нажмите в голове строки, чтобы выделить товар! Чтобы выделить несколько, удерживайте при этом Ctrl!", @"Не выбраны товары!");
      }
      else
      {
        AddProductsInOrder();
        tabControlMain.SelectTab("Order");
      }
    }

    void AddProductsInOrder()
    {
      if (comboBoxOrderCat.SelectedIndex == -1)
      {
        MessageBox.Show(@"Выберите сначала категорию заказа!", @"Просьба!");
        tabControlMain.SelectTab("Order");
        comboBoxOrderCat.Focus();
        return;
      }

      int alreadyAdded = 0;
      int noMoreProduser = 0;//To control that 1 produser in 1 supply order

      int discount = textBoxClient.Text == "" ? 0 :
        (from row in _db._ds.Tables["Client"].AsEnumerable()
         where row.Field<int>("Id") == Convert.ToInt32(textBoxClient.Text.Substring(textBoxClient.Text.IndexOf("Id") + 2))
         select row.Field<int>("Discount")).First();

      int curId = client.GetCurIdentity("Products_in_Order");

      foreach (DataGridViewRow r in dataGridViewProducts.SelectedRows)
      {
        if (_order.ClientId == 0 && comboBoxOrderCat.SelectedItem.ToString() == "Поставка")
        {
          DataRow dr = _db._ds.Tables["Client"].Rows.Find(Convert.ToInt32(r.Cells["ProduserId"].Value));

          string clntName = dr.Field<string>("LastName") + @" " + dr.Field<string>("FirstName") + @" " + dr.Field<string>("MiddleName");
          if (dr.Field<string>("FirmName") != null)
          {
            clntName = dr.Field<string>("FirmName") + @" (" + clntName + @")";
          }
          textBoxClient.Text = clntName + @" Id" + dr.Field<int>("Id");
          _order.ClientId = dr.Field<int>("Id");
        }

        if (_order.ClientId!= 0 && _order.ClientId != Convert.ToInt32(r.Cells["ProduserId"].Value))
        {
          noMoreProduser++;
          continue;
        }

        var rowExist = dataGridViewProdsInOrder.Rows.Cast<DataGridViewRow>()
          .FirstOrDefault(ro => Convert.ToInt32(ro.Cells["ProductId"].Value) == Convert.ToInt32(r.Cells["Id"].Value));

        if (rowExist == null)
        {
          int index = dataGridViewProdsInOrder.Rows.Add();
          DataGridViewRow dgvr = dataGridViewProdsInOrder.Rows[index];
          dgvr.Cells["Id"].Value = curId++;
          dgvr.Cells["ProductId"].Value = r.Cells["Id"].Value;
          dgvr.Cells["Title"].Value = r.Cells["Title"].Value;

          dgvr.Cells["Price"].Value
            = comboBoxOrderCat.SelectedItem.ToString() == "Поставка"
              ? r.Cells["PurchasePrice"].Value
              : r.Cells["Price"].Value;

          dgvr.Cells["Quantity"].Value = 1;
          dgvr.Cells["FinalPrice"].Value = comboBoxOrderCat.SelectedItem.ToString() == "Поставка"
            ? r.Cells["PurchasePrice"].Value
            : Convert.ToDouble(r.Cells["Price"].Value) - ((Convert.ToDouble(r.Cells["Price"].Value)/100)*discount);
          dgvr.Cells["Sum"].Value = dgvr.Cells["FinalPrice"].Value;
        }
        else
        {
          alreadyAdded++;
        }
      }

      sumRefresh();
      numericUpDownProdQuantity.Value = Convert.ToDecimal(dataGridViewProdsInOrder.Rows[0].Cells["Quantity"].Value);

      if (alreadyAdded > 0)
      {
        MessageBox.Show(@"Некоторые товары уже были добавлены. Изменяйте их количество, а не добавляйте заново");
      }

      if (noMoreProduser > 0)
      {
        MessageBox.Show(@"Некоторые товары не были добавлены, потому что не поставляются поставщиком, закреплённым за текущим заказом. Закажите их поставку в другом заказе.");
      }
    }

    void sumRefresh()
    {
      double sum = 0.0;
      for (int i = 0; i < dataGridViewProdsInOrder.Rows.Count; i++)
      {
        dataGridViewProdsInOrder.Rows[i].Cells["Sum"].Value =
          Convert.ToInt32(dataGridViewProdsInOrder.Rows[i].Cells["Quantity"].Value) *
          Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["FinalPrice"].Value);
        sum += Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Sum"].Value);
      }
      textBoxAllSum.Text = sum.ToString();
    }

    private void OnNewProduct(object sender, EventArgs e)
    {
      SetProduct(new Product());
    }

    private void OnEditProduct(object sender, EventArgs e)
    {
      int index = dataGridViewProducts.CurrentCell.RowIndex;
      
      Product product = new Product();
      
      product.SetValues(
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["Id"].Value),
        dataGridViewProducts.Rows[index].Cells["Title"].Value.ToString(),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["PurchasePrice"].Value),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["Markup"].Value),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["Price"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["DepartmentId"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["ProduserId"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["Quantity"].Value),
        Convert.ToBoolean(dataGridViewProducts.Rows[index].Cells["CriticalQ"].Value));

      SetProduct(product);
    }

    void SetProduct(Product product)
    {
      if (product.Id == 0)
      {
        product.SetId(client.GetCurIdentity("Product") + 1);
      }

      ProductForm pf = new ProductForm(product, _db._ds.Tables["Department"], _db._ds.Tables["Client"]);
      pf.ShowDialog();

      product = pf.product;

      if (product.Id != 0)
      {
        client.SetProduct(product);
        //_waitForResponse.WaitOne();
      }

      bsProducts = new BindingSource { DataSource = _db._ds.Tables["Product"] };
      dataGridViewProducts.DataSource = bsProducts;
      bindingNavigatorProducts.BindingSource = bsProducts;

      ProductsBinding();
    }

    private void textBoxStringToFind_TextChanged(object sender, EventArgs e)
    {
      if (String.IsNullOrWhiteSpace(textBoxStringToFind.Text))
      {
        bsProducts = new BindingSource { DataSource = _db._ds.Tables["Product"] };
        dataGridViewProducts.DataSource = bsProducts;
        bindingNavigatorProducts.BindingSource = bsProducts;

        ProductsBinding();
      }
      else
      {
        string filterStr = "";
        
        switch (comboBoxCriteriaFilterProduct.SelectedItem.ToString())
        {
          case "Номер (Id)":
            filterStr = "Id = " + textBoxStringToFind.Text;
            break;
          case "Наименование":
            filterStr = "Title like '%" + textBoxStringToFind.Text + "%'";
            break;
          case "Цена (+-25%)":
            filterStr = "Price > " + (Convert.ToDouble(textBoxStringToFind.Text) - (Convert.ToDouble(textBoxStringToFind.Text) / 100 * 25)).ToString(CultureInfo.CreateSpecificCulture("en-GB")) + " and Price < " + (Convert.ToDouble(textBoxStringToFind.Text) + (Convert.ToDouble(textBoxStringToFind.Text) / 100 * 25)).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            break;
          case "Отдел":
          {
            var depIds = _db._ds.Tables["Department"].AsEnumerable()
              .Where(_ => _.Field<string>("Name").IndexOf(textBoxStringToFind.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
              .Select(d => d.Field<int>("Id"));
            if (depIds.Count(g => g != 0) != 0)
            {
              StringBuilder ids = new StringBuilder();

              foreach (int id in depIds)
              {
                ids.Append(id + ", ");
              }

              ids.Remove(ids.Length - 2, 1);

              filterStr = "DepartmentId in (" + ids + ")";
            }
            else
            {
              return;
            }
            break;
          }
          case "Поставщик":
          {
            var depIds = _db._ds.Tables["Client"].AsEnumerable()
              .Where(_ => _.Field<string>("FirmName") != null
                && _.Field<string>("FirmName").IndexOf(textBoxStringToFind.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
              .Select(d => d.Field<int>("Id"));
            if (depIds.Count(g => g != 0) != 0)
            {
              StringBuilder ids = new StringBuilder();

              foreach (int id in depIds)
              {
                ids.Append(id + ", ");
              }

              ids.Remove(ids.Length - 2, 1);

              filterStr = "ProduserId in (" + ids + ")";
            }
            else
            {
              return;
            }
            break;
          }
          case "Количество (+-25%)":
          filterStr = "Quantity > " + (Convert.ToInt32(textBoxStringToFind.Text) - (Convert.ToDouble(textBoxStringToFind.Text) / 100 * 25)).ToString(CultureInfo.CreateSpecificCulture("en-GB")) + " and Quantity < " + (Convert.ToInt32(textBoxStringToFind.Text) + (Convert.ToDouble(textBoxStringToFind.Text) / 100 * 25)).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            break;
          case "Критическое количество (0-нет, 1-да)":
            filterStr = "CriticalQ = " + Convert.ToInt32(textBoxStringToFind.Text);
            break;
        }

        DataView dvFilter = new DataView(_db._ds.Tables["Product"]);
        
        dvFilter.RowFilter = filterStr;

        bsProducts = new BindingSource { DataSource = dvFilter };
        dataGridViewProducts.DataSource = bsProducts;
        bindingNavigatorProducts.BindingSource = bsProducts;

        ProductsBinding();
        bsProducts_PositionChanged(bsProducts, new EventArgs());
      }
    }

    private void ClearFindStringBtn_Click(object sender, EventArgs e)
    {
      textBoxStringToFind.Text = String.Empty;
      comboBoxCriteriaFilterProduct.SelectedIndex = -1;

      //bsProducts = new BindingSource { DataSource = _db._ds.Tables["Product"] };
      //dataGridViewClients.DataSource = bsProducts;
      //bindingNavigatorClients.BindingSource = bsProducts;

      //ProductsBinding();
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      DataView dvFilter = new DataView(_db._ds.Tables["Product"]);
      dvFilter.RowFilter = _db._ds.Tables["Product"].Columns[dataGridViewProducts.CurrentCell.ColumnIndex].ColumnName +
        " = '" + dataGridViewProducts.CurrentCell.Value + "'";
      dataGridViewProducts.DataSource = dvFilter;
    }

    private void resetFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dataGridViewProducts.DataSource = _db._ds.Tables["Product"];
    }

    void bsProducts_PositionChanged(object sender, EventArgs e)
    {
      if (bsProducts.Position == -1)
      {
        return;
      }
      string produser = _db._ds.Tables["Client"].Rows.Find(Convert.ToInt32(dataGridViewProducts.Rows[bsProducts.Position].Cells["ProduserId"].Value))["FirmName"].ToString();
      textBoxProducer.Text = produser;

      string dep = _db._ds.Tables["Department"].Rows.Find(Convert.ToInt32(dataGridViewProducts.Rows[bsProducts.Position].Cells["DepartmentId"].Value))["Name"].ToString();
      textBoxDepartment.Text = dep;

      PurchasePriceNumericUpDown.Value = Convert.ToDecimal(dataGridViewProducts.Rows[bsProducts.Position].Cells["PurchasePrice"].Value);
      MarkupNumericUpDown.Value = Convert.ToDecimal(dataGridViewProducts.Rows[bsProducts.Position].Cells["Markup"].Value);
    }

    private void comboBoxCriteriaFilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
      textBoxStringToFind.Enabled = ((sender as ComboBox).SelectedIndex != -1);
    }

    private void DelProductBtn_Click(object sender, EventArgs e)
    {
      int index = dataGridViewProducts.CurrentCell.RowIndex;

      Product product = new Product();

      product.SetValues(
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["Id"].Value),
        dataGridViewProducts.Rows[index].Cells["Title"].Value.ToString(),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["PurchasePrice"].Value),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["Markup"].Value),
        Convert.ToDouble(dataGridViewProducts.Rows[index].Cells["Price"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["DepartmentId"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["ProduserId"].Value),
        Convert.ToInt32(dataGridViewProducts.Rows[index].Cells["Quantity"].Value),
        Convert.ToBoolean(dataGridViewProducts.Rows[index].Cells["CriticalQ"].Value));

      client.DelProduct(product);
      //_waitForResponse.WaitOne();
    }

    #endregion


    #region Order tabpage methods

    private void AddClientToOrder_Click(object sender, EventArgs e)
    {
      if ((comboBoxOrderCat.SelectedItem != null && comboBoxOrderCat.SelectedItem.ToString() == "Поставка")
        && textBoxClient.Text != String.Empty)
      {
        MessageBox.Show(@"Клиент уже добавлен как поставщик указанных товаров!", @"Поставщик уже добавлен");
        return;
      }

      ClientFindForm cff = new ClientFindForm(_db._ds.Tables["Client"], _db.Rights);
      cff.ShowDialog();

      Client clnt = cff.clnt;

      if (clnt.Id != 0)
      {
        string clntName = clnt.LastName +@" "+ clnt.FirstName +@" "+ clnt.MiddleName;
        string clntFullName;
        if (clnt.FirmName != String.Empty)
        {
          clntFullName = clnt.FirmName + @" (" + clntName + @")";
        }
        else
        {
          clntFullName = clntName;
        }
        textBoxClient.Text = clntFullName + @" Id" + clnt.Id;
      }
    }

    void discountRefresh()
    {
      string clId = textBoxClient.Text.Substring(textBoxClient.Text.IndexOf("Id") + 2);
      int discount = textBoxClient.Text == "" ? 0 : (from r in _db._ds.Tables["Client"].AsEnumerable()
                                                     where r.Field<int>("Id") == Convert.ToInt32(clId)
                                                     select r.Field<int>("Discount")).First();
      for (int i = 0; i < dataGridViewProdsInOrder.Rows.Count; i++)
      {
        dataGridViewProdsInOrder.Rows[i].Cells["FinalPrice"].Value =
          Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Price"].Value) -
          (Convert.ToDouble(dataGridViewProdsInOrder.Rows[i].Cells["Price"].Value) / 100 * discount);
      }
    }

    private void checkBoxDefaultAdress_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxDefaultAdress.Checked)
      {
        if (textBoxClient.Text == "")
        {
          checkBoxDefaultAdress.Checked = false;
          MessageBox.Show(@"Сначала выберите клиента!");
          AddClientToOrder.Select();
        }
        else
        {
          string clntId = textBoxClient.Text.Substring(textBoxClient.Text.IndexOf("Id") + 2);

          textBoxDeliverAdress.Text = _db._ds.Tables["Client"].Rows.Find(clntId)["Adress"].ToString();
        }
      }
      else
      {
        textBoxDeliverAdress.Text = "";
      }
    }

    private void checkBoxDefaultAdress_Click(object sender, EventArgs e)
    {
      if (textBoxClient.Text == "")
      {
        checkBoxDefaultAdress.Checked = false;
        MessageBox.Show(@"Сначала выберите клиента!");
        AddClientToOrder.Select();
      }
    }

    private void numericUpDownProdQuantity_ValueChanged(object sender, EventArgs e)
    {
      if (numericUpDownProdQuantity.Value == 0)
      {
        MessageBox.Show(@"Количество не может быть равно 0! Удалите продукт!", @"Ошибка");
        numericUpDownProdQuantity.Value = 1;
        return;
      }

      if (dataGridViewProdsInOrder.CurrentRow != null)
      {
        dataGridViewProdsInOrder.CurrentRow.Cells["Quantity"].Value = numericUpDownProdQuantity.Value;
        sumRefresh();

        if (comboBoxOrderCat.SelectedItem != null
          && comboBoxOrderCat.SelectedItem.ToString() == "Продажа")
        {
          int rest =
            (int)
              _db._ds.Tables["Product"].Rows.Find(
                Convert.ToInt32(dataGridViewProdsInOrder.CurrentRow.Cells["ProductId"].Value))["Quantity"] -
            ((int)numericUpDownProdQuantity.Value);

          if (rest < 0)
          {
            MessageBox.Show(@"Товар закончился!", @"Внимание!");
            numericUpDownProdQuantity.Value -= 1;
          }
        }
      }
      else
      {
        MessageBox.Show(@"Выделите строку перед изменением количества");
      }
    }

    private void buttonAddProductToOrder_Click(object sender, EventArgs e)
    {
      if (comboBoxOrderCat.SelectedIndex == -1)
      {
        MessageBox.Show(@"Выберите сначала категорию заказа!", @"Просьба!");
        comboBoxOrderCat.Focus();
        return;
      }
      tabControlMain.SelectTab("Products");
    }

    private void buttonConfirmOrder_Click(object sender, EventArgs e)
    {
      //Проверка полей
      if (comboBoxOrderCat.SelectedIndex == -1)
      {
        MessageBox.Show(@"Вы не выбрали категорию заказа!", @"Выберите категорию!", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        comboBoxOrderCat.Focus();
        return;
      }

      if (comboBoxEmpl.SelectedIndex == -1)
      {
        MessageBox.Show(@"Вы не выбрали продавца!", @"Выберите продавца!", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        comboBoxEmpl.Focus();
        return;
      }

      if (String.IsNullOrWhiteSpace(textBoxClient.Text))
      {
        MessageBox.Show(@"Вы не выбрали клиента!", @"Выберите клиента!", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        textBoxClient.Focus();
        return;
      }

      if (dateTimePickerOrderDate.Value.Date == dateTimePickerOrderDateDeliver.Value.Date)
      {
        DialogResult dr = MessageBox.Show(@"Вы уверены, что доставка будет в день заказа?",
          @"Даты заказа и доставки совпадают!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dr == DialogResult.No)
        {
          dateTimePickerOrderDateDeliver.Focus();
          return;
        }
      }

      if (String.IsNullOrWhiteSpace(textBoxDeliverAdress.Text))
      {
        MessageBox.Show(@"Вы не ввели адрес доставки!", @"Введите адрес!", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        textBoxDeliverAdress.Focus();
        return;
      }

      //Оформление заказа

      int emplId = (from r in _db._ds.Tables["Employee"].AsEnumerable()
                    where
                      (r.Field<string>("LastName") + " " + r.Field<string>("FirstName") + " " + r.Field<string>("MiddleName")) ==
                      comboBoxEmpl.SelectedItem.ToString()
                    select r.Field<int>("Id")).First();

      Dictionary<int, ProductInOrder> prodsDictionary = (from DataGridViewRow row in dataGridViewProdsInOrder.Rows
        let id = Convert.ToInt32(row.Cells["Id"].Value)
        let orderId = Convert.ToInt32(textBoxOrderId.Text)
        let productId = Convert.ToInt32(row.Cells["ProductId"].Value)
        let quantity = Convert.ToInt32(row.Cells["Quantity"].Value)
        let finalPrice = Convert.ToDouble(row.Cells["FinalPrice"].Value)
        let sum = Convert.ToDouble(row.Cells["Sum"].Value)
        select new ProductInOrder
        {
          Id = id, OrderId = orderId, ProductId = productId, Quantity = quantity, FinalPrice = finalPrice, Sum = sum
        }).ToDictionary(pio => pio.ProductId);

      //int curPio = client.GetCurIdentity("Products_in_Order");

      string clntId = textBoxClient.Text.Substring(textBoxClient.Text.IndexOf("Id") + 2);

      _order.SetValues(Convert.ToInt32(textBoxOrderId.Text),
        comboBoxOrderCat.SelectedItem.ToString(),
        emplId,
        Convert.ToInt32(clntId),
        dateTimePickerOrderDate.Value,
        dateTimePickerOrderDateDeliver.Value,
        textBoxDeliverAdress.Text,
        Convert.ToDouble(textBoxAllSum.Text),
        prodsDictionary);

      client.SetOrder(_order);
      //_waitForResponse.WaitOne();

      _order.Clear();
      clearOrder();
    }

    private void buttonFindOrder_Click(object sender, EventArgs e)
    {
      OrderFindForm off = new OrderFindForm(
        _db._ds.Tables["Order"],
        _db._ds.Tables["Employee"],
        _db._ds.Tables["Products_in_Order"]
        );
      off.ShowDialog();

      if (off.order.Id == 0)
      {
        return;
      }

      _order = off.order;

      textBoxOrderId.Text = _order.Id.ToString();

      DataRow emplRow = _db._ds.Tables["Employee"].Rows.Find(_order.EmployeeId);

      comboBoxEmpl.SelectedItem = emplRow.Field<string>("LastName") + " " + emplRow.Field<string>("FirstName") + " " + emplRow.Field<string>("MiddleName");

      comboBoxOrderCat.SelectedItem = _order.Category;
      dateTimePickerOrderDate.Value = _order.OrderDate;

      DataRow dr = _db._ds.Tables["Client"].Rows.Find(_order.ClientId);
      string clntName = dr.Field<string>("LastName") + @" " + dr.Field<string>("FirstName") + @" " + dr.Field<string>("MiddleName");
      if (dr.Field<string>("FirmName") != null)
      {
        clntName = dr.Field<string>("FirmName") + @" (" + clntName + @")";
      }
      textBoxClient.Text = clntName + @" Id" + dr.Field<int>("Id");

      dateTimePickerOrderDateDeliver.Value = _order.DeliveryDate;
      textBoxDeliverAdress.Text = _order.DeliveryAdress;

      var products = from row in _db._ds.Tables["Products_in_Order"].AsEnumerable()
                     where row.Field<int>("OrderId") == _order.Id
                     select row;

      dataGridViewProdsInOrder.Rows.Clear();

      foreach (var product in products)
      {
        int index = dataGridViewProdsInOrder.Rows.Add();
        dataGridViewProdsInOrder.Rows[index].Cells["Id"].Value = product["Id"];

        DataRow prodRow = _db._ds.Tables["Product"].Rows.Find(product["ProductId"]);
        dataGridViewProdsInOrder.Rows[index].Cells["ProductId"].Value = prodRow["Id"];
        dataGridViewProdsInOrder.Rows[index].Cells["Title"].Value = prodRow["Title"];
        dataGridViewProdsInOrder.Rows[index].Cells["Price"].Value = prodRow["Price"];

        dataGridViewProdsInOrder.Rows[index].Cells["Quantity"].Value = product["Quantity"];
        dataGridViewProdsInOrder.Rows[index].Cells["FinalPrice"].Value = product["FinalPrice"];
        dataGridViewProdsInOrder.Rows[index].Cells["Sum"].Value = product["Sum"];
      }
      sumRefresh();
      CanselOrderBtn.Enabled = true;
      buttonClearOrder.Enabled = false;
    }

    private void textBoxClient_TextChanged(object sender, EventArgs e)
    {
      if (textBoxClient.Text == String.Empty)
      {
        return;
      }

      if (comboBoxOrderCat.SelectedItem != null && comboBoxOrderCat.SelectedItem.ToString() != "Поставка")
      {
        discountRefresh();
        sumRefresh();
      }

      checkBoxDefaultAdress.Enabled = true;
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
      _order.Clear();
      _order.SetId(client.GetCurIdentity("Order") + 1);

      textBoxOrderId.Text = _order.Id.ToString();

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

      CanselOrderBtn.Enabled = false;
      buttonClearOrder.Enabled = true;

      checkBoxDefaultAdress.Enabled = false;

    }

    private void buttonDelProdFromOrder_Click(object sender, EventArgs e)
    {
      if (dataGridViewProdsInOrder.CurrentRow != null)
      {
        _order.Products.Remove(Convert.ToInt32(dataGridViewProdsInOrder.Rows[dataGridViewProdsInOrder.CurrentRow.Index].Cells["Id"].Value));
        dataGridViewProdsInOrder.Rows.RemoveAt(dataGridViewProdsInOrder.CurrentRow.Index);
      }
      else
      {
        MessageBox.Show(@"Не выделена ни одна строка", @"Ошибка!");
      }
    }

    private void CanselOrderBtn_Click(object sender, EventArgs e)
    {
      DialogResult dr = MessageBox.Show(@"Заказ будет полностью удалён! Вы уверены?", @"Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (dr == DialogResult.No)
      {
        return;
      }

      if ((client.GetCurIdentity("Order") + 1) > Convert.ToInt32(textBoxOrderId.Text))
      {
        client.RemoveOrder(_order);
        //_waitForResponse.WaitOne();
      }

      clearOrder();
    }

    #endregion


    #region Client tabpage methods

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      DataView dvFilter = new DataView(_db._ds.Tables["Client"]);
      dvFilter.RowFilter = _db._ds.Tables["Client"].Columns[dataGridViewClients.CurrentCell.ColumnIndex].ColumnName +
        " = '" + dataGridViewClients.CurrentCell.Value + "'";
      dataGridViewClients.DataSource = dvFilter;
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      dataGridViewClients.DataSource = _db._ds.Tables["Client"];
    }

    void bsClients_PositionChanged(object sender, EventArgs e)
    {
      if (_db.Rights["Client_Sex"])
      {
        string sex = _db._ds.Tables["Client"].Rows[bsClients.Position]["Sex"].ToString();

        radioButtonClientMale.Checked = sex == "мужской";
        radioButtonClientFemale.Checked = sex == "женский";
      }
    }

    private void dataGridViewClients_DoubleClick(object sender, EventArgs e)
    {
      if (dataGridViewClients.CurrentCell != null)
      {
        int clientID =
          Convert.ToInt32(_db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex].ItemArray[0]);

        Client clnt = new Client();

        clnt.SetValues(clientID,
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["LastName"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["FirstName"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["MiddleName"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Sex"].ToString(),
          Convert.ToDateTime(_db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["BirthDay"]),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone1"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone2"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Phone3"].ToString(),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Adress"].ToString(),
          Convert.ToDouble(_db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["SumOrders"]),
          Convert.ToInt32(_db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["Discount"]),
          _db._ds.Tables["Client"].Rows[dataGridViewClients.CurrentCell.RowIndex]["FirmName"].ToString()
          );

        ClientForm cf = new ClientForm(clnt);
        cf.ShowDialog();

        clnt = cf.client;

        if (clnt.Id != 0)
        {
          client.SetClient(clnt);
          //_waitForResponse.WaitOne();
        }
      }
      else
      {
        MessageBox.Show(@"Нажмите в строке, чтобы выделить клиента", @"Error" );
      }
    }

    private void NewClientBtn_Click(object sender, EventArgs e)
    {
      Client clnt = new Client();

      clnt.SetId(client.GetCurIdentity("Client") + 1);

      ClientForm cf = new ClientForm(clnt);
      cf.ShowDialog();

      clnt = cf.client;

      if (clnt.Id != 0)
      {
        client.SetClient(clnt);
        //_waitForResponse.WaitOne();
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
      DataView dvFilter = new DataView(_db._ds.Tables["Client"]);

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
      bsClients = new BindingSource { DataSource = _db._ds.Tables["Client"] };
      dataGridViewClients.DataSource = bsClients;
      bindingNavigatorClients.BindingSource = bsClients;

      ClientsBinding();
    }

    private void ClientFindBtn_Click(object sender, EventArgs e)
    {
      ClientFindForm cff = new ClientFindForm(_db._ds.Tables["Client"], _db.Rights);
      cff.ShowDialog();

      Client clnt = cff.clnt;

      if (clnt.Id != 0)
      {
        foreach (DataGridViewRow row in dataGridViewClients.Rows)
        {
          if (Convert.ToInt32(row.Cells[0].Value) == clnt.Id)
          {
            bsClients.Position = dataGridViewClients.Rows.IndexOf(row);
            break;
          }
        }
      }
    }

    private void buttonBindToOrder_Click(object sender, EventArgs e)
    {
      textBoxClient.Text = textBoxClientID.Text;
    }

    #endregion


    #region Reports tabpage methods

    private void ShowСostsNBenefitsBtn_Click(object sender, EventArgs e)
    {
      if (СostsNBenefitsCriterCB.SelectedIndex == -1)
      {
        MessageBox.Show(@"Выберите критерий сравнения затрат и прибыли", @"Критерий!");
        СostsNBenefitsCriterCB.Focus();
      }
      else if (СostsNBenefitsCriterCB.SelectedItem.ToString() == "по отделам")
      {

        var depsCostsNProfits =
          _db._ds.Tables["Products_in_Order"].AsEnumerable()
            .Where(pio => _db._ds.Tables["Order"].AsEnumerable()
                          .Where(o => o.Field<int>("Id") == pio.Field<int>("OrderId")
                          && o.Field<string>("Category") == "Продажа")
                          .Select(o => o).Any())
            .Select(pio => new GistogramDrawer.Pair2ForGraphic
                {
                  name =
                        _db._ds.Tables["Department"].AsEnumerable()
                        .Where(_ => _.Field<int>("Id") ==
                          _db._ds.Tables["Product"].AsEnumerable()
                          .Where(id => id.Field<int>("Id") == pio.Field<int>("ProductId"))
                          .Select(d => d.Field<int>("DepartmentId")).First())
                        .Select(d => d.Field<string>("Name")).First(),
                  value0 =
                    _db._ds.Tables["Product"].AsEnumerable()
                    .Where(_ => _.Field<int>("Id") ==
                      pio.Field<int>("ProductId"))
                    .Select(p => p.Field<decimal>("PurchasePrice") * pio.Field<int>("Quantity")).First(),
                  value1 = 
                  _db._ds.Tables["Product"].AsEnumerable()
                    .Where(_ => _.Field<int>("Id") ==
                      pio.Field<int>("ProductId"))
                    .Select(p => (pio.Field<decimal>("FinalPrice") - p.Field<decimal>("PurchasePrice")) * pio.Field<int>("Quantity")).First()
                }
              );

        var depsCostsNProfitsGrouped = depsCostsNProfits
          .GroupBy(_ => _.name)
          .Select(g => new GistogramDrawer.Pair2ForGraphic
          {
            name = g.First().name,
            value0 = g.Sum(s => s.value0),
            value1 = g.Sum(s => s.value1)
          });

        data2ForGraphic =
          depsCostsNProfitsGrouped.ToList();

        marksNames =  new List<string>{"Затраты", "Прибыль"};

        GistogramDrawer.DrawGistogramWithAccumulation(data2ForGraphic, marksNames, splitContainer1.Panel2);
        curGraphic = 2;
      }
      else if (СostsNBenefitsCriterCB.SelectedItem.ToString() == "по поставщикам")
      {
        var produsersCostsNProfits =
          _db._ds.Tables["Products_in_Order"].AsEnumerable()
          .Where(pio => _db._ds.Tables["Order"].AsEnumerable()
                  .Where(o => o.Field<int>("Id") == pio.Field<int>("OrderId")
                  && o.Field<string>("Category") == "Продажа")
                  .Select(o => o).Any())
          .Select(pio => new GistogramDrawer.Pair2ForGraphic
          {
            name =
                  _db._ds.Tables["Client"].AsEnumerable()
                  .Where(_ => _.Field<int>("Id") ==
                    _db._ds.Tables["Product"].AsEnumerable()
                    .Where(id => id.Field<int>("Id") == pio.Field<int>("ProductId"))
                    .Select(d => d.Field<int>("ProduserId")).First())
                  .Select(d => d.Field<string>("FirmName")).First(),
            value0 =
              _db._ds.Tables["Product"].AsEnumerable()
              .Where(_ => _.Field<int>("Id") ==
                pio.Field<int>("ProductId"))
              .Select(p => p.Field<decimal>("PurchasePrice") * pio.Field<int>("Quantity")).First(),
            value1 =
            _db._ds.Tables["Product"].AsEnumerable()
              .Where(_ => _.Field<int>("Id") ==
                pio.Field<int>("ProductId"))
              .Select(p => (pio.Field<decimal>("FinalPrice") - p.Field<decimal>("PurchasePrice")) * pio.Field<int>("Quantity")).First()
          }
          );

        var depsCostsNProfitsGrouped = produsersCostsNProfits
          .GroupBy(_ => _.name)
          .Select(g => new GistogramDrawer.Pair2ForGraphic
          {
            name = g.First().name,
            value0 = g.Sum(s => s.value0),
            value1 = g.Sum(s => s.value1)
          });

        data2ForGraphic =
          depsCostsNProfitsGrouped.ToList();

        marksNames = new List<string> { "Затраты", "Прибыль" };

        GistogramDrawer.DrawGistogramWithAccumulation(data2ForGraphic, marksNames, splitContainer1.Panel2);
        curGraphic = 2;
      }
    }

    private void ShowSalesCB_Click(object sender, EventArgs e)
    {
      if (SalesCriterCB.SelectedIndex == -1)
      {
        MessageBox.Show(@"Выберите критерий выборки по количеству продаж!", @"Критерий!");
        SalesCriterCB.Focus();
      }
      else if (SalesCriterCB.SelectedItem.ToString() == "по работникам")
      {
        dataForGraphic =
          _db._ds.Tables["Employee"].AsEnumerable()
            .Select(g => new GistogramDrawer.PairForGraphic
            {
              name = g.Field<string>("LastName"),
              value = g.Field<decimal>("SumOrders")
            }).ToList();

        GistogramDrawer.DrawGistogram(dataForGraphic, splitContainer1.Panel2);
        curGraphic = 1;
      }
      else if (SalesCriterCB.SelectedItem.ToString() == "по отделам")
      {
        var saleOrders =
          _db._ds.Tables["Employee"].AsEnumerable()
            .GroupBy(g => g.Field<int>("DepartmentId"))
            .Select(g => new
            {
              depId = g.First().Field<int>("DepartmentId"),
              sum = g.Sum(_ => _.Field<decimal>("SumOrders"))
            });

        dataForGraphic = new List<GistogramDrawer.PairForGraphic>();
        foreach (var order in saleOrders)
        {
          string depName = (from d in _db._ds.Tables["Department"].AsEnumerable()
            where d.Field<int>("Id") == order.depId
            select d.Field<string>("Name")).FirstOrDefault();
          dataForGraphic.Add(new GistogramDrawer.PairForGraphic { name = depName, value = order.sum });
        }
        GistogramDrawer.DrawGistogram(dataForGraphic, splitContainer1.Panel2);
        curGraphic = 1;
      }
    }

    private void ShowProfitBtn_Click(object sender, EventArgs e)
    {
      if (ProfitBeginDate.Value.Date == ProfitEndDate.Value.Date)
      {
        MessageBox.Show(@"Даты совпадают! Выберите начало и конец периода времени!", @"Период не выбран!");
        ProfitBeginDate.Focus();
        return;
      }

      if (ProfitEndDate.Value.Date - ProfitBeginDate.Value.Date < TimeSpan.FromDays(31))
      {
        MessageBox.Show(@"Малый период! Выберите больше 1 месяца!", @"Период мал!");
        ProfitBeginDate.Focus();
        return;
      }
      
      var profits = _db._ds.Tables["Products_in_Order"].AsEnumerable()
          .Where(pio => _db._ds.Tables["Order"].AsEnumerable()
                  .Where(o => (o.Field<int>("Id") == pio.Field<int>("OrderId")
                  && o.Field<string>("Category") == "Продажа")
                  && (o.Field<DateTime>("OrderDate") >= ProfitBeginDate.Value.Date
                    && o.Field<DateTime>("OrderDate") <= ProfitEndDate.Value.Date))
                  .Select(o => o).Any())
          .Select(pio => new GistogramDrawer.PairForGraphic
          {
            name =
                  _db._ds.Tables["Order"].AsEnumerable()
                  .Where(_ => _.Field<int>("Id") == pio.Field<int>("OrderId"))
                  .Select(o => o.Field<DateTime>("OrderDate").Year.ToString()
                    + "." + o.Field<DateTime>("OrderDate").Month.ToString()).First(),
            value =
                  _db._ds.Tables["Product"].AsEnumerable()
                  .Where(_ => _.Field<int>("Id") ==
                   pio.Field<int>("ProductId"))
                  .Select(p => (pio.Field<decimal>("FinalPrice") - p.Field<decimal>("PurchasePrice")) * pio.Field<int>("Quantity")).First()
          }
          );

      var depsCostsNProfitsGrouped = profits
          .OrderBy(_ => _.name)
          .GroupBy(_ => _.name)
          .Select(g => new GistogramDrawer.PairForGraphic
          {
            name = g.First().name,
            value = g.Sum(s => s.value)
          });

      dataForGraphic =
        depsCostsNProfitsGrouped.ToList();

      GistogramDrawer.DrawGistogram(dataForGraphic, splitContainer1.Panel2);
      curGraphic = 1;
    }

    private void ShowRanksBtn_Click(object sender, EventArgs e)
    {
      if (ProfitBeginDate.Value.Date == ProfitEndDate.Value.Date)
      {
        MessageBox.Show(@"Даты совпадают! Выберите начало и конец периода времени!", @"Период не выбран!");
        ProfitBeginDate.Focus();
        return;
      }

      if (ProfitEndDate.Value.Date - ProfitBeginDate.Value.Date < TimeSpan.FromDays(31))
      {
        MessageBox.Show(@"Малый период! Выберите больше 1 месяца!", @"Период мал!");
        ProfitBeginDate.Focus();
        return;
      }

      var profits = _db._ds.Tables["Products_in_Order"].AsEnumerable()
          .Where(pio => _db._ds.Tables["Order"].AsEnumerable()
                  .Where(o => (o.Field<int>("Id") == pio.Field<int>("OrderId")
                  && o.Field<string>("Category") == "Продажа")
                  && (o.Field<DateTime>("OrderDate") >= ProfitBeginDate.Value.Date
                    && o.Field<DateTime>("OrderDate") <= ProfitEndDate.Value.Date))
                  .Select(o => o).Any())
          .Select(pio => new GistogramDrawer.PairForGraphic
          {
            name =
                  _db._ds.Tables["Order"].AsEnumerable()
                  .Where(_ => _.Field<int>("Id") == pio.Field<int>("OrderId"))
                  .Select(o => o.Field<DateTime>("OrderDate").Year.ToString()
                    + "." + o.Field<DateTime>("OrderDate").Month.ToString()).First(),
            value =
                  _db._ds.Tables["Product"].AsEnumerable()
                  .Where(_ => _.Field<int>("Id") ==
                   pio.Field<int>("ProductId"))
                  .Select(p => (pio.Field<decimal>("FinalPrice") - p.Field<decimal>("PurchasePrice")) * pio.Field<int>("Quantity")).First()
          }
          );

      var depsCostsNProfitsGrouped = profits
          .OrderBy(_ => _.name)
          .GroupBy(_ => _.name)
          .Select(g => new GistogramDrawer.PairForGraphic
          {
            name = g.First().name,
            value = g.Sum(s => s.value)
          });

      dataForGraphic =
        depsCostsNProfitsGrouped.ToList();

      GistogramDrawer.DrawGistogram(dataForGraphic, splitContainer1.Panel2);
      curGraphic = 1;
    }

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
    {
      switch (curGraphic)
      {
        case 1://Gistogram
          GistogramDrawer.DrawGistogram(dataForGraphic, splitContainer1.Panel2);
          break;
        case 2://GistogramWithAccumulation
          GistogramDrawer.DrawGistogramWithAccumulation(data2ForGraphic, marksNames, splitContainer1.Panel2);
          break;
      }
    }

    #endregion


    #region Menegement tabpage methods

            private
      void newPositionBtn_Click(object sender, EventArgs e)
    {
      Position position = new Position();

      position.SetId(client.GetCurIdentity("Position") + 1);

      PositionForm pf = new PositionForm(position);
      pf.ShowDialog();

      position = pf.position;

      if (position.Id != 0)
      {
        client.SetPosition(position);
        //_waitForResponse.WaitOne();
      }
    }

    private void ChangePositionBtn_Click(object sender, EventArgs e)
    {
      Position position = new Position();

      if (dataGridViewRoles.CurrentRow != null)
      {
        position.SetValues(
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Id"].Value),
          dataGridViewRoles.CurrentRow.Cells["Title"].Value.ToString(),
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Products"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Order"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Clients"].Value) == 1,
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
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_Discount"].Value) == 1,
          Convert.ToInt32(dataGridViewRoles.CurrentRow.Cells["Client_FirmName"].Value) == 1
          );

        PositionForm pf = new PositionForm(position);
        pf.ShowDialog();

        position = pf.position;

        client.SetPosition(position);
        //_waitForResponse.WaitOne();
      }
    }

    private void NewUserBtn_Click(object sender, EventArgs e)
    {
      User user = new User();

      user.SetId(client.GetCurIdentity("User") + 1);

      UserForm uf = new UserForm(user, _db._ds.Tables["Position"], _db._ds.Tables["User"]);
      uf.ShowDialog();

      user = uf.user;

      if (user.Id != 0)
      {
        DataRow newRow = _db._ds.Tables["User"].NewRow();
        newRow["Id"] = user.Id;
        newRow["PositionId"] = user.PositionId;
        newRow["Nick"] = user.Nick;
        newRow["Password"] = user.Password;
        _db._ds.Tables["User"].Rows.Add(newRow);

        _db._adapterUser.Update(_db._ds.Tables["User"]);

        user.Clear();
      }

    }

    private void newDepToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Department dep = new Department();

      dep.SetId(client.GetCurIdentity("Department") + 1);

      DepartmentForm df = new DepartmentForm(dep);
      df.ShowDialog();

      dep = df.dep;

      if (dep.Id != 0)
      {
        client.SetDepartment(dep);

        //_waitForResponse.WaitOne();

        treeViewDepEmpl.BeginUpdate();
        treeViewDepEmpl.Nodes.Add(new TreeNode(dep.Name));
        treeViewDepEmpl.EndUpdate();
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

      Employee employee = new Employee();

      employee.SetId(client.GetCurIdentity("Employee") + 1);

      EmployeeForm ef = new EmployeeForm(
        employee,
        depNode,
        _db._ds.Tables["Department"],
        _db._ds.Tables["Position"],
        _db._ds.Tables["User"],
        client
        );
      ef.ShowDialog();

      employee = ef.employee;

      User user = ef.user;

      if (employee.Id != 0)
      {
        client.SetUser(user);
        //_waitForResponse.WaitOne();

        client.SetEmployee(employee);
        //_waitForResponse.WaitOne();
      }
    }

    private void toolStripMenuItem5_Click(object sender, EventArgs e)
    {
      Employee employee = new Employee();

      if (treeViewDepEmpl.SelectedNode != null)
      {
        DataRow dr = _db._ds.Tables["Employee"].Rows.Find(treeViewDepEmpl.SelectedNode.Name);

        employee.SetValues(
            Convert.ToInt32(dr["Id"]),
            dr["LastName"].ToString(),
            dr["FirstName"].ToString(),
            dr["MiddleName"].ToString(),
            Convert.ToInt32(dr["DepartmentId"]),
            Convert.ToInt32(dr["UserId"]),
            Convert.ToDateTime(dr["AcceptanceDate"]),
            Convert.ToDouble(dr["Bonuses"]),
            Convert.ToDouble(dr["SumOrders"])
            );

        EmployeeForm ef = new EmployeeForm(
          employee,
          employee.DepartmentId,
          _db._ds.Tables["Department"],
          _db._ds.Tables["Position"],
          _db._ds.Tables["User"],
          client
          );
        ef.ShowDialog();

        employee = ef.employee;

        if (employee.Id != 0)
        {
          client.SetEmployee(employee);
          //_waitForResponse.WaitOne();
        }
      }
    }

    private void PositionDelBtn_Click(object sender, EventArgs e)
    {
      if (dataGridViewRoles.CurrentRow != null)
      {
        if (MessageBox.Show(@"Вы уверены?", @"Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
          _db._ds.Tables["Position"].Rows.RemoveAt(dataGridViewRoles.CurrentRow.Index);
        }
      }
    }

    private void UserDelBtn_Click(object sender, EventArgs e)
    {
      if (dataGridViewUsers.CurrentRow != null)
      {
        if (MessageBox.Show(@"Вы уверены?", @"Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
          _db._ds.Tables["User"].Rows.RemoveAt(dataGridViewUsers.CurrentRow.Index);

          _db._adapterUser.Update(_db._ds.Tables["User"]);
        }
      }
    }

    private void UserEditBtn_Click(object sender, EventArgs e)
    {
      User user = new User();

      if (dataGridViewUsers.CurrentRow != null)
      {
        user.SetValues(
          Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["Id"].Value),
          Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["PositionId"].Value),
          dataGridViewUsers.CurrentRow.Cells["Nick"].Value.ToString(),
          dataGridViewUsers.CurrentRow.Cells["Password"].Value.ToString()
          );

        UserForm pf = new UserForm(user, _db._ds.Tables["Position"], _db._ds.Tables["User"]);
        pf.ShowDialog();

        if (user.Id != 0)
        {
          _db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Id"] = user.Id;
          _db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["PositionId"] = user.PositionId;
          _db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Nick"] = user.Nick;
          _db._ds.Tables["User"].Rows[dataGridViewUsers.CurrentRow.Index]["Password"] = user.Password;

          _db._adapterUser.Update(_db._ds.Tables["User"]);
        }
      }
      else
      {
        MessageBox.Show(@"Выделите строку с пользователем!");
      }
    }

    private void editDepToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Department dep = new Department();

      if (treeViewDepEmpl.SelectedNode != null)
      {
        for (int i = 0; i < _db._ds.Tables["Department"].Rows.Count; i++)
        {
          string depName = _db._ds.Tables["Department"].Rows[i]["Name"].ToString();
          if (depName == treeViewDepEmpl.SelectedNode.Text)
          {
            dep.SetValues(
              Convert.ToInt32(_db._ds.Tables["Department"].Rows[i]["Id"]),
              _db._ds.Tables["Department"].Rows[i]["Name"].ToString());

            DepartmentForm df = new DepartmentForm(dep);
            df.ShowDialog();

            dep = df.dep;

            if (dep.Id != 0)
            {
              _db._ds.Tables["Department"].Rows[i]["Name"] = dep.Name;

              _db._adapterDepartment.Update(_db._ds.Tables["Department"]);
            }

            treeViewDepEmpl.BeginUpdate();
            treeViewDepEmpl.SelectedNode.Text = dep.Name;
            treeViewDepEmpl.EndUpdate();

          }
        }
      }
    }

    private void delDepToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }


    #endregion

    #region IStoreServiceCallback membres
    
    public void DbRenew(DataBase db)
    {
      if (_db.Equals(db))
      {
        return;
      }

      _db = db;

      Invoke(new Action(() =>
      {
        bsProducts = new BindingSource { DataSource = _db._ds.Tables["Product"] };
        dataGridViewProducts.DataSource = bsProducts;
        dataGridViewProducts.Columns[0].Visible = false;
        bindingNavigatorProducts.BindingSource = bsProducts;

        ProductsBinding();

        bsClients = new BindingSource { DataSource = _db._ds.Tables["Client"] };
        dataGridViewClients.DataSource = bsClients;
        dataGridViewClients.Columns[0].Visible = false;
        bindingNavigatorClients.BindingSource = bsClients;

        ClientsBinding();

        dataGridViewRoles.DataSource = _db._ds.Tables["Position"];

        dataGridViewUsers.DataSource = _db._ds.Tables["User"];

        treeViewUpdate();

      }));
      
      //_waitForResponse.Set();
    }

    #endregion

  }
}