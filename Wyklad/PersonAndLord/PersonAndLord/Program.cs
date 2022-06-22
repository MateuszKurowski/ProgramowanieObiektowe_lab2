using System;

namespace PersonAndLord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();
            var person = new Person(name);
            Person lord = new Lord(person);

            Console.WriteLine(person.Name);
            Console.WriteLine(lord.Name);
        }
    }

    public class Person
    {
        public string Name { get; }

        public Person(string name)
        {
            Name = name;
        }
    }

    public class Lord : Person
    {
        public Lord(Person person) : base ("Sir " + person.Name) { }
    }
}