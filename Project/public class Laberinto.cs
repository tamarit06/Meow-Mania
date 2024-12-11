using Spectre.Console;
using System;
using System.Collections.Generic;

public class Laberinto
{
    private int[,] tablero;
    public int sizeX { get; set; }
    public int sizeY { get; set; }
    private Random rand = new Random();

    public Laberinto(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        InicializarTablero();
        GenerarLaberintoConPrim();
    }

    private void InicializarTablero()
    {
        tablero = new int[sizeX, sizeY];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tablero[i, j] = 1;  // 1 representa pared, 0 camino
            }
        }
    }

    private void GenerarLaberintoConPrim()
    {
        List<(int, int)> celdasAbiertas = new List<(int, int)>();
        int[] dx = { -1, 1, 0, 0 };  
        int[] dy = { 0, 0, -1, 1 };

        int inicioX = 1, inicioY = 1;
        tablero[inicioX, inicioY] = 0; 
        celdasAbiertas.Add((inicioX, inicioY));

        while (celdasAbiertas.Count > 0)
        {
            var (x, y) = celdasAbiertas[rand.Next(celdasAbiertas.Count)];
            List<(int, int)> celdasAdyacentes = new List<(int, int)>();
            for (int i = 0; i < 4; i++)
            {
                int nuevoX = x + dx[i] * 2;
                int nuevoY = y + dy[i] * 2;
                if (EsValido(nuevoX, nuevoY) && tablero[nuevoX, nuevoY] == 1)
                {
                    celdasAdyacentes.Add((nuevoX, nuevoY));
                }
            }
            if (celdasAdyacentes.Count > 0)
            {
                var (nx, ny) = celdasAdyacentes[rand.Next(celdasAdyacentes.Count)];
                tablero[nx, ny] = 0;
                tablero[(x + nx) / 2, (y + ny) / 2] = 0; 
                celdasAbiertas.Add((nx, ny));
            }
            else
            {
                celdasAbiertas.Remove((x, y));
            }
        }
    }

   public void MostrarLaberinto()
   {//poner la ficha 
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (tablero[i, j] == 0)
                {
                    // Imprimir espacio vacío
                    AnsiConsole.Markup("[green] [/]");
                }
                else
                {
                    // Imprimir bloque del laberinto en verde
                    AnsiConsole.Markup("[green]█[/]");
                }
            }
            AnsiConsole.WriteLine(""); 
        }
   }

    public bool EsValido(int x, int y)
    {
        return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
    }

    public int[,] Tablero => tablero;
}
