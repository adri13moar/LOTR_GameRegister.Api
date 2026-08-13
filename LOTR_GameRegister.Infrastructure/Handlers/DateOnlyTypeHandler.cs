using Dapper;
using System.Data;

namespace LOTR_GameRegister.Infrastructure.Handlers;

/// <summary>
/// Maps <see cref="DateOnly"/> values to and from SQL Server <c>DATE</c> columns via Dapper.
/// </summary>
public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    /// <summary>
    /// Writes the <see cref="DateOnly"/> value to a parameter typed as a SQL Server <c>DATE</c>.
    /// </summary>
    /// <param name="parameter">The database parameter to set.</param>
    /// <param name="value">The date value to write.</param>
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        parameter.DbType = DbType.Date;
    }

    /// <summary>
    /// Reads a SQL Server <c>DATE</c> value back into a <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="value">The database value to parse.</param>
    /// <returns>The parsed <see cref="DateOnly"/>.</returns>
    public override DateOnly Parse(object value)
    {
        return DateOnly.FromDateTime((DateTime)value);
    }
}