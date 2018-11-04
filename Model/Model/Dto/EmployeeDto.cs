using System;

namespace Common.Model.Dto
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int? SectionID { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? RetirementDate { get; set; }

        public virtual DepartmentDto Department { get; set; }
        public virtual SectionDto Section { get; set; }
    }
}