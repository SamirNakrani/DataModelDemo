using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask:BaseObject
    {
        public virtual DateTime? DateCompleted { get; set; }

        public virtual String Subject { get; set; }

        [FieldSize(FieldSizeAttribute.Unlimited)]
        public virtual String Description { get; set; }

        public virtual DateTime? DueDate { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual int PercentCompleted { get; set; }

        private TaskStatus status;
        public virtual IList<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public virtual Priority Priority { get; set; }

        public virtual TaskStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                if (isLoaded)
                {
                    if (value == TaskStatus.Completed)
                    {
                        DateCompleted = DateTime.Now;
                    }
                    else
                    {
                        DateCompleted = null;
                    }
                }
            }
        }

        [Action(ImageName = "State_Task_Completed")]
        public void MarkCompleted()
        {
            Status = TaskStatus.Completed;
        }

        private bool isLoaded = false;
        public override void OnLoaded()
        {
            isLoaded = true;
        }
        public override void OnCreated()
        {
            Priority = Priority.Normal;
        }

    }
    public enum TaskStatus
    {
        [ImageName("State_Task_NotStarted")]
        NotStarted,
        [ImageName("State_Task_InProgress")]
        InProgress,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        WaitingForSomeoneElse,
        [ImageName("State_Task_Deferred")]
        Deferred,
        [ImageName("State_Task_Completed")]
        Completed
    }

    public enum Priority
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

}

