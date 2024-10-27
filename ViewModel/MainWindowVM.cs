using Autodesk.Revit.UI;
using System.Diagnostics;
using DLL;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB;
using System.Linq;
using System.Collections.Generic;
using LinkedElementByID.View;
using LinkedElementByID.DLL;

namespace LinkedElementByID
{
    public class MainWindowVM : NotifyPropertyClass
    {
        #region Properties
        ExternalEvent exEvent = OpenMainWindowCommand.exEvent;
        private static TextBoxElements _textBoxElements = new TextBoxElements();
        public static TextBoxElements textBoxElements
        {
            get { return _textBoxElements; }
            set
            {
                _textBoxElements = value;
            }
        }
        private static RevitLinksCombox _revitLinksCombox = new RevitLinksCombox();
        public static RevitLinksCombox revitLinksCombox
        {
            get { return _revitLinksCombox; }
            set
            {
                _revitLinksCombox = value;
            }
        }
        private static readonly BuiltInCategory RevitLinkID = (BuiltInCategory)(-2001352);
        #endregion
        #region Constructor
        public MainWindowVM()
        {
            MethodsCommands viewModelCommands = new MethodsCommands();
            GetLinkedElements.GetRevitLinkTypes();
            if (GetLinkedElements.revitLinkTypes.Count != 0)
                foreach (var LinkR in GetLinkedElements.revitLinkTypes) { revitLinksCombox.Add(LinkR.Name.ToString()); }
        }
        #endregion
        #region Images
        public void ImageClick()
        {
            string UriMtool = "https://instudio-engineers.com";
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = UriMtool;
            process.Start();
        }
        private string imageSource = LogDirectors.Imagesfilepath;

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Button Commands
        public static RelayCommands SearchElementID { get; set; }
        public static RelayCommands GetElementID { get; set; }
        #endregion
    }
}
