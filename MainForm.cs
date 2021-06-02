/*
 * Created by SharpDevelop.
 * User: ZBX
 * Date: 2020/9/5
 * Time: 14:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DayNightFix
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		Dictionary<string, int> fileList = new Dictionary<string, int>();
		
		void btnAddFileClick(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog() {
				Title = "Select Model Files",
				Filter = "CSV Model|*.csv|B3D Model|*.b3d",
				Multiselect = true,
				RestoreDirectory = true
			};
			int filesAdded = 0;
			if (ofd.ShowDialog() == DialogResult.OK) {
				fileListView.BeginUpdate();
				foreach (var file in ofd.FileNames) {
					if (!fileList.ContainsKey(file)) {
						UpdateFileList(file, 0, false);
						filesAdded++;
					}
				}
				fileListView.EndUpdate();
				fileListView.Items[fileListView.Items.Count - 1].EnsureVisible();
				if (filesAdded < ofd.FileNames.Length) {
					MessageBox.Show(string.Format("{0} new files added to the list, while {1} files selected are already in the list.",
	                  filesAdded, ofd.FileNames.Length - filesAdded), 
	                  "Duplicate Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}
		
		void BtnAddFolderClick(object sender, EventArgs e)
		{
			var fbd = new DayNightFix.FolderBrowserDialog.FolderSelectDialog() {
				Title = "Select Directory (Subdirectories will also be searched)"
			};
			int filesAdded = 0;
			if (Directory.Exists(MainSettings.Default.LastDirDirectory)) {
				fbd.InitialDirectory = MainSettings.Default.LastDirDirectory;
			}
			if (fbd.Show(this.Handle)) {
				MainSettings.Default.LastDirDirectory = fbd.FileName;
				MainSettings.Default.Save();
				var allowedExtensions = new [] {".csv", ".b3d"};
				var allFiles = Directory.GetFiles(fbd.FileName, "*.*", SearchOption.AllDirectories)
					.Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToArray();
				fileListView.BeginUpdate();
				foreach (var file in allFiles) {
					if (!fileList.ContainsKey(file)) {
						UpdateFileList(file, 0, false);
						filesAdded++;
					}
				}
				fileListView.EndUpdate();
				fileListView.Items[fileListView.Items.Count - 1].EnsureVisible();
				if (filesAdded < allFiles.Length) {
					MessageBox.Show(string.Format("{0} new files added to the list, while {1} files selected are already in the list.",
	                  filesAdded, allFiles.Length - filesAdded), 
	                  "Duplicate Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}
		
		void UpdateFileList(string key, int state, bool notDryRun){
			string[] stateMsg = {"Pending", "Processing", "Good", "Fixed", "Will Fix"};
			string message = (state > 10) ? (stateMsg[notDryRun ? 3 : 4] + " " + (state - 10).ToString()) : stateMsg[state];
			if (fileList.ContainsKey(key)) {
				fileList[key] = state;
				fileListView.Items[key].SubItems[2].Text = message;
				fileListView.Items[key].EnsureVisible();
			} else {
				fileList.Add(key, state);
				fileListView.Items.Add(key, Path.GetFileName(key), "");
				fileListView.Items[key].SubItems.Add(key);
				fileListView.Items[key].SubItems.Add(message);
			}
		}
		
		void BtnStartClick(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do not forget to backup your files, if they are very important. " +
			                    "Input files are going to be directly modified. Proceed?", "Notice",
			                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
			                    == DialogResult.OK) {
				if (!fixWorker.IsBusy) fixWorker.RunWorkerAsync(true);
			}
		}
		
		void FixWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			this.Invoke(new Action<bool>(setBtnsEnabled), false);
			int count = SyntaxPatch.PatchAllFiles(fileList.Keys.ToArray(), updateUI, (bool)e.Argument);
			this.Invoke(new Action<bool>(setBtnsEnabled), true);
			if ((bool)e.Argument) {
				MessageBox.Show("Done. " + count.ToString() + " file(s) fixed.", "Completed", 
			                MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				MessageBox.Show("Done. " + count.ToString() + " file(s) will be modified if you press " +
				                "\"Start\".", "Test Run Completed",
			                MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		
		void setBtnsEnabled(bool enabled){
			btnStart.Enabled = btnDryRun.Enabled 
				= btnAddFile.Enabled = btnAddFolder.Enabled = btnRemoveFile.Enabled
				= enabled;
			this.UseWaitCursor = !enabled;
		}
		
		void updateUI(string key, int state, bool notDryRun) {
			this.BeginInvoke(new Action<string, int, bool>(UpdateFileList), key, state, notDryRun);
		}
		
		void BtnDryRunClick(object sender, EventArgs e)
		{
			if (!fixWorker.IsBusy) fixWorker.RunWorkerAsync(false);
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://www.zbx1425.tk");
		}
		
		void FileListViewDoubleClick(object sender, EventArgs e)
		{
			if (fileListView.SelectedIndices.Count > 0 && fileListView.SelectedItems[0].SubItems[1].Text != ""){
				System.Windows.Forms.Clipboard.SetText(fileListView.SelectedItems[0].SubItems[1].Text);
			}
		}
		
		void BtnRemoveFileClick(object sender, EventArgs e)
		{
			if (fileListView.SelectedItems.Count == 0) {
				if (MessageBox.Show("None selected. Remove all?", "Notice",
			                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
			                    == DialogResult.Yes) {
					fileList.Clear();
					fileListView.Items.Clear();
				}
			} else {
				fileListView.BeginUpdate();
				foreach (ListViewItem item in fileListView.SelectedItems) {
					fileList.Remove(item.Text);
					fileListView.Items.Remove(item);
				}
				fileListView.EndUpdate();
			}
		}
		
		void FileListViewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete) {
				BtnRemoveFileClick(null, null);
			} else if (e.KeyCode == Keys.Insert || e.KeyCode == Keys.I) {
				if (e.Control) {
					BtnAddFolderClick(null, null);
				} else {
					btnAddFileClick(null, null);
				}
			}
		}
	}
}
