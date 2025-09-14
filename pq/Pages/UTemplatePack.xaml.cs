using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for TemplatePack.xaml
    /// </summary>
    public partial class UTemplatePack : UserControl, INotifyPropertyChanged
    {
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool isloaded2;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoaded2
        {
            get { return isloaded2; }
            set
            {
                if (isloaded2 != value)
                {
                    isloaded2 = value;
                    OnPropertyChanged("IsLoaded2");
                }

            }
        }
        public UTemplatePack()
        {
            try
            {

                LoadComplete(null, null);

                DataContext = this;
            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage(e.Message, "usertmplexcept", MessageBoxButton.OK);
            }

        }

        private void LoadComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoaded2 = true;
            LinkCollection lc = Helper.Helper.GetTmplLinks();
            UserTemplates.mt.Links = lc;
            TemplateCountText.Text = (lc.Count - 1).ToString();
            ChampText.Text = lc[1].DisplayName.ToUpper().Split('.')[1];
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
