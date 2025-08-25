using Domain.ValueObjects;
using RsjFramework.Entities;

namespace Domain;
public  class Customer : Entity<long>
{
    public FullName FullName { get; protected set; }
    public string Email { get; protected set; }
    public DateTime DateOfBirth { get; protected set; }
    private Customer() { }
    public Customer(FullName fullName, string email, DateTime dateOfBirth)
    {


        FullName = fullName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
     
}
