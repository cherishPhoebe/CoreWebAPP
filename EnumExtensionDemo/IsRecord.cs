using System;
using System.Collections.Generic;
using System.Text;

namespace EnumExtensionDemo
{
    public class IsRecordAttribute : Attribute
    {
        public bool IsRecord { get; private set; }

        public IsRecordAttribute(bool value) {
            IsRecord = value;
        }

    }
}
