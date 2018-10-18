using DataMatrix.net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1.Rappipagos1
{
    public partial class Procesar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (btnUpload.HasFile)
            {
                // 1. Upload File
                string rappipagosFolder = "/Rappipagos1/Files/";
                if (ConfigurationManager.AppSettings != null)
                {
                    rappipagosFolder = ConfigurationManager.AppSettings["Rappipagos_Folder"].ToString();
                }

                string name_complete = rappipagosFolder + btnUpload.FileName;

                // 2. Process File
                string sourcePdf = Server.MapPath(name_complete);
                string outputPath = Server.MapPath(rappipagosFolder);

                if (!File.Exists(sourcePdf))
                    btnUpload.PostedFile.SaveAs(sourcePdf);

                ExtractImagesFromPDF(sourcePdf, outputPath);
            }
        }

        private string DecodeText(string sFileName)
        {
            DmtxImageDecoder decoder = new DmtxImageDecoder();
            System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap(sFileName);
            List<string> oList = decoder.DecodeImage(oBitmap);

            StringBuilder sb = new StringBuilder();
            sb.Length = 0;
            foreach (string s in oList)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }

        public void ExtractImagesFromPDF(string sourcePdf, string outputPath)
        {
            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);

            for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
            {
                PdfDictionary pg = pdf.GetPageN(pageNumber);
                PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
                if (xobj != null)
                {
                    foreach (PdfName name in xobj.Keys)
                    {
                        PdfObject obj = xobj.Get(name);
                        if (obj.IsIndirect())
                        {
                            PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                            PdfName type = (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));
                            if (PdfName.IMAGE.Equals(type))
                            {
                                int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                                PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                                PdfStream pdfStrem = (PdfStream)pdfObj;
                                byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                                if ((bytes != null))
                                {
                                    try
                                    {
                                        using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                                        {
                                            memStream.Position = 0;
                                            System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                                            // must save the file while stream is open.
                                            if (!Directory.Exists(outputPath))
                                                Directory.CreateDirectory(outputPath);

                                            string path = Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
                                            EncoderParameters parms = new EncoderParameters(1);
                                            parms.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                            // GetImageEncoder is found below this method
                                            ImageCodecInfo jpegEncoder = GetImageEncoder("JPEG");
                                            img.Save(path, jpegEncoder, parms);
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static System.Drawing.Imaging.ImageCodecInfo GetImageEncoder(string imageType)
        {
            imageType = imageType.ToUpperInvariant();

            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatDescription == imageType)
                {
                    return info;
                }
            }

            return null;
        }

    }
}