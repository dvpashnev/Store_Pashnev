namespace Store_Pashnev
{
  partial class OrderFindForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.btnFindClient = new System.Windows.Forms.Button();
      this.comboBoxCategory = new System.Windows.Forms.ComboBox();
      this.label24 = new System.Windows.Forms.Label();
      this.textBoxSumFrom = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.textBoxClient = new System.Windows.Forms.TextBox();
      this.textBoxAdress = new System.Windows.Forms.TextBox();
      this.textBoxID = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnTakeOrder = new System.Windows.Forms.Button();
      this.btnExit = new System.Windows.Forms.Button();
      this.label10 = new System.Windows.Forms.Label();
      this.textBoxSumTo = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.dateTimePickerODTo = new System.Windows.Forms.DateTimePicker();
      this.labelBD2 = new System.Windows.Forms.Label();
      this.labelBD1 = new System.Windows.Forms.Label();
      this.dateTimePickerODFrom = new System.Windows.Forms.DateTimePicker();
      this.dateTimePickerDDTo = new System.Windows.Forms.DateTimePicker();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.dateTimePickerDDFrom = new System.Windows.Forms.DateTimePicker();
      this.textBoxEmployee = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewOrders
      // 
      this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewOrders.Location = new System.Drawing.Point(12, 154);
      this.dataGridViewOrders.Name = "dataGridViewOrders";
      this.dataGridViewOrders.Size = new System.Drawing.Size(823, 256);
      this.dataGridViewOrders.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(24, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(177, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Введите критерии поиска заказа";
      // 
      // btnFindClient
      // 
      this.btnFindClient.Location = new System.Drawing.Point(429, 89);
      this.btnFindClient.Name = "btnFindClient";
      this.btnFindClient.Size = new System.Drawing.Size(92, 23);
      this.btnFindClient.TabIndex = 45;
      this.btnFindClient.Text = "Добавить";
      this.btnFindClient.UseVisualStyleBackColor = true;
      // 
      // comboBoxCategory
      // 
      this.comboBoxCategory.FormattingEnabled = true;
      this.comboBoxCategory.Items.AddRange(new object[] {
            "Продажа",
            "Возврат",
            "Поставка"});
      this.comboBoxCategory.Location = new System.Drawing.Point(276, 29);
      this.comboBoxCategory.Name = "comboBoxCategory";
      this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
      this.comboBoxCategory.TabIndex = 44;
      this.comboBoxCategory.Text = "Выберите тип";
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(244, 32);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(26, 13);
      this.label24.TabIndex = 43;
      this.label24.Text = "Тип";
      // 
      // textBoxSumFrom
      // 
      this.textBoxSumFrom.Location = new System.Drawing.Point(85, 104);
      this.textBoxSumFrom.Name = "textBoxSumFrom";
      this.textBoxSumFrom.Size = new System.Drawing.Size(62, 20);
      this.textBoxSumFrom.TabIndex = 42;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(24, 107);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(55, 13);
      this.label7.TabIndex = 41;
      this.label7.Text = "Сумма от";
      // 
      // textBoxClient
      // 
      this.textBoxClient.Location = new System.Drawing.Point(323, 91);
      this.textBoxClient.Name = "textBoxClient";
      this.textBoxClient.Size = new System.Drawing.Size(100, 20);
      this.textBoxClient.TabIndex = 39;
      // 
      // textBoxAdress
      // 
      this.textBoxAdress.Location = new System.Drawing.Point(677, 68);
      this.textBoxAdress.Multiline = true;
      this.textBoxAdress.Name = "textBoxAdress";
      this.textBoxAdress.Size = new System.Drawing.Size(158, 39);
      this.textBoxAdress.TabIndex = 38;
      // 
      // textBoxID
      // 
      this.textBoxID.Location = new System.Drawing.Point(91, 33);
      this.textBoxID.Name = "textBoxID";
      this.textBoxID.Size = new System.Drawing.Size(100, 20);
      this.textBoxID.TabIndex = 35;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(24, 76);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(57, 13);
      this.label9.TabIndex = 33;
      this.label9.Text = "Продавец";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(617, 65);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(54, 26);
      this.label8.TabIndex = 32;
      this.label8.Text = "Адрес\r\nдоставки";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(274, 94);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(43, 13);
      this.label6.TabIndex = 31;
      this.label6.Text = "Клиент";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(439, 33);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(33, 13);
      this.label5.TabIndex = 30;
      this.label5.Text = "Дата";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(617, 23);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(54, 26);
      this.label4.TabIndex = 29;
      this.label4.Text = "Дата\r\nдоставки";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(24, 36);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(61, 13);
      this.label3.TabIndex = 28;
      this.label3.Text = "Номер (ID)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(24, 138);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(109, 13);
      this.label2.TabIndex = 46;
      this.label2.Text = "Найденные заказы:";
      // 
      // btnTakeOrder
      // 
      this.btnTakeOrder.Location = new System.Drawing.Point(49, 416);
      this.btnTakeOrder.Name = "btnTakeOrder";
      this.btnTakeOrder.Size = new System.Drawing.Size(142, 23);
      this.btnTakeOrder.TabIndex = 47;
      this.btnTakeOrder.Text = "Работать с заказом";
      this.btnTakeOrder.UseVisualStyleBackColor = true;
      this.btnTakeOrder.Click += new System.EventHandler(this.btnTakeOrder_Click);
      // 
      // btnExit
      // 
      this.btnExit.Location = new System.Drawing.Point(242, 416);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(75, 23);
      this.btnExit.TabIndex = 48;
      this.btnExit.Text = "Выйти";
      this.btnExit.UseVisualStyleBackColor = true;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(154, 110);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(19, 13);
      this.label10.TabIndex = 49;
      this.label10.Text = "до";
      // 
      // textBoxSumTo
      // 
      this.textBoxSumTo.Location = new System.Drawing.Point(179, 104);
      this.textBoxSumTo.Name = "textBoxSumTo";
      this.textBoxSumTo.Size = new System.Drawing.Size(70, 20);
      this.textBoxSumTo.TabIndex = 50;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(653, 125);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 51;
      this.button1.Text = "Искать";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(759, 125);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 52;
      this.button2.Text = "Очистить";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // dateTimePickerODTo
      // 
      this.dateTimePickerODTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimePickerODTo.Location = new System.Drawing.Point(495, 45);
      this.dateTimePickerODTo.Name = "dateTimePickerODTo";
      this.dateTimePickerODTo.Size = new System.Drawing.Size(94, 20);
      this.dateTimePickerODTo.TabIndex = 56;
      // 
      // labelBD2
      // 
      this.labelBD2.AutoSize = true;
      this.labelBD2.Location = new System.Drawing.Point(470, 47);
      this.labelBD2.Name = "labelBD2";
      this.labelBD2.Size = new System.Drawing.Size(19, 13);
      this.labelBD2.TabIndex = 55;
      this.labelBD2.Text = "до";
      // 
      // labelBD1
      // 
      this.labelBD1.AutoSize = true;
      this.labelBD1.Location = new System.Drawing.Point(471, 22);
      this.labelBD1.Name = "labelBD1";
      this.labelBD1.Size = new System.Drawing.Size(18, 13);
      this.labelBD1.TabIndex = 54;
      this.labelBD1.Text = "от";
      // 
      // dateTimePickerODFrom
      // 
      this.dateTimePickerODFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimePickerODFrom.Location = new System.Drawing.Point(495, 19);
      this.dateTimePickerODFrom.Name = "dateTimePickerODFrom";
      this.dateTimePickerODFrom.Size = new System.Drawing.Size(94, 20);
      this.dateTimePickerODFrom.TabIndex = 53;
      // 
      // dateTimePickerDDTo
      // 
      this.dateTimePickerDDTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimePickerDDTo.Location = new System.Drawing.Point(696, 42);
      this.dateTimePickerDDTo.Name = "dateTimePickerDDTo";
      this.dateTimePickerDDTo.Size = new System.Drawing.Size(94, 20);
      this.dateTimePickerDDTo.TabIndex = 60;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(671, 44);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(19, 13);
      this.label11.TabIndex = 59;
      this.label11.Text = "до";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(672, 19);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(18, 13);
      this.label12.TabIndex = 58;
      this.label12.Text = "от";
      // 
      // dateTimePickerDDFrom
      // 
      this.dateTimePickerDDFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimePickerDDFrom.Location = new System.Drawing.Point(696, 16);
      this.dateTimePickerDDFrom.Name = "dateTimePickerDDFrom";
      this.dateTimePickerDDFrom.Size = new System.Drawing.Size(94, 20);
      this.dateTimePickerDDFrom.TabIndex = 57;
      // 
      // textBoxEmployee
      // 
      this.textBoxEmployee.Location = new System.Drawing.Point(87, 73);
      this.textBoxEmployee.Name = "textBoxEmployee";
      this.textBoxEmployee.Size = new System.Drawing.Size(138, 20);
      this.textBoxEmployee.TabIndex = 40;
      // 
      // OrderFindForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(859, 450);
      this.ControlBox = false;
      this.Controls.Add(this.dateTimePickerDDTo);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.dateTimePickerDDFrom);
      this.Controls.Add(this.dateTimePickerODTo);
      this.Controls.Add(this.labelBD2);
      this.Controls.Add(this.labelBD1);
      this.Controls.Add(this.dateTimePickerODFrom);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBoxSumTo);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.btnExit);
      this.Controls.Add(this.btnTakeOrder);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnFindClient);
      this.Controls.Add(this.comboBoxCategory);
      this.Controls.Add(this.label24);
      this.Controls.Add(this.textBoxSumFrom);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.textBoxEmployee);
      this.Controls.Add(this.textBoxClient);
      this.Controls.Add(this.textBoxAdress);
      this.Controls.Add(this.textBoxID);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridViewOrders);
      this.Name = "OrderFindForm";
      this.Text = "Заказ";
      this.Load += new System.EventHandler(this.OrderFindForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewOrders;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnFindClient;
    private System.Windows.Forms.ComboBox comboBoxCategory;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.TextBox textBoxSumFrom;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox textBoxClient;
    private System.Windows.Forms.TextBox textBoxAdress;
    private System.Windows.Forms.TextBox textBoxID;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnTakeOrder;
    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox textBoxSumTo;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.DateTimePicker dateTimePickerODTo;
    private System.Windows.Forms.Label labelBD2;
    private System.Windows.Forms.Label labelBD1;
    private System.Windows.Forms.DateTimePicker dateTimePickerODFrom;
    private System.Windows.Forms.DateTimePicker dateTimePickerDDTo;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.DateTimePicker dateTimePickerDDFrom;
    private System.Windows.Forms.TextBox textBoxEmployee;
  }
}