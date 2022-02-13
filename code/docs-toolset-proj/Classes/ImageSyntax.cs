/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocsToolset
{
    public class ImageSyntax
    {
        /// <summary>
        /// Convert standard Markdown image syntax to Docs Markdown syntax.
        /// </summary>
        /// <param name="docFilePath">The file path of a text document to search.</param>
        /// <param name="isBorder">Boolean value for the image border attribute.</param>
        /// <returns>Replacement stats.</returns>
        public static async Task<(int replaceCount, int deprecatedTitleCount)> UpdateSyntaxAsync(string docFilePath, bool isBorder)
        {
            // Run I/O task asynchronously.
            (int, int) replacementStats = await Task.Run(() =>
            {
                // Read file content.
                string fileContent = File.ReadAllText(docFilePath);

                int replaceCount = 0;
                int deprecatedImageTitleCount = 0;

                // Standard Markdown syntax (2 forms): 
                //   1) ![<alt-text>](<img-path>)
                //   2) ![<alt-text>](<img-path> "<img-title>")
                // Docs Markdown syntax:
                //   :::image type="content" source="<folderPath>" alt-text="<alt text>":::

                string findPattern = @"!\[(?<altText>.*?)\]\((?<imgPath>.*?)(\s+""(?<imgTitle>.*?)""\s*)?\)";

                string newFileContent = Regex.Replace(fileContent, findPattern, m =>
                {
                    replaceCount++;

                    // Construct Docs Markdown image syntax (ensure alt-text ends in a full-stop).
                    string imgPath = m.Groups["imgPath"].ToString().Trim();
                    string altText = m.Groups["altText"].ToString().Trim().TrimEnd(new[] { '.' }) + ".";
                    string docsMd = $":::image type=\"content\" source=\"{imgPath}\" border=\"{isBorder.ToString().ToLower()}\" alt-text=\"{altText}\":::";

                    // Docs Markdown syntax does not support image titles. Track the number of titles removed.
                    if (m.Groups["imgTitle"].ToString().Length > 0)
                        deprecatedImageTitleCount++;

                    return docsMd;
                });

                // Replace file content.
                File.WriteAllText(docFilePath, newFileContent);

                // Return replacement stats.
                return (replaceCount, deprecatedImageTitleCount);
            });

            return replacementStats;
        }
    }
}
