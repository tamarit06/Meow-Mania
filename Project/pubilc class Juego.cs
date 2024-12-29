using System.Collections.Generic;
public class Juego
{
    public List<Fichas> fichas{get; set;}
    public List<Jugador> jugadores{get; set;}
    private Laberinto laberinto;
    
    public Juego()
    {
        Console.Clear();
        jugadores=new List<Jugador>();
        int[] PosicionesX={1,19};
        int[] PosicionesY={1,19};
        laberinto = new Laberinto(21, 21);
       fichas = new List<Fichas>
        {
            new Fichas("Ficha 1", new HabilidadDuplicarPuntos(),10, 20 ),
            new Fichas("Ficha 2",new HabilidadSuperVelocidad(),10, 10),
            new Fichas("Ficha 3", new HabilidadTeletransportacion(),1, 20),
            new Fichas("Ficha 4", new HabilidadAtrabezarPared(),1, 5 ),
            new Fichas("Ficha 5", new HabilidadInmunidad(),10, 10),
        };
        
        for (int i = 0; i <2; i++)
        {
            Console.WriteLine($"Jugador {i + 1}, ingresa tu nombre:");
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
        int fichasDisponibles=5;
        Console.WriteLine($"{jugador.Nombre}, elige una ficha:");
        for (int i = 0; i < fichas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {fichas[i].Nombre} (Habilidad: {fichas[i].Habilidad.Nombre}) (Velocidad: {fichas[i].Velocidad})  (Tiempo de enfriamiento: {fichas[i].Habilidad.TiempoEnfriamiento})");
        }

        int seleccion = int.Parse(Console.ReadLine()) - 1;

        if (seleccion < 0 || seleccion >= fichasDisponibles)
        {
            Console.WriteLine("Selección no válida. Intenta de nuevo.");
            return ElegirFicha(jugador); // Volver a llamar al método si la selección es inválida
        }

        Fichas fichaElegida =fichas[seleccion];
        fichas.RemoveAt(seleccion); // Eliminar la ficha elegida de la lista
        fichasDisponibles--;
        return fichaElegida;
    }

     public void Jugar()
    {   int i=0;
       // Console.Clear();
        while (true)
        {   
            for (int j = 0; j < jugadores[i%2].FichaElegida.Velocidad; j++)
            {
                jugadores[0].MostrarCaracteristicasJugadores(jugadores);
                laberinto.MostrarLaberinto();
                 Movimiento(jugadores[i%2]);
                 Console.Clear();
                 
            }
            
             if (jugadores[i%2].FichaElegida.Habilidad is HabilidadSuperVelocidad habilidadSuperVelocidad)
        {
            if (habilidadSuperVelocidad.TurnosRestantes ==jugadores[i%2].FichaElegida.Habilidad.TiempoEnfriamiento)
            {
                habilidadSuperVelocidad.RestablecerVelocidad(jugadores[i % 2]);
            }
            jugadores[i%2].FichaElegida.ActualizarEnfriamiento();
        }
            i++;
            //ver siha ganado o no
            
        }
    }

    public void Movimiento(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, usa W (arriba), A (izquierda), S (abajo), D (derecha):");
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

    }