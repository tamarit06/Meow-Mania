public abstract class Habilidad
{
    public string Nombre { get; set; }
    public int TiempoEnfriamiento { get; set; } // Tiempo de enfriamiento en turnos
    public int TurnosRestantes { get; set; }
    public bool Activo;
     
    public Habilidad()
    {
        TurnosRestantes=0;
        Activo=false;
    }
    public abstract void Activar(Jugador jugador, Laberinto laberinto);

    public void ActualizarEnfriamiento()
    {
        if (TurnosRestantes > 0)
        {
            TurnosRestantes--;
        }
    }

    public bool PuedeActivar()
    {
        return TurnosRestantes == 0; // La habilidad se puede activar si no hay turnos restantes
    }
}

public class HabilidadDuplicarPuntos : Habilidad
{
    public HabilidadDuplicarPuntos()
    {
        Nombre = "Duplicar Puntos";
        TiempoEnfriamiento=5;
    }

    public override void Activar(Jugador jugador, Laberinto laberinto)
    {
       if (PuedeActivar())
        {
            Activo=true;
            TurnosRestantes = TiempoEnfriamiento; // Reiniciar el tiempo de enfriamiento
        }
    }
}

public class HabilidadSuperVelocidad : Habilidad
{
    int original;

    public HabilidadSuperVelocidad()
    {
        Nombre = "Supervelocidad";
        TiempoEnfriamiento=1;
    }

    public override void Activar(Jugador jugador, Laberinto laberinto)
    {
        original=jugador.FichaElegida.VelocidadOriginal;
        if (PuedeActivar())
        {
            jugador.FichaElegida.VelocidadOriginal+=11;
            jugador.FichaElegida.Velocidad+=10;
            TurnosRestantes = TiempoEnfriamiento; // Reiniciar el tiempo de enfriamiento
        }
        
    }

     public void RestablecerVelocidadHabilidad(Jugador jugador)
    {
        jugador.FichaElegida.VelocidadOriginal = original; 
        jugador.FichaElegida.Velocidad=original;
    }
}

public class HabilidadInmunidad : Habilidad
{
    public HabilidadInmunidad()
    {
        Nombre = "Inmunidad a trampa";
        TiempoEnfriamiento=2;
    }

    public override void Activar(Jugador jugador, Laberinto laberinto)
    {
       if (PuedeActivar())
       {
        jugador.FichaElegida.EsInmune=true;
        TurnosRestantes = TiempoEnfriamiento; // Reiniciar el tiempo de enfriamiento
       }
    }

    public void QuitarInmunidad(Jugador jugador)
    {
       jugador.FichaElegida.EsInmune=false;
    }
}

public class HabilidadTeletransportacion : Habilidad
{
    private Random rand = new Random();
    List<(int x,int y)> posicionesCamino;
    public HabilidadTeletransportacion()
    {
        Nombre = "Teletransportación";
        TiempoEnfriamiento=3;
    }

    public override void Activar(Jugador jugador, Laberinto laberinto)
    {
        if (PuedeActivar())
        {
            posicionesCamino=laberinto.PosicionesCamino();
            var nuevaPos = posicionesCamino[rand.Next(posicionesCamino.Count)];
    
         // Actualizar la posición del jugador
         laberinto.tablero[jugador.PositionX, jugador.PositionY].HayJugador = false;
         laberinto.tablero[jugador.PositionX, jugador.PositionY].jugador = null;

        jugador.PositionX = nuevaPos.x;
        jugador.PositionY = nuevaPos.y;

        laberinto.tablero[jugador.PositionX, jugador.PositionY].HayJugador = true;
        laberinto.tablero[jugador.PositionX, jugador.PositionY].jugador = jugador;

        TurnosRestantes = TiempoEnfriamiento; // Reiniciar el tiempo de enfriamiento
        }
    }
}
public class HabilidadAtravesarPared : Habilidad
{
    public HabilidadAtravesarPared()
    {
        Nombre = "Atravesar Pared";
        TiempoEnfriamiento=5;
    }

    public override void Activar(Jugador jugador, Laberinto laberinto)
    {
       if (PuedeActivar())
       {
        Activo=true;
        TurnosRestantes = TiempoEnfriamiento; // Reiniciar el tiempo de enfriamiento
       }

    }
}