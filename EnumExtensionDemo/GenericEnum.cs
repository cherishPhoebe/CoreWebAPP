using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EnumExtensionDemo
{
    public enum GenericEnum
    {        
        [IsRecord(true)]
        [Description("Blanched Almond Color")]
        BlanchedAlmond = 1,
        [IsRecord(true)]
        [Description("Dark Sea Green Color")]
        DarkSeaGreen = 2,
        [IsRecord(false)]
        [Description("Deep Sky Blue Color")]
        DeepSkyBlue = 3,
        [IsRecord(false)]
        [Description("Rosy Brown Color")]
        RosyBrown = 4

    }
}
