using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Order : BaseObject
    {
        public virtual string OrderNumber { get; set; }

        public virtual Product Product { get; set; }
        [DataSourceProperty("Product.Accessories")]
        public virtual Accessory Accessory { get; set; }
    }
}
