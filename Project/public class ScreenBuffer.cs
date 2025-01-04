using Spectre.Console;
using System;

public class ScreenBuffer
{
    private string[,] buffer; // Matriz que almacena los caracteres y colores
    private int width;
    private int height;

    public ScreenBuffer(int width, int height)
    {
        this.width = width;
        this.height = height;
        buffer = new string[height, width];
        ClearBuffer();
    }

    // Limpia el buffer con espacios en blanco
    public void ClearBuffer()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                buffer[y, x] = " "; // Espacio en blanco
            }
        }
    }

    // Dibuja un carácter con formato (colores, símbolos) en una posición específica del buffer
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
       Console.SetCursorPosition(0, 0);
        Console.CursorVisible=false;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                AnsiConsole.Markup(buffer[y, x]); // Renderiza el contenido con formato
            }
            Console.WriteLine(); // Salto de línea al final de cada fila
        }
    }
}