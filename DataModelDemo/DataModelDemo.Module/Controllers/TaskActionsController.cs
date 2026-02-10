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
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelDemo.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class TaskActionsController : ViewController {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    private ChoiceActionItem setPriorityItem;
    private ChoiceActionItem setStatusItem;
    public TaskActionsController() {
        // Target required Views (via the TargetXXX properties) and create their Actions.
        // TargetViewType = ViewType.ListView;
        // SimpleAction simpleAction1 = new SimpleAction(this, "SimpleAction1", PredefinedCategory.View);
        // simpleAction1.Execute += (s, e) {
        //    // Implement business logic: https://docs.devexpress.com/eXpressAppFramework/113711.
        // }
        TargetObjectType = typeof(DemoTask);
        SingleChoiceAction SetTaskAction = new SingleChoiceAction(this, "SetTaskAction", PredefinedCategory.Edit)
        {
            Caption = "Set Task",
            //Specify the display mode for the Action's items. Here the items are operations that you perform against selected records.
            ItemType = SingleChoiceActionItemType.ItemIsOperation,
            //Set the Action to become available in the Task List View when a user selects one or more objects.
            SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
        };
        setPriorityItem =
       new ChoiceActionItem(CaptionHelper.GetMemberCaption(typeof(DemoTask), "Priority"), null);
        SetTaskAction.Items.Add(setPriorityItem);
        FillItemWithEnumValues(setPriorityItem, typeof(BusinessObjects.Priority));

        setStatusItem =
           new ChoiceActionItem(CaptionHelper.GetMemberCaption(typeof(DemoTask), "Status"), null);
        SetTaskAction.Items.Add(setStatusItem);
        FillItemWithEnumValues(setStatusItem, typeof(BusinessObjects.TaskStatus));
        SetTaskAction.Execute += SetTaskAction_Execute;

    }
    private void FillItemWithEnumValues(ChoiceActionItem parentItem, Type enumType)
    {
        EnumDescriptor ed = new EnumDescriptor(enumType);
        foreach (object current in ed.Values)
        {
            ChoiceActionItem item = new ChoiceActionItem(ed.GetCaption(current), current);
            item.ImageName = ImageLoader.Instance.GetEnumValueImageName(current);
            parentItem.Items.Add(item);
        }
    }
    private void SetTaskAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        /*Create a new ObjectSpace if the Action is used in List View.
          Use this ObjectSpace to manipulate the View's selected objects.*/
        IObjectSpace objectSpace = View is ListView ?
            Application.CreateObjectSpace(typeof(DemoTask)) : View.ObjectSpace;
        ArrayList objectsToProcess = new ArrayList(e.SelectedObjects);
        if (e.SelectedChoiceActionItem.ParentItem == setPriorityItem)
        {
            foreach (Object obj in objectsToProcess)
            {
                DemoTask objInNewObjectSpace = (DemoTask)objectSpace.GetObject(obj);
                objInNewObjectSpace.Priority = (BusinessObjects.Priority)e.SelectedChoiceActionItem.Data;
            }
        }
        else
            if (e.SelectedChoiceActionItem.ParentItem == setStatusItem)
        {
            foreach (Object obj in objectsToProcess)
            {
                DemoTask objInNewObjectSpace = (DemoTask)objectSpace.GetObject(obj);
                objInNewObjectSpace.Status = (BusinessObjects.TaskStatus)e.SelectedChoiceActionItem.Data;
            }
        }
        objectSpace.CommitChanges();
        View.ObjectSpace.Refresh();
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
