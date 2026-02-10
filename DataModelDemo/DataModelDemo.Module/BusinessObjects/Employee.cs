using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelDemo.Module.BusinessObjects
{
    [ObjectCaptionFormat("{0:FullName}")]
    [DefaultProperty(nameof(FullName))]
    [DefaultClassOptions]
    public class Employee: BaseObject
    {
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual String MiddleName { get; set; }

        public virtual DateTime? Birthday { get; set; }

        //Use this attribute to hide or show the editor of this property in the UI.
        [Browsable(false)]
        public virtual int TitleOfCourtesy_Int { get; set; }

        //Use this attribute to exclude the property from database mapping.
        [NotMapped]
        public virtual TitleOfCourtesy TitleOfCourtesy { get; set; }

        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public String FullName
        {
            get { return ObjectFormatter.Format(FullNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public String DisplayName
        {
            get { return FullName; }
        }

        public static String FullNameFormat = "{FirstName} {MiddleName} {LastName}";

        [FieldSize(255)]
        public virtual String Email { get; set; }

        [RuleRegularExpression(@"^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$",CustomMessageTemplate = @"Invalid website URL.")]
        public virtual string WebPageAddress { get; set; }

        [StringLength(4096)]
        public virtual string Notes { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<DemoTask> DemoTasks { get; set; } = new ObservableCollection<DemoTask>();
        public virtual Position Position { get; set; }

        [DataSourceProperty("Department.Employees", DataSourcePropertyIsNullMode.SelectAll), DataSourceCriteria("Position.Title = 'Manager'")]
        public virtual Employee Manager { get; set; }
    }
    public enum TitleOfCourtesy
    {
        Dr,
        Miss,
        Mr,
        Mrs,
        Ms
    }
}
