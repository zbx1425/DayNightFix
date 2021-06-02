/*
 * Created by SharpDevelop.
 * User: ZBX
 * Date: 2020/9/5
 * Time: 14:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace DayNightFix
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnAddFile = new System.Windows.Forms.Button();
			this.btnAddFolder = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.fileListView = new System.Windows.Forms.ListView();
			this.fileName = new System.Windows.Forms.ColumnHeader();
			this.filePath = new System.Windows.Forms.ColumnHeader();
			this.status = new System.Windows.Forms.ColumnHeader();
			this.fixWorker = new System.ComponentModel.BackgroundWorker();
			this.btnDryRun = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.btnRemoveFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnAddFile
			// 
			this.btnAddFile.Location = new System.Drawing.Point(13, 13);
			this.btnAddFile.Name = "btnAddFile";
			this.btnAddFile.Size = new System.Drawing.Size(75, 23);
			this.btnAddFile.TabIndex = 0;
			this.btnAddFile.Text = "Add Files";
			this.btnAddFile.UseVisualStyleBackColor = true;
			this.btnAddFile.Click += new System.EventHandler(this.btnAddFileClick);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Location = new System.Drawing.Point(94, 13);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(181, 23);
			this.btnAddFolder.TabIndex = 1;
			this.btnAddFolder.Text = "Add All Files in a Folder";
			this.btnAddFolder.UseVisualStyleBackColor = true;
			this.btnAddFolder.Click += new System.EventHandler(this.BtnAddFolderClick);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(567, 316);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// fileListView
			// 
			this.fileListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.fileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.fileName,
			this.filePath,
			this.status});
			this.fileListView.FullRowSelect = true;
			this.fileListView.Location = new System.Drawing.Point(13, 42);
			this.fileListView.Name = "fileListView";
			this.fileListView.Size = new System.Drawing.Size(629, 268);
			this.fileListView.TabIndex = 3;
			this.fileListView.UseCompatibleStateImageBehavior = false;
			this.fileListView.View = System.Windows.Forms.View.Details;
			this.fileListView.DoubleClick += new System.EventHandler(this.FileListViewDoubleClick);
			this.fileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileListViewKeyDown);
			// 
			// fileName
			// 
			this.fileName.Text = "Name";
			this.fileName.Width = 267;
			// 
			// filePath
			// 
			this.filePath.Text = "Path";
			this.filePath.Width = 230;
			// 
			// status
			// 
			this.status.Text = "Status";
			this.status.Width = 100;
			// 
			// fixWorker
			// 
			this.fixWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FixWorkerDoWork);
			// 
			// btnDryRun
			// 
			this.btnDryRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDryRun.Location = new System.Drawing.Point(486, 316);
			this.btnDryRun.Name = "btnDryRun";
			this.btnDryRun.Size = new System.Drawing.Size(75, 23);
			this.btnDryRun.TabIndex = 5;
			this.btnDryRun.Text = "Test Run";
			this.btnDryRun.UseVisualStyleBackColor = true;
			this.btnDryRun.Click += new System.EventHandler(this.BtnDryRunClick);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(49, 14);
			this.linkLabel1.Location = new System.Drawing.Point(13, 321);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(400, 19);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "(C) Zbx1425 2020, www.zbx1425.cn, support@zbx1425.cn\r\n";
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// btnRemoveFile
			// 
			this.btnRemoveFile.Location = new System.Drawing.Point(281, 13);
			this.btnRemoveFile.Name = "btnRemoveFile";
			this.btnRemoveFile.Size = new System.Drawing.Size(100, 23);
			this.btnRemoveFile.TabIndex = 2;
			this.btnRemoveFile.Text = "Remove Files";
			this.btnRemoveFile.UseVisualStyleBackColor = true;
			this.btnRemoveFile.Click += new System.EventHandler(this.BtnRemoveFileClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(654, 351);
			this.Controls.Add(this.btnRemoveFile);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.btnDryRun);
			this.Controls.Add(this.fileListView);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnAddFolder);
			this.Controls.Add(this.btnAddFile);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(670, 390);
			this.Name = "MainForm";
			this.Text = "DayNightFix v1.3 by zbx1425";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.ColumnHeader filePath;
		private System.Windows.Forms.Button btnRemoveFile;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button btnDryRun;
		private System.ComponentModel.BackgroundWorker fixWorker;
		private System.Windows.Forms.ColumnHeader status;
		private System.Windows.Forms.ColumnHeader fileName;
		private System.Windows.Forms.ListView fileListView;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnAddFolder;
		private System.Windows.Forms.Button btnAddFile;
	}
}
