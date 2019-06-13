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
using System.Linq;
using System.Text;

namespace UmlFromCode
{
    public static class Utils
    {
        public static string Repit(this string s, int size)
        {
            StringBuilder buff = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                buff.Append(s);
            }
            return buff.ToString();
        }

        public static int GetSafeHashCode(this object obj, int defHashCode = 0)
        {
            if (obj == null)
            {
                return defHashCode;
            }
            return obj.GetHashCode();
        }

        public static int GetHashCode(params object[] objects)
        {
            int hashCode = 0;
            foreach (object obj in objects.Where(o => o != null))
            {
                hashCode ^= obj.GetHashCode();
            }
            return hashCode;
        }

        public static int NumberOfSetBits(uint i)
        {
            // https://stackoverflow.com/questions/109023/how-to-count-the-number-of-set-bits-in-a-32-bit-integer
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (int) (unchecked(((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);

        }

        public static int NumberOfSetBits(ulong i)
        {
            // https://stackoverflow.com/questions/2709430/count-number-of-bits-in-a-64-bit-long-big-integer
            i = i - ((i >> 1) & 0x5555555555555555UL);
            i = (i & 0x3333333333333333UL) + ((i >> 2) & 0x3333333333333333UL);
            return (int) (unchecked(((i + (i >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
        }
    }
}
