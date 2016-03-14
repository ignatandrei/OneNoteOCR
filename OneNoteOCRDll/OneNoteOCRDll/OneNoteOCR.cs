using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Office.Interop.OneNote;

namespace OneNoteOCRDll
{
    /// <summary>
    /// ocr with one node
    /// </summary>
    public class OneNoteOCR
    {
        /// <summary>
        /// verify one note exists on pc
        /// </summary>
        public void Verify()
        {

            var a = new Application();
            Marshal.ReleaseComObject(a);
            a = null;
            Thread.Sleep(2000);
        }
        /// <summary>
        /// recognize text in image
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public string RecognizeImage(string ImagePath)
        {
            var a = new Application();
            string sections;
            a.GetHierarchy(null, HierarchyScope.hsSections, out sections);
            var doc = XDocument.Parse(sections);
            var ns = doc.Root.Name.Namespace;
            var node = doc.Descendants(ns + "Section").First();
            var s = node.Attribute("ID").Value;
            string p;
            a.CreateNewPage(s, out p);
            InsertImage(ImagePath, p);
            //update the note page 
            Thread.Sleep(2000);
            var str = "";
            a.GetPageContent(p, out str, PageInfo.piBasic, XMLSchema.xsCurrent);
            doc = XDocument.Parse(str);
            node = doc.Descendants().First(t => t.Name.LocalName == "OCRText");
            a.DeleteHierarchy(p, deletePermanently: true);

            Marshal.ReleaseComObject(a);

            return node.Value;

        }

        void InsertImage(string pathImage, string existingPageId)
        {
            string strNamespace = "http://schemas.microsoft.com/office/onenote/2013/onenote";
            string m_xmlImageContent =
                "<one:Image><one:Size width=\"{1}\" height=\"{2}\" isSetByUser=\"true\" /><one:Data>{0}</one:Data></one:Image>";
            string m_xmlNewOutline =
                "<?xml version=\"1.0\"?><one:Page xmlns:one=\"{2}\" ID=\"{1}\"><one:Title><one:OE><one:T><![CDATA[{3}]]></one:T></one:OE></one:Title>{0}</one:Page>";
            string pageToBeChange = "RecognizeImage" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileString;
            using (var bitmap = new Bitmap(pathImage))
            {
                var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                fileString = Convert.ToBase64String(stream.ToArray());

                var onenoteApp = new Application();




                string imageXmlStr = string.Format(m_xmlImageContent, fileString, bitmap.Width, bitmap.Height);
                string pageChangesXml = string.Format(m_xmlNewOutline,
                    new object[] { imageXmlStr, existingPageId, strNamespace, pageToBeChange });

                onenoteApp.UpdatePageContent(pageChangesXml);
                Marshal.ReleaseComObject(onenoteApp);
                onenoteApp = null;
            }
        }
    }
}
