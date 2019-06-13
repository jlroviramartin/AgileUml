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
    public class ConcatSelector<T> : ISelector<T>
    {
        public ConcatSelector(params ISelector<T>[] selectors)
        {
            this.selectors = selectors;
        }

        public ConcatSelector(IEnumerable<ISelector<T>> selectors)
        {
            this.selectors = selectors;
        }

        #region private

        private readonly IEnumerable<ISelector<T>> selectors;

        #endregion

        #region ISelector<string>

        public bool Select(T item)
        {
            return selectors.Where(selector => selector.Select(item)).Any();
        }

        #endregion
    }
}
