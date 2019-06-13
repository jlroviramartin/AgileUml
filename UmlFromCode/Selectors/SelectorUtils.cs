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
using System.Collections.Generic;
using System.Linq;

namespace UmlFromCode.Selectors
{
    public static class SelectorUtils
    {
        public static bool ContainsSimpleRegex(string value)
        {
            return value.Contains("*") || value.Contains("?");
        }

        public static bool ContainsFullRegex(string value)
        {
            return value.Contains("(");
        }

        public static IEnumerable<string> GetFixed(string[] values)
        {
            // This method points out if the string value is a not a regular expression.
            bool IsFixed(string value)
            {
                return !ContainsSimpleRegex(value) && !ContainsFullRegex(value);
            }

            return values.Where(IsFixed);
        }

        public static ISelector<string> BuildSelector(string[] values)
        {
            // This method points out if the string value is a:
            //   0: normal string
            //   1: simple regular expression.
            //   2: full regular expression.
            int SplitFunction(string value)
            {
                if (ContainsSimpleRegex(value))
                {
                    return 1;
                }
                if (ContainsFullRegex(value))
                {
                    return 2;
                }
                return 0;
            }

            IList<IEnumerable<string>> split = values.Split(SplitFunction, 3);

            ISelector<string> selector = new ConcatSelector<string>(
                new SetSelector<string>(split[0]).SequenceOfOne()
                    .Concat(split[1].Select(regex => (ISelector<string>) new SimpleRegexSelector(regex)))
                    .Concat(split[2].Select(regex => (ISelector<string>) new RegexSelector(regex)))
            );
            return selector;
        }
    }
}
