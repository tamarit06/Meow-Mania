
using Spectre.Console;

public class Jugador
{
    public string Nombre { get; set; }
    public Fichas FichaElegida { get; set; }
    public int PositionX;
    public int PositionY;
    private Laberinto laberinto;
    public int identificador { get; set; }  //jugador 1 o 2
    public int Puntuacion{get; set;}
    
    public Jugador(string Nombre, int identificador, int PositionX, int PositionY, Laberinto laberinto)
    {
        this.Nombre=Nombre;
        this.identificador=identificador;
        this.PositionX=PositionX;
        this.PositionY=PositionY;
        this.laberinto=laberinto;
        laberinto.tablero[PositionX,PositionY].HayJugador=true; 
        Puntuacion=0;
    }

    public void MostrarCaracteristicasJugadores(Jugador jugador)
    {
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
    
        AnsiConsole.Render(tabla);
    }
}
