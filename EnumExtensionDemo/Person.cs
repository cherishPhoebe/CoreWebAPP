using System;
using System.Collections.Generic;
using System.Text;

namespace EnumExtensionDemo
{
    public class Person
    {
        public Person(string firstName, string lastName) => FirstName = firstName;

        public string FirstName { get; }

        public string LastName { get; }

    }
}
