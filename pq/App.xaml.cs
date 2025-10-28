using FirstFloor.ModernUI.Presentation;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows;
using System.Windows.Media;

namespace pq
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Example: set DataDirectory to a folder inside the app
            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);

            var settings = pq.Properties.Settings.Default;

            // Load theme
            AppearanceManager.Current.ThemeSource = new Uri(settings.ThemeSource, UriKind.RelativeOrAbsolute);

            // Load accent color from string
            if (!string.IsNullOrWhiteSpace(settings.AccentColor))
            {
                var color = (Color)ColorConverter.ConvertFromString(settings.AccentColor);
                AppearanceManager.Current.AccentColor = color;
            }



            // Optional: auto-save on changes
            AppearanceManager.Current.PropertyChanged += (s, ev) =>
            {
                if (ev.PropertyName == nameof(AppearanceManager.Current.ThemeSource))
                    settings.ThemeSource = AppearanceManager.Current.ThemeSource.ToString();

                if (ev.PropertyName == nameof(AppearanceManager.Current.AccentColor))
                    settings.AccentColor = AppearanceManager.Current.AccentColor.ToString();

                settings.Save();
            };
        }
        public App()
        {
            //if (!IsRunAsAdministrator())
            //{
            //    var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

            //    // The following properties run the new process as administrator
            //    processInfo.UseShellExecute = true;
            //    processInfo.Verb = "runas";

            //    // Start the new process
            //    try
            //    {
            //        Process.Start(processInfo);
            //    }
            //    catch (Exception)
            //    {
            //        // The user did not allow the application to run as administrator
            //        MessageBox.Show("Sorry, this application must be run as Administrator.");
            //    }

            //    // Shut down the current process
            //    Application.Current.Shutdown();
            //}
        }
        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            pq.Properties.Settings.Default.Save();
        }
    }
}
