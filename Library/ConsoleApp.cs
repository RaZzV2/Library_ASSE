using Library.models;
using System;
using System.Configuration;
using System.Data.Entity;

namespace Library
{
    internal class ConsoleApp
    {
        static void Main(string[] args)
        {
            var context = new LibraryContext();
        }
    }
}
