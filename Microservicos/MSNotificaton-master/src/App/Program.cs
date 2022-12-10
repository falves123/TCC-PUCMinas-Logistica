using DevPrime.Web;
using DevPrime.Stack.App.Load;
using System;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new DevPrimeApp<Startup>(new App(), args);
        }
    }
}