namespace Ejercicio2_VectorObjeto.Models;

// CLASE = plantilla que describe una entidad del dominio (un alumno).
// Agrupa en un solo objeto los tres atributos que en el Ejercicio 1 estaban
// dispersos en tres vectores de índice común. Así los datos viajan siempre juntos.
public class Alumno
{
    // ENCAPSULAMIENTO con auto-properties: { get; set; } genera un campo privado
    // de respaldo invisible y controla el acceso mediante getter y setter.
    public int LU { get; set; }
    public string Nombre { get; set; } = string.Empty; // = string.Empty evita valores null
    public string Nota { get; set; } = string.Empty;
}
