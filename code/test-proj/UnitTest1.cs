using DocsToolset;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests : BaseTest
    {
        [Test, Category("Find")]
        public async Task TestFindPhrases()
        {
            var docFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "testdocs", "testdoc.md");
            var phraseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "testdocs", "phrases-test.xlsx");

            // Call method under test.
            var phraseItemList =
                await PhraseSearch.FindPhrasesAsync(docFilePath, phraseFilePath);

            // Validate.
            var foundPhraseCount = phraseItemList.Where(x => x.IsFound).Count();
            Assert.IsTrue(foundPhraseCount == 16);
        }

        [Test, Category("Replace")]
        public async Task TestReplaceImageSyntax()
        {
            var docFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "testdocs", "testdoc-image-syntax1.md");

            var resFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "testdocs", "testdoc-image-syntax2.md");

            // Get original content.
            var origDocContent = File.ReadAllText(docFilePath);

            try
            {
                // Call method under test.
                (int replaceCount, int deprecatedTitleCount) = 
                    await ImageSyntax.UpdateSyntaxAsync(docFilePath, isBorder: true);

                // Create result file content.
                //File.WriteAllText(resFilePath, File.ReadAllText(docFilePath));

                // Validate.
                Assert.IsTrue(replaceCount == 10);
                Assert.IsTrue(deprecatedTitleCount == 5);
                Assert.IsTrue(string.Equals(File.ReadAllText(docFilePath), File.ReadAllText(resFilePath)));
            }
            finally
            {
                // Restore original content.
                File.WriteAllText(docFilePath, origDocContent);
            }
        }

        [Test, Category("Replace")]
        public async Task TestCodeSnippetRefSyntax()
        {
            var docFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "testdocs", "testdoc-snippet-syntax1.md");

            var resFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "testdocs", "testdoc-snippet-syntax2.md");

            // Save original content.
            var origDocContent = File.ReadAllText(docFilePath);

            try
            {
                // Call method under test.
                (int replaceCount, int missingSnippetIdCount, int deprecatedSnippetNameCount) = 
                    await CodeSnippetSyntax.UpdateSyntaxAsync(docFilePath);

                // Create result file content.
                //File.WriteAllText(resFilePath, File.ReadAllText(docFilePath));

                // Validate.
                Assert.IsTrue(replaceCount == 10);
                Assert.IsTrue(missingSnippetIdCount == 4);
                Assert.IsTrue(deprecatedSnippetNameCount == 5);
                Assert.IsTrue(string.Equals(File.ReadAllText(docFilePath), File.ReadAllText(resFilePath)));
            }
            finally
            {
                // Restore original content.
                File.WriteAllText(docFilePath, origDocContent);
            }
        }
    }
}