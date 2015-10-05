namespace Store_Pashnev
{
  partial class ProductForm
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
      this.textBoxQuantity = new System.Windows.Forms.TextBox();
      this.textBoxPrice = new System.Windows.Forms.TextBox();
      this.labelQuantity = new System.Windows.Forms.Label();
      this.labelProducer = new System.Windows.Forms.Label();
      this.labelDepartment = new System.Windows.Forms.Label();
      this.labelPrice = new System.Windows.Forms.Label();
      this.textBoxTitle = new System.Windows.Forms.TextBox();
      this.labelTitle = new System.Windows.Forms.Label();
      this.textBoxID = new System.Windows.Forms.TextBox();
      this.labelID = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.comboBoxDep = new System.Windows.Forms.ComboBox();
      this.comboBoxProducer = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // textBoxQuantity
      // 
      this.textBoxQuantity.Location = new System.Drawing.Point(102, 208);
      this.textBoxQuantity.Name = "textBoxQuantity";
      this.textBoxQuantity.Size = new System.Drawing.Size(124, 20);
      this.textBoxQuantity.TabIndex = 23;
      // 
      // textBoxPrice
      // 
      this.textBoxPrice.Location = new System.Drawing.Point(102, 101);
      this.textBoxPrice.Name = "textBoxPrice";
      this.textBoxPrice.Size = new System.Drawing.Size(124, 20);
      this.textBoxPrice.TabIndex = 20;
      // 
      // labelQuantity
      // 
      this.labelQuantity.AutoSize = true;
      this.labelQuantity.Location = new System.Drawing.Point(13, 202);
      this.labelQuantity.Name = "labelQuantity";
      this.labelQuantity.Size = new System.Drawing.Size(66, 26);
      this.labelQuantity.TabIndex = 19;
      this.labelQuantity.Text = "Количество\r\nна складе";
      // 
      // labelProducer
      // 
      this.labelProducer.AutoSize = true;
      this.labelProducer.Location = new System.Drawing.Point(13, 170);
      this.labelProducer.Name = "labelProducer";
      this.labelProducer.Size = new System.Drawing.Size(65, 13);
      this.labelProducer.TabIndex = 18;
      this.labelProducer.Text = "Поставщик";
      // 
      // labelDepartment
      // 
      this.labelDepartment.AutoSize = true;
      this.labelDepartment.Location = new System.Drawing.Point(13, 137);
      this.labelDepartment.Name = "labelDepartment";
      this.labelDepartment.Size = new System.Drawing.Size(38, 13);
      this.labelDepartment.TabIndex = 17;
      this.labelDepartment.Text = "Отдел";
      // 
      // labelPrice
      // 
      this.labelPrice.AutoSize = true;
      this.labelPrice.Location = new System.Drawing.Point(13, 104);
      this.labelPrice.Name = "labelPrice";
      this.labelPrice.Size = new System.Drawing.Size(33, 13);
      this.labelPrice.TabIndex = 16;
      this.labelPrice.Text = "Цена";
      // 
      // textBoxTitle
      // 
      this.textBoxTitle.Location = new System.Drawing.Point(102, 68);
      this.textBoxTitle.Name = "textBoxTitle";
      this.textBoxTitle.Size = new System.Drawing.Size(124, 20);
      this.textBoxTitle.TabIndex = 15;
      // 
      // labelTitle
      // 
      this.labelTitle.AutoSize = true;
      this.labelTitle.Location = new System.Drawing.Point(13, 71);
      this.labelTitle.Name = "labelTitle";
      this.labelTitle.Size = new System.Drawing.Size(83, 13);
      this.labelTitle.TabIndex = 14;
      this.labelTitle.Text = "Наименование";
      // 
      // textBoxID
      // 
      this.textBoxID.Location = new System.Drawing.Point(102, 33);
      this.textBoxID.Name = "textBoxID";
      this.textBoxID.Size = new System.Drawing.Size(124, 20);
      this.textBoxID.TabIndex = 13;
      // 
      // labelID
      // 
      this.labelID.AutoSize = true;
      this.labelID.Location = new System.Drawing.Point(13, 36);
      this.labelID.Name = "labelID";
      this.labelID.Size = new System.Drawing.Size(61, 13);
      this.labelID.TabIndex = 12;
      this.labelID.Text = "Номер (ID)";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(22, 253);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 24;
      this.button1.Text = "Сохранить";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(119, 253);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 25;
      this.button2.Text = "Выйти";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // comboBoxDep
      // 
      this.comboBoxDep.FormattingEnabled = true;
      this.comboBoxDep.Location = new System.Drawing.Point(102, 136);
      this.comboBoxDep.Name = "comboBoxDep";
      this.comboBoxDep.Size = new System.Drawing.Size(124, 21);
      this.comboBoxDep.TabIndex = 26;
      // 
      // comboBoxProducer
      // 
      this.comboBoxProducer.FormattingEnabled = true;
      this.comboBoxProducer.Location = new System.Drawing.Point(102, 167);
      this.comboBoxProducer.Name = "comboBoxProducer";
      this.comboBoxProducer.Size = new System.Drawing.Size(121, 21);
      this.comboBoxProducer.TabIndex = 27;
      // 
      // ProductForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(242, 293);
      this.ControlBox = false;
      this.Controls.Add(this.comboBoxProducer);
      this.Controls.Add(this.comboBoxDep);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBoxQuantity);
      this.Controls.Add(this.textBoxPrice);
      this.Controls.Add(this.labelQuantity);
      this.Controls.Add(this.labelProducer);
      this.Controls.Add(this.labelDepartment);
      this.Controls.Add(this.labelPrice);
      this.Controls.Add(this.textBoxTitle);
      this.Controls.Add(this.labelTitle);
      this.Controls.Add(this.textBoxID);
      this.Controls.Add(this.labelID);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ProductForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Товар";
      this.Load += new System.EventHandler(this.ProductForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxQuantity;
    private System.Windows.Forms.TextBox textBoxPrice;
    private System.Windows.Forms.Label labelQuantity;
    private System.Windows.Forms.Label labelProducer;
    private System.Windows.Forms.Label labelDepartment;
    private System.Windows.Forms.Label labelPrice;
    private System.Windows.Forms.TextBox textBoxTitle;
    private System.Windows.Forms.Label labelTitle;
    private System.Windows.Forms.TextBox textBoxID;
    private System.Windows.Forms.Label labelID;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ComboBox comboBoxDep;
    private System.Windows.Forms.ComboBox comboBoxProducer;
  }
}