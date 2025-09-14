using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pq.Model
{
    public class MyButt
    {
        public string MyContent { get; set; }
        public Color MyColor { get; set; }
        public string MyCommand { get; set; }
        public int Span { get; set; }
        public bool IsSelected { get; set; }
    }
}
