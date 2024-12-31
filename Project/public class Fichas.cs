using System.Collections.Generic;
using System.Linq;

public class Fichas
{
    public string Nombre { get; set; }
    public Habilidad Habilidad { get; set; }//tienes q ver esto
    public int Velocidad { get; set; } // cantidad de casillas a caminar
    public bool EsInmune {get; set;}

    public Fichas(string nombre, Habilidad habilidad, int velocidad)
    {
        Nombre = nombre;
        Habilidad = habilidad;
        Velocidad = velocidad;
        EsInmune=false;
    }

    
    public override string ToString()
    {
        return $"Nombre: {Nombre}, Habilidad: {Habilidad.Nombre},Tiempo de Enfriamiento: {Habilidad.TiempoEnfriamiento}s, Velocidad: {Velocidad}u/s";
    }

     public void ActualizarEnfriamiento()
    {
        Habilidad.ActualizarEnfriamiento();
    }
}
