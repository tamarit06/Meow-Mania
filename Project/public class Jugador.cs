
using Spectre.Console;

public class Jugador
{
    private int posicionX;
    private int posicionY;
    private Laberinto laberinto;
    
    public Jugador(int posicionX, int posicionY, Laberinto laberinto)
    {
        this.posicionX=posicionX;
        this.posicionY=posicionY;
        this.laberinto=laberinto;
        
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
}
