using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using System.Reflection;
using System.Windows.Media.Imaging;
using LinkedElementByID.View;
using DLL;

namespace LinkedElementByID.Application
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ExternalApp : IExternalApplication
    {
        /// <summary>
        /// Create Tab, Panel, Button for the plugin
        /// </summary>
        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            string TabName = "In.Studio";
            string PanelName = "General";
            RibbonPanel panelAnnotation = null;
            try
            {
                application.CreateRibbonTab(TabName);
            }
            catch
            {
            }
            try
            {
                //Check IF In.Studio Tab and General Panel is Existing:
                var AllTabs = application.GetRibbonPanels(TabName);
                foreach (var tab in AllTabs)
                {
                    if (tab.Name == PanelName)
                    {
                        panelAnnotation = tab;
                        break;
                    }
                }
                if (null == panelAnnotation)
                {
                    panelAnnotation = application.CreateRibbonPanel(TabName, PanelName);
                }
                var ButtonLable = "Linked Elements";
                bool ExistButton = false;
                foreach (var PannelItem in panelAnnotation.GetItems())
                {
                    if (PannelItem.ItemType == RibbonItemType.PushButton)
                    {
                        if (PannelItem.ItemText == ButtonLable)
                        {
                            ExistButton = true;
                            break;
                        }
                    }
                }
                if (!ExistButton)
                {
                    // Insert Icons for Button and ToolTip
                    var mTools = new PushButtonData(ButtonLable, ButtonLable, Assembly.GetExecutingAssembly().Location, "LinkedElementByID.OpenMainWindowCommand")
                    {
                        ToolTipImage = new BitmapImage(new Uri($@"{LogDirectors.MianIconPath}")),
                        ToolTip = "Select Linked Elements by IDs"
                    };
                    var mToolslayer = panelAnnotation.AddItem(mTools) as PushButton;
                    mToolslayer.LargeImage = new BitmapImage(new Uri($@"{LogDirectors.LargeIconPath}"));
                }
            }
            catch
            {
            }
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
