using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pq.Pages
{
    /// <summary>
 /// Interaction logic for Img.xaml
 /// </summary>
    public partial class Img : UserControl
    {
        public Img()
        {
            InitializeComponent();


            // Set initial ink stroke attributes.
            var drawingAttributes = new DrawingAttributes();
            drawingAttributes.Color = Color.FromRgb(111, 111, 111);
            drawingAttributes.FitToCurve = true;
            MyInk.DefaultDrawingAttributes = drawingAttributes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.GestureOnly;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MyInk.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ModernDialog.ShowMessage("about to save", "saver", MessageBoxButton.OK);
            string sigPath = System.IO.Path.GetTempFileName();
            sigPath = sigPath.Replace("tmp", "jpeg");

            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(sigPath, FileMode.Create);

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)MyInk.ActualWidth, (int)MyInk.ActualHeight, 96d, 96d, PixelFormats.Default);
            rtb.Render(MyInk);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            encoder.Save(fs);
            ModernDialog.ShowMessage(sigPath, sigPath, MessageBoxButton.OK);
            fs.Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }

        Point currentPoint, mouseLeftDownPoint;
        bool IsDrawing = false;
    private void inkcanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        IsDrawing = true;
        MyInk.Strokes.Clear();

        mouseLeftDownPoint = e.GetPosition((IInputElement)sender);
    }

    Stroke st = null;
    private void inkcanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (IsDrawing)
        {
                mouseLeftDownPoint = e.GetPosition((IInputElement)sender);
            StylusPointCollection pts = new StylusPointCollection();

            pts.Add(new StylusPoint(mouseLeftDownPoint.X, mouseLeftDownPoint.Y));
            pts.Add(new StylusPoint(currentPoint.X, currentPoint.Y));

            if (st != null)
                    MyInk.Strokes.Remove(st);
            st = new customStroke(pts);

            st.DrawingAttributes.Color = Colors.DarkOrange;
                MyInk.Strokes.Add(st);
        }
    }

    private void inkcanvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (st != null)
        {
            MyInk.Strokes.Remove(st);
                MyInk.Strokes.Add(st.Clone());
        }
        IsDrawing = false;
    }
}


public class customStroke : Stroke
{
    public customStroke(StylusPointCollection pts)
        : base(pts)
    {
        this.StylusPoints = pts;
    }

    protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
    {
        if (drawingContext == null)
        {
            throw new ArgumentNullException("drawingContext");
        }
        if (null == drawingAttributes)
        {
            throw new ArgumentNullException("drawingAttributes");
        }
        DrawingAttributes originalDa = drawingAttributes.Clone();
        SolidColorBrush brush2 = new SolidColorBrush(drawingAttributes.Color);
        brush2.Freeze();
        StylusPoint stp = this.StylusPoints[0];
        StylusPoint sp = this.StylusPoints[1];
        double radius = System.Math.Sqrt(System.Math.Pow((double)(sp.X - stp.X), 2) + System.Math.Pow((double)(sp.Y - stp.Y), 2)) / 2.0;
            drawingContext.DrawText(new FormattedText("pleti ga rođo",CultureInfo.CurrentCulture,FlowDirection.LeftToRight, new Typeface(new FontFamily("Comic Sans MS"), new FontStyle(), new FontWeight(), new FontStretch()), 22d,brush2), new Point(sp.X,sp.Y));
      //  drawingContext.DrawEllipse(brush2, null, new Point((sp.X + stp.X) / 2.0, (sp.Y + stp.Y) / 2.0), radius, radius);
    }
}
}
