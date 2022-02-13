/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocsToolset
{
    public partial class Ui : Form
    {
        #region Initialization

        public Ui()
        {
            InitializeComponent();

            // Defaults.
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            Text = $"Docs toolset v{version}";
            cmbReplaceOptions.Items.Add("Convert Markdown image syntax to Docs Markdown");
            cmbReplaceOptions.Items.Add("Convert legacy code snippet reference syntax to Docs Markdown");
            cmbReplaceOptions.Items.Add("Replace words with their contracted form");
            cmbReplaceOptions.SelectedIndex = -1;

            // Events.
            Shown += Ui_Shown;
            btnBrowsePhraseFile.Click += BtnBrowsePhraseFile_Click;
            btnBrowseDocPath.Click += BtnBrowseDocPath_Click;
            btnOpenPhraseFile.Click += BtnOpenPhraseFile_Click;
            btnRun.Click += BtnRun_Click;
            tabFind.Enter += TabFind_Enter;
            tabFindOptions.Enter += TabFindOptions_Enter;
            tabReplace.Enter += TabReplace_Enter;
        }

        private void Ui_Shown(object sender, EventArgs e)
        {
            // Restore saved paths.
            txtPhraseFilePath.Text = Properties.Settings.Default.PhraseFilePath;
            txtDocFilePath.Text = Properties.Settings.Default.DocFilePath;

            // Initialize DataGridView.
            InitializeDgv();
        }

        #endregion

        #region UI events

        #region Find tab events

        private void TabFind_Enter(object sender, EventArgs e)
        {
            btnRun.Enabled = true;
            txtDocFilePath.Enabled = true;
            btnBrowseDocPath.Enabled = true;
        }

        private void BtnBrowseDocPath_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Directory.Exists(Path.GetDirectoryName(txtDocFilePath.Text)))
                openFileDialog.InitialDirectory = Path.GetDirectoryName(txtDocFilePath.Text);
            openFileDialog.Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            txtDocFilePath.Text = openFileDialog.FileName;
            btnRun.Focus();

            // Save doc file path.
            Properties.Settings.Default.DocFilePath = openFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Find options tab events

        private void TabFindOptions_Enter(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            txtDocFilePath.Enabled = true;
            btnBrowseDocPath.Enabled = true;
        }

        private void BtnBrowsePhraseFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Directory.Exists(Path.GetDirectoryName(txtPhraseFilePath.Text)))
                openFileDialog.InitialDirectory = Path.GetDirectoryName(txtPhraseFilePath.Text);
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            txtPhraseFilePath.Text = openFileDialog.FileName;
            btnRun.Focus();

            // Save phrase file path.
            Properties.Settings.Default.PhraseFilePath = openFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        private void BtnOpenPhraseFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtPhraseFilePath.Text))
            {
                MessageBox.Show("Phrase file not found", "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new Process
            {
                StartInfo = new ProcessStartInfo(txtPhraseFilePath.Text)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        #endregion

        #region Replace tab events

        private void TabReplace_Enter(object sender, EventArgs e)
        {
            btnRun.Enabled = true;
            txtDocFilePath.Enabled = true;
            btnBrowseDocPath.Enabled = true;
        }

        #endregion

        #region Run button events

        private async void BtnRun_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate doc file path.
                if ((Equals(tabControl.SelectedTab.Text, "Find") || Equals(tabControl.SelectedTab.Text, "Replace")) && !File.Exists(txtDocFilePath.Text))
                {
                    MessageBox.Show("File not found", "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate phrase file path.
                if (Equals(tabControl.SelectedTab.Text, "Find") && !File.Exists(txtPhraseFilePath.Text))
                {
                    MessageBox.Show("Phrase file not found. Select the Options tab and enter the path to the phrase file, which is typically located in the root folder of the project.", "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save doc path.
                Properties.Settings.Default.DocFilePath = txtDocFilePath.Text;
                Properties.Settings.Default.Save();

                // Set UI busy indicators.
                ShowUiWaitAnimation(true);

                // Select action.
                if (Equals(tabControl.SelectedTab.Text, "Find"))
                {
                    await FindTargetPhrasesAsync(docFilePath: txtDocFilePath.Text, phraseFilePath: txtPhraseFilePath.Text);
                }
                else if (Equals(tabControl.SelectedTab.Text, "Replace") && cmbReplaceOptions.SelectedIndex == -1)
                {
                    MessageBox.Show("First select a replace action.", "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (Equals(tabControl.SelectedTab.Text, "Replace") && cmbReplaceOptions.Text.Contains("image syntax"))
                {
                    var dr = MessageBox.Show("Add a border?", "Docs toolset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Cancel)
                        return;
                    await UpdateImageSyntaxAsync(docFilePath: txtDocFilePath.Text, isBorder: dr == DialogResult.Yes);
                }
                else if (Equals(tabControl.SelectedTab.Text, "Replace") && cmbReplaceOptions.Text.Contains("code snippet reference syntax"))
                {
                    await UpdateCodeSnippetSyntaxAsync(docFilePath: txtDocFilePath.Text);
                }
                else if (Equals(tabControl.SelectedTab.Text, "Replace") && cmbReplaceOptions.Text.Contains("contracted"))
                {
                    await UseWordContractionsAsync(docFilePath: txtDocFilePath.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowUiWaitAnimation(false);
            }
        }

        #region Phrase search stub

        private async Task FindTargetPhrasesAsync(string docFilePath, string phraseFilePath)
        {
            // Run I/O task asynchronously.
            var phraseItemList = await PhraseSearch.FindPhrasesAsync(docFilePath, phraseFilePath);

            // Display phrase items.
            var bindingList = new BindingList<PhraseItem>();
            phraseItemList.Where(x => x.IsFound).ToList().ForEach(x => bindingList.Add(x));
            dgv.DataSource = bindingList;
            DgvResize(null, null);

            ShowUiWaitAnimation(false);

            // Handle no results found.
            if (!phraseItemList.Any(x => x.IsFound))
            {
                MessageBox.Show("None found", "Docs toolset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Replace image syntax stub

        private async Task UpdateImageSyntaxAsync(string docFilePath, bool isBorder = true)
        {
            // Run I/O task asynchronously.
            (int replaceCount, int deprecatedTitleCount) = await ImageSyntax.UpdateSyntaxAsync(docFilePath, isBorder);

            ShowUiWaitAnimation(false);

            // Show result stats.
            if (replaceCount > 0)
            {
                string updatedSyntaxMsg = $"{replaceCount} standard Markdown image(s) replaced with Docs Markdown.";
                string deprecatedTitleMsg = deprecatedTitleCount > 0 ? $" {deprecatedTitleCount} image title(s) removed." : "";
                MessageBox.Show(updatedSyntaxMsg + deprecatedTitleMsg);
            }
            else
            {
                MessageBox.Show("No standard Markdown image(s) found.");
            }
        }

        #endregion

        #region Replace code-snippet reference syntax stub

        private async Task UpdateCodeSnippetSyntaxAsync(string docFilePath)
        {
            // Run I/O task asynchronously.
            (int replaceCount, int missingSnippetIdCount, int deprecatedSnippetNameCount) = await CodeSnippetSyntax.UpdateSyntaxAsync(docFilePath);

            ShowUiWaitAnimation(false);

            // Show result stats.
            if (replaceCount > 0)
            {
                string updatedSyntaxMsg = $"{replaceCount} legacy code snippet reference(s) replaced with Docs Markdown.";
                string missingSnippetIdMsg = missingSnippetIdCount > 0 ? $" {missingSnippetIdCount} snippet ID(s) are missing." : "";
                string deprecatedSnippetNameMsg = deprecatedSnippetNameCount > 0 ? $" {deprecatedSnippetNameCount} snippet name(s) removed." : "";
                MessageBox.Show(updatedSyntaxMsg + missingSnippetIdMsg + deprecatedSnippetNameMsg);
            }
            else
            {
                MessageBox.Show("No legacy code snippet reference(s) found.");
            }
        }

        #endregion

        #region Apply contractions stub

        private async Task UseWordContractionsAsync(string docFilePath)
        {
            // Run I/O task asynchronously.
            (int replaceCount, int unused1) = await ContractedWords.UseWordContractionsAsync(docFilePath);

            ShowUiWaitAnimation(false);

            // Show result stats.
            if (replaceCount > 0)
            {
                string updatedSyntaxMsg = $"{replaceCount} word contractions applied.";
                MessageBox.Show(updatedSyntaxMsg);
            }
            else
            {
                MessageBox.Show("No word contractions applied.");
            }
        }

        #endregion

        #endregion

        #endregion

        #region UI helper methods

        #region Animation methods

        private void ShowUiWaitAnimation(bool animate)
        {
            // Set temporary focus during animation.
            if (animate) btnFocus.Focus();
            else btnRun.Focus();

            // Handle animation.
            pbxSpinner.Visible = animate;
            btnRun.Visible = !animate;
        }

        #endregion

        #region DataGridView helper methods

        private void InitializeDgv()
        {
            // Defaults.
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = true;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersVisible = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoGenerateColumns = true;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.BackgroundColor = Color.FromArgb(250, 250, 250);
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv.ReadOnly = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(160, 160, 160);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 10.25F, FontStyle.Bold);
            dgv.RowsDefaultCellStyle.Font = new Font("Verdana", 10.25F, FontStyle.Regular);

            // Events.
            dgv.Resize += DgvResize;
        }

        private void DgvResize(object sender, EventArgs e)
        {
            var widthList = new List<int>();

            // Set column autosize mode and store widths.
            for (var i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                widthList.Add(dgv.Columns[i].Width);
            }

            // Remove column autosize mode and set column widths.
            for (var i = 0; i < widthList.Count; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = widthList[i];
            }

            // Expand last column to fill table.
            if (dgv.Columns.Count > 0)
            {
                dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                widthList.RemoveAt(dgv.Columns.Count - 1);
            }
        }

        #endregion

        #endregion
    }
}