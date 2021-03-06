using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("http://software.suence.com/", "OTLog.UI.Core.Converters")]
[assembly: XmlnsDefinition("http://software.suence.com/", "OTLog.UI.Core.AttachedProperties")]
[assembly: XmlnsDefinition("http://software.suence.com/", "OTLog.UI.Core.Tools")]
[assembly: XmlnsDefinition("http://software.suence.com/", "OTLog.UI.Core.Behaviors")]
[assembly: XmlnsDefinition("http://software.suence.com/", "OTLog.UI.Core.MarkupExtensions")]
