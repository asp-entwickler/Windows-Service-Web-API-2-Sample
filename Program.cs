﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Service_Web_API_2_Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {

                Console.ReadLine();

                WinServiceWebApi service1 = new WinServiceWebApi(args);
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new WinServiceWebApi(args) };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
