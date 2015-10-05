namespace Store_Pashnev
{
  partial class UserForm
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
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.textBoxId = new System.Windows.Forms.TextBox();
      this.comboBoxPos = new System.Windows.Forms.ComboBox();
      this.textBoxNick = new System.Windows.Forms.TextBox();
      this.textBoxPassword = new System.Windows.Forms.TextBox();
      this.textBoxConfirm = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.labelOldPassword = new System.Windows.Forms.Label();
      this.tbOldPassword = new System.Windows.Forms.TextBox();
      this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Номер (Id)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(23, 49);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(98, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Должность (роль)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(23, 79);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(27, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Ник";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(23, 144);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(80, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Новый пароль";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(23, 176);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(88, 26);
      this.label5.TabIndex = 4;
      this.label5.Text = "Подтверждение\r\nпароля";
      // 
      // textBoxId
      // 
      this.textBoxId.Location = new System.Drawing.Point(127, 16);
      this.textBoxId.Name = "textBoxId";
      this.textBoxId.Size = new System.Drawing.Size(124, 20);
      this.textBoxId.TabIndex = 5;
      // 
      // comboBoxPos
      // 
      this.comboBoxPos.FormattingEnabled = true;
      this.comboBoxPos.Location = new System.Drawing.Point(127, 46);
      this.comboBoxPos.Name = "comboBoxPos";
      this.comboBoxPos.Size = new System.Drawing.Size(124, 21);
      this.comboBoxPos.TabIndex = 6;
      // 
      // textBoxNick
      // 
      this.textBoxNick.Location = new System.Drawing.Point(128, 74);
      this.textBoxNick.Name = "textBoxNick";
      this.textBoxNick.Size = new System.Drawing.Size(123, 20);
      this.textBoxNick.TabIndex = 7;
      this.textBoxNick.TextChanged += new System.EventHandler(this.textBoxNick_TextChanged);
      // 
      // textBoxPassword
      // 
      this.textBoxPassword.Location = new System.Drawing.Point(128, 141);
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.PasswordChar = '*';
      this.textBoxPassword.Size = new System.Drawing.Size(123, 20);
      this.textBoxPassword.TabIndex = 8;
      // 
      // textBoxConfirm
      // 
      this.textBoxConfirm.Location = new System.Drawing.Point(128, 176);
      this.textBoxConfirm.Name = "textBoxConfirm";
      this.textBoxConfirm.PasswordChar = '*';
      this.textBoxConfirm.Size = new System.Drawing.Size(122, 20);
      this.textBoxConfirm.TabIndex = 9;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(33, 214);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 10;
      this.button1.Text = "Сохранить";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(159, 214);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(81, 23);
      this.button2.TabIndex = 11;
      this.button2.Text = "Отменить";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // labelOldPassword
      // 
      this.labelOldPassword.AutoSize = true;
      this.labelOldPassword.Location = new System.Drawing.Point(24, 111);
      this.labelOldPassword.Name = "labelOldPassword";
      this.labelOldPassword.Size = new System.Drawing.Size(84, 13);
      this.labelOldPassword.TabIndex = 12;
      this.labelOldPassword.Text = "Старый пароль";
      this.labelOldPassword.Visible = false;
      // 
      // tbOldPassword
      // 
      this.tbOldPassword.Enabled = false;
      this.tbOldPassword.Location = new System.Drawing.Point(127, 108);
      this.tbOldPassword.Name = "tbOldPassword";
      this.tbOldPassword.PasswordChar = '$';
      this.tbOldPassword.Size = new System.Drawing.Size(123, 20);
      this.tbOldPassword.TabIndex = 13;
      this.tbOldPassword.Visible = false;
      // 
      // errorProvider1
      // 
      this.errorProvider1.ContainerControl = this;
      // 
      // UserForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(280, 261);
      this.ControlBox = false;
      this.Controls.Add(this.tbOldPassword);
      this.Controls.Add(this.labelOldPassword);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBoxConfirm);
      this.Controls.Add(this.textBoxPassword);
      this.Controls.Add(this.textBoxNick);
      this.Controls.Add(this.comboBoxPos);
      this.Controls.Add(this.textBoxId);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(296, 300);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(296, 300);
      this.Name = "UserForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Пользователь";
      this.Load += new System.EventHandler(this.UserForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBoxId;
    private System.Windows.Forms.ComboBox comboBoxPos;
    private System.Windows.Forms.TextBox textBoxNick;
    private System.Windows.Forms.TextBox textBoxPassword;
    private System.Windows.Forms.TextBox textBoxConfirm;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label labelOldPassword;
    private System.Windows.Forms.TextBox tbOldPassword;
    private System.Windows.Forms.ErrorProvider errorProvider1;
  }
}