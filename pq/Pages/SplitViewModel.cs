using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using pq.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace pq.Pages
{


    public class SplitViewModel
        : NotifyPropertyChanged
    {

        public List<FileExtensionItem> SelectedFEISs { get; set; }
        public string Username { get; set; }
        public bool IsLoading { get; set; }
        public string RegKey { get; set; }

        public string Searchit = "";
        public SplitViewModel SVM { get; set; }
        public int Flee = 0;
        private List<FileExtensionItem> feis { get; set; }
        private List<MyButt> myButts = new List<MyButt>();
        public SplitViewModel(int fyle)
        {
            RegKey = Helper.Helper.RegBase.ToString();
            Username = Helper.Helper.Username;
            try
            {

                Flee = fyle;
                if (fyle == -1)
                {
                    FEIs = Helper.Helper.GetFeteAll();
                }
                else if (fyle == 6)
                {
                    FEIs = Helper.Helper.GetFetePicks();
                }
                else
                    FEIs = Helper.Helper.GetFeteByFyle(fyle);


                SVM = this;
            }
            catch (Exception)
            {
                FEIs = new List<FileExtensionItem>();
            }
        }


        public void ItemTemplateChanged(object sender, SelectionChangedEventArgs e)
        {
            //        MessageBox.Show("Posoli ga baybe");
            // SelectedFE.TemplateID = "ssssss";
        }
        public void NewCommand_Executed2(object sender, ExecutedRoutedEventArgs e)
        {

            if (SelectedFEISs.Count > 0)
            {
                foreach (var fe in SelectedFEISs)

                {
                    Helper.Helper.SetRegKeyInvFile(fe.Name.Replace(".", ""));
                    Helper.Helper.All.Where(x => x.ID == fe.ID).First().TemplateID = "0";
                }

                if (SelectedFEISs.Count > 1)
                {
                    SelectedFE.TemplateID = "0";
                }
            }
            else
                ModernDialog.ShowMessage("nevalja", "111111", MessageBoxButton.OK);
            //SelectedFE.TemplateID = "0";
            Notify();
            //  svm.RaisePropertyChange.prope.PropertyChanged(this, new PropertyChangedEventArgs("SelectedFE"));
            //svm.OnFEPropertyChanged(this, new PropertyChangedEventArgs("SelectedFE"));

        }
        public void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //        MessageBox.Show("Posoli ga baybe");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = SelectedFE.FullName + " (*" + SelectedFE.Name + ")|*" + SelectedFE.Name;
            openFileDialog.InitialDirectory = @"c:\";
            //C: \Users\Lovre\Templates
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.SafeFileName;
                string sourcePath = openFileDialog.FileName;
                string sourceFile = sourcePath;

                ModernDialog md = new ModernDialog();
                md.Owner = Application.Current.MainWindow;
                Button b1 = new Button();
                Button b2 = new Button();
                b1.Content = "OK";
                b1.Click += (x, y) => { md.DialogResult = true; };
                b2.Content = "Cancel";
                b2.Click += (x, y) => { md.DialogResult = false; };
                Button[] ba = { b1, b2 };
                md.Buttons = ba;
                ContentPresenter cp = new ContentPresenter();
                Canvas c = Application.Current.TryFindResource("appbar_layer_arrange_sendbackward") as Canvas;
                cp.Content = c;
                md.Content = cp;
                bool bl = (bool)md.ShowDialog();
                if (bl)
                {


                    try
                    {
                        if (Helper.Helper.RegBase == Registry.ClassesRoot)
                        {
                            List<string> ls = Helper.Helper.GetAllUsernames();
                            foreach (var lsi in ls)
                            {
                                string targetPath2 = @"C:\\Users\\" + lsi + "\\Templates";

                                if (System.IO.Directory.Exists(@"C:\\Users\\" + lsi + "\\"))
                                {
                                    string destFile2 = System.IO.Path.Combine(targetPath2, fileName);
                                    System.IO.File.Copy(sourceFile, destFile2, true);
                                    ModernDialog.ShowMessage("Added for - " + lsi, lsi, MessageBoxButton.OK);
                                }

                            }
                        }
                        else
                        {
                            string targetPath = @"C:\\Users\\" + Environment.UserName + "\\Templates";

                            // Use Path class to manipulate file and directory paths.

                            string destFile = System.IO.Path.Combine(targetPath, fileName);
                            System.IO.File.Copy(sourceFile, destFile, true);
                        }

                        string s = SelectedFE.Name.Replace(".", "");

                        Helper.Helper.SetRegKeyFile(s, fileName);
                    }
                    catch (Exception d)
                    {
                        ModernDialog.ShowMessage(d.Message, "fail", MessageBoxButton.OK);


                    }

                    SelectedFE.TemplateID = openFileDialog.SafeFileName;
                    Notify();
                }

            }
            //Gat.Controls.OpenDialogView newdialoge = new OpenDialog();

            //FETdetails UserControl1Control = sender as FETdetails;
            //UserControl1Control.Flaya.NewCommand_Executed(sender, e);

            // newdialoge.Owner = Application.Current.MainWindow;
            // newdialoge.Show();
            //if (newdialoge.DialogResult == true)
            {

                //  svm.SelectedFE.TemplateID = "22222222";
                // svm.Notify();
                //  svm.RaisePropertyChange.prope.PropertyChanged(this, new PropertyChangedEventArgs("SelectedFE"));
                //svm.OnFEPropertyChanged(this, new PropertyChangedEventArgs("SelectedFE"));
            }

        }
        private FileExtensionItem selectedfe;
        public FileExtensionItem SelectedFE
        {
            get { return this.selectedfe; }
            set
            {
                if (this.selectedfe != value)
                {
                    this.selectedfe = value;
                    OnPropertyChanged("SelectedFE");
                }
            }
        }
        private int cols = 3;
        public int Cols
        {
            get { return this.cols; }
            set
            {
                if (this.cols != value)
                {
                    this.cols = value;
                    OnPropertyChanged("Cols");

                }
            }
        }
        public List<FileExtensionItem> FEIs
        {
            get { return this.feis; }
            set { this.feis = value; OnPropertyChanged("FEIs"); }
        }
        public List<MyButt> MyButts
        {
            get { return this.myButts; }
        }

        public SplitViewModel DataContext { get; private set; }

        public List<FileExtensionItem> Search(string token)
        {

            Searchit = token;
            OnPropertyChanged("SelectedFE");
            if (Flee == -1)
            {
                return FEIs = Helper.Helper.GetFeteAll().Where(x => x.Name.StartsWith( token.ToUpper()) | x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();
            }
            else if (Flee == 6)
            {
                return FEIs = Helper.Helper.GetFetePicks().Where(x => x.Name.StartsWith( token.ToUpper()) || x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();
            }
            else
                return FEIs = Helper.Helper.GetFeteByFyle(Flee).Where(x => x.Name.StartsWith( token.ToUpper()) || x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();


        }
        public void someshit()
        {
            ModernDialog.ShowMessage("22", "22", MessageBoxButton.OK);
        }
        public void Notify()
        {
            RegKey = Helper.Helper.RegBase.ToString();
            OnPropertyChanged("RegKey");
            OnPropertyChanged("IsLoading");
            OnPropertyChanged("SelectedFE");
            if (Flee == -1)
            {
                FEIs = Helper.Helper.GetFeteAll().Where(x => x.Name.StartsWith("." + Searchit.ToUpper()) || x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();
            }
            else if (Flee == 6)
            {
                FEIs = Helper.Helper.GetFetePicks().Where(x => x.Name.StartsWith("." + Searchit.ToUpper()) | x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();
            }
            else
                FEIs = Helper.Helper.GetFeteByFyle(Flee).Where(x => x.Name.StartsWith("." + Searchit.ToUpper()) || x.FullName.ToUpper().Contains(Searchit.ToUpper()) || x.FEMTE.ToString().ToUpper().Contains(Searchit.ToUpper()) || x.FT.ToString().ToUpper().Contains(Searchit.ToUpper())).ToList();



        }
        public void OnFEPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Sync(e.PropertyName);
        }

        private void Sync(string prop)
        {

            if (prop == "SelectedFE")
            {

                Debug.WriteLine("sdssdsdsd");
            }
        }

    }
}
