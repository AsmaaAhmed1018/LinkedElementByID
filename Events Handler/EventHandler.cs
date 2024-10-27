using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;


namespace LinkedElementByID.Events_Handler
{
    public class EventHandler : IExternalEventHandler
    {
        public static MainMethodsHandler.MethodsName mainMethodsName;

        [Obsolete]
        public void Execute(UIApplication app)
        {
            Document doc = OpenMainWindowCommand.document;
            try
            {
                //notifying me of raised event
                Trace.WriteLine("Raised Main");
                try
                {
                    if (mainMethodsName == MainMethodsHandler.MethodsName.GetLinkedElement)
                    {
                        using (Transaction tx = new Transaction(doc, "Select Linked Elements"))
                        {
                            tx.Start();
                            MainMethodsHandler.MethodsHandler();
                            tx.Commit();
                            tx.Dispose();
                        }
                        mainMethodsName = MainMethodsHandler.MethodsName.GetActiveView;
                        MainMethodsHandler.MethodsHandler();
                    }
                    else if (mainMethodsName == MainMethodsHandler.MethodsName.GetLinkedElementID)
                    {
                        using (Transaction tx = new Transaction(doc, "Get ID of Selected Linked Elements"))
                        {
                            tx.Start();
                            MainMethodsHandler.MethodsHandler();
                            tx.Commit();
                        }
                    }
                }
                catch (Exception e)
                {
                    //catch whatever exception
                    throw e;
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
        public string GetName()
        {
            return "Main Event Handler";
        }
    }
}

