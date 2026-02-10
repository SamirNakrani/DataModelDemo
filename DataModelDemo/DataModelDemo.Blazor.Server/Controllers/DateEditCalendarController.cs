using DataModelDemo.Module.BusinessObjects;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;

namespace DataModelDemo.Blazor.Server.Controllers
{
    public partial class DateEditCalendarController : ObjectViewController<DetailView, Employee>
    {
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

             var gridListEditor = View.Items
                .OfType<DxGridListEditor>()
                .FirstOrDefault();

            if (gridListEditor != null)
            {
                gridListEditor.GridModel.ColumnResizeMode = GridColumnResizeMode.ColumnsContainer;

                foreach (DxGridColumnWrapper column in gridListEditor.Columns)
                {
                    if (column.PropertyName == $"{nameof(Employee.TitleOfCourtesy)}")
                    {
                        column.DxGridDataColumnModel.FilterMenuButtonDisplayMode = GridFilterMenuButtonDisplayMode.Never;
                    }
                    column.MinWidth = 50;
                }
            }
        }
    }
}

