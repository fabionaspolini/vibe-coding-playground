namespace GeografiaService.Domain.Enums;

/// <summary>
/// Enum que representa os tipos de subdivisões geográficas dos países.
/// </summary>
public enum EstadoTipo
{
    /// <summary>
    /// Estado - subdivisão padrão.
    /// </summary>
    State,

    /// <summary>
    /// Província - subdivisão comum no Canadá e Argentina.
    /// </summary>
    Province,

    /// <summary>
    /// Departamento - subdivisão comum na Colômbia e outros países.
    /// </summary>
    Department,

    /// <summary>
    /// Distrito - subdivisão administrativa em alguns países.
    /// </summary>
    District,

    /// <summary>
    /// Região - subdivisão comum em alguns países.
    /// </summary>
    Region
}

