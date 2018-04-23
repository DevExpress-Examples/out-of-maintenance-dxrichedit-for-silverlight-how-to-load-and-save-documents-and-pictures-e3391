#region usings
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.Utils;
#endregion #usings

namespace Export_Import_Example
{
    public partial class MainPage : UserControl
    {
        string sampleText = @"{\rtf1\deff0{\fonttbl{\f0 Times New Roman;}}
{\colortbl\red0\green0\blue0 ;\red0\green0\blue255 ;\red51\green51\blue153 ;\red204\green255\blue255 ;\red0\green0\blue128 ;\red204\green153\blue255 ;}
{\stylesheet {\ql\cf0 Normal;}{\*\cs1\cf0 Default Paragraph Font;}}
\sectd\pard\plain\qc\fs52\par\pard\plain\qc\fs52\par\pard\plain\qc{\fs52\cf0 This}{\cf0  }
{\fs32\cf0 te}{\b\fs32\cf0 xt}{\b\cf0  }{\b\fs96\cf0 c}{\b\super\fs96\cf0 o}{\b\fs96\cf0 n}
{\b\sub\fs96\cf0 t}{\b\fs96\cf0 a}{\b\super\fs96\cf0 i}{\b\fs96\cf0 n}{\b\sub\fs96\cf0 s}
{\b\cf2  }{\b\ul\cf3\chcbpat4 diffe}{\b\ul\cf0 rent }{\b\i\ul\fs96\cf0 chara}{\b\ul\fs96\cf0 cter f}
{\b\ul\fs96\cf5 or}{\b\fs96\cf5 mat}{\b\fs96\cf0 ting in one }{\fs96\cf0 paragraph.}\par}";

        public MainPage()
        {
            InitializeComponent();
        }
        #region #insertimage
        private void richEditControl1_Loaded(object sender, RoutedEventArgs e)
        {
            richEditControl1.CreateNewDocument();
            richEditControl1.Document.AppendRtfText(sampleText);
            richEditControl1.Document.InsertImage(richEditControl1.Document.Paragraphs[1].Range.Start, CreateImageFromResx("sl-richedit-logo.png"));
        }
        private DocumentImageSource CreateImageFromResx(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("Export_Import_Example.Images." + name);
            DocumentImageSource dim = DocumentImageSource.FromStream(stream);
            return dim;
        }
        #endregion #insertimage
        private void btnSaveDOC_Click(object sender, RoutedEventArgs e)
        {
            #region #savedoc
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = ".doc";
            sfdlg.Filter = "Word 97-2003 Document (*.doc)|*.doc";
            if (sfdlg.ShowDialog() == true) {
                Stream fs = sfdlg.OpenFile();
                richEditControl1.SaveDocument(fs, DocumentFormat.Doc);
                fs.Close();
            }
            #endregion #savedoc
        }

        private void btnSaveImage_Click(object sender, RoutedEventArgs e)
        {
            #region #saveimage
            DocumentImageCollection imgs = richEditControl1.Document.GetImages(richEditControl1.Document.Selection);
            if (imgs.Count > 0) {
                MemoryStream ms = new MemoryStream(imgs[0].Image.GetImageBytesSafe(RichEditImageFormat.Png));
                SaveFileDialog sfdlg = new SaveFileDialog();
                sfdlg.DefaultExt = ".png";
                sfdlg.Filter = "PNG Image (*.png)|*.png";
                if (sfdlg.ShowDialog() == true) {
                    Stream fs = sfdlg.OpenFile();
                    ms.WriteTo(fs);
                    fs.Close();
                }
                ms.Close();
            }
            #endregion #saveimage
        }

        private void btnSaveHTMLStorage_Click(object sender, RoutedEventArgs e)
        {
            #region #savehtmlstorage
            IUriProviderService service = richEditControl1.GetService<IUriProviderService>();
            if (service != null) {
                service.RegisterProvider(new IsolatedStorageUriProvider("HTMLFiles"));
            }

            try {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication()) {
                    store.CreateDirectory("HTMLFiles");
                    using (IsolatedStorageFileStream fStream = store.CreateFile("HTMLFiles\\test.html"))
                        richEditControl1.SaveDocument(fStream, DocumentFormat.Html);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(string.Format("Error saving file.{0}{1}", Environment.NewLine, ex.Message));
            }
            #endregion #savehtmlstorage
        }

        private void btnSaveHTMLEmbed_Click(object sender, RoutedEventArgs e)
        {
            #region #savehtmlembed
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = ".html";
            sfdlg.Filter = "Web page with embedded images (*.html)|*.html";
            if (sfdlg.ShowDialog() == true) {
                Stream fs = sfdlg.OpenFile();
                richEditControl1.Options.Export.Html.EmbedImages = true;
                richEditControl1.SaveDocument(fs, DocumentFormat.Html);
                fs.Close();
            }
            #endregion #savehtmlembed
        }

        private void btnLoadDOC_Click(object sender, RoutedEventArgs e)
        {
            #region #loaddoc
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Multiselect = false;
            ofdlg.Filter = "Word 97-2003 Files (*.doc)|*.doc";
            if (ofdlg.ShowDialog() == true) {
                richEditControl1.CreateNewDocument();
                try {
                    richEditControl1.LoadDocument(ofdlg.File.OpenRead(), DocumentFormat.Doc);
                }
                catch (Exception ex) {
                    MessageBox.Show(string.Format("Error loading file.{0}{1}", Environment.NewLine, ex.Message));
                }
            }
            #endregion #loaddoc
        }

        private void btnLoadHTMLStorage_Click(object sender, RoutedEventArgs e)
        {
            #region #loadhtmlstorage
            IUriStreamService uriService = richEditControl1.GetService<IUriStreamService>();
            uriService.RegisterProvider(new IsolatedStorageStreamProvider("HTMLFiles\\"));
            
            try {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication()) {
                    if (!store.FileExists("HTMLFiles\\test.html"))
                        return;

                    using (IsolatedStorageFileStream fstream = store.OpenFile("HTMLFiles\\test.html", FileMode.Open)) {
                        richEditControl1.LoadDocument(fstream, DocumentFormat.Html);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(string.Format("Error saving file.{0}{1}", Environment.NewLine, ex.Message));
            }
            #endregion #loadhtmlstorage
        }

    }
}
