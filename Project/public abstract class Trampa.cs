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

public class TrampaAumentarTiempoDeEnfriamientoo : Trampa
{
    public override void Activar(Jugador jugador)
    {
        jugador.AumentarTiempoDeEnfriamiento();
    }
}

public class TrampaDisminuirVelocidad : Trampa
{
    public override void Activar(Jugador jugador)
    {
        jugador.DisminuirVelocidad();
    }
}