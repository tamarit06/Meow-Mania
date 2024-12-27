using System.Collections.Generic;
public class Juego
{
    public List<Fichas> fichas{get; set;}
    public List<Jugador> jugadores{get; set;}
    private Laberinto laberinto;
    
    public Juego()
    {
        jugadores=new List<Jugador>();
        int[] PosicionesX={1,19};
        int[] PosicionesY={1,19};
        laberinto = new Laberinto(21, 21);
       fichas = new List<Fichas>
        {
            new Fichas("Ficha 1", "volar",10, 23 ),
            new Fichas("Ficha 2", "volar",10, 23),
            new Fichas("Ficha 3", "volar",10, 23 ),
            new Fichas("Ficha 4", "volar",10, 23 ),
            new Fichas("Ficha 5", "volar",10, 23),
        };
        
        for (int i = 0; i <2; i++)
        {
            Console.WriteLine($"Jugador {i + 1}, ingresa tu nombre:");
            string nombre = Console.ReadLine();
            Jugador jugador = new Jugador(nombre,i+1, PosicionesX[i], PosicionesY[i], laberinto);
            laberinto.tablero[PosicionesX[i], PosicionesY[i]].jugador = jugador;
            jugador.FichaElegida = ElegirFicha(jugador);
            jugadores.Add(jugador);
        }

       
    }
    
        public Fichas ElegirFicha(Jugador jugador)
    {
        int fichasDisponibles=5;
        Console.WriteLine($"{jugador.Nombre}, elige una ficha:");
        for (int i = 0; i < fichas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {fichas[i].Nombre} (Habiliad: {fichas[i].Habilidad}) (Velocidad: {fichas[i].Velocidad})  (Tiempo de enfriamiento: {fichas[i].TiempoEnfriamiento})");
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
    {int i=0;
        while (true)
        {   
            
            laberinto.MostrarLaberinto();
            JugarTurno(jugadores[i%2]);
            Console.Clear();
            i++;
            //ver siha ganado o no
            
        }
    }

    public void JugarTurno(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, usa W (arriba), A (izquierda), S (abajo), D (derecha):");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        Console.WriteLine();

        int newPositionX=jugador.PositionX;
        int newPositionY=jugador.PositionY;

        switch (keyInfo.Key)
        {
            case ConsoleKey.W://mover arriba
            newPositionX--;
            break;

            case ConsoleKey.S:
            newPositionX++;
            break;

            case ConsoleKey.D:
            newPositionY++;
            break;

            case ConsoleKey.A:
            newPositionY--;
            break;
            
            default:
            return;
        }

        if (EsMovimientoValido(newPositionX, newPositionY))
        {   
            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = false;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = null;

            jugador.PositionX=newPositionX;
            jugador.PositionY=newPositionY;

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = true;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = jugador;
        }
        else JugarTurno(jugador);

        

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

    // Si pasa todas las validaciones, el movimiento es válido
    return true;
}

    }