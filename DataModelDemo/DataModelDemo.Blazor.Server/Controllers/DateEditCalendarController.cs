using DataModelDemo.Module.BusinessObjects;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;

namespace DataModelDemo.Blazor.Server.Controllers
{
    public class DateCalendarController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            View.CustomizeViewItemControl<DateTimePropertyEditor>(
                this,
                editor =>
                {
                    editor.ComponentModel.PickerDisplayMode = DevExpress.Blazor.DatePickerDisplayMode.ScrollPicker;
                },
                [nameof(Employee.Birthday),nameof(DemoTask.DueDate)]
            );
        }
    }
}
