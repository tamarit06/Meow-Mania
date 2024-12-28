
using Spectre.Console;

public class Jugador
{
    public string Nombre { get; set; }
    public Fichas FichaElegida { get; set; }
    public int PositionX;
    public int PositionY;
    private Laberinto laberinto;
    public int representacionEnConsola { get; set; }
    
    public Jugador(string Nombre, int representacionEnConsola, int PositionX, int PositionY, Laberinto laberinto)
    {
        this.Nombre=Nombre;
        this.representacionEnConsola=representacionEnConsola;
        this.PositionX=PositionX;
        this.PositionY=PositionY;
        this.laberinto=laberinto;
        laberinto.tablero[PositionX,PositionY].HayJugador=true;  
    }

    public void DevolverCeldas(int celdas)
    {
        //Logica para devolver a la posicion anterior

    }
    public void AnularHabilidad()
    {
        // Logica para anular habilidades del jugador
    }

    public void QuitarTurno()
    {
       // Logica para quitar turno al jugador
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

// definir metodo o propoedad ficha elegida
}
