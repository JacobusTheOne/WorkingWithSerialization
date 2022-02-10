namespace Packt.Shared;
using System.Xml.Serialization;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
class Program
{
    static void Main(string[] args){
        List<Person> people = new()
    {
        new(30000M){
            FirstName = "Alice",
            LastName = "Smith",
            DateOfBirth = new(1974, 3, 14)
        },
        new(40000M)
        {
            FirstName = "Bob",
            LastName = "Jones",
            DateOfBirth = new(1969, 11, 23)
        },
        new(20000M)
        {
            FirstName = "Charlie",
            LastName = "Cox",
            DateOfBirth = new(1984, 5, 4),
            Children = new()
            {
                new(0M){
                    FirstName = "Sally",
                    LastName = "Cox",
                    DateOfBirth = new(2000, 7, 12)
                }
            }
        }
    };

    XmlSerializer xs = new(people.GetType());
    string path = Combine(CurrentDirectory, "people.xml");

    using(FileStream stream = File.Create(path)){
        xs.Serialize(stream, people);
    }

    WriteLine("Written {0:N0} bytes of XML to {1}",
                arg0: new FileInfo(path).Length,
                arg1: path);
    WriteLine();
    WriteLine(File.ReadAllText(path));
    }
    
}

public class Person
{
    public Person(decimal initialSalary)
    {
        Salary = initialSalary;
    }
    public string? FirstName {get; set;}
    public string? LastName{ get; set;}
    public DateTime DateOfBirth {get; set;}
    public HashSet<Person>? Children { get; set;}
    protected decimal Salary { get; set; }
}
