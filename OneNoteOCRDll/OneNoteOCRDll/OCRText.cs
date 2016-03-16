using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNoteOCRDll
{
    /// <summary>
    /// text and rectangle
    /// </summary>
    public class OCRText
    {
        /// <summary>
        /// text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// rectangle
        /// </summary>
        public RectangleF Rect { get; set; }
    }
}
