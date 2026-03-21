using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using WCMS.Common.Utilities;

namespace WebSystemDeployer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void cmdBrowseSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtSource.Text = dialog.SelectedPath;
            }

            EnableOptions();
        }

        private void EnableOptions()
        {
            var source = txtSource.Text.Trim();

            tabControl1.Enabled = !string.IsNullOrEmpty(source);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            EnableOptions();
        }

        private void cmdBrowseTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtTarget.Text = dialog.SelectedPath;
            }
        }

        private void cmdStartCopy_Click(object sender, EventArgs e)
        {
            bool copyMade = false;

            var sourceFolder = txtSource.Text.Trim();
            var targetFolder = txtTarget.Text.Trim();
            if (Directory.Exists(sourceFolder) && Directory.Exists(targetFolder))
            {
                StringReader reader = new StringReader(txtSourceFiles.Text.Trim());
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var sourceFile = FileHelper.Combine(sourceFolder, line);
                    var targetFile = FileHelper.Combine(targetFolder, line);

                    if (File.Exists(sourceFile))
                    {
                        var targetFileFolder = FileHelper.GetFolder(targetFile);
                        if (!Directory.Exists(targetFileFolder))
                            Directory.CreateDirectory(targetFileFolder);

                        File.Copy(sourceFile, targetFile, true);

                        if (!copyMade)
                            copyMade = true;
                    }
                }
            }

            if (copyMade)
                MessageBox.Show("Copy operation done!");
            else
                MessageBox.Show("Nothing to copy.");
        }
    }
}
