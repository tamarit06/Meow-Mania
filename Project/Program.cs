  using System; 
  using Spectre.Console;
    class Program
   {
    static void Main(string[] args) 
    { 
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Juego juego=new Juego();
        juego.Jugar();

    } 

   }