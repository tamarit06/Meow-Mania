public abstract class Trampa
{
    public abstract void Activar(Jugador jugador);
}

public class TrampaQuitarPunto : Trampa
{

    public override void Activar(Jugador jugador)
    {
        jugador.QuitarPunto();
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
        jugador.DisminuirVelocidad();
    }
}