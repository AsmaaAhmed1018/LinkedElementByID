using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LinkedElementByID.View;
using System;
using System.Diagnostics;
using System.Windows;

namespace LinkedElementByID
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class OpenMainWindowCommand : IExternalCommand
    {
        #region Properties
        public static ExternalEvent exEvent { get; set; }
        public static Document document { get; set; }
        public static UIDocument uiDocument { get; set; }
        public static UIApplication uiApplication { get; set; }
        public static MainWindow mainWindow;
        // For Dealing with all Revit Versions
        [Obsolete]
        #endregion
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            exEvent = ExternalEvent.Create(new Events_Handler.EventHandler());
            uiApplication = commandData.Application;
            uiDocument = commandData.Application.ActiveUIDocument;
            document = uiDocument.Document;
            try
            {
                // Check If MainWindow Doesn't Exist
                bool ThereIsOne = false;
                if (mainWindow == null)
                {
                    Process MainPross = null;
                    foreach (Process process in Process.GetProcessesByName("Revit"))
                    {
                        if (process.MainWindowTitle == "LinkedElements")
                        {
                            MainPross = process;
                            ThereIsOne = true;
                            break;
                        }
                    }
                    if (!ThereIsOne)
                    {
                        mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else
                    {
                        MainPross.Close();
                        mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                }
                else
                {
                    mainWindow.Close();
                    mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Result.Failed;
            }
        }
    }
}
