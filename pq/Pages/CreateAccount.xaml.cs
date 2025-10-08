using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using pq.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : UserControl, IContent
    {
        public static CreateAccount createAccount;
        public TextBox EName;
        public TextBox EDonate;
        public ComboBox EFlag;
        public PasswordBox EPassword;

        private bool IsAdmin;
        public CreateAccount()
        {

            InitializeComponent();
            createAccount = this;
            country.ItemsSource = Enum.GetValues(typeof(CountryFlag.CountryCode)).Cast<CountryFlag.CountryCode>();

            EName = exuser;
         //   EEmail = email;
            EDonate = donate;
          //  EDob = dob;
            EFlag = country;
            EPassword = expass;
        }


        private void Province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  if (Province.SelectedIndex == -1)
            {
                return;
            }
        //    List<City> cities = Helper.Helper.WorldCities.Where(x => x.Province == Province.SelectedItem.ToString()).ToList();
           // City.ItemsSource = cities;
        }

        private void country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (country.SelectedIndex == -1)
            {
                return;
            }
            List<City> cities = Helper.Helper.WorldCities.Where(x => x.CountryCode == country.SelectedItem.ToString()).ToList();
          //  City.ItemsSource = cities;
          //  Province.ItemsSource = cities.GroupBy(x => x.Province).Select(x => x.FirstOrDefault().Province).ToList();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            createAccount = null;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            createAccount = null;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            createAccount = this;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            createAccount = null;
        }
    }
}
