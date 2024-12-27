using System.Collections.Generic;
using System.Linq;

public class Fichas
{
    public string Nombre { get; set; }
    public string Habilidad { get; set; }
    public int TiempoEnfriamiento { get; set; } // turnos
    public float Velocidad { get; set; } // cantidad de casillas a caminar

    public Fichas(string nombre, string habilidad, int tiempoEnfriamiento, float velocidad)
    {
        Nombre = nombre;
        Habilidad = habilidad;
        TiempoEnfriamiento = tiempoEnfriamiento;
        Velocidad = velocidad;
    }

    
    public override string ToString()
    {
        return $"Nombre: {Nombre}, Habilidad: {Habilidad}, Tiempo de Enfriamiento: {TiempoEnfriamiento}s, Velocidad: {Velocidad}u/s";
    }
}
