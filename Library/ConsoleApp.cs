using Library.models;
using DataMapper;
using System;
using System.Configuration;
using System.Data.Entity;
using log4net;

namespace Library
{
    internal class ConsoleApp
    {
        static void Main(string[] args)
        {
            var context = new LibraryContext();
            // Environment.SetEnvironmentVariable("LOG_FILE_PATH", "D:\\log.txt");
            log4net.Util.LogLog.InternalDebugging = true;
        }
    }
}
