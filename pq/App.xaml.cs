﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace pq
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
               //if (!IsRunAsAdministrator())
               //   {
               //       var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

               //       // The following properties run the new process as administrator
               //       processInfo.UseShellExecute = true;
               //       processInfo.Verb = "runas";

               //       // Start the new process
               //       try
               //       {
               //           Process.Start(processInfo);
               //       }
               //       catch (Exception)
               //       {
               //           // The user did not allow the application to run as administrator
               //           MessageBox.Show("Sorry, this application must be run as Administrator.");
               //       }

               //       // Shut down the current process
               //       Application.Current.Shutdown();
               //   }
        }
        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
