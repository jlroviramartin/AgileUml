﻿// Copyright 2019 Jose Luis Rovira Martin
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
namespace UmlFromCode.PlantUml
{
    public abstract class Relationship<TEnd1, TEnd2> /*: MemberInfo*/
    {
        public Relationship(TEnd1 end1, TEnd2 end2, string name = null)
        {
            this.End1 = end1;
            this.End2 = end2;
            this.Name = name;
        }

        public TEnd1 End1 { get; }
        public TEnd2 End2 { get; }

        public string Name { get; }
    }
}
