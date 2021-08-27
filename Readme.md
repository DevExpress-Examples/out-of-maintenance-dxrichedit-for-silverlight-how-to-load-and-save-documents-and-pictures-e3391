<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128606109/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3391)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [IsolatedStorageStreamProvider.cs](./CS/Export_Import_Example/IsolatedStorageStreamProvider.cs) (VB: [IsolatedStorageStreamProvider.vb](./VB/Export_Import_Example/IsolatedStorageStreamProvider.vb))
* [IsolatedStorageUriProvider.cs](./CS/Export_Import_Example/IsolatedStorageUriProvider.cs) (VB: [IsolatedStorageUriProvider.vb](./VB/Export_Import_Example/IsolatedStorageUriProvider.vb))
* [MainPage.xaml](./CS/Export_Import_Example/MainPage.xaml) (VB: [MainPage.xaml](./VB/Export_Import_Example/MainPage.xaml))
* [MainPage.xaml.cs](./CS/Export_Import_Example/MainPage.xaml.cs) (VB: [MainPage.xaml.vb](./VB/Export_Import_Example/MainPage.xaml.vb))
<!-- default file list end -->
# DXRichEdit for Silverlight: How to load and save documents and pictures


<p>This example demonstrates various ways to save and load a document. They are listed below:</p><p>- save to a file in MS Word 97-2003 format (.doc) (using <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXtraRichEditRichEditDocumentServer_SaveDocumenttopic"><u>SaveDocument</u></a> method )<br />
- save HTML file with pictures to Isolated Storage (using custom <a href="http://documentation.devexpress.com/#Silverlight/clsDevExpressXtraRichEditServicesIUriProvidertopic"><u>IUriProvider</u></a> descendant)<br />
- save HTML file with embedded pictures (using <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXtraRichEditExportHtmlDocumentExporterOptions_EmbedImagestopic"><u>EmbedImages</u></a> option)<br />
- save selected picture (using <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditAPINativeSubDocument_GetImagestopic"><u>GetImages</u></a> and <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditUtilsRichEditImage_GetImageBytesSafetopic"><u>GetImageBytesSafe</u></a> methods)<br />
- load a file in MS Word 97-2003 format (.doc) (using <a href="http://documentation.devexpress.com/#Silverlight/DevExpressXtraRichEditRichEditDocumentServer_LoadDocumenttopic"><u>LoadDocument</u></a> method)<br />
- load HTML file with pictures from Isolated Storage (using custom <a href="http://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditServicesIUriStreamProvidertopic"><u>IUriStreamProvider</u></a> descendant)</p>

<br/>


