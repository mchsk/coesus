﻿using System;
using System.Collections.Generic;
using System.Text;

namespace coesus
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings._Apply(args);
            Print._Title();
            Print._Version();
            Print._Settings();
            
            //ConsoleKeyInfo cki;
            //// Prevent example from ending if CTL+C is pressed.
            //Console.TreatControlCAsInput = true;

            //Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
            //Console.WriteLine("Press the Escape (Esc) key to quit: \n");
            //do
            //{
            //    cki = Console.ReadKey();
            //    Console.Write(" --- You pressed ");
            //    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
            //    if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
            //    if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
            //    Console.WriteLine(cki.Key.ToString());
            //    Console.WriteLine(Settings.Server.Runtime());
            //} while (cki.Key != ConsoleKey.Escape);

        }
    }
}
