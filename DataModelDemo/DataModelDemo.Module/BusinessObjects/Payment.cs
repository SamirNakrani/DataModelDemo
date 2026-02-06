using DevExpress.ExpressApp.DC;
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
    public class Payment : BaseObject
    {
        public virtual decimal Rate { get; set; }
        public virtual float Hours { get; set; }

        [PersistentAlias("Rate * Hours")]
        public decimal Amount
        {
            get { return EvaluateAlias<decimal>(); }
        }
    }
}
