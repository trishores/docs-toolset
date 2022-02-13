/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license.
*/

using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocsToolset
{
    public class ContractedWords
    {
        /// <summary>
        /// Contract word forms.
        /// </summary>
        /// <param name="docFilePath">The file path of a text document to modify.</param>
        /// <returns>Replacement stats.</returns>
        public static async Task<(int replaceCount, int unused)> UseWordContractionsAsync(string docFilePath)
        {
            // Run I/O task asynchronously.
            (int, int) replacementStats = await Task.Run(() =>
            {
                // Read file content.
                string fileContent = File.ReadAllText(docFilePath);

                int replaceCount = 0;
                int unused = 0;

                (string, string)[] findPatterns = 
                {
                    (@"([Aa])re not", "aren't"),
                    (@"([Cc])annot", "can't"),
                    (@"([Cc])an not", "can't"),
                    (@"([Cc])ould not", "couldn't"),
                    (@"([Dd])id not", "didn't"),
                    (@"([Dd])o not", "don't"),
                    (@"([Dd])oes not", "doesn't"),
                    (@"([Ll])et us", "let's"),
                    (@"([Hh])ere is", "here's"),
                    (@"([Ii])t is", "it's"),
                    (@"([Ii])s not", "isn't"),
                    //(@"([Ii])t will", "it'll"),   // acrolinx flags this contraction as rare.
                    (@"([Ss])hould not", "shouldn't"),
                    (@"([Tt])hat is", "that's"),
                    (@"([Tt])here is", "there's"),
                    (@"([Tt])hey are", "they're"),
                    (@"([Ww])ill not", "won't"),
                    (@"([Ww])ould not", "wouldn't"),
                    (@"([Yy])ou are", "you're"),
                    (@"([Yy])ou have(?! the)", "you've"), // ignore phrases such as "you have the option to...".
                    (@"([Yy])ou will", "you'll"),
                };

                string newFileContent = fileContent;

                foreach (var findPattern in findPatterns)
                {
                    newFileContent = Regex.Replace(newFileContent, $@"\b{findPattern.Item1}\b", m =>
                    {
                        replaceCount++;

                        // Construct contraction.
                        string contractedForm = m.Groups[1].ToString() + findPattern.Item2.Substring(1);
                        return contractedForm;
                    });
                }

                // Replace file content.
                File.WriteAllText(docFilePath, newFileContent);

                // Return replacement stats.
                return (replaceCount, unused);
            });

            return replacementStats;
        }
    }
}
