using CountryFlag;
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
    public partial class ChangeData : UserControl, IContent
    {
        public static ChangeData createAcc;
        public TextBox EName;
        public TextBox EEmail;
        public TextBox EDonate;
        public ComboBox EFlag;
        public DatePicker EDob;

        private bool IsAdmin;
        public ChangeData()
        {

            InitializeComponent();
            ExPro ep = Helper.Helper.Synched();
            country.ItemsSource = Enum.GetValues(typeof(CountryFlag.CountryCode)).Cast<CountryFlag.CountryCode>();

            EName = exuser;
            exuser.Text = ep.ExUsername;
            EEmail = email;
            email.Text = ep.Email;
            EDonate = donate;
            donate.Text = ep.DonateUrl;
            EDob = dob;
            dob.SelectedDate = ep.Dob;
            EFlag = country;
            CountryCode cc;
            Enum.TryParse(ep.Country.ToString(), out cc);
            country.SelectedItem = cc;

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
