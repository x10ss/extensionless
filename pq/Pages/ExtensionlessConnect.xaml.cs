using FirstFloor.ModernUI.Presentation;
using System;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for ExtensionlessConnect.xaml
    /// </summary>
    public partial class ExtensionlessConnect : UserControl
    {
        public ExtensionlessConnect()
        {
            InitializeComponent();
            LinkCollection lc = new LinkCollection();
            if (Helper.Helper.Synched() == null)
            {
                Link l2 = new Link();
                l2.Source = new Uri("/Pages/Login.xaml", UriKind.RelativeOrAbsolute);
                l2.DisplayName = "Login";
                lc.Add(l2);

                Link l1 = new Link();
                l1.Source = new Uri("/Pages/CreateAccount.xaml", UriKind.RelativeOrAbsolute);
                l1.DisplayName = "Create account";
                lc.Add(l1);

            }
            else
            {
                Link l4 = new Link();
                l4.Source = new Uri("/Pages/ChangeData.xaml", UriKind.RelativeOrAbsolute);
                l4.DisplayName = "Change Data";
                lc.Add(l4);

                Link l5 = new Link();
                l5.Source = new Uri("/Pages/Disconnect.xaml", UriKind.RelativeOrAbsolute);
                l5.DisplayName = "Disconnect";
                lc.Add(l5);

                Link l6 = new Link();
                l6.Source = new Uri("/Pages/Delete.xaml", UriKind.RelativeOrAbsolute);
                l6.DisplayName = "Delete";
                lc.Add(l6);
            }

            ExConnTabLinks.Links = lc;
            ExConnTabLinks.SelectedSource = lc[0].Source;



        }
    }
}
