using System.Collections.Generic;
using Spectre.Console;
public class Juego
{
    public List<Fichas> fichas{get; set;}
    public List<Jugador> jugadores{get; set;}
    private Laberinto laberinto;
    
    public Juego()
    {
       Console.Clear();
        AnsiConsole.Live(new Panel(""))
        .Start(ctx =>
           {
         var panel = new Panel($"[underline bold italic yellow]¡Bienvenido al juego!🎮[/]")
                .Border(BoxBorder.Double)
                .Header("[yellow][/]");

           ctx.UpdateTarget(panel);
        
           });

            // Repetir la melodía 2 veces
            for (int i = 0; i < 2; i++)
            {
                // Melodía simple
                Console.Beep(659, 200); // Mi
                Console.Beep(659, 200); // Mi
                Console.Beep(659, 200); // Mi
                Console.Beep(523, 200); // Do
                Console.Beep(659, 200); // Mi
                Console.Beep(784, 400); // Sol
                Console.Beep(392, 400); // Sol (una octava más baja)
                Console.Beep(523, 400); // Do
            }

        Console.Clear();
        jugadores=new List<Jugador>();
        int[] PosicionesX={1,19};
        int[] PosicionesY={1,19};
        laberinto = new Laberinto(21, 21);
       fichas = new List<Fichas>
        {
            new Fichas("Ficha 1", new HabilidadDuplicarPuntos(),10),
            new Fichas("Ficha 2",new HabilidadSuperVelocidad(),8),
            new Fichas("Ficha 3", new HabilidadTeletransportacion(),10),
            new Fichas("Ficha 4", new HabilidadAtrabezarPared(),8),
            new Fichas("Ficha 5", new HabilidadInmunidad(),12),
        };
        
        for (int i = 0; i <2; i++)
        {
           AnsiConsole.Markup($"[blue]⚜️ Jugador {i + 1}, ingresa tu nombre:⚜️[/]\n");
            string nombre = Console.ReadLine();
            Jugador jugador = new Jugador(nombre,i+1, PosicionesX[i], PosicionesY[i], laberinto);
            laberinto.tablero[PosicionesX[i], PosicionesY[i]].jugador = jugador;
            jugador.FichaElegida = ElegirFicha(jugador);
            jugadores.Add(jugador);
            Console.Clear();
        }
    }
    
  

public Fichas ElegirFicha(Jugador jugador)
{
    // Lista de fichas disponibles
    int fichasDisponibles = fichas.Count;

    // Crear una lista de opciones para el menú
    var opciones = new List<string>();
    for (int i = 0; i < fichas.Count; i++)
    {
        opciones.Add($"{i + 1}. {fichas[i].Nombre} (Habilidad: {fichas[i].Habilidad.Nombre}, Velocidad: {fichas[i].Velocidad}, Tiempo de enfriamiento: {fichas[i].Habilidad.TiempoEnfriamiento})");
    }

    // Mostrar el menú y permitir al usuario seleccionar una opción
    var seleccion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[blue]⚜️ Por favor, elige una ficha:⚜️[/]")
            .AddChoices(opciones));

    // Obtener el índice de la opción seleccionada
    int indiceSeleccionado = opciones.IndexOf(seleccion);

