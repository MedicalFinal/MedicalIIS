using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class DepartmentCategory
    {
        public DepartmentCategory()
        {
            Departments = new HashSet<Department>();
        }

        public int DeptCategoryId { get; set; }
        public string DeptCategoryName { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
