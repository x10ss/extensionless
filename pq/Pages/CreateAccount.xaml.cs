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
        public static CreateAccount createAcc;
        public TextBox EName;
        public TextBox EEmail;
        public TextBox EDonate;
        public ComboBox EFlag;
        public DatePicker EDob;

        private bool IsAdmin;
        public CreateAccount()
        {

            InitializeComponent();
            country.ItemsSource = Enum.GetValues(typeof(CountryFlag.CountryCode)).Cast<CountryFlag.CountryCode>();

            EName = exuser;
            EEmail = email;
            EDonate = donate;
            EDob = dob;
            EFlag = country;
        }


        private void Province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Province.SelectedIndex == -1)
            {
                return;
            }
            List<City> cities = Helper.Helper.WorldCities.Where(x => x.Province == Province.SelectedItem.ToString()).ToList();
            City.ItemsSource = cities;
        }

        private void country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (country.SelectedIndex == -1)
            {
                return;
            }
            List<City> cities = Helper.Helper.WorldCities.Where(x => x.CountryCode == country.SelectedItem.ToString()).ToList();
            City.ItemsSource = cities;
            Province.ItemsSource = cities.GroupBy(x => x.Province).Select(x => x.FirstOrDefault().Province).ToList();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            createAcc = null;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            createAcc = this;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
