namespace Common.Model.Dto
{
    public class SectionDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int FloorID { get; set; }
        public bool Locked { get; set; }

        public virtual FloorDto Floor { get; set; }
        public virtual DepartmentDto Department { get; set; }
    }
}