using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace LinkedElementByID
{
    public class MethodsCommands : NotifyPropertyClass
    {
        #region Properties
        private static Document document = OpenMainWindowCommand.document;
        private static UIDocument uidoc = OpenMainWindowCommand.uiDocument;
        ExternalEvent exEvent = OpenMainWindowCommand.exEvent;
        #endregion
        public MethodsCommands()
        {
            MainWindowVM.SearchElementID = new RelayCommands(Excute_SearchByElementID_Button, CanExcute_SearchByElementID_Button);
            MainWindowVM.GetElementID = new RelayCommands(Execut_GetLinkedElementID_Button, CanExecute_GetLinkedElementID_Button);
        }
        public void Excute_SearchByElementID_Button(object parameter)
        {
            Events_Handler.EventHandler.mainMethodsName = MainMethodsHandler.MethodsName.GetLinkedElement;
            exEvent.Raise();
        }
        public bool CanExcute_SearchByElementID_Button(object parameter)
        {
            return true;
        }
        public void Execut_GetLinkedElementID_Button(object parameter)
        {
            Events_Handler.EventHandler.mainMethodsName = MainMethodsHandler.MethodsName.GetLinkedElementID;
            exEvent.Raise();
        }
        public bool CanExecute_GetLinkedElementID_Button(object parameter)
        {
            return true;
        }
    }
}
