public abstract class Trampa
{
    public abstract void Activar(Jugador jugador);
}

public class TrampaDevolver : Trampa
{
    private int celdasDevolver;

    public TrampaDevolver(int celdasDevolver)
    {
        this.celdasDevolver = celdasDevolver;
    }

    public override void Activar(Jugador jugador)
    {
        jugador.DevolverCeldas(celdasDevolver);
    }
}

public class TrampaAnularHabilidad : Trampa
{
    public override void Activar(Jugador jugador)
    {
        jugador.AnularHabilidad();
    }
}

public class TrampaQuitarTurno : Trampa
{
    public override void Activar(Jugador jugador)
    {
        jugador.QuitarTurno();
    }
}