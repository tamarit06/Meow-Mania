public abstract class Trampa
{
    public abstract void Activar(Jugador jugador);
}

public class TrampaQuitarPunto : Trampa
{

    public override void Activar(Jugador jugador)
    {
        if (jugador.Puntuacion>0)
        {
             jugador.Puntuacion--;
        }
    }
}

public class TrampaAumentarTurnosRestantes : Trampa
{
    public override void Activar(Jugador jugador)
    {
        jugador.FichaElegida.Habilidad.TurnosRestantes++;
    }
}

public class TrampaDisminuirVelocidad : Trampa
{
    public override void Activar(Jugador jugador)
    {
         jugador.FichaElegida.Velocidad--;
         jugador.FichaElegida.VelocidadOriginal--;
    }
}