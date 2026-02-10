using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DataModelDemo.Module.BusinessObjects;

namespace DataModelDemo.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class ClearContactTasksController : ViewController {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    public ClearContactTasksController() {
        // Target required Views (via the TargetXXX properties) and create their Actions.
        TargetViewType = ViewType.DetailView;
        TargetObjectType = typeof(DemoTask);

        SimpleAction clearTaskAction = new SimpleAction(this, "ClearTaskAction", PredefinedCategory.View) {
            Caption = "Clear Task",
            ConfirmationMessage = "Are you sure you want to clear this task?",
            ImageName = "Action_Clear"
        };

        clearTaskAction.Execute += ClearTaskAction_Execute;
    }

    private void ClearTaskAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
        DemoTask task = (DemoTask)View.CurrentObject;
        
        if (task?.Employee != null) {
            task.Employee.DemoTasks.Remove(task);
            ObjectSpace.SetModified(task.Employee, View.ObjectTypeInfo.FindMember(nameof(Employee.DemoTasks)));
        }

        ObjectSpace.Delete(task);
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
