using System;

namespace Berechnung
{
    class Berechnung
    {
        public int ap1 = 0;
        public int ap2schriftlich = 0;
        public int ap2muendlich = 0;
        public int ap2schriftlichbwl = 0;
        public int ap2schriftlichpug = 0;
        public int ap2schriftlichprog = 0;

        static void Main()
        {
            Console.WriteLine("Gebe die Note der AP1 ein: ");
            string ap1s = Console.ReadLine();
            int AP1 = Convert.ToInt32(ap1s);
            int notehigh = 6;
            if (AP1 > notehigh)
            {
                Console.WriteLine("Note ist nicht im erlaubten bereich!");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Gebe die Note der AP2 schriftlich ein: ");
                string ap2s = Console.ReadLine();
                int ap2schriftlich = Convert.ToInt32(ap2s);
                if (ap2schriftlich > notehigh)
                {
                    Console.WriteLine("Note ist nicht im erlaubten bereich!");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Gebe die Note der AP2 mündlich ein: ");
                    string ap2m = Console.ReadLine();
                    int ap2muendlich = Convert.ToInt32(ap2m);
                    if (ap2muendlich > notehigh)
                    {
                        Console.WriteLine("Note ist nicht im erlabten bereich");
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }

}