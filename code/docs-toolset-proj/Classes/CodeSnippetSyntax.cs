/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocsToolset
{
    public class CodeSnippetSyntax
    {
        /// <summary>
        /// Convert standard Markdown snippet syntax to Docs Markdown syntax.
        /// </summary>
        /// <param name="docFilePath">The file path of a text document to search.</param>
        /// <returns>Replacement stats.</returns>
        public static async Task<(int, int, int)> UpdateSyntaxAsync(string docFilePath)
        {
            // Run I/O task asynchronously.
            (int, int, int) replacementStats = await Task.Run(() =>
            {
                // Read file content.
                string fileContent = File.ReadAllText(docFilePath);

                int replaceCount = 0;
                int missingSnippetIdCount = 0;
                int deprecatedSnippetNameCount = 0;

                // Standard Markdown syntax (forms): 
                //   1) [!code-<codeLang>(<codefilePath>)]
                //   2) [!code-<codeLang>[<snippetName>](<codefilePath>)]
                //   3) [!code-<codeLang>(<codefilePath>#<snippetId>)]
                //   2) [!code-<codeLang>[<snippetName>](<codefilePath>#<snippetId>)]
                // Docs Markdown syntax:
                //   :::code language="<codeLang>" source="<codefilePath>" id="<snippetId>":::

                string findPattern = @"\[!code-(?<codeLang>.*?)(\[(?<snippetName>.*?)\])?\((?<codefilePath>.*?)(#(?<snippetId>.*?))?\)\]";

                string newFileContent = Regex.Replace(fileContent, findPattern, m =>
                {
                    replaceCount++;

                    // Construct Docs Markdown code snippet syntax.
                    string codeLang = m.Groups["codeLang"].ToString().Trim();
                    string snippetName = m.Groups["snippetName"].ToString().Trim();
                    string codefilePath = m.Groups["codefilePath"].ToString().Trim();
                    string snippetId = m.Groups["snippetId"].ToString().Trim();
                    string docsMd = $":::code language=\"{codeLang}\" source=\"{codefilePath}\" id=\"{snippetId}\":::";

                    // Track the number of missing snippetIds.
                    if (m.Groups["snippetId"].ToString().Length == 0)
                        missingSnippetIdCount++;

                    // Docs Markdown syntax doesn't support snippet names. Track the number of names removed.
                    if (m.Groups["snippetName"].ToString().Length > 0)
                        deprecatedSnippetNameCount++;

                    return docsMd;
                });

                // Replace file content.
                File.WriteAllText(docFilePath, newFileContent);

                // Return replacement stats.
                return (replaceCount, missingSnippetIdCount, deprecatedSnippetNameCount);
            });

            return replacementStats;
        }
    }
}
