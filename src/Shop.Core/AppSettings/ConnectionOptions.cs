using System.ComponentModel.DataAnnotations;
using Shop.Core.Abstractions;

namespace Shop.Core.AppSettings;

public sealed class ConnectionOptions : BaseOptions
{
    public const string ConfigSectionPath = "ConnectionStrings";

    [Required]
    public string SqlConnection { get; private init; }

    [Required]
    public string NoSqlConnection { get; private init; }
}