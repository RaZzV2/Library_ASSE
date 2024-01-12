using Library.models;
using DataMapper;
using System;
using System.Configuration;
using System.Data.Entity;
using log4net;
using ServiceLayer.Services;
using DataMapper.SQLServerDAO;
using DomainModel;
using System.Runtime.InteropServices;

namespace Library
{
    internal class ConsoleApp
    {
        static void Main(string[] args)
        {
            var configurator = log4net.Config.XmlConfigurator.Configure();
            if (configurator != null)
            {
                // Log4net a fost inițializat cu succes
                Console.WriteLine("log4net was initialized successfully.");
            }
            IReaderIDAO iReaderIDAO = new ReaderDAO();
            ReaderService readerService = new ReaderService(iReaderIDAO);

            readerService.Add(new Reader
            {
                ReaderFirstName = "Gigel",
                ReaderLastName = "Dorel",
                Address = "Strada x Da",
                EmailAddress = "razzkk@gmail.com",
                Role = false,
                PhoneNumber = "0732139910"
            });

        }
    }
}
