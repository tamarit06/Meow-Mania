
using Spectre.Console;

public class Jugador
{
    public string Nombre { get; set; }
    public Fichas FichaElegida { get; set; }
    public int PositionX;
    public int PositionY;
    private Laberinto laberinto;
    public int representacionEnConsola { get; set; }
    public int Puntuacion{get; set;}
    
    public Jugador(string Nombre, int representacionEnConsola, int PositionX, int PositionY, Laberinto laberinto)
    {
        this.Nombre=Nombre;
        this.representacionEnConsola=representacionEnConsola;
        this.PositionX=PositionX;
        this.PositionY=PositionY;
        this.laberinto=laberinto;
        laberinto.tablero[PositionX,PositionY].HayJugador=true; 
        Puntuacion=0;
    }

    public void MostrarCaracteristicasJugadores(Jugador jugador)
    {
    // Crear una tabla para mostrar las características
    var tabla = new Table();
    tabla.Border=TableBorder.Double;
    tabla.AddColumn("[salmon1]Jugador[/]");
    tabla.AddColumn("[salmon1]Puntuación[/]");
    tabla.AddColumn("[salmon1]Tiempo de enfriamiento[/]");
    tabla.AddColumn("[salmon1]Velocidad[/]");
    tabla.AddColumn("[salmon1]Habilidad[/]");

        tabla.AddRow(
            $"[skyblue3]{jugador.Nombre}[/]",
            $"[skyblue3]{jugador.Puntuacion}[/]", 
            $"[skyblue3]{jugador.FichaElegida.Habilidad.TurnosRestantes}[/]",
            $"[skyblue3]{jugador.FichaElegida.Velocidad}[/]",
            $"[skyblue3]{jugador.FichaElegida.Habilidad.Nombre}[/]"    
        );
    

    // Mostrar la tabla en la consola
    AnsiConsole.Render(tabla);
    }

    public void QuitarPunto()
    {
        if (Puntuacion>0)
        {
             Puntuacion--;
        }

    }

    public void DisminuirVelocidad()
    {
       FichaElegida.Velocidad--;
    }
    public bool EsCeldaValida(int celdaX, int celdaY)
{
    if (celdaX < 0 || celdaX >= laberinto.sizeX-1 || celdaY < 0 || celdaY >= laberinto.sizeY-1)
    {
        return false; //La celda está fuera de los límites del laberinto
    }
    if (laberinto.Tablero[celdaX, celdaY].Tipo==Celda.TipoCelda.Pared)//aqui falta algo
    {
        return false; //La celda es una pared
    }
    return true; //La celda es válida
    }
}
