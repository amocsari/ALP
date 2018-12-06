using System;
using System.Text;

namespace Common.Model.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DepartmentID { get; set; }
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
                DepartmentID = DepartmentID,
                Department = (DepartmentDto)Department?.Copy(),
                SectionID = SectionID,
                Section = (SectionDto)Section?.Copy(),
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
            return Id == other.Id
                   && Name == other.Name
                   && DepartmentID == other.DepartmentID
                   && SectionID == other.SectionID
                   && EmailAddress == other.EmailAddress
                   && RetirementDate == other.RetirementDate;
        }

        public void UpdateByDto(EmployeeDto sourceDto)
        {
            Id = sourceDto.Id;
            Name = sourceDto.Name;
            DepartmentID = sourceDto.DepartmentID;
            SectionID = sourceDto.SectionID;
            EmailAddress = sourceDto.EmailAddress;
            RetirementDate = sourceDto.RetirementDate;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{{ Id = {Id}");
            sb.Append($", Name = {Name}");
            sb.Append($", DepartmentId = {DepartmentID}");
            sb.Append($", SectionId = {SectionID}");
            sb.Append($", EmailAddress = {EmailAddress}");
            sb.Append($", RetirementDate = {RetirementDate?.ToShortDateString()}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}