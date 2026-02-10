using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Product : BaseObject
    {
        public virtual string Name { get; set; }

        public virtual IList<Accessory> Accessories { get; set; }
               = new ObservableCollection<Accessory>();
    }
}
