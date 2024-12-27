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
        Laberinto laberinto = new Laberinto(21, 21);
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

        laberinto.MostrarLaberinto();
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
    {
        while (true)
        {
            foreach (var jugador in jugadores)
            {
                JugarTurno(jugador);
                //ver siha ganado o no
            }
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
            jugador.PositionX=newPositionX;
            jugador.PositionY=newPositionY;
        }

    }

        public bool EsMovimientoValido(int x, int y)
    {
        return x >= 0 && x < laberinto.sizeX-1 && 
               y >= 0 && y < laberinto.sizeY-1 &&
               laberinto.Tablero[x,y].Tipo!=Celda.TipoCelda.Camino;
    }
}
