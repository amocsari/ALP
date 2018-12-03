using System;

namespace Common.Model.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int? SectionID { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? RetirementDate { get; set; }

        public virtual DepartmentDto Department { get; set; }
        public virtual SectionDto Section { get; set; }

        public EmployeeDto Copy()
        {
            return new EmployeeDto
            {
                Id = Id,
                Name = Name,
                DepartmentID =  DepartmentID,
                Department = (DepartmentDto)Department.Copy(),
                SectionID = SectionID,
                Section = (SectionDto)Section.Copy(),
                EmailAddress = EmailAddress,
                RetirementDate = RetirementDate
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("A munkavállaló nevét kötelező kitölteni!");
            }

            if (Department == null)
            {
                throw new Exception("A munkavállaló osztályát kötelező kitölteni!");
            }
        }

        public bool Equals(EmployeeDto other)
        {
            throw new NotImplementedException();
        }

        public void UpdateByDto(EmployeeDto sourceDto)
        {
            throw new NotImplementedException();
        }
    }
}