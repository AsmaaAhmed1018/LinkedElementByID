using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using System.Linq;
using DLL;
using System.Windows;
using LinkedElementByID.DLL;

namespace LinkedElementByID
{
    public class MainMethodsHandler : MainWindowVM
    {
        private static Document document = OpenMainWindowCommand.document;
        private static UIDocument uidoc = OpenMainWindowCommand.uiDocument;
        private static Selection selection;
        private static View3D view3D = null;
        public enum MethodsName
        {
            GetLinkedElement, GetLinkedElementID, GetActiveView
        }
        public static void MethodsHandler(object Item = null)
        {
            switch (Events_Handler.EventHandler.mainMethodsName)
            {
                case MethodsName.GetLinkedElement:
                    try
                    {
                        var Ele = GetLinkedElements.GetLinkedElement(textBoxElements.ElementIDText).element;
                        if (Ele != null) {
                            var PathD = Ele.Document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero);
                            var rr = OpenMainWindowCommand.document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero);
                            if (Ele != null)
                            {
                                var direction = new XYZ(-1, 1, -1);
                                var IsThere = new FilteredElementCollector(document).OfClass(typeof(View3D)).Cast<View3D>().Where(x => x.IsTemplate == false).Any(y => y.Name == "Linked Elements View");
                                if (!IsThere)
                                {
                                    view3D = GetLinkedElements.Get3DView(OpenMainWindowCommand.document);
                                    view3D.Name = "Linked Elements View";
                                }
                                else
                                {
                                    view3D = new FilteredElementCollector(document).OfClass(typeof(View3D)).Cast<View3D>().Where(x => x.IsTemplate == false).Where(y => y.Name == "Linked Elements View").FirstOrDefault();
                                }
                                var BOFele = Ele.get_BoundingBox(view3D);
                                view3D.SetSectionBox(BOFele);
                                LocationPoint El = Ele.Location as LocationPoint;
                                view3D.IsSectionBoxActive = true;
                                var oo = view3D.GetSectionBox();
                                var ScopeBoxElements = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_VolumeOfInterest).ToElementIds().ToList();
                                view3D.HideElements(ScopeBoxElements);
                                uidoc.RefreshActiveView();
                            }
                        }
                    }
                    catch (Exception exc)
                    { }
                    break;
                case MethodsName.GetLinkedElementID:
                    try
                    {
                        textBoxElements.LinkedElementID = IdOfLinkedElements.GetLinkElementID(ObjectType.LinkedElement).elementId.ToString();
                        textBoxElements.ElementIDText = textBoxElements.LinkedElementID;
                        if (OpenMainWindowCommand.mainWindow.WindowState != WindowState.Normal)
                        {
                            OpenMainWindowCommand.mainWindow.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            OpenMainWindowCommand.mainWindow.WindowState = WindowState.Minimized;
                            OpenMainWindowCommand.mainWindow.WindowState = WindowState.Normal;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    break;                  
                case MethodsName.GetActiveView:
                    try
                    {
                        uidoc.ActiveView = view3D;
                        uidoc.RequestViewChange(view3D);
                        uidoc.RefreshActiveView();
                    }
                    catch (Exception ex){}
                    break;
            }
        }
    }
}
