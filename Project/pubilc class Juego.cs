using System;
using System.Collections.Generic;
using Spectre.Console;

public class Juego
{
    public List<Fichas> fichas { get; set; }
    public List<Jugador> jugadores { get; set; }
    private Laberinto laberinto;
    private ScreenBuffer ScreenBuffer; 

    public Juego()
    {
        ScreenBuffer = new ScreenBuffer(25, 25);
        Console.Clear();
    
        Console.CursorVisible=false;
          string message = @"
                ███╗   ███╗███████╗ ██████╗ ██╗    ██╗    ███╗   ███╗ █████╗ ███╗   ██╗██╗ █████╗ 
                ████╗ ████║██╔════╝██╔═══██╗██║    ██║    ████╗ ████║██╔══██╗████╗  ██║██║██╔══██╗
                ██╔████╔██║█████╗  ██║   ██║██║ █╗ ██║    ██╔████╔██║███████║██╔██╗ ██║██║███████║
                ██║╚██╔╝██║██╔══╝  ██║   ██║██║███╗██║    ██║╚██╔╝██║██╔══██║██║╚██╗██║██║██╔══██║
                ██║ ╚═╝ ██║███████╗╚██████╔╝╚███╔███╔╝    ██║ ╚═╝ ██║██║  ██║██║ ╚████║██║██║  ██║
                ╚═╝     ╚═╝╚══════╝ ╚═════╝  ╚══╝╚══╝     ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝╚═╝  ╚═╝ 
        ";
            AnsiConsole.Markup($"[bold SkyBlue1]{message}[/]");
        
        // Repetir la melodía 2 veces
        for (int i = 0; i < 2; i++)
        {
            Console.Beep(659, 200); // Mi
            Console.Beep(659, 200); // Mi
            Console.Beep(659, 200); // Mi
            Console.Beep(523, 200); // Do
            Console.Beep(659, 200); // Mi
            Console.Beep(784, 400); // Sol
            Console.Beep(392, 400); // Sol (una octava más baja)
            Console.Beep(523, 400); // Do
        }

            string[] explicacion = new string[5];
            explicacion[0] = @"Bienvenido a Meow Mania, un juego de laberinto para dos jugadores donde tú y tu oponente 
asumirán el papel de adorables gatos en busca de deliciosos pescados. El objetivo es simple: recolectar la mayor cantidad de pescados posible antes de que se agoten.";
                
            explicacion[1] = @"Selección de Fichas:
            Antes de comenzar, cada jugador debe elegir una de las cinco fichas disponibles.
            Cada ficha tiene características únicas que influirán en tu estilo de juego:
               -Velocidad: Indica cuántas casillas puedes moverte en cada turno.
               -Habilidad Especial: Cada ficha cuenta con una habilidad única que estará vigente durante un turno:
                    1. Duplicar Puntos: permite al jugador duplicar el valor de cada pez.
                    2. Supervelocidad: el jugador puede moverse 10 casillas más de lo habitual.
                    3. Atravesar Pared: permite al jugador atravesar cualquier pared del laberinto.
                    4. Inmunidad a Trampa: el jugador es inmune a cualquier trampa que pueda encontrar.
                    5. Teletransportación: La teletransportación permite al jugador moverse a un lugar 
                    aleatorio dentro del laberinto una sola vez en ese turno.
               -Tiempo de Enfriamiento: Después de usar tu habilidad, deberás esperar un número determinado 
                de turnos antes de poder utilizarla nuevamente.";

            explicacion[2] = @"Recolección de Pescados:
            A medida que te mueves por el laberinto, por las teclas W(arriba), S(abajo), D(derecha), A(izquierda) encontrarás pescados esparcidos por el terreno. 
            Cada pescado que recojas te otorgará puntos.";

            explicacion[3] = @" Trampas:
            El laberinto está lleno de trampas que pueden afectar tu rendimiento:
            🕐 Trampa de Tiempo: Aumenta el tiempo de enfriamiento de tu habilidad,lo que significa que 
            tendrás que esperar más turnos para volver a usarla.
            ⛔ Trampa Puntos: Si caes en esta trampa, perderás un punto, lo que puede ser crucial en la 
            competencia.
            🛑 Trampa Velocidad: Reduce tu velocidad, limitando la cantidad de casillas que puedes mover 
            en tu próximo turno.";

            explicacion[4] = @"Final del Juego:
            El juego continúa hasta que no queden más pescados en el laberinto.";

            int index = 0;

            while (index<5)
            {
                Console.Clear();
                var panel = new Panel($"[bold italic gray]{explicacion[index]}[/]")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.SkyBlue1));
                 AnsiConsole.Write(panel);
                 Console.WriteLine("Presione cualquier tecla para continuar");
                Console.ReadKey();
                index = index+1;
            }

        jugadores = new List<Jugador>();
        int[] PosicionesX = { 1, 23};
        int[] PosicionesY = { 1, 23};
        laberinto = new Laberinto(25, 25);
        fichas = new List<Fichas>
        {
            new Fichas("Ficha 1", new HabilidadDuplicarPuntos(), 20),
            new Fichas("Ficha 2", new HabilidadSuperVelocidad(), 20),
            new Fichas("Ficha 3", new HabilidadTeletransportacion(), 20),
            new Fichas("Ficha 4", new HabilidadAtravesarPared(), 20),
            new Fichas("Ficha 5", new HabilidadInmunidad(), 20),
        };

        Console.Clear();

        for (int i = 0; i < 2; i++)
        {
            AnsiConsole.Markup($"[SkyBlue1]Jugador {i + 1}, ingresa tu nombre:[/]\n");
            string nombre = Console.ReadLine();
            Jugador jugador = new Jugador(nombre, i + 1, PosicionesX[i], PosicionesY[i], laberinto);
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
    var highlightStyle = new Style().Foreground(Color.SkyBlue1);

    // Lista de opciones para el menú
    var opciones = new List<string>();
    for (int i = 0; i < fichas.Count; i++)
    {
        opciones.Add($"{i + 1}. {fichas[i].Nombre} (Habilidad: {fichas[i].Habilidad.Nombre}, Velocidad: {fichas[i].Velocidad}, Tiempo de enfriamiento: {fichas[i].Habilidad.TiempoEnfriamiento})");
    }

    // Mostrar el menú
    var seleccion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[SkyBlue1]Por favor, elige una ficha:[/]")
            .HighlightStyle(highlightStyle)
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
         int i=0;
        while (true)
        {   
            for (int j = 0; j < jugadores[i%2].FichaElegida.VelocidadOriginal; j++)
            {
                
                laberinto.DibujarEnBuffer(ScreenBuffer);
                ScreenBuffer.Render();
                jugadores[0].MostrarCaracteristicasJugadores(jugadores[i%2]);
                Movimiento(jugadores[i%2], ref j);
            }
               jugadores[i%2].FichaElegida.ActualizarEnfriamiento();
              
               RestablecerVelocidad(jugadores[i%2]);
           
               jugadores[i%2].FichaElegida.Habilidad.Activo=false; //desactivar la habilidad

             if (jugadores[i%2].FichaElegida.Habilidad is HabilidadSuperVelocidad habilidadSuperVelocidad)
            {
                if (habilidadSuperVelocidad.TurnosRestantes==jugadores[i%2].FichaElegida.Habilidad.TiempoEnfriamiento-1)
                {
                    habilidadSuperVelocidad.RestablecerVelocidadHabilidad(jugadores[i%2]);
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
         string mensajeVictoria="";
        if (jugadores[0].Puntuacion==jugadores[1].Puntuacion)
        {
            mensajeVictoria=@"
            ███████╗███╗░░░███╗██████╗░░█████╗░████████╗███████╗  
            ██╔════╝████╗░████║██╔══██╗██╔══██╗╚══██╔══╝██╔════╝  
            █████╗░░██╔████╔██║██████╔╝███████║░░░██║░░░█████╗░░  
            ██╔══╝░░██║╚██╔╝██║██╔═══╝░██╔══██║░░░██║░░░██╔══╝░░  
            ███████╗██║░╚═╝░██║██║░░░░░██║░░██║░░░██║░░░███████╗  
            ╚══════╝╚═╝░░░░░╚═╝╚═╝░░░░░╚═╝░░╚═╝░░░╚═╝░░░╚══════╝  
            ";
        }
        else if (jugadores[0].Puntuacion>jugadores[1].Puntuacion)
        {
           mensajeVictoria=@"
            ███████╗███████╗██╗     ██╗ ██████╗██╗██████╗  █████╗ ██████╗ ███████╗███████╗
            ██╔════╝██╔════╝██║     ██║██╔════╝██║██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔════╝
            █████╗  █████╗  ██║     ██║██║     ██║██║  ██║███████║██║  ██║█████╗  ███████╗
            ██╔══╝  ██╔══╝  ██║     ██║██║     ██║██║  ██║██╔══██║██║  ██║██╔══╝  ╚════██║
            ██║     ███████╗███████╗██║╚██████╗██║██████╔╝██║  ██║██████╔╝███████╗███████║
            ╚═╝     ╚══════╝╚══════╝╚═╝ ╚═════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚══════╝
                                                                                        
            ██████╗ ██╗      █████╗ ██╗   ██╗███████╗██████╗     ███╗ 
            ██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝██╔════╝██╔══██╗   ████║
            ██████╔╝██║     ███████║ ╚████╔╝ █████╗  ██████╔╝     ██║
            ██╔═══╝ ██║     ██╔══██║  ╚██╔╝  ██╔══╝  ██╔══██╗     ██║
            ██║     ███████╗██║  ██║   ██║   ███████╗██║  ██║     ██║
            ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝     ╚═╝
                                                                    
            ██╗  ██╗ █████╗ ███████╗     ██████╗  █████╗ ███╗   ██╗ █████╗ ██████╗  ██████╗ 
            ██║  ██║██╔══██╗██╔════╝    ██╔════╝ ██╔══██╗████╗  ██║██╔══██╗██╔══██╗██╔═══██╗
            ███████║███████║███████╗    ██║  ███╗███████║██╔██╗ ██║███████║██║  ██║██║   ██║
            ██╔══██║██╔══██║╚════██║    ██║   ██║██╔══██║██║╚██╗██║██╔══██║██║  ██║██║   ██║
            ██║  ██║██║  ██║███████║    ╚██████╔╝██║  ██║██║ ╚████║██║  ██║██████╔╝╚██████╔╝
            ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═════╝  ╚═════╝ 
           ";
        }

        else
        {
            mensajeVictoria=@"
            ███████╗███████╗██╗     ██╗ ██████╗██╗██████╗  █████╗ ██████╗ ███████╗███████╗
            ██╔════╝██╔════╝██║     ██║██╔════╝██║██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔════╝
            █████╗  █████╗  ██║     ██║██║     ██║██║  ██║███████║██║  ██║█████╗  ███████╗
            ██╔══╝  ██╔══╝  ██║     ██║██║     ██║██║  ██║██╔══██║██║  ██║██╔══╝  ╚════██║
            ██║     ███████╗███████╗██║╚██████╗██║██████╔╝██║  ██║██████╔╝███████╗███████║
            ╚═╝     ╚══════╝╚══════╝╚═╝ ╚═════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚══════╝
                                                                                        
            ██████╗ ██╗      █████╗ ██╗   ██╗███████╗██████╗      ██████╗
            ██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝██╔════╝██╔══██╗     ╚════██╗
            ██████╔╝██║     ███████║ ╚████╔╝ █████╗  ██████╔╝       ███╔═╝
            ██╔═══╝ ██║     ██╔══██║  ╚██╔╝  ██╔══╝  ██╔══██╗     ██╔══╝
            ██║     ███████╗██║  ██║   ██║   ███████╗██║  ██║     ███████╗
            ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝     ╚══════╝
                                                                    
            ██╗  ██╗ █████╗ ███████╗     ██████╗  █████╗ ███╗   ██╗ █████╗ ██████╗  ██████╗ 
            ██║  ██║██╔══██╗██╔════╝    ██╔════╝ ██╔══██╗████╗  ██║██╔══██╗██╔══██╗██╔═══██╗
            ███████║███████║███████╗    ██║  ███╗███████║██╔██╗ ██║███████║██║  ██║██║   ██║
            ██╔══██║██╔══██║╚════██║    ██║   ██║██╔══██║██║╚██╗██║██╔══██║██║  ██║██║   ██║
            ██║  ██║██║  ██║███████║    ╚██████╔╝██║  ██║██║ ╚████║██║  ██║██████╔╝╚██████╔╝
            ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═════╝  ╚═════╝                     
            ";
        }
        Console.Clear();
        
            AnsiConsole.Markup($"[bold green]{mensajeVictoria}[/]");
        
        Console.Beep(E4, 200); // Mi
        Console.Beep(E4, 200); // Mi
        Console.Beep(E4, 200); // Mi
        Console.Beep(G4, 200); // Sol
        Console.Beep(A4, 200); // La
        Console.Beep(G4, 200); // Sol
        
        Thread.Sleep(100);      // Pausa breve

        Console.Beep(F4, 200); // Fa
        Console.Beep(E4, 200); // Mi
        Console.Beep(D4, 200); // Re
        Console.Beep(C4, 200); // Do
    }

    public void Movimiento(Jugador jugador,ref int j )
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.H && jugador.FichaElegida.Habilidad.PuedeActivar()) // Activar habilidad
    {
        jugador.FichaElegida.Habilidad.Activar(jugador, laberinto);
        j--; //para que la activacion de la habilidad no sea un movimiento
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

        //Atravesar pared
        if (laberinto.tablero[newPositionX, newPositionY].Tipo==Celda.TipoCelda.Pared &&
            jugador.FichaElegida.Habilidad is HabilidadAtravesarPared &&
            jugador.FichaElegida.Habilidad.Activo &&  //boleanos
            EsMovimientoValido(newPositionX+difx, newPositionY+dify))
        {
           

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = false;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = null;

            jugador.PositionX=newPositionX+difx;
            jugador.PositionY=newPositionY+dify; //moverse a la posicion  despues de la pared

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = true;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = jugador;
             jugador.FichaElegida.Velocidad--;


        }

        else if (EsMovimientoValido(newPositionX, newPositionY))
        {   
            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = false;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = null;

            jugador.PositionX=newPositionX;
            jugador.PositionY=newPositionY;

            laberinto.tablero[jugador.PositionX,jugador.PositionY].HayJugador = true;
            laberinto.tablero[jugador.PositionX,jugador.PositionY].jugador = jugador;
            jugador.FichaElegida.Velocidad--;
        }
        else Movimiento(jugador,ref j);

        if (laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Trampa &&
            !jugador.FichaElegida.EsInmune)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].TrampaAsociada.Activar(jugador);
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
            Console.Beep(300, 300);
        }

        if (laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Pescado&&
        jugador.FichaElegida.Habilidad is HabilidadDuplicarPuntos && jugador.FichaElegida.Habilidad.Activo)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
            jugador.Puntuacion+=2;
             Console.Beep(2000, 300);
        }

        else if(laberinto.tablero[jugador.PositionX, jugador.PositionY].Tipo==Celda.TipoCelda.Pescado)
        {
            laberinto.tablero[jugador.PositionX, jugador.PositionY].ConvertirEnCamino();
            jugador.Puntuacion++;
            Console.Beep(2000, 300);
        }
    }
    
       public bool EsMovimientoValido(int x, int y)
    {
        // Validar que el laberinto y el tablero estén inicializados
        if (laberinto == null || laberinto.Tablero == null)
        {
            return false;
        }

        // Validar que las coordenadas estén dentro de los límites del tablero
        if (x < 0 || x >= laberinto.sizeX || y < 0 || y >= laberinto.sizeY)
        {
            return false;
        }

        // Validar que la celda no sea una pared
        if (laberinto.Tablero[x, y].Tipo == Celda.TipoCelda.Pared)
        {
            return false;
        }
        // verificar si hay otro jugador
        if (laberinto.Tablero[x, y].HayJugador)
        {
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

      public void RestablecerVelocidad(Jugador jugador)
    {
        jugador.FichaElegida.Velocidad = jugador.FichaElegida.VelocidadOriginal; 
    }

    // Definición de frecuencias 
        int C4 = 261; // Do
        int D4 = 294; // Re
        int E4 = 329; // Mi
        int F4 = 349; // Fa
        int G4 = 392; // Sol
        int A4 = 440; // La
        int B4 = 493; // Si

}