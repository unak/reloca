namespace reloca
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.pbxScreen = new System.Windows.Forms.PictureBox();
			this.btnPrevScreen = new System.Windows.Forms.Button();
			this.btnNextScreen = new System.Windows.Forms.Button();
			this.lblScreenName = new System.Windows.Forms.Label();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.cbxTitle = new System.Windows.Forms.CheckBox();
			this.cbxClass = new System.Windows.Forms.CheckBox();
			this.cbxProcess = new System.Windows.Forms.CheckBox();
			this.cbxLocation = new System.Windows.Forms.CheckBox();
			this.cbxSize = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnRelocate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).BeginInit();
			this.SuspendLayout();
			// 
			// pbxScreen
			// 
			this.pbxScreen.BackColor = System.Drawing.Color.Navy;
			this.pbxScreen.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbxScreen.Location = new System.Drawing.Point(0, 0);
			this.pbxScreen.Name = "pbxScreen";
			this.pbxScreen.Size = new System.Drawing.Size(556, 218);
			this.pbxScreen.TabIndex = 0;
			this.pbxScreen.TabStop = false;
			this.pbxScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxScreen_Paint);
			this.pbxScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxScreen_MouseClick);
			// 
			// btnPrevScreen
			// 
			this.btnPrevScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnPrevScreen.Location = new System.Drawing.Point(187, 224);
			this.btnPrevScreen.Name = "btnPrevScreen";
			this.btnPrevScreen.Size = new System.Drawing.Size(85, 29);
			this.btnPrevScreen.TabIndex = 1;
			this.btnPrevScreen.Text = "≪";
			this.btnPrevScreen.UseVisualStyleBackColor = true;
			this.btnPrevScreen.Click += new System.EventHandler(this.btnPrevScreen_Click);
			// 
			// btnNextScreen
			// 
			this.btnNextScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnNextScreen.Location = new System.Drawing.Point(278, 224);
			this.btnNextScreen.Name = "btnNextScreen";
			this.btnNextScreen.Size = new System.Drawing.Size(85, 29);
			this.btnNextScreen.TabIndex = 2;
			this.btnNextScreen.Text = "≫";
			this.btnNextScreen.UseVisualStyleBackColor = true;
			this.btnNextScreen.Click += new System.EventHandler(this.btnNextScreen_Click);
			// 
			// lblScreenName
			// 
			this.lblScreenName.AutoSize = true;
			this.lblScreenName.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblScreenName.Location = new System.Drawing.Point(12, 226);
			this.lblScreenName.Name = "lblScreenName";
			this.lblScreenName.Size = new System.Drawing.Size(78, 23);
			this.lblScreenName.TabIndex = 3;
			this.lblScreenName.Text = "Screen 1";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.Location = new System.Drawing.Point(459, 224);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(85, 29);
			this.btnRefresh.TabIndex = 4;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// cbxTitle
			// 
			this.cbxTitle.AutoSize = true;
			this.cbxTitle.Location = new System.Drawing.Point(10, 264);
			this.cbxTitle.Name = "cbxTitle";
			this.cbxTitle.Size = new System.Drawing.Size(47, 16);
			this.cbxTitle.TabIndex = 8;
			this.cbxTitle.Text = "Title";
			this.cbxTitle.UseVisualStyleBackColor = true;
			// 
			// cbxClass
			// 
			this.cbxClass.AutoSize = true;
			this.cbxClass.Location = new System.Drawing.Point(10, 286);
			this.cbxClass.Name = "cbxClass";
			this.cbxClass.Size = new System.Drawing.Size(53, 16);
			this.cbxClass.TabIndex = 9;
			this.cbxClass.Text = "Class";
			this.cbxClass.UseVisualStyleBackColor = true;
			// 
			// cbxProcess
			// 
			this.cbxProcess.AutoCheck = false;
			this.cbxProcess.AutoSize = true;
			this.cbxProcess.Checked = true;
			this.cbxProcess.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxProcess.Enabled = false;
			this.cbxProcess.Location = new System.Drawing.Point(10, 308);
			this.cbxProcess.Name = "cbxProcess";
			this.cbxProcess.Size = new System.Drawing.Size(65, 16);
			this.cbxProcess.TabIndex = 10;
			this.cbxProcess.Text = "Process";
			this.cbxProcess.UseVisualStyleBackColor = true;
			// 
			// cbxLocation
			// 
			this.cbxLocation.AutoSize = true;
			this.cbxLocation.Location = new System.Drawing.Point(10, 330);
			this.cbxLocation.Name = "cbxLocation";
			this.cbxLocation.Size = new System.Drawing.Size(67, 16);
			this.cbxLocation.TabIndex = 11;
			this.cbxLocation.Text = "Location";
			this.cbxLocation.UseVisualStyleBackColor = true;
			this.cbxLocation.CheckedChanged += new System.EventHandler(this.cbxLocation_CheckedChanged);
			// 
			// cbxSize
			// 
			this.cbxSize.AutoSize = true;
			this.cbxSize.Location = new System.Drawing.Point(10, 352);
			this.cbxSize.Name = "cbxSize";
			this.cbxSize.Size = new System.Drawing.Size(45, 16);
			this.cbxSize.TabIndex = 12;
			this.cbxSize.Text = "Size";
			this.cbxSize.UseVisualStyleBackColor = true;
			this.cbxSize.CheckedChanged += new System.EventHandler(this.cbxSize_CheckedChanged);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(10, 374);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(85, 29);
			this.btnSave.TabIndex = 13;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnRelocate
			// 
			this.btnRelocate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRelocate.Location = new System.Drawing.Point(459, 374);
			this.btnRelocate.Name = "btnRelocate";
			this.btnRelocate.Size = new System.Drawing.Size(85, 29);
			this.btnRelocate.TabIndex = 14;
			this.btnRelocate.Text = "Relocate All";
			this.btnRelocate.UseVisualStyleBackColor = true;
			this.btnRelocate.Click += new System.EventHandler(this.btnRelocate_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 466);
			this.Controls.Add(this.btnRelocate);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.cbxSize);
			this.Controls.Add(this.cbxLocation);
			this.Controls.Add(this.cbxProcess);
			this.Controls.Add(this.cbxClass);
			this.Controls.Add(this.cbxTitle);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.lblScreenName);
			this.Controls.Add(this.btnNextScreen);
			this.Controls.Add(this.btnPrevScreen);
			this.Controls.Add(this.pbxScreen);
			this.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.Name = "FormMain";
			this.Text = "Reloca";
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pbxScreen;
		private System.Windows.Forms.Button btnPrevScreen;
		private System.Windows.Forms.Button btnNextScreen;
		private System.Windows.Forms.Label lblScreenName;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.CheckBox cbxTitle;
		private System.Windows.Forms.CheckBox cbxClass;
		private System.Windows.Forms.CheckBox cbxProcess;
		private System.Windows.Forms.CheckBox cbxLocation;
		private System.Windows.Forms.CheckBox cbxSize;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnRelocate;
	}
}

