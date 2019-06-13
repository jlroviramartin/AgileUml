// Copyright 2019 Jose Luis Rovira Martin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Text.RegularExpressions;

namespace UmlFromCode.Selectors
{
    /// <summary>
    /// This class implements a selector that uses a full version of regular expressions.
    /// </summary>
    public class RegexSelector : ISelector<string>
    {
        public RegexSelector(string pattern, RegexOptions options = RegexOptions.None)
        {
            this.regex = new Regex(pattern, options);
        }

        #region private

        private readonly Regex regex;

        #endregion

        #region ISelector<string>

        public bool Select(string str)
        {
            return this.regex.Match(str).Success;
        }

        #endregion
    }
}
