using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for FlagPicker.xaml
    /// </summary>
    /// 

    public partial class FlagPicker : ModernDialog
    {
        private CountryFlag.CountryCode ctry;
        public CountryFlag.CountryCode CTRY
        {
            get { return ctry; }
            set
            {
                if (value != ctry)
                {
                    ctry = value;
                }
            }
        }

        public FlagPicker()
        {

            InitializeComponent();
            DataContext = this;
            CTRIES.ItemsSource = Enum.GetValues(typeof(CountryFlag.CountryCode)).Cast<CountryFlag.CountryCode>();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }

        private void SRCH_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CTRIES.ItemsSource = Enum.GetValues(typeof(CountryFlag.CountryCode)).Cast<CountryFlag.CountryCode>().Where(x => x.ToString().ToUpper().Contains(SRCH.Text.ToUpper()));

        }
    }
}
