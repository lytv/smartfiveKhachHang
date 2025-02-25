using Ardalis.Result;
using Shop.Core;

namespace Shop.Domain.ValueObjects;

public sealed record Email
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="address">The email address.</param>
    private Email(string address) =>
        Address = address.ToLowerInvariant().Trim();

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    public Email() { } // Default constructor for EF/ORM

    /// <summary>
    /// Gets the email address.
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// Creates a new <see cref="Email"/> instance.
    /// </summary>
    /// <param name="emailAddress">The email address to create.</param>
    /// <returns>A <see cref="Result{T}"/> with the created <see cref="Email"/> if successful, or an error message if not.</returns>
    public static Result<Email> Create(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            return Result<Email>.Error("O endereço de e-mail deve ser informado.");

        return !RegexPatterns.EmailIsValid.IsMatch(emailAddress)
            ? Result<Email>.Error("O endereço de e-mail não é valido.")
            : Result<Email>.Success(new Email(emailAddress));
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="Email"/> object.
    /// </summary>
    /// <returns>A string that represents the current <see cref="Email"/> object.</returns>
    public override string ToString() =>
        Address;
}