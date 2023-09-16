using System.Diagnostics.CodeAnalysis;
using System.Net;

internal class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public required string Country { get; set; }

    public required int _Age;
}

internal class Employee : Person
{
    public required string Function { get; set; }

    [SetsRequiredMembers]
    public Employee(string firstName, 
        string lastName, string function)
    {
        FirstName = firstName;
        LastName = lastName;
        Function = function;
    }
}