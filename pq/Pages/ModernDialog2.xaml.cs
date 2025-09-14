using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for ModernDialog2.xaml
    /// </summary>
    public partial class ModernDialog2 : ModernDialog
    {
        public ModernDialog2()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
            FrameworkElement fel = (Parent as FrameworkElement);
            FrameworkElement fel2 = (fel.Parent as FrameworkElement);
            FrameworkElement fel3 = (fel2.Parent as FrameworkElement);
            FrameworkElement fel4 = (fel3.Parent as FrameworkElement);
            FrameworkElement fel5 = (fel4.Parent as FrameworkElement);
        }

        private void ModernDialog_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
