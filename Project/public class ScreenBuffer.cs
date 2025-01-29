using Spectre.Console;
using System;

public class ScreenBuffer
{
    private string[,] buffer; // Matriz que almacena los caracteres
    private int width;
    private int height;

    public ScreenBuffer(int width, int height)
    {
        this.width = width;
        this.height = height;
        buffer = new string[height, width];
    }

    // Dibuja un carácter en una posición específica del buffer
    public void Draw(int x, int y, string content)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            buffer[y, x] = content;
        }
    }

    // Copia el contenido del buffer a la consola
    public void Render()
    {
        Console.CursorVisible = false;

        // Calcular las posiciones de inicio para centrar la matriz
        int startX =28;
        int startY =0;

        for (int y = 0; y < height; y++)
        {
           Console.SetCursorPosition(startX, startY + y); // Mover el cursor a la posición centrada
            for (int x = 0; x < width; x++)
            {
                AnsiConsole.Markup(buffer[y, x]); // Renderiza el contenido con formato
            }
            Console.WriteLine(); // Salto de línea al final de cada fila
        }
       
    }
}
