using System;
using System.Text;

namespace Model.Model
{
    public class EmployeeFilterInfo
    {
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        //public int RoleId { get; set; }
        //public DateTime LeaveDate { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{{ Name = {Name}");
            sb.Append($", DepartmentId = {DepartmentId}");
            sb.Append($", SectionId = {SectionId}");
            //sb.Append($", RoleId = {RoleId}, ");
            //sb.Append($", LeaveDate = {LeaveDate.ToShortDateString()}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
