using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using PropertyNotify;

namespace Demo
{
    partial class MagicBall
    {
        [PropertyNotify]
        int position = -2; //in lightyears
        [PropertyNotify]
        int size = 9000; //in centimeters
        [PropertyNotify]
        string name = "Kevin"; //in english
    }







    class PropertyNotifyDemo
    {
        public static void DoSomething()
        {
            MagicBall kevin = new MagicBall();

            kevin.PropertyChanged += Print;

            kevin.Name = "Kevin 2";
            kevin.Position += 5;
            kevin.Size = kevin.GetHashCode();
        }

        public static void Print(object obj, PropertyChangedEventArgs args)
        {
            Console.WriteLine(args.PropertyName + ": " + obj);
        }
    }
}
