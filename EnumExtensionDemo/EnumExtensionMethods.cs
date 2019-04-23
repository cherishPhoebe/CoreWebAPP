using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EnumExtensionDemo
{
    public static class EnumExtensionMethods
    {
        public static object GetAttribute(this Enum GenericEnum) {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfos = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfos != null && memberInfos.Length > 0)) {
                var _attributes = memberInfos[0].GetCustomAttributes();
                foreach (var attr in _attributes) {
                    Console.WriteLine(attr.ToString());
                }
            }


            return null;
        }
    }
}
