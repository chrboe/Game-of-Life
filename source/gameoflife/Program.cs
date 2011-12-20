using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameoflife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 41);
            Console.SetBufferSize(80, 41);

            string[,] feld = new string[80, 40];
            string towrite = "*";
            int[,] nachbarn = new int[80, 40];
            bool auswahl = true;
            bool stepbystep = false;
            bool end = false;
            bool start = true;
            bool error = false;
            int auswahlx = 0;
            int auswahly = 0;
            int people = 0;
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            

            for (int y = 0; y <= 39; y++)
            {
                for (int x = 0; x <= 79; x++)
                {
                    feld[x, y] = " ";
                }
            }

            Console.WriteLine("Willkommen zum Game of Life!");
            while(start == true)
            {
                if (error == true)
                {
                    Console.Clear();
                    Console.WriteLine("Fehler! Nur ein ASCII-Symbol eingeben! (Kein Leerzeichen)");
                }
                Console.WriteLine("Bitte geben sie das Symbol für die Menschen an:");
                Console.Write(">");
                try
                {
                    towrite = Console.ReadLine();
                    if (towrite.Length == 1)
                        start = false;
                    else
                        error = true;
                    if (towrite == " ")
                    {
                        start = true;
                        error = true;
                    }
                }
                catch
                {
                    start = true;
                    error = true;
                }
            }
            Console.CursorVisible = false;

            Console.Clear();

            Console.WriteLine("Hilfe:");
            Console.WriteLine("Platzieren sie zuerst die Menschen!");
            Console.WriteLine("Mit den Pfeiltasten steuern sie den Cursor.");
            Console.WriteLine("Drücken sie die Leertaste zum Platzieren und die Backspace Taste zum Löschen.");
            Console.WriteLine("Mit S starten sie die Schritt-für-Schritt Wiedergabe.");
            Console.WriteLine("Mit Escape löschen sie alle Menschen.");
            Console.WriteLine();
            Console.WriteLine("Um die Simulation zu starten, drücken sie nach dem Platzieren Enter.");
            Console.ReadLine();
            Console.Clear();

            while (auswahl == true)
            {
                for (int y = 0; y <= 39; y++)
                {
                    for (int x = 0; x <= 79; x++)
                    {
                        Console.Write(feld[x, y]);
                    }
                }
                Console.SetCursorPosition(auswahlx, auswahly);
                Console.Write(towrite);
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        auswahlx--;
                        break;
                    case ConsoleKey.RightArrow:
                        auswahlx++;
                        break;
                    case ConsoleKey.UpArrow:
                        auswahly--;
                        break;
                    case ConsoleKey.DownArrow:
                        auswahly++;
                        break;
                    case ConsoleKey.Spacebar:
                        feld[auswahlx, auswahly] = towrite;
                        break;
                    case ConsoleKey.Backspace:
                        feld[auswahlx, auswahly] = " ";
                        break;
                    case ConsoleKey.Enter:
                        auswahl = false;
                        break;
                    case ConsoleKey.Escape:
                        for (int y = 0; y <= 39; y++)
                        {
                            for (int x = 0; x <= 79; x++)
                            {
                                feld[x, y] = " ";
                            }
                        }
                        break;
                    case ConsoleKey.S:
                        auswahl = false;
                        stepbystep = true;
                        break;
                    case ConsoleKey.D1:
                        feld[39, 20] = towrite;
                        feld[40, 20] = towrite;
                        feld[41, 20] = towrite;
                        break;
                }
                Console.Clear();
            }

            while (end == false)
            {
                for (int y = 1; y <= 38; y++)
                {
                    for (int x = 1; x <= 78; x++)
                    {
                        if (feld[x + 1, y + 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x + 1, y - 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x - 1, y - 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x, y + 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x, y - 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x - 1, y + 1] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x + 1, y] == towrite)
                            nachbarn[x, y] += 1;
                        if (feld[x - 1, y] == towrite)
                            nachbarn[x, y] += 1;
                    }
                }
                for (int y = 1; y <= 38; y++)
                {
                    for(int x = 1; x <= 78; x++)
                    {
                        if(feld[x,y] == towrite)
                        {
                            if (nachbarn[x, y] < 2)
                                feld[x, y] = " ";
                            if (nachbarn[x, y] > 3)
                                feld[x, y] = " ";
                            if (nachbarn[x, y] == 2 || nachbarn[x,y] == 3)
                                feld[x, y] = towrite;
                        }
                        else
                        {
                            if (nachbarn[x, y] == 3)
                                feld[x, y] = towrite;
                        }
                    }
                }
                for (int y = 0; y <= 39; y++)
                {
                    for (int x = 0; x <= 79; x++)
                    {
                        Console.Write(feld[x, y]);
                    }

                }
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Bevölkerung: " + people);
                for (int a = 0; a <= 79; a++)
                {
                    for (int b = 0; b <= 39; b++)
                        nachbarn[a, b] = 0;
                }

                // FRAME ENDE

                people = 0;

                for (int y = 0; y <= 39; y++)
                {
                    for (int x = 0; x <= 79; x++)
                    {
                        if (feld[x, y] == towrite)
                            people += 1;
                    }

                }
                if (people == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Das Leben ist ausgestorben.");
                    System.Threading.Thread.Sleep(2000);
                    end = true;
                }
                
                if (stepbystep == true)
                    Console.ReadLine();
                else
                    System.Threading.Thread.Sleep(100);
                Console.SetCursorPosition(0,0);
            }
        }
    }
}
