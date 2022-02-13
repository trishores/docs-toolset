/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocsToolset
{
    public class PhraseSearch
    {
        /// <summary>
        /// Search for non-ideal words and phrases in a text document and display suggested replacements.
        /// </summary>
        /// <param name="docFilePath">The file path of a text document to search.</param>
        /// <param name="phraseFilePath">The file path of an Excel file containing target phrases and suggested replacements.</param>
        /// <returns>List of PhraseItems indicating found status.</returns>
        public static async Task<IEnumerable<PhraseItem>> FindPhrasesAsync(string docFilePath, string phraseFilePath)
        {
            // Run I/O task asynchronously.
            var phraseItemList = await Task.Run(() =>
            {
                var phraseItemList = ExcelTools.ReadExcelFile(phraseFilePath);

                // Read file content.
                var fileContent = File.ReadAllText(docFilePath);

                // Find phrase items.
                foreach (var phraseItem in phraseItemList)
                {
                    if (phraseItem.IsRegex)
                    {
                        // User regex search.
                        var phrasePattern = phraseItem.TargetPhrase;
                        var phraseMatches = Regex.Matches(fileContent, phrasePattern);
                        foreach (Match match in phraseMatches)
                        {
                            phraseItemList.First(x => x.TargetPhrase == phraseItem.TargetPhrase).IsFound = true;
                            break;
                        }
                    }
                    else
                    {
                        // Boundary search 1.
                        var phrasePattern = @"\b" + Regex.Escape(phraseItem.TargetPhrase.ToLower()) + @"\b";
                        var phraseMatches = Regex.Matches(fileContent.ToLower(), phrasePattern);
                        foreach (Match match in phraseMatches)
                        {
                            phraseItemList.First(x => x.TargetPhrase == match.Value).IsFound = true;
                        }

                        // Boundary search 2.
                        var edgePhrasePattern = @"(?<!\w)" + Regex.Escape(phraseItem.TargetPhrase.ToLower()) + @"(?!\w)";
                        var edgePhraseMatches = Regex.Matches(fileContent.ToLower(), edgePhrasePattern);
                        foreach (Match match in edgePhraseMatches)
                        {
                            phraseItemList.First(x => x.TargetPhrase == match.Value).IsFound = true;
                        }
                    }
                }

                return phraseItemList;
            });

            return phraseItemList;
        }
    }
}
