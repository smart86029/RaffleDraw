namespace RaffleDraw.App
{
    partial class MainForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pageTabControl = new MetroFramework.Controls.MetroTabControl();
            this.prizeTabPage = new MetroFramework.Controls.MetroTabPage();
            this.pageTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageTabControl
            // 
            this.pageTabControl.Controls.Add(this.prizeTabPage);
            this.pageTabControl.Location = new System.Drawing.Point(38, 103);
            this.pageTabControl.Name = "pageTabControl";
            this.pageTabControl.SelectedIndex = 0;
            this.pageTabControl.Size = new System.Drawing.Size(1148, 509);
            this.pageTabControl.TabIndex = 0;
            this.pageTabControl.UseSelectable = true;
            // 
            // prizeTabPage
            // 
            this.prizeTabPage.HorizontalScrollbarBarColor = true;
            this.prizeTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this.prizeTabPage.HorizontalScrollbarSize = 10;
            this.prizeTabPage.Location = new System.Drawing.Point(4, 38);
            this.prizeTabPage.Name = "prizeTabPage";
            this.prizeTabPage.Size = new System.Drawing.Size(1140, 467);
            this.prizeTabPage.TabIndex = 0;
            this.prizeTabPage.Text = "獎項";
            this.prizeTabPage.VerticalScrollbarBarColor = true;
            this.prizeTabPage.VerticalScrollbarHighlightOnWheel = false;
            this.prizeTabPage.VerticalScrollbarSize = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pageTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "中華電信國際分公司感恩餐會抽獎";
            this.pageTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl pageTabControl;
        private MetroFramework.Controls.MetroTabPage prizeTabPage;
    }
}