    // Obtener la ficha elegida
    Fichas fichaElegida = fichas[indiceSeleccionado];
    fichas.RemoveAt(indiceSeleccionado); // Eliminar la ficha elegida de la lista
    return fichaElegida;
}

     public void Jugar()
    {  
        Console.Clear();
         int i=0;
        while (true)
        {   
            for (int j = 0; j < jugadores[i%2].FichaElegida.Velocidad; j++)
            {
                jugadores[0].MostrarCaracteristicasJugadores(jugadores);
                laberinto.MostrarLaberinto();
                 Movimiento(jugadores[i%2]);
                 Console.Clear();
                 
            }
               jugadores[i%2].FichaElegida.ActualizarEnfriamiento();
            
             if (jugadores[i%2].FichaElegida.Habilidad is HabilidadSuperVelocidad habilidadSuperVelocidad)
            {
                if (habilidadSuperVelocidad.TurnosRestantes !=jugadores[i%2].FichaElegida.Habilidad.TiempoEnfriamiento)
                {
                    habilidadSuperVelocidad.RestablecerVelocidad(jugadores[i%2]);
                }
                
            }

            if (jugadores[i%2].FichaElegida.Habilidad is HabilidadInmunidad habilidadInmunidad)
            {
                if (habilidadInmunidad.TurnosRestantes !=jugadores[i%2].FichaElegida.Habilidad.TiempoEnfriamiento)
                {
                    habilidadInmunidad.QuitarInmunidad(jugadores[i%2]);
                }
            }
            i++;
            
            if (!HayPescado())
            {
                break;
            }
            
        }

        if (jugadores[0].Puntuacion==jugadores[1].Puntuacion)
        {
            AnsiConsole.Markup("[yellow]Empate🐈[/]");
        }
        else if (jugadores[0].Puntuacion>jugadores[1].Puntuacion)
        {
            AnsiConsole.Markup($"[yellow]{jugadores[0].Nombre} ha ganado😸🎉[/]");
        }
        else{AnsiConsole.Markup($"[yellow]{jugadores[1].Nombre} ha ganado😸🎉[/]");}
    }

    public void Movimiento(Jugador jugador)
    {
       AnsiConsole.Markup($"[red]{jugador.Nombre}, usa W (arriba), A (izquierda), S (abajo), D (derecha):[/]");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        Console.WriteLine();

        if (keyInfo.Key == ConsoleKey.H && jugador.FichaElegida.Habilidad.PuedeActivar()) // Activar habilidad
    {
        jugador.FichaElegida.Habilidad.Activar(jugador, laberinto);
        return; //salir del  metodo luego de activar la habilidad
    }

        int newPositionX=jugador.PositionX;
        int newPositionY=jugador.PositionY;

        switch (keyInfo.Key)
        {
            case ConsoleKey.W://mover arriba
            newPositionX--;
            break;

            case ConsoleKey.S://mover abajo
            newPositionX++;
            break;

            case ConsoleKey.D: //mover derecha
            newPositionY++;
            break;

            case ConsoleKey.A: //mover izquierda
            newPositionY--;
            break;
            
            default:
            return;
        }
         int difx=newPositionX-jugador.PositionX;
         int dify=newPositionY-jugador.PositionY;

        if (laberinto.tablero[newPositionX, newPositionY].Tipo==Celda.TipoCelda.Pared &&
            jugador.FichaElegida.Habilidad is HabilidadAtrabezarPared &&
            jugador.FichaElegida.Habilidad.TurnosRestantes == jugador.FichaElegida.Habilidad.TiempoEnfriamiento &&
            EsMovimientoValido(newPositionX+difx, newPositionY+dify))
        {
           

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = false;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = null;

            jugador.PositionX=newPositionX+difx;
            jugador.PositionY=newPositionY+dify;

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = true;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = jugador;


        }

        else if (EsMovimientoValido(newPositionX, newPositionY))
        {   
            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = false;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = null;

            jugador.PositionX=newPositionX;
            jugador.PositionY=newPositionY;

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = true;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = jugador;
        }
        else Movimiento(jugador);

        if (laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Trampa &&
            !jugador.FichaElegida.EsInmune)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].TrampaAsociada.Activar(jugador);
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
        }

        if (laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Pescado&&
        jugador.FichaElegida.Habilidad is HabilidadDuplicarPuntos && jugador.FichaElegida.Habilidad.TurnosRestantes==jugador.FichaElegida.Habilidad.TiempoEnfriamiento)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
            jugador.Puntuacion+=2;
        }

        else if(laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Pescado)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
            jugador.Puntuacion++;
        }
    }
    
       public bool EsMovimientoValido(int x, int y)
    {
        // Validar que el laberinto y el tablero estén inicializados
        if (laberinto == null || laberinto.Tablero == null)
        {
            Console.WriteLine("Error: El laberinto no está inicializado correctamente.");
            return false;
        }

        // Validar que las coordenadas estén dentro de los límites del tablero
        if (x < 0 || x >= laberinto.sizeX || y < 0 || y >= laberinto.sizeY)
        {
            Console.WriteLine($"Error: Movimiento fuera de los límites del laberinto. Coordenadas: ({x}, {y})");
            return false;
        }

        // Validar que la celda no sea una pared
        if (laberinto.Tablero[x, y].Tipo == Celda.TipoCelda.Pared)
        {
            Console.WriteLine("Error: Movimiento inválido. La celda es una pared.");
            return false;
        }
        // verificar si hay otro jugador
        if (laberinto.Tablero[x, y].HayJugador)
        {
            Console.WriteLine("Error: Movimiento inválido. La celda tiene otro jugador.");
            return false;
        }

        // Si pasa todas las validaciones, el movimiento es válido
        return true;
    }

    private bool HayPescado()
    {
        for (int i = 0; i < laberinto.sizeX; i++)
        {
            for (int j = 0; j < laberinto.sizeY; j++)
            {
                if (laberinto.tablero[i,j].Tipo==Celda.TipoCelda.Pescado)
                {
                    return true;
                }
            }
        }

        return false;
    }

    }