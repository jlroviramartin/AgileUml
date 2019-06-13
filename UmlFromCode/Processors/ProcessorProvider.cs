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
using System;
using System.Collections.Generic;

namespace UmlFromCode.Processors
{
    public class ProcessorProvider<TShare> : IProcessorProvider<TShare>
    {
        public ProcessorProvider()
        {
        }

        public void Register<T>(IProcessor<T, TShare> processor)
        {
            this.mapProcessor.Add(typeof(T), processor);
        }

        #region private

        private object GetProcessor(Type type)
        {
            if (!this.mapProcessor.TryGetValue(type, out object processor))
            {
                throw new IndexOutOfRangeException();
            }
            return processor;
        }

        private readonly Dictionary<Type, object> mapProcessor = new Dictionary<Type, object>();

        #endregion

        #region IProcessorProvider<TShare>

        public IProcessor<T, TShare> GetProcessor<T>()
        {
            return (IProcessor<T, TShare>) this.GetProcessor(typeof(T));
        }

        #endregion
    }
}
