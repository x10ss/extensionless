using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace pq.MyConverters
{
    public class RealTmpl2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (String.IsNullOrEmpty(value?.ToString() ?? ""))
            {
                case true:
                    return false;
                case false:
                    return true;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Binary : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {

                if ((bool)value)
                {
                    return "Binary";
                }
                else
                {
                    return "Text";
                }
            }
            catch (Exception)
            {

                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Aux : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {

                if ((bool)value)
                {
                    return "• AUX";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Mine : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {

                if ((bool)value)
                {
                    return "• My";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Open : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                {
                    return "Open";
                }
                else
                {
                    return "Proprietary";
                }
            }
            catch (Exception)
            {
                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Percival : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((decimal)(decimal.Parse(value.ToString()) * 100 / 125)).ToString("0.##");

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class InvBoolToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch ((bool)value)
                {
                    case true:
                        return Visibility.Collapsed;
                    case false:
                        return Visibility.Visible;


                }
                return false;
            }
            catch (Exception)
            {
                return Visibility.Collapsed;
                throw;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class DegreeToPoint : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Point point = new Point();
            try
            {

                point.X = Math.Cos((double)value);
                point.Y = Math.Sin((double)value);



                return point;
            }
            catch (Exception)
            {
                return Visibility.Collapsed;
                throw;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class InvDoubleOneZero : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return -(double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class MyBoolToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch ((bool)value)
                {
                    case false:
                        return Visibility.Collapsed;
                    case true:
                        return Visibility.Visible;


                }
                return false;
            }
            catch (Exception)
            {
                return Visibility.Collapsed;
                throw;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class NullToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch ((bool)value)
                {
                    case false:
                        return Visibility.Collapsed;
                    case true:
                        return Visibility.Collapsed;


                }
                return false;
            }
            catch (Exception)
            {
                return Visibility.Visible;
                throw;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Tmpl2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "0":
                    return Visibility.Collapsed;
                case "1":
                    return Visibility.Visible;

                default:
                    return Visibility.Visible;

            }
          //  return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class InvTmpl2Trool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString() == "0")
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Hidden;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }

    public class InvTmpl2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "0":
                    return Visibility.Visible;
                case "1":
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;


            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class ThirdBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                bool b = (bool)value;

                return !b;



            }
            catch (Exception)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class FourthBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                bool b = (bool)value;

                return b;


            }
            catch (Exception)
            {
                return null;
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class InvBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch ((bool)value == true)
                {
                    case true:
                        return false;
                    case false:
                        return true;


                }
                return false;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class ConverterSample22 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (String.IsNullOrEmpty(value?.ToString() ?? ""))
            {
                case true:
                    return Visibility.Collapsed;
                case false:
                    return Visibility.Visible;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class BorderBras : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "":
                    return new SolidColorBrush(Color.FromArgb((byte)128, (byte)200, (byte)100, (byte)50)).ToString();
                case "0":
                    return new SolidColorBrush(Color.FromArgb((byte)128, (byte)128, (byte)128, (byte)128)).ToString();
                case "1":
                    return new SolidColorBrush(Color.FromArgb((byte)128, (byte)30, (byte)50, (byte)220)).ToString();
                default:
                    return new SolidColorBrush(Color.FromArgb((byte)128, (byte)75, (byte)170, (byte)65)).ToString();

            }
          //  return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }

    public class ConverterSample : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (String.IsNullOrEmpty(value?.ToString() ?? ""))
            {
                case true:
                    return Visibility.Collapsed;
                case false:
                    return Visibility.Visible;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Application.Current.TryFindResource("Accent").ToString();
            }
            else
            {
                return Application.Current.TryFindResource("ItemTextDisabled").ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Brush2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Application.Current.TryFindResource("ItemText").ToString();
            }
            else
            {
                return Application.Current.TryFindResource("ItemTextDisabled").ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Synch : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "5")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Synch2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Application.Current.TryFindResource("Accent") as SolidColorBrush;
            }
            else
            {
                return Application.Current.TryFindResource("ItemTextDisabled") as SolidColorBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Ach : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((int)(value) == 0)
                {
                    switch (parameter.ToString())
                    {
                        case "A1a":
                            return new SolidColorBrush(Color.FromRgb(233, 22, 255));
                        case "A2a":
                            return new SolidColorBrush(Color.FromRgb(11, 22, 255));
                        case "A3a":
                            return new SolidColorBrush(Color.FromRgb(44, 55, 255));
                        case "A4a":
                            return new SolidColorBrush(Color.FromRgb(115, 55, 111));
                        case "A5a":
                            return new SolidColorBrush(Color.FromRgb(122, 222, 111));
                        case "A6a":
                            return new SolidColorBrush(Color.FromRgb(11, 222, 21));
                        case "A7a":
                            return new SolidColorBrush(Color.FromRgb(44, 21, 111));
                        case "A8a":
                            return new SolidColorBrush(Color.FromRgb(111, 55, 55));
                        case "A9a":
                            return new SolidColorBrush(Color.FromRgb(55, 66, 88));
                        default:
                            return new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Ach2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((int)(value) > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class RealConverterSample : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (!(String.IsNullOrEmpty(value?.ToString() ?? "")) && (value.ToString() != "0"))
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Collapsed;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class DateString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return (((DateTime)value).ToShortDateString() + " " + ((DateTime)value).ToShortTimeString());
            }
            catch (Exception)
            {

                return "No record";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class ConverterSample2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (String.IsNullOrEmpty(value?.ToString() ?? ""))
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Hidden;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
    public class Bool2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((bool)value)
            {
                case true:
                    return System.Drawing.Color.LightGreen;
                case false:
                    return System.Drawing.Color.LightCoral;


            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
}
