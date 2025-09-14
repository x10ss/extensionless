using System;
using System.Windows.Media;

namespace pq.Model
{
    public class FileExtensionItem
    {
        public Color PGB { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool? IsEnabled { get; set; }
        public bool IsNotEnabled { get; set; }
        public bool? IsMine { get; set; }
        public bool? IsExtended { get; set; }
        public bool? IsOpen { get; set; }
        public bool? IsBinary { get; set; }
        public bool? IsUsed { get; set; }
        public string TemplateID { get; set; }
        public Color FEColor { get; set; }
        public FileExtensionTypeEnum FETE { get; set; }
        public FileExtensionMidTypeEnum FEMTE { get; set; }
        public string IsDark { get; set; }
        public FyleTipe FT { get; set; }
        public DateTime? Last { get; set; }
    }
}
