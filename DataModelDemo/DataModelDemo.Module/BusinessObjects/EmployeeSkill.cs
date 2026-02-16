using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class EmployeeSkill : BaseObject
    {
        [RuleRequiredField]
        public virtual Employee Employee { get; set; }

        [RuleRequiredField]
        public virtual Skill Skill { get; set; }
    }

}
