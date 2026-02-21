using System;
using System.Windows.Forms;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;
using CritterSimulator.CritterStorage.Bunso;
using CritterSimulator.CritterStorage.Ryan;
using CritterSimulator.CritterStorage.Colton;
using CritterSimulator.CritterStorage.Josh;

namespace CritterSimulator
{
    // CSE 142 Homework 8 (Critters)
    // Authors: Stuart Reges and Marty Stepp
    //
    // CritterMain provides the main method for a simple simulation program. Alter
    // the number of each critter added to the simulation if you want to experiment
    // with different scenarios. You can also alter the width and height passed to
    // the CritterFrame constructor.
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CritterFrame frame = new CritterFrame(70, 50);

            // Uncomment each of these lines as you complete these classes
            //frame.Add(30, typeof(Bear));
            frame.Add(30, typeof(Lion));
            //frame.Add(30, typeof(ZigZag));
            //frame.Add(30, typeof(Boon));
            //frame.Add(30, typeof(Cinnamon));
            //frame.Add(30, typeof(Giant));
            //frame.Add(30, typeof(Atom));
            //frame.Add(30, typeof(EMBO));
            frame.Add(30, typeof(FlyTrap));
            frame.Add(30, typeof(Food));
            frame.Add(50, typeof(Cartographer));

            frame.Start();
            Application.Run(frame);
        }
    }
}
