namespace Domain.Enums;

/// <summary>
/// Representa os tipos de subdivisão administrativa de um país.
/// </summary>
public enum SubdivisionType
{
    /// <summary>
    /// Estado (utilizado em países como Brasil, EUA, México).
    /// </summary>
    State,

    /// <summary>
    /// Província (utilizado em países como Canadá, Argentina, China).
    /// </summary>
    Province,

    /// <summary>
    /// Departamento (utilizado em países como Colômbia, França, Uruguai).
    /// </summary>
    Department,

    /// <summary>
    /// Distrito (utilizado em países como Japão, Panamá, Botswana).
    /// </summary>
    District
}
