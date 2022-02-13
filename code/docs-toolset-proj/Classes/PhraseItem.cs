/*
 * Copyright (C) 2021 Tris Shores
 * Open source software. Licensed under the MIT license: https://opensource.org/licenses/MIT
*/

using System.ComponentModel;

namespace DocsToolset
{
    /// <summary>
    /// Object that encapsulates a set of properties for a phrase.
    /// </summary>
    public class PhraseItem
    {
        [DisplayName("Found")]
        public string TargetPhrase { get; set; }
        public string Suggestion { get; set; }
        [Browsable(false)]
        public bool IsRegex { get; set; }
        [Browsable(false)]
        public bool IsFound { get; set; }

        public PhraseItem(string targetPhrase, string suggestion, string mode)
        {
            TargetPhrase = targetPhrase;
            Suggestion = suggestion;
            IsRegex = mode?.ToLower() == "regex";
        }
    }
}
