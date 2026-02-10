using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Accessory : BaseObject
    {
        public virtual String Name { get; set; }
        public virtual bool IsGlobal { get; set; }
        public virtual Product Product { get; set; }
    }   
}
