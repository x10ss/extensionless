using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        private MainWindow _mainWindow;
        public List()
        {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;


            // ListLinksList.Links = Helper.Helper.GetLL(Model.Folder.ExTensionLess);
            /*   Dialog newdialoge = new Dialog();
               newdialoge.Owner = _mainWindow;
               newdialoge.ShowDialog();*/
        }
    }
}
