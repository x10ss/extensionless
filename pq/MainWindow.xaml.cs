using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
namespace pq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {

        public Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.BottomRight,
                offsetX: 0,
                offsetY: 0);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(0.66),
                maximumNotificationCount: MaximumNotificationCount.FromCount(100));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        public MainWindow()
        {
            InitializeComponent();
            Helper.Helper.InitTemplates(null, null);
            LinkCollection lc = new LinkCollection();

            ////////////////// Main
            //Link l4 = new Link();
            //l4.Source = new Uri("/Pages/Home.xaml", UriKind.RelativeOrAbsolute);
            //l4.DisplayName = "←○";
            //  lc.Add(l4);
            ////////////////// F1

            Link l3 = new Link();
            l3.Source = new Uri("/Pages/Extensionless.xaml", UriKind.RelativeOrAbsolute);
            l3.DisplayName = " ABOUT ";
            lc.Add(l3);
            Link l2 = new Link();
            l2.Source = new Uri("/Pages/SettingsPage.xaml", UriKind.RelativeOrAbsolute);
            l2.DisplayName = " HELP ";
            lc.Add(l2);
            ///////////////////// USER
            ///   Link l2 = new Link();

            ///////////////////// USER
            /*  Link l = new Link();
              l.Source = new Uri("/Pages/User.xaml", UriKind.RelativeOrAbsolute);
              l.DisplayName = "| " + Helper.Helper.Username + " |";
              lc.Add(l);*///old
            /*  Link l1new = new Link();
              l1new.Source = new Uri("/Pages/Profile.xaml", UriKind.RelativeOrAbsolute);
              l1new.DisplayName = "| Account |";
              //  lc.Add(l1new); 
              Link l2new = new Link();
              l2new.Source = new Uri("/Pages/MyTemplatePack.xaml", UriKind.RelativeOrAbsolute);
              l2new.DisplayName = "| Exhibit |";*/
            // lc.Add(l2new);
            ///////////////////// TMPL
            Link l5 = new Link();
            l5.Source = new Uri("/Pages/Templates.xaml", UriKind.RelativeOrAbsolute);
            l5.DisplayName = "| Templates |";
            //lc.Add(l5);
            ////////////////////////// Settings
            Link l6 = new Link();
            l6.Source = new Uri("/Pages/Settings/Appearance.xaml", UriKind.RelativeOrAbsolute);
            l6.DisplayName = " OPTIONS ";
            lc.Add(l6);

            
            ////////////////////////// ABOUT


            ////////////////////////////// End Links
            //  LinkGroup lg = new LinkGroup();

            //lg.GroupKey = "Top";
            // lg.DisplayName = "mENU";

            //lg.Links.Add(l4);
            //    lg.Links.Add(l2);
            //
            //    lg.Links.Add(l6);
            //    lg.Links.Add(l3);

            LinkGroup lg2 = new LinkGroup();
            lg2.DisplayName = "ACCOUNT";
            Link l3new = new Link();
            l3new.Source = new Uri("/Pages/ControlsStylesDataGrid.xaml", UriKind.RelativeOrAbsolute);
            l3new.DisplayName = "| Forge |";
            //Link l4new = new Link();
            //l4new.Source = new Uri("/Pages/ExtendedLib.xaml", UriKind.RelativeOrAbsolute);
            //l4new.DisplayName = "| Warehouse |";
            Link l42 = new Link();
            l42.Source = new Uri("/Pages/Profile.xaml", UriKind.RelativeOrAbsolute);
            l42.DisplayName = Environment.UserName;
           
            //lg.Links.Add(l4);
            //  lg2.Links.Add(l1new);

            // lg2.Links.Add(l2new);
            lg2.Links.Add(l3new);
            //lg2.Links.Add(l4new);
            lg2.Links.Add(l42);
            ////////////////////
            
            MenuLinkGroups[0].Links.Add(l42);
            MenuLinkGroups[0].DisplayName = "Dashboard";
            MenuLinkGroups[0].Links.Add(l3new);
            //MenuLinkGroups[0].Links.Add(l4new);
            //    MenuLinkGroups.Add(lg);
            TitleLinks = lc;
            Helper.Helper.Top2 = lc;
            LinkCollection lc2 = new LinkCollection();


            ////////////////// F1
            Link l2b = new Link();
            l2b.Source = new Uri("/Pages/Home.xaml", UriKind.RelativeOrAbsolute);
            l2b.DisplayName = "| Dashboard |";
            lc2.Add(l2b);
            ////////////////////////// ABOUT
            Link l5b = new Link();
            l5b.Source = new Uri("/Pages/Profile.xaml", UriKind.RelativeOrAbsolute);
            l5b.DisplayName = "| " + Environment.UserName + " |";
            lc2.Add(l5b);
            ///////////////////// USER    Link lb = new Link();
            Link l6b = new Link();
            l6b.Source = new Uri("/Pages/MyTemplatePack.xaml", UriKind.RelativeOrAbsolute);
            l6b.DisplayName = "| My Exhibit |";
            lc2.Add(l6b);

            Link lb = new Link();
            lb.Source = new Uri("/Pages/Split.xaml", UriKind.RelativeOrAbsolute);
            lb.DisplayName = "| Switchbox |";
            lc2.Add(lb);

            ////////////////////////// ABOUT
            Link l4b = new Link();
            l4b.Source = new Uri("/Pages/Templates.xaml", UriKind.RelativeOrAbsolute);
            l4b.DisplayName = "| exhibition |";
            lc2.Add(l4b);
            ////////////////////////// ABOUT
            //Link l3b = new Link();
            //l3b.Source = new Uri("/Pages/ExtendedLib.xaml", UriKind.RelativeOrAbsolute);
            //l3b.DisplayName = "| warehouse |";
            //lc2.Add(l3b);     ////////////////////////// ABOUT
            Link l3c = new Link();
            l3c.Source = new Uri("/Pages/ControlsStylesDataGrid.xaml", UriKind.RelativeOrAbsolute);
            l3c.DisplayName = "| forge |";
            lc2.Add(l3c);



            Helper.Helper.RegBase = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Classes", true);


            // Helper.Helper.Top1 = lc2;
        }

        public void MojHandle(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "SelectedLink")
            {
                object fv = FindName("FileViewer");


                LinkGroup hlb = (LinkGroup)sender;
            }

        }
        void employeeTabList_SelectedSourceChanged(object sender, SourceEventArgs e)
        {
            if (e.Source.OriginalString.EndsWith("EditEmployeeDetail.xaml"))
            {
                // You may want to set some property in that page's ViewModel, for example, indicating the selected User ID.
            }
        }
        private void MyHome2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {


        }


        private void MW_Closing(object sender, CancelEventArgs e)
        {
            ModernDialog.ShowMessage("!", ":(", MessageBoxButton.OK);
        }

        private void MW_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }

}
