using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkedElementByID;
using System.Xml.Linq;

namespace DLL
{
    public class GetLinkedElements : NotifyPropertyClass
    {
        #region Properties
        private Document document = OpenMainWindowCommand.document;
        private UIDocument uidoc = OpenMainWindowCommand.uiDocument;
        public static List<RevitLinkType> revitLinkTypes = new List<RevitLinkType>();
        public static List<Document> RevitLinks { get; set; }
        private static readonly BuiltInCategory RevitLinkID = (BuiltInCategory)(-2001352);
        #endregion
        #region ShowElement Button
        public static void GetRevitLinkTypes()
        {
            try
            {
                RevitLinks = new List<Document>();
                revitLinkTypes = new FilteredElementCollector(OpenMainWindowCommand.document).
                OfCategory(RevitLinkID).OfClass(typeof(RevitLinkType)).Cast<RevitLinkType>().ToList();
              
                for (int i = 0; i < revitLinkTypes.Count; i++)
                {
                    foreach (Document item1 in OpenMainWindowCommand.uiApplication.Application.Documents)
                    {
                        if (item1.Title.Equals(revitLinkTypes[i].Name.Replace(".rvt", "")))
                        {
                            RevitLinks.Add(item1);
                        }
                    }
                }
            }
            catch (Exception ex){}
        }
        public static (Element element, View3D View) GetLinkedElement(string elementId)
        {
            Element element = null;
            View3D view3D = null;
            int NewId = 0;
            int.TryParse(elementId,out NewId);
            ElementId elementId1 = new ElementId(NewId);
            GetRevitLinkTypes();
            List<Document> RevitLinks = GetLinkedElements.RevitLinks;
            foreach (var linkDocument in RevitLinks)
            {
                if (linkDocument.Title+".rvt" == OpenMainWindowCommand.mainWindow.AllRevitLinks.SelectedItem.ToString())
                {
                    FilteredElementCollector collector = new FilteredElementCollector(linkDocument);
                    bool istrue = linkDocument.IsLinked;
                    if (istrue)
                    {
                        view3D = collector.OfClass(typeof(View3D)).Cast<View3D>().Where(x => x.IsTemplate == false).FirstOrDefault();
                        element = linkDocument.GetElement(elementId1);
                        if (element != null)
                        break;
                    }
                }
            }
            return (element, view3D);
        }
        public static View3D Get3DView (Document doc) 
        {
            var collector = new FilteredElementCollector(OpenMainWindowCommand.document);
            var viewFamilyType = collector.OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
              .FirstOrDefault(x => x.ViewFamily == ViewFamily.ThreeDimensional);
            return View3D.CreateIsometric(OpenMainWindowCommand.document, viewFamilyType.Id);
        }
        #endregion
    }
}
