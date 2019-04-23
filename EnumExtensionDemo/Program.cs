using System;
using System.Text;

namespace EnumExtensionDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var colorEnumsString = GetFriendlyColorEnums();


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        public static string GetFriendlyColorEnums()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (GenericEnum colorEnum in Enum.GetValues(typeof(GenericEnum)))
            {
                stringBuilder.Append(colorEnum.GetAttribute() + "|");
            }
            return stringBuilder.ToString();
        }
    }
}
