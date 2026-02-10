using DataModelDemo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelDemo.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class PopupNotesController : ViewController {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    public PopupNotesController() {
        // Target required Views (via the TargetXXX properties) and create their Actions.
        // TargetViewType = ViewType.ListView;
        // SimpleAction simpleAction1 = new SimpleAction(this, "SimpleAction1", PredefinedCategory.View);
        // simpleAction1.Execute += (s, e) {
        //    // Implement business logic: https://docs.devexpress.com/eXpressAppFramework/113711.
        // }
        TargetObjectType = typeof(DemoTask);
        TargetViewType = ViewType.DetailView;
        PopupWindowShowAction showNotesAction = new PopupWindowShowAction(this, "ShowNotesAction", PredefinedCategory.Edit)
        {
            Caption = "Show Notes"
        };

        showNotesAction.CustomizePopupWindowParams += ShowNotesAction_CustomizePopupWindowParams;
        showNotesAction.Execute += ShowNotesAction_Execute;

    }
    private void ShowNotesAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
    {
        e.View = Application.CreateListView(typeof(Note), true);
    }
    private void ShowNotesAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
    {
        DemoTask task = (DemoTask)View.CurrentObject;
        foreach (Note note in e.PopupWindowViewSelectedObjects)
        {
            if (!string.IsNullOrEmpty(task.Description))
            {
                task.Description += Environment.NewLine;
            }
            // Add selected note texts to a Task's description
            task.Description += note.Text;
        }
        View.ObjectSpace.CommitChanges();
    }
    protected override void OnActivated() {
        base.OnActivated();
        // Perform various tasks depending on the target View,
        // customize view items: https://docs.devexpress.com/eXpressAppFramework/120092.
    }
    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        // Access and customize the target View control.
    }
    protected override void OnDeactivated() {
        // Unsubscribe from previously subscribed events and release other references and resources.
        base.OnDeactivated();
    }
}
