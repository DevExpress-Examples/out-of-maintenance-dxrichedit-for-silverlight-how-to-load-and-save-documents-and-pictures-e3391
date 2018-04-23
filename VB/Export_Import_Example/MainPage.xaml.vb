#Region "#usings"
Imports System
Imports System.IO
Imports System.IO.IsolatedStorage
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.Office.Services
Imports DevExpress.XtraRichEdit.Utils
Imports DevExpress.Office.Utils
#End Region ' #usings

Namespace Export_Import_Example
    Partial Public Class MainPage
        Inherits UserControl

        Private sampleText As String = "{\rtf1\deff0{\fonttbl{\f0 Times New Roman;}}" & ControlChars.CrLf & _
"{\colortbl\red0\green0\blue0 ;\red0\green0\blue255 ;\red51\green51\blue153 ;\red204\green255\blue255 ;\red0\green0\blue128 ;\red204\green153\blue255 ;}" & ControlChars.CrLf & _
"{\stylesheet {\ql\cf0 Normal;}{\*\cs1\cf0 Default Paragraph Font;}}" & ControlChars.CrLf & _
"\sectd\pard\plain\qc\fs52\par\pard\plain\qc\fs52\par\pard\plain\qc{\fs52\cf0 This}{\cf0  }" & ControlChars.CrLf & _
"{\fs32\cf0 te}{\b\fs32\cf0 xt}{\b\cf0  }{\b\fs96\cf0 c}{\b\super\fs96\cf0 o}{\b\fs96\cf0 n}" & ControlChars.CrLf & _
"{\b\sub\fs96\cf0 t}{\b\fs96\cf0 a}{\b\super\fs96\cf0 i}{\b\fs96\cf0 n}{\b\sub\fs96\cf0 s}" & ControlChars.CrLf & _
"{\b\cf2  }{\b\ul\cf3\chcbpat4 diffe}{\b\ul\cf0 rent }{\b\i\ul\fs96\cf0 chara}{\b\ul\fs96\cf0 cter f}" & ControlChars.CrLf & _
"{\b\ul\fs96\cf5 or}{\b\fs96\cf5 mat}{\b\fs96\cf0 ting in one }{\fs96\cf0 paragraph.}\par}"

        Public Sub New()
            InitializeComponent()
        End Sub
        #Region "#insertimage"
        Private Sub richEditControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            richEditControl1.CreateNewDocument()
            richEditControl1.Document.AppendRtfText(sampleText)
            richEditControl1.Document.Images.Insert(richEditControl1.Document.Paragraphs(1).Range.Start, CreateImageFromResx("sl-richedit-logo.png"))
        End Sub
        Private Function CreateImageFromResx(ByVal name As String) As DocumentImageSource
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim stream As Stream = assembly.GetManifestResourceStream("Images." & name)
            Dim [dim] As DocumentImageSource = DocumentImageSource.FromStream(stream)
            Return [dim]
        End Function
        #End Region ' #insertimage
        Private Sub btnSaveDOC_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#savedoc"
            Dim sfdlg As New SaveFileDialog()
            sfdlg.DefaultExt = ".doc"
            sfdlg.Filter = "Word 97-2003 Document (*.doc)|*.doc"
            If sfdlg.ShowDialog() = True Then
                Dim fs As Stream = sfdlg.OpenFile()
                richEditControl1.SaveDocument(fs, DocumentFormat.Doc)
                fs.Close()
            End If
'            #End Region ' #savedoc
        End Sub

        Private Sub btnSaveImage_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#saveimage"
            Dim imgs As ReadOnlyDocumentImageCollection = richEditControl1.Document.Images.Get(richEditControl1.Document.Selection)
            If imgs.Count > 0 Then
                Dim ms As New MemoryStream(imgs(0).Image.GetImageBytesSafe(OfficeImageFormat.Png))
                Dim sfdlg As New SaveFileDialog()
                sfdlg.DefaultExt = ".png"
                sfdlg.Filter = "PNG Image (*.png)|*.png"
                If sfdlg.ShowDialog() = True Then
                    Dim fs As Stream = sfdlg.OpenFile()
                    ms.WriteTo(fs)
                    fs.Close()
                End If
                ms.Close()
            End If
'            #End Region ' #saveimage
        End Sub

        Private Sub btnSaveHTMLStorage_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#savehtmlstorage"
            Dim service As IUriProviderService = richEditControl1.GetService(Of IUriProviderService)()
            If service IsNot Nothing Then
                service.RegisterProvider(New IsolatedStorageUriProvider("HTMLFiles"))
            End If

            Try
                Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                    store.CreateDirectory("HTMLFiles")
                    Using fStream As IsolatedStorageFileStream = store.CreateFile("HTMLFiles\test.html")
                        richEditControl1.SaveDocument(fStream, DocumentFormat.Html)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(String.Format("Error saving file.{0}{1}", Environment.NewLine, ex.Message))
            End Try
'            #End Region ' #savehtmlstorage
        End Sub

        Private Sub btnSaveHTMLEmbed_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#savehtmlembed"
            Dim sfdlg As New SaveFileDialog()
            sfdlg.DefaultExt = ".html"
            sfdlg.Filter = "Web page with embedded images (*.html)|*.html"
            If sfdlg.ShowDialog() = True Then
                Dim fs As Stream = sfdlg.OpenFile()
                richEditControl1.Options.Export.Html.EmbedImages = True
                richEditControl1.SaveDocument(fs, DocumentFormat.Html)
                fs.Close()
            End If
'            #End Region ' #savehtmlembed
        End Sub

        Private Sub btnLoadDOC_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#loaddoc"
            Dim ofdlg As New OpenFileDialog()
            ofdlg.Multiselect = False
            ofdlg.Filter = "Word 97-2003 Files (*.doc)|*.doc"
            If ofdlg.ShowDialog() = True Then
                richEditControl1.CreateNewDocument()
                Try
                    richEditControl1.LoadDocument(ofdlg.File.OpenRead(), DocumentFormat.Doc)
                Catch ex As Exception
                    MessageBox.Show(String.Format("Error loading file.{0}{1}", Environment.NewLine, ex.Message))
                End Try
            End If
'            #End Region ' #loaddoc
        End Sub

        Private Sub btnLoadHTMLStorage_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
'            #Region "#loadhtmlstorage"
            Dim uriService As IUriStreamService = richEditControl1.GetService(Of IUriStreamService)()
            uriService.RegisterProvider(New IsolatedStorageStreamProvider("HTMLFiles\"))

            Try
                Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                    If Not store.FileExists("HTMLFiles\test.html") Then
                        Return
                    End If

                    Using fstream As IsolatedStorageFileStream = store.OpenFile("HTMLFiles\test.html", FileMode.Open)
                        richEditControl1.LoadDocument(fstream, DocumentFormat.Html)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(String.Format("Error saving file.{0}{1}", Environment.NewLine, ex.Message))
            End Try
'            #End Region ' #loadhtmlstorage
        End Sub

    End Class
End Namespace
