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
using System.Reflection;

namespace UmlFromCode.PlantUml
{
    public class Association : Relationship<Type, Type>
    {
        public static Association New(
            Type @class, MemberInfo member, EndType endType, string label,
            Type @class2,
            string name,
            Direction direction = Direction.None)
        {
            return new Association(@class, member, endType, label,
                                   class2, null, EndType.None, null,
                                   name, direction);
        }

        public static Association New(
            Type class1, MemberInfo member1, EndType endType1, string label1,
            Type class2, MemberInfo member2, EndType endType2, string label2,
            string name,
            Direction direction = Direction.None)
        {
            if (Compare(class1, member1, class2, member2) > 0)
            {
                // Reverse.
                return new Association(class2, member2, endType2, label2,
                                       class1, member1, endType1, label1,
                                       name, direction);
            }
            else
            {
                return new Association(class1, member1, endType1, label1,
                                       class2, member2, endType2, label2,
                                       name, direction);
            }
        }

        public Type Class1 => this.End1;
        public Type Class2 => this.End2;

        public MemberInfo Member1 { get; }
        public MemberInfo Member2 { get; }

        public Direction Direction { get; }

        public EndType EndType1 { get; }
        public EndType EndType2 { get; }

        public string Label1 { get; }
        public string Label2 { get; }

        #region private

        private Association(
            Type class1, MemberInfo member1, EndType endType1, string label1,
            Type class2, MemberInfo member2, EndType endType2, string label2,
            string name,
            Direction direction = Direction.None)
            : base(class1, class2, name)
        {
            //this.End1 = class1;
            this.Member1 = member1;
            this.EndType1 = endType1;
            this.Label1 = label1;

            //this.End2 = class2;
            this.Member2 = member2;
            this.EndType2 = endType2;
            this.Label2 = label2;

            //this.Name = name;
            this.Direction = direction;
        }

        private static int Compare(Type class1, MemberInfo member1, Type class2, MemberInfo member2)
        {
            int c = class1.FullName.CompareTo(class2.FullName);
            if (c != 0)
            {
                return c;
            }
            return member1.Name.CompareTo(member2.Name);
        }

        #endregion

        #region object

        public override bool Equals(object obj)
        {
            if (obj is Association other)
            {
                // Test A - B y B - A
                return
                    (object.Equals(this.End1, other.End1)
                        && object.Equals(this.Member1, other.Member1)
                        && object.Equals(this.End2, other.End2)
                        && object.Equals(this.Member2, other.Member2))
                        /*|| (object.Equals(this.End1, other.End2)
                            && object.Equals(this.Member1, other.Member2)
                            && object.Equals(this.End2, other.End1)
                            && object.Equals(this.Member2, other.Member1))*/;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.End1, this.Member1, this.End2, this.Member2);
        }

        #endregion
    }
}
