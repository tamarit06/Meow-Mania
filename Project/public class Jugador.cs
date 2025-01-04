
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
    tabla.AddColumn("[blue]Jugador[/]");
    tabla.AddColumn("[yellow]Puntuación[/]");
    tabla.AddColumn("[green]Tiempo de enfriamiento[/]");
    tabla.AddColumn("[red]Velocidad[/]");
    tabla.AddColumn("[yellow]Habilidad[/]");

        tabla.AddRow(
            $"[blue]{jugador.Nombre}[/]",
            $"[yellow]{jugador.Puntuacion}[/]", 
            $"[green]{jugador.FichaElegida.Habilidad.TurnosRestantes}[/]",
            $"[red]{jugador.FichaElegida.Velocidad}[/]",
            $"[yellow]{jugador.FichaElegida.Habilidad.Nombre}[/]"    
        );
    

    // Mostrar la tabla en la consola
    AnsiConsole.Render(tabla);
    }

    public void QuitarPunto()
    {
        Puntuacion--;

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
