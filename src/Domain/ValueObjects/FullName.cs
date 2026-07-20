using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects;
public class FullName : BaseValueObject<FullName>
{
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public FullName() { }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Fail<FullName>("FirstName cannot be null.");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Fail<FullName>("LastName cannot be null.");
        if (firstName.Length > 100)
        {
            return Result.Fail<FullName>("The FirstName length is a maximum of 100 characters.");

        }
        if (lastName.Length > 100)
        {
            return Result.Fail<FullName>("The LastName length is a maximum of 100 characters.");
        }
        return Result.Ok(new FullName(firstName, lastName));
    }

    protected override int GetHashCodeCore()
    {
        return FirstName.GetHashCode() + LastName.GetHashCode();
    }

    protected override bool IsEqual(FullName other)
    {
        return FirstName.Equals(other.FirstName, StringComparison.InvariantCultureIgnoreCase) && LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase);
    }

    public static implicit operator FullName(Result<FullName> value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return $"{FirstName ?? ""} {LastName ?? ""}";
    }
}
