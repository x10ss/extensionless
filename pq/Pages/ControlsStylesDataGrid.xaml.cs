using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using pq.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace pq.Pages
{
    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            MyFEx course = (value as BindingGroup).Items[0] as MyFEx;
            if (course.Name == "")
            {
                string error = "Start Date must be earlier than End Date.";
                ControlsStylesDataGrid.Me.StatusTxt.Text = error;
                return new ValidationResult(false,
                    error);
            }
            else
            {
                ControlsStylesDataGrid.Me.StatusTxt.Text = "";

                return ValidationResult.ValidResult;
            }
        }
    }
    // taken from MSDN (http://msdn.microsoft.com/en-us/library/system.windows.controls.datagrid.aspx)
    public enum OrderStatus { None, New, Processing, Shipped, Received };
    public class MyFEx : IEditableObject, INotifyPropertyChanged
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID == value) return;
                _ID = value;
                OnPropertyChanged("ID");
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value) return;
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (_FullName == value) return;
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }
        private bool? _IsOpen;
        public bool? IsOpen
        {
            get { return _IsOpen; }
            set
            {
                if (_IsOpen == value) return;
                _IsOpen = value;
                OnPropertyChanged("IsOpen");
            }
        }
        private bool? _IsUsed;
        public bool? IsUsed
        {
            get { return _IsUsed; }
            set
            {
                if (_IsUsed == value) return;
                _IsUsed = value;
                OnPropertyChanged("IsUsed");
            }
        }
        private bool? _IsBinary;
        public bool? IsBinary
        {
            get { return _IsBinary; }
            set
            {
                if (_IsBinary == value) return;
                _IsBinary = value;
                OnPropertyChanged("IsBinary");
            }
        }
        private FyleTipe _FT;
        public FyleTipe FT
        {
            get { return _FT; }
            set
            {
                if (_FT == value) return;
                _FT = value;
                OnPropertyChanged("FT");
            }
        }
        private FileExtensionMidTypeEnum _FEMTE;
        public FileExtensionMidTypeEnum FEMTE
        {
            get { return _FEMTE; }
            set
            {
                if (_FEMTE == value) return;
                _FEMTE = value;
                OnPropertyChanged("FEMTE");
            }
        }
        private MyFEx backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as MyFEx;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            ControlsStylesDataGrid.Me.StatusTxt.Text = "";
            this.Name = backupCopy.Name;
            this.FullName = backupCopy.FullName;
            this.FEMTE = backupCopy.FEMTE;
            this.FT = backupCopy.FT;
            this.IsOpen = backupCopy.IsOpen;
            this.IsBinary = backupCopy.IsBinary;
        }

        public void EndEdit()
        {
            if (!inEdit) return;

            inEdit = false;
            backupCopy = null;
            if (this.ID == 0)
            {
                this.ID = Helper.Helper.InsertMyFEx(this);
                if (this.ID == 0)
                {
                    ModernDialog.ShowMessage("nemere", "2", System.Windows.MessageBoxButton.OK);
                    ControlsStylesDataGrid.Me.DG1.ItemsSource = (ControlsStylesDataGrid.Me.DG1.ItemsSource as List<MyFEx>).Where(x => x.ID != 0).ToList();
                    ControlsStylesDataGrid.Me.Custdata = ControlsStylesDataGrid.Me.DG1.ItemsSource as List<MyFEx>;

                }
            }
            else
            {
                ModernDialog.ShowMessage("update", "2", System.Windows.MessageBoxButton.OK);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

    }
    public partial class ControlsStylesDataGrid : UserControl, IContent
    {
        public static ControlsStylesDataGrid Me;
        public MyFEx Selected { get; set; }
        public List<MyFEx> Custdata { get; set; }


        public ControlsStylesDataGrid()
        {
            InitializeComponent();
            Me = this;
            Custdata = GetData();
            //Bind the DataGrid to the customer data
            DG1.DataContext = this;
            DG1.InitializingNewItem += (sender, e) =>
            {
                MyFEx newCourse = e.NewItem as MyFEx;
                newCourse.Name = "";

            };


        }

        public List<MyFEx> GetData()
        {
            var customers = new List<MyFEx>();
            foreach (var item in Helper.Helper.GetMine())
            {
                customers.Add(new MyFEx { ID = item.ID, Name = item.Name, FullName = item.FullName, FEMTE = item.FEMTE, FT = item.FT, IsOpen = item.IsOpen, IsBinary = item.IsBinary, IsUsed = item.IsUsed });
            }



            return customers;
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DG1.ItemsSource = (DG1.ItemsSource as List<MyFEx>).Where(x => x != Selected).ToList();

        }

        private void DG1_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }

        private void DG1_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
        }

        private void DG1_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


        }

        private void DG1_AddingNewItem(object sender, AddingNewItemEventArgs e)


        {
            var index = DG1.Items.IndexOf(DG1.CurrentItem);


            var row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(index);
        }

        private void DG1_AddingNewItem_1(object sender, AddingNewItemEventArgs e)
        {

        }

        private void DG1_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

        private void DG1_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(Me as UserControl).Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(Me as UserControl).Visibility = System.Windows.Visibility.Collapsed;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Helper.Helper.getDBST();
            IsMine.IsChecked = Helper.Helper.ST.IsMine;
            on.Visibility = Helper.Helper.ST.IsMine ? Visibility.Visible : Visibility.Collapsed;
            off.Visibility = Helper.Helper.ST.IsMine ? Visibility.Collapsed : Visibility.Visible;
            //  Helper.Helper.SetTop(false);
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void IsExtended_Checked(object sender, RoutedEventArgs e)
        {
            using (var ent = new Entities())
            {

                Setting st = ent.Settings.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                st.IsMine = true;
                on.Visibility = Visibility.Visible;
                off.Visibility = Visibility.Collapsed;
                ent.SaveChanges();
            }
        }

        private void IsExtended_Unchecked(object sender, RoutedEventArgs e)
        {
            using (var ent = new Entities())
            {
                Setting st = ent.Settings.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                st.IsMine = false;
                off.Visibility = Visibility.Visible;
                on.Visibility = Visibility.Collapsed;
                ent.SaveChanges();
            }
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}



/// <summary>
/// Interaction logic for ControlsStylesDataGrid.xaml
/// </summary>
