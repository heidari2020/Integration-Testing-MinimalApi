using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects; 
 public class Email : BaseValueObject<Email>
{
    public string Value { get; private set; }
    private Email(string value)
    {
        Value = value;
    }
    public Email() { }
    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Fail<Email>("Email cannot be null.");
        if (!Regex.IsMatch(email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
            return Result.Fail<Email>("Email must be valid.");

        return Result.Ok(new Email(email));
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    protected override bool IsEqual(Email other)
    {
        return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public static implicit operator Email(Result<Email> value)
    {
        return value.Value;
    }
    public static implicit operator string(Email value)
    {
        return value.Value;
    }
}
