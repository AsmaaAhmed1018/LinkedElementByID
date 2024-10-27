using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;

namespace LinkedElementByID.DLL
{
    public class IdOfLinkedElements : NotifyPropertyClass
    {
        #region Properties
        private Document document = OpenMainWindowCommand.document;
        private UIDocument uidoc = OpenMainWindowCommand.uiDocument;
        #endregion
        #region GetIDButton
        public static (Reference element, ElementId elementId) GetLinkElementID(ObjectType objectType)
        {
            try
            {
                Selection selection = OpenMainWindowCommand.uiDocument.Selection;
                var SelectedElement = selection.PickObject(objectType);

                if (SelectedElement == null)
                {
                    TaskDialog.Show("Select Linked Element", "There Is No Elements Selected.");
                    return (null, null);
                }
                var elementId = SelectedElement.LinkedElementId;
                return (SelectedElement, elementId);
            }
            catch (Exception e)
            {
                TaskDialog.Show("Revit Error", e.Message);
                return (null, null);
            }
        }
        #endregion
    }
}
