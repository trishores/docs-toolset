/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Sheets = Microsoft.Office.Interop.Excel.Sheets;
using Application = Microsoft.Office.Interop.Excel.Application;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Windows.Forms;
using System.IO;

namespace DocsToolset
{
    internal class ExcelTools
    {
        internal static IEnumerable<PhraseItem> ReadExcelFile(string excelFilePath)
        {
            // Define Excel objects.
            Application excelApp = null;
            Workbooks excelWorkbooks = null;
            Workbook excelWorkbook = null;
            Sheets excelWorksheets = null;
            Worksheet excelWorksheet = null;
            Range excelRange = null;

            // Create list to store Excel row data.
            var phraseItemList = new List<PhraseItem>();

            try
            {
                // Start Excel and get application object.
                excelApp = new Application();
                if (excelApp == null) return null;

                // Open Excel workbook.
                excelWorkbooks = excelApp.Workbooks;
                excelWorkbook = excelWorkbooks.Open(excelFilePath, ReadOnly: true);
                excelWorksheets = excelWorkbook.Sheets;
                excelWorksheet = (Worksheet)excelWorksheets[1];
                excelRange = excelWorksheet.Cells;

                // Skip header row.
                var i = 2;

                // Store each row's data in a PhraseItem.
                do
                {
                    // Get column A data.
                    var phraseCell = excelRange[i, 1];
                    var phrase = Convert.ToString(phraseCell.Value2);
                    // Release Excel cell resource.
                    Marshal.FinalReleaseComObject(phraseCell);

                    // Exit look on first empty column A cell.
                    if (string.IsNullOrWhiteSpace(phrase)) 
                        break;

                    // Get column B data.
                    var suggCell = excelRange[i, 2];
                    var suggestion = Convert.ToString(suggCell.Value);
                    // Release Excel cell resource.
                    Marshal.FinalReleaseComObject(suggCell);

                    // Get column C data.
                    var modeCell = excelRange[i, 3];
                    var mode = Convert.ToString(modeCell.Value);
                    // Release Excel cell resource.
                    Marshal.FinalReleaseComObject(modeCell);

                    phraseItemList.Add(new PhraseItem(phrase, suggestion, mode));

                    i++;
                }
                while (true);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Excel error:\r\n{e.Message}");
                return null;
            }
            finally
            {
                // Quit Excel.
                excelWorkbook.Close(); 
                excelWorkbooks.Close();
                excelApp.Application.Quit();
                excelApp.Quit();

                // Release all Excel resources.
                Marshal.ReleaseComObject(excelRange);
                Marshal.FinalReleaseComObject(excelWorksheet);
                Marshal.FinalReleaseComObject(excelWorksheets);
                Marshal.FinalReleaseComObject(excelWorkbook);
                Marshal.FinalReleaseComObject(excelWorkbooks);
                Marshal.FinalReleaseComObject(excelApp);

                // Force garbage collection.
                GC.Collect();
            }
            return phraseItemList;
        }

        internal static void WriteExcelFile(ref string filePath, string pipeSeparatedValues)
        {
            // Define Excel objects.
            Application excelApp = null;
            Workbooks excelWorkbooks = null;
            Workbook excelWorkbook = null;
            Sheets excelWorksheets = null;
            Worksheet excelWorksheet = null;
            Range excelRange = null;
            Hyperlinks excelHyperlinks = null;

            try
            {
                // Start Excel and get application object.
                excelApp = new Application();
                excelApp.Visible = false;
                excelApp.UserControl = false;
                excelApp.DisplayAlerts = false;
                if (excelApp == null) return;

                // Open Excel workbook.
                excelWorkbooks = excelApp.Workbooks;
                excelWorkbook = excelWorkbooks.Add("");
                excelWorksheets = excelWorkbook.Sheets;
                excelWorksheet = excelWorksheets[1];
                excelHyperlinks = excelWorksheet.Hyperlinks;

                // Add table headers.
                excelRange = excelWorksheet.Range["A1", "K1"];
                excelRange.Font.Bold = true;
                excelRange.Interior.Color = XlRgbColor.rgbLightGrey;
                Marshal.FinalReleaseComObject(excelRange);

                // Get row data.
                var rowsData = pipeSeparatedValues.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var rowNumber = 1;

                foreach (var rowData in rowsData)
                {
                    // Get cell data.
                    var columnsData = rowData.Split(" ||| ", StringSplitOptions.RemoveEmptyEntries);

                    // Iterate cells in row.
                    for (var i = 0; i < columnsData.Length; i++)
                    {
                        // Separate cell text from cell comment.
                        var cellText = columnsData[i].Split("$$");

                        // Get cell reference.
                        Range cell = excelWorksheet.Cells[rowNumber, i + 1];

                        // Add cell text.
                        cell.Value2 = cellText[0].Trim();

                        // Add cell comment.
                        if (cellText.Length > 1 && cellText[1].Trim().Length > 0)
                            cell.AddComment(cellText[1].Trim());

                        // Add cell hyperlink.
                        if (columnsData[i].Trim().StartsWith("https://")) 
                            excelHyperlinks.Add(excelWorksheet.Cells[rowNumber, i + 1], columnsData[i].Trim());

                        // Release Excel cell resources.
                        Marshal.FinalReleaseComObject(cell);
                    }
                    rowNumber++;
                }

                // Apply formatting:
                excelRange = excelWorksheet.Range["A1", "K1"];
                //excelRange.EntireColumn.WrapText = false;
                excelRange.EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                excelRange.EntireColumn.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelRange.EntireColumn.AutoFit();
                Marshal.FinalReleaseComObject(excelRange);

                filePath = excelApp.GetSaveAsFilename(InitialFilename: filePath, FileFilter: "Excel files (*.xlsx), *.xlsx", FilterIndex: 1, "Select report filename").ToString();
                if (File.Exists(filePath))
                {
                    excelWorkbook.SaveAs(filePath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Excel error:\r\n{e.Message}");
            }
            finally
            {
                // Quit Excel.
                excelWorkbook.Close();
                excelWorkbooks.Close();
                excelApp.Application.Quit();
                excelApp.Quit();

                // Release all Excel resources.
                Marshal.ReleaseComObject(excelHyperlinks);
                Marshal.ReleaseComObject(excelRange);
                Marshal.FinalReleaseComObject(excelWorksheet);
                Marshal.FinalReleaseComObject(excelWorksheets);
                Marshal.FinalReleaseComObject(excelWorkbook);
                Marshal.FinalReleaseComObject(excelWorkbooks);
                Marshal.FinalReleaseComObject(excelApp);

                // Force garbage collection.
                GC.Collect();
            }
        }
    }
}
