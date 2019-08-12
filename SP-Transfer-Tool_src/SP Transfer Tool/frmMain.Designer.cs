namespace SP_Transfer_Tool
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnSelectTargetFile = new System.Windows.Forms.Button();
            this.btnSelectSRCFile = new System.Windows.Forms.Button();
            this.tbxTargetFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxSRCFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblProgress);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.btnTransfer);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectTargetFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectSRCFile);
            this.splitContainer1.Panel1.Controls.Add(this.tbxTargetFile);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.tbxSRCFile);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 98;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(698, 82);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(53, 12);
            this.lblProgress.TabIndex = 7;
            this.lblProgress.Text = "Progress";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(-1, 80);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(693, 16);
            this.progressBar1.TabIndex = 0;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(671, 10);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(117, 64);
            this.btnTransfer.TabIndex = 6;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnSelectTargetFile
            // 
            this.btnSelectTargetFile.Location = new System.Drawing.Point(614, 51);
            this.btnSelectTargetFile.Name = "btnSelectTargetFile";
            this.btnSelectTargetFile.Size = new System.Drawing.Size(32, 23);
            this.btnSelectTargetFile.TabIndex = 5;
            this.btnSelectTargetFile.Text = "...";
            this.btnSelectTargetFile.UseVisualStyleBackColor = true;
            this.btnSelectTargetFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // btnSelectSRCFile
            // 
            this.btnSelectSRCFile.Location = new System.Drawing.Point(614, 12);
            this.btnSelectSRCFile.Name = "btnSelectSRCFile";
            this.btnSelectSRCFile.Size = new System.Drawing.Size(32, 23);
            this.btnSelectSRCFile.TabIndex = 4;
            this.btnSelectSRCFile.Text = "...";
            this.btnSelectSRCFile.UseVisualStyleBackColor = true;
            this.btnSelectSRCFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // tbxTargetFile
            // 
            this.tbxTargetFile.Location = new System.Drawing.Point(95, 52);
            this.tbxTargetFile.Name = "tbxTargetFile";
            this.tbxTargetFile.ReadOnly = true;
            this.tbxTargetFile.Size = new System.Drawing.Size(518, 21);
            this.tbxTargetFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target File:";
            // 
            // tbxSRCFile
            // 
            this.tbxSRCFile.Location = new System.Drawing.Point(95, 13);
            this.tbxSRCFile.Name = "tbxSRCFile";
            this.tbxSRCFile.ReadOnly = true;
            this.tbxSRCFile.Size = new System.Drawing.Size(518, 21);
            this.tbxSRCFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SRC File:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Location = new System.Drawing.Point(3, 17);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.Size = new System.Drawing.Size(792, 326);
            this.tbxLog.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.Text = "SP Transfer Tool";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbxTargetFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxSRCFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnSelectTargetFile;
        private System.Windows.Forms.Button btnSelectSRCFile;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxLog;
    }
}

