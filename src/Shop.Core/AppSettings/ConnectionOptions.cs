using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Shop.Core.SharedKernel;

namespace Shop.Core.AppSettings;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public sealed class ConnectionOptions : IAppOptions
{
    static string IAppOptions.ConfigSectionPath => "ConnectionStrings";

    [Required]
    public string SqlConnection { get; private init; }

    [Required]
    public string NoSqlConnection { get; private init; }

    [Required]
    public string CacheConnection { get; private init; }
}