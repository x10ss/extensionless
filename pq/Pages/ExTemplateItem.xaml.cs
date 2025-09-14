using pq.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for TemplatePack.xaml
    /// </summary>
    public partial class ExTemplateItem : UserControl, INotifyPropertyChanged
    {
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool? didvote;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool? DidVote
        {
            get { return didvote; }
            set
            {
                if (value != didvote)
                {
                    didvote = value;
                    OnPropertyChanged("DidVote");
                }
            }
        }
        public ExTemplateItem()
        {
            InitializeComponent();
            DataContext = this;
            DidVote = Helper.Helper.Synched() == null ? (bool?)null : Helper.Helper.rnd.NextDouble() > 0.5 ? true : false;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Parent == null)
            {
                return;
            }
            (((this.Parent as FrameworkElement).Parent as Grid).Children[0] as ListBox).SelectionChanged += LoadTmpl;


        }

        private void LoadTmpl(object sender, SelectionChangedEventArgs e)
        {

            if (Parent == null)
            {
                return;

            }
            DidVote = Helper.Helper.Synched() == null ? (bool?)null : Helper.Helper.rnd.NextDouble() > 0.5 ? true : false;

            Grid g = (Parent as FrameworkElement).Parent as Grid;
            int ind = ((((g).Children[0] as ListBox).SelectedIndex));
            ExTemplate et = Helper.Helper.Tmpl[ind - 1];
            UsernameTxt.Text = et.Username.ToUpper();
            RatingTxt.Text = et.Rating.ToString();
        }
    }
}
