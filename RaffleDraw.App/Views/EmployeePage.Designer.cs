namespace RaffleDraw.App.Views
{
    partial class EmployeePage
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.employeesComboBox = new MetroFramework.Controls.MetroComboBox();
            this.importEmployeeLink = new MetroFramework.Controls.MetroLink();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.employeesComboBox);
            this.metroPanel1.Controls.Add(this.importEmployeeLink);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 3);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1280, 717);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // employeesComboBox
            // 
            this.employeesComboBox.FormattingEnabled = true;
            this.employeesComboBox.ItemHeight = 23;
            this.employeesComboBox.Location = new System.Drawing.Point(180, 77);
            this.employeesComboBox.Name = "employeesComboBox";
            this.employeesComboBox.Size = new System.Drawing.Size(121, 29);
            this.employeesComboBox.TabIndex = 3;
            this.employeesComboBox.UseSelectable = true;
            // 
            // importEmployeeLink
            // 
            this.importEmployeeLink.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importEmployeeLink.Location = new System.Drawing.Point(31, 26);
            this.importEmployeeLink.Name = "importEmployeeLink";
            this.importEmployeeLink.Size = new System.Drawing.Size(75, 23);
            this.importEmployeeLink.TabIndex = 2;
            this.importEmployeeLink.Text = "載入員工";
            this.importEmployeeLink.UseSelectable = true;
            this.importEmployeeLink.Click += new System.EventHandler(this.importEmployeeLink_Click);
            // 
            // EmployeePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel1);
            this.Name = "EmployeePage";
            this.Size = new System.Drawing.Size(1280, 720);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLink importEmployeeLink;
        private MetroFramework.Controls.MetroComboBox employeesComboBox;
    }
}
