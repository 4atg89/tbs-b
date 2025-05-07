using System.Text.RegularExpressions;
using MySqlConnector;

namespace Auth.Data.Exceptions;

public class UserAlreadyExists(string fieldName) : Exception($"Field {fieldName} should be unique")
{
    public string FieldName { get; } = fieldName;

    public static UserAlreadyExists From(MySqlException ex)
    {
        var match = Regex.Match(ex.Message, @"for key '(.+?)'");
        var indexName = match.Success ? match.Groups[1].Value : null;

        var fieldName = indexName?.Split('_').LastOrDefault();

        return new UserAlreadyExists(fieldName);
    }
}