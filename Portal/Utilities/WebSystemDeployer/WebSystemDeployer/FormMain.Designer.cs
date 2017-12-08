namespace WebSystemDeployer
{
    partial class FormMain
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
            this.cmdBrowseSource = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.cmdBrowseTarget = new System.Windows.Forms.Button();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.tabDBSchema = new System.Windows.Forms.TabPage();
            this.txtSourceFiles = new System.Windows.Forms.TextBox();
            this.cmdStartCopy = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdBrowseSource
            // 
            this.cmdBrowseSource.Location = new System.Drawing.Point(497, 12);
            this.cmdBrowseSource.Name = "cmdBrowseSource";
            this.cmdBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseSource.TabIndex = 0;
            this.cmdBrowseSource.Text = "Browse...";
            this.cmdBrowseSource.UseVisualStyleBackColor = true;
            this.cmdBrowseSource.Click += new System.EventHandler(this.cmdBrowseSource_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(63, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(428, 20);
            this.txtSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabFiles);
            this.tabControl1.Controls.Add(this.tabDBSchema);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 427);
            this.tabControl1.TabIndex = 3;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.txtTarget);
            this.tabGeneral.Controls.Add(this.cmdBrowseTarget);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(548, 317);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Target:";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(65, 19);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(381, 20);
            this.txtTarget.TabIndex = 4;
            // 
            // cmdBrowseTarget
            // 
            this.cmdBrowseTarget.Location = new System.Drawing.Point(452, 19);
            this.cmdBrowseTarget.Name = "cmdBrowseTarget";
            this.cmdBrowseTarget.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseTarget.TabIndex = 3;
            this.cmdBrowseTarget.Text = "Browse...";
            this.cmdBrowseTarget.UseVisualStyleBackColor = true;
            this.cmdBrowseTarget.Click += new System.EventHandler(this.cmdBrowseTarget_Click);
            // 
            // tabFiles
            // 
            this.tabFiles.Controls.Add(this.txtSourceFiles);
            this.tabFiles.Controls.Add(this.panel2);
            this.tabFiles.Location = new System.Drawing.Point(4, 22);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiles.Size = new System.Drawing.Size(582, 401);
            this.tabFiles.TabIndex = 1;
            this.tabFiles.Text = "Files";
            this.tabFiles.UseVisualStyleBackColor = true;
            // 
            // tabDBSchema
            // 
            this.tabDBSchema.Location = new System.Drawing.Point(4, 22);
            this.tabDBSchema.Name = "tabDBSchema";
            this.tabDBSchema.Size = new System.Drawing.Size(582, 401);
            this.tabDBSchema.TabIndex = 2;
            this.tabDBSchema.Text = "DB Schema";
            this.tabDBSchema.UseVisualStyleBackColor = true;
            // 
            // txtSourceFiles
            // 
            this.txtSourceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceFiles.Location = new System.Drawing.Point(3, 37);
            this.txtSourceFiles.Multiline = true;
            this.txtSourceFiles.Name = "txtSourceFiles";
            this.txtSourceFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSourceFiles.Size = new System.Drawing.Size(576, 361);
            this.txtSourceFiles.TabIndex = 0;
            // 
            // cmdStartCopy
            // 
            this.cmdStartCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStartCopy.Location = new System.Drawing.Point(496, 5);
            this.cmdStartCopy.Name = "cmdStartCopy";
            this.cmdStartCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdStartCopy.TabIndex = 1;
            this.cmdStartCopy.Text = "Start Copy";
            this.cmdStartCopy.UseVisualStyleBackColor = true;
            this.cmdStartCopy.Click += new System.EventHandler(this.cmdStartCopy_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdBrowseSource);
            this.panel1.Controls.Add(this.txtSource);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 46);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdStartCopy);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(576, 34);
            this.panel2.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 473);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabFiles.ResumeLayout(false);
            this.tabFiles.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdBrowseSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button cmdBrowseTarget;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.TabPage tabDBSchema;
        private System.Windows.Forms.Button cmdStartCopy;
        private System.Windows.Forms.TextBox txtSourceFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

