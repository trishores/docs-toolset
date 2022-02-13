
namespace DocsToolset
{
    partial class Ui
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ui));
            this.pbxSpinner = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnBrowseDocPath = new System.Windows.Forms.Button();
            this.txtDocFilePath = new DocsToolset.TextBoxX();
            this.btnFocus = new System.Windows.Forms.Button();
            this.tabReplace = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbReplaceOptions = new System.Windows.Forms.ComboBox();
            this.tabFindOptions = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPhraseFilePath = new DocsToolset.TextBoxX();
            this.btnOpenPhraseFile = new System.Windows.Forms.Button();
            this.btnBrowsePhraseFile = new System.Windows.Forms.Button();
            this.tabFind = new System.Windows.Forms.TabPage();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tabControl = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpinner)).BeginInit();
            this.tabReplace.SuspendLayout();
            this.tabFindOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxSpinner
            // 
            this.pbxSpinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxSpinner.Image = global::DocsToolset.Properties.Resources.spinner;
            this.pbxSpinner.Location = new System.Drawing.Point(794, 340);
            this.pbxSpinner.Name = "pbxSpinner";
            this.pbxSpinner.Size = new System.Drawing.Size(31, 31);
            this.pbxSpinner.TabIndex = 19;
            this.pbxSpinner.TabStop = false;
            this.pbxSpinner.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(781, 339);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(57, 33);
            this.btnRun.TabIndex = 18;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnBrowseDocPath
            // 
            this.btnBrowseDocPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDocPath.BackgroundImage = global::DocsToolset.Properties.Resources.folder;
            this.btnBrowseDocPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowseDocPath.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnBrowseDocPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDocPath.Location = new System.Drawing.Point(744, 341);
            this.btnBrowseDocPath.Name = "btnBrowseDocPath";
            this.btnBrowseDocPath.Size = new System.Drawing.Size(29, 29);
            this.btnBrowseDocPath.TabIndex = 17;
            this.btnBrowseDocPath.UseVisualStyleBackColor = true;
            // 
            // txtDocFilePath
            // 
            this.txtDocFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDocFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtDocFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtDocFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtDocFilePath.BorderColor = System.Drawing.SystemColors.Window;
            this.txtDocFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocFilePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtDocFilePath.Hint = "Document path";
            this.txtDocFilePath.Location = new System.Drawing.Point(14, 341);
            this.txtDocFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDocFilePath.MaxLength = 32767;
            this.txtDocFilePath.Multiline = false;
            this.txtDocFilePath.Name = "txtDocFilePath";
            this.txtDocFilePath.Padding = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.txtDocFilePath.Password = false;
            this.txtDocFilePath.ReadOnly = false;
            this.txtDocFilePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocFilePath.SelectedText = "";
            this.txtDocFilePath.SelectionLength = 0;
            this.txtDocFilePath.SelectionStart = 0;
            this.txtDocFilePath.Size = new System.Drawing.Size(731, 29);
            this.txtDocFilePath.TabIndex = 20;
            this.txtDocFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDocFilePath.WordWrap = false;
            // 
            // btnFocus
            // 
            this.btnFocus.Location = new System.Drawing.Point(10, 10);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(1, 1);
            this.btnFocus.TabIndex = 21;
            this.btnFocus.UseVisualStyleBackColor = true;
            // 
            // tabReplace
            // 
            this.tabReplace.Controls.Add(this.label1);
            this.tabReplace.Controls.Add(this.cmbReplaceOptions);
            this.tabReplace.Location = new System.Drawing.Point(4, 29);
            this.tabReplace.Name = "tabReplace";
            this.tabReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tabReplace.Size = new System.Drawing.Size(844, 303);
            this.tabReplace.TabIndex = 2;
            this.tabReplace.Text = "Replace";
            this.tabReplace.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Replace action";
            // 
            // cmbReplaceOptions
            // 
            this.cmbReplaceOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplaceOptions.FormattingEnabled = true;
            this.cmbReplaceOptions.Location = new System.Drawing.Point(30, 49);
            this.cmbReplaceOptions.Name = "cmbReplaceOptions";
            this.cmbReplaceOptions.Size = new System.Drawing.Size(491, 28);
            this.cmbReplaceOptions.TabIndex = 0;
            // 
            // tabFindOptions
            // 
            this.tabFindOptions.Controls.Add(this.groupBox1);
            this.tabFindOptions.Location = new System.Drawing.Point(4, 29);
            this.tabFindOptions.Name = "tabFindOptions";
            this.tabFindOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabFindOptions.Size = new System.Drawing.Size(844, 303);
            this.tabFindOptions.TabIndex = 1;
            this.tabFindOptions.Text = "Find options";
            this.tabFindOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPhraseFilePath);
            this.groupBox1.Controls.Add(this.btnOpenPhraseFile);
            this.groupBox1.Controls.Add(this.btnBrowsePhraseFile);
            this.groupBox1.Location = new System.Drawing.Point(21, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(803, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit phrase file";
            // 
            // txtPhraseFilePath
            // 
            this.txtPhraseFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhraseFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPhraseFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPhraseFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPhraseFilePath.BorderColor = System.Drawing.SystemColors.Window;
            this.txtPhraseFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhraseFilePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPhraseFilePath.Hint = "Phrase file path";
            this.txtPhraseFilePath.Location = new System.Drawing.Point(19, 35);
            this.txtPhraseFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPhraseFilePath.MaxLength = 32767;
            this.txtPhraseFilePath.Multiline = false;
            this.txtPhraseFilePath.Name = "txtPhraseFilePath";
            this.txtPhraseFilePath.Padding = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.txtPhraseFilePath.Password = false;
            this.txtPhraseFilePath.ReadOnly = false;
            this.txtPhraseFilePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPhraseFilePath.SelectedText = "";
            this.txtPhraseFilePath.SelectionLength = 0;
            this.txtPhraseFilePath.SelectionStart = 0;
            this.txtPhraseFilePath.Size = new System.Drawing.Size(739, 29);
            this.txtPhraseFilePath.TabIndex = 23;
            this.txtPhraseFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhraseFilePath.WordWrap = false;
            // 
            // btnOpenPhraseFile
            // 
            this.btnOpenPhraseFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenPhraseFile.Location = new System.Drawing.Point(18, 71);
            this.btnOpenPhraseFile.Name = "btnOpenPhraseFile";
            this.btnOpenPhraseFile.Size = new System.Drawing.Size(58, 32);
            this.btnOpenPhraseFile.TabIndex = 9;
            this.btnOpenPhraseFile.Text = "Open";
            this.btnOpenPhraseFile.UseVisualStyleBackColor = true;
            // 
            // btnBrowsePhraseFile
            // 
            this.btnBrowsePhraseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowsePhraseFile.BackgroundImage = global::DocsToolset.Properties.Resources.folder;
            this.btnBrowsePhraseFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowsePhraseFile.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnBrowsePhraseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePhraseFile.Location = new System.Drawing.Point(757, 35);
            this.btnBrowsePhraseFile.Name = "btnBrowsePhraseFile";
            this.btnBrowsePhraseFile.Size = new System.Drawing.Size(29, 29);
            this.btnBrowsePhraseFile.TabIndex = 8;
            this.btnBrowsePhraseFile.UseVisualStyleBackColor = true;
            // 
            // tabFind
            // 
            this.tabFind.Controls.Add(this.dgv);
            this.tabFind.Location = new System.Drawing.Point(4, 29);
            this.tabFind.Name = "tabFind";
            this.tabFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabFind.Size = new System.Drawing.Size(844, 303);
            this.tabFind.TabIndex = 0;
            this.tabFind.Text = "Find";
            this.tabFind.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(11, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 25;
            this.dgv.Size = new System.Drawing.Size(822, 284);
            this.dgv.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabFind);
            this.tabControl.Controls.Add(this.tabFindOptions);
            this.tabControl.Controls.Add(this.tabReplace);
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(852, 336);
            this.tabControl.TabIndex = 0;
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 377);
            this.Controls.Add(this.txtDocFilePath);
            this.Controls.Add(this.pbxSpinner);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnBrowseDocPath);
            this.Controls.Add(this.btnFocus);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "Ui";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Docs toolset";
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpinner)).EndInit();
            this.tabReplace.ResumeLayout(false);
            this.tabReplace.PerformLayout();
            this.tabFindOptions.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabFind.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxSpinner;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnBrowseDocPath;
        private DocsToolset.TextBoxX txtDocFilePath;
        private System.Windows.Forms.Button btnFocus;
        private System.Windows.Forms.TabPage tabReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReplaceOptions;
        private System.Windows.Forms.TabPage tabFindOptions;
        private System.Windows.Forms.GroupBox groupBox1;
        private DocsToolset.TextBoxX txtPhraseFilePath;
        private System.Windows.Forms.Button btnOpenPhraseFile;
        private System.Windows.Forms.Button btnBrowsePhraseFile;
        private System.Windows.Forms.TabPage tabFind;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TabControl tabControl;
    }
}

