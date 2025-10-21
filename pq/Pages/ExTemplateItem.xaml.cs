using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using pq.Model;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        int IsUserSynched;
        private bool didvote;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool DidVote
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

            Grid g = (Parent as FrameworkElement).Parent as Grid;
            int ind = ((((g).Children[0] as ListBox).SelectedIndex));
            ExTemplate et = Helper.Helper.Tmpl[ind - 1];
            UsernameTxt.Text = et.Username.ToUpper();
            RatingTxt.Text = et.Rating.ToString();

            string query = "SELECT ID FROM boilpack WHERE Username='" + Helper.Helper.Synched().Username + "';";

            var cmd = new MySqlCommand(query, DBConnection.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IsUserSynched = reader.GetInt32(0);
                if (IsUserSynched == 0)
                    return;
                else
                {

                    string query2 = "SELECT ForID FROM ratings WHERE FromID='" + IsUserSynched + "';";

                    var cmd2 = new MySqlCommand(query2, DBConnection.Connection);
                    var reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int IsFor = reader2.GetInt16(0);
                        if (IsFor != 0)
                            if (et.ID == IsFor)
                            {
                                IsRated.Foreground = (Brush)Application.Current.TryFindResource("Accent");

                                return;
                            }

                            else
                            {
                                IsRated.Foreground = (Brush)Application.Current.TryFindResource("ItemTextDisabled");
                                return;
                            }

                    }
                    DBConnection.Close();
                }


            }





        }


        private void IsRated_Click(object sender, RoutedEventArgs e)
        {

            Grid g = (Parent as FrameworkElement).Parent as Grid;
            int ind = ((((g).Children[0] as ListBox).SelectedIndex));
            ExTemplate et = Helper.Helper.Tmpl[ind - 1];
            
            string query2 = "SELECT ForID FROM ratings WHERE FromID='" + IsUserSynched + "';";

            var cmd2 = new MySqlCommand(query2, DBConnection.Connection);
            var reader2 = cmd2.ExecuteReader(); 
            DBConnection.Close();
            if (reader2.HasRows == false)
            {
             
                string query4 = "insert into ratings (ForID, FromID) values('" + et.ID + "','" + IsUserSynched + "');";
                var cmd = new MySqlCommand(query4, DBConnection.Connection);
                int reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    MessageBox.Show("Bravo2222 - " + reader.ToString());
                }
                else
                {
                    MessageBox.Show("Fail222 22- " + reader.ToString());
                }

                DBConnection.Close();
                LoadTmpl(null,null);
            }
            while (reader2.Read())
            {
                int IsFor = reader2.GetInt16(0);


                    string query = "delete from ratings where   FromID='" + IsUserSynched + "';";
                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        MessageBox.Show("Bravo - " + reader.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Fail - " + reader.ToString());
                    }

                    DBConnection.Close();
                     LoadTmpl(null, null);
                }
            }
        }
    }

