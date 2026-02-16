using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(Name))]
    public class Skill : BaseObject
    {
        [RuleRequiredField]
        public virtual string Name { get; set; }  

        public virtual IList<EmployeeSkill> Employees { get; set; }  
            = new ObservableCollection<EmployeeSkill>();
    }

}
