internal class Person
{   
    #region Properties
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    #endregion
    public string GetPersonDetails()
    {
        return $"{FirstName} {LastName}\n{new Address().GetAddress(Street, Number, City, PostalCode, Country)}";
    }
    //public Address GetAddress()
    //{ return new Address() ; }
}

file class Address
{
    public string GetAddress(string street,
        string number, string city,
        string postalCode, string country)
    {
        return $"{street} {number} " +
            $"\n{city} {postalCode} " +
            $"\n{country}";
    }
}
file enum PersonType
{
    Customer,
    Staff,
    Unknown
}