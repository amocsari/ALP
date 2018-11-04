namespace Common.Model.Dto
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeID { get; set; }
        public bool Locked { get; set; }

        public virtual EmployeeDto Employee { get; set; }
    }
}