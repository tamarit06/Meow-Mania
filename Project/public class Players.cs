using System.Collections.Generic;
using System.Linq;

public class Fichas
{
    public string Nombre { get; set; }
    public string Habilidad { get; set; }
    public int TiempoEnfriamiento { get; set; } // turnos
    public float Velocidad { get; set; } // cantidad de casillas a caminar
    public int PosX {get; set; }
    public int PosY {get; set; }
    private Laberinto laberinto;

    public Fichas(string nombre, string habilidad, int tiempoEnfriamiento, float velocidad, int x, int y, Laberinto laberinto)
    {
        Nombre = nombre;
        Habilidad = habilidad;
        TiempoEnfriamiento = tiempoEnfriamiento;
        Velocidad = velocidad;
        PosX=x;
        PosY=y;
        this.laberinto=laberinto;
    }

    public void Move(ConsoleKey tecla)
    {
        int newPositionX=PosX;
        int newPositionY=PosY;

        switch (tecla)
        {
            case ConsoleKey.UpArrow:
            newPositionX--;
            break;

            case ConsoleKey.DownArrow:
            newPositionX++;
            break;

            case ConsoleKey.RightArrow:
            newPositionY++;
            break;

            case ConsoleKey.LeftArrow:
            newPositionY--;
            break;
            
            default:
            return;
        }

        if (EsMovimientoValido(newPositionX, newPositionY))
        {
            PosX=newPositionX;
            PosY=newPositionY;
        }
    }

    public bool EsMovimientoValido(int x, int y)
    {
        return x >= 0 && x < laberinto.sizeX-1 && 
               y >= 0 && y < laberinto.sizeY-1 &&
               laberinto.Tablero[x,y] != 1;
    }
    public override string ToString()
    {
        return $"Nombre: {Nombre}, Habilidad: {Habilidad}, Tiempo de Enfriamiento: {TiempoEnfriamiento}s, Velocidad: {Velocidad}u/s";
    }
}
