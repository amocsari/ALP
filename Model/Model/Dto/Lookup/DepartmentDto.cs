using System;
using System.Text;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single Department
    /// </summary>
    public class DepartmentDto : LookupDtoBase
    {
        /// <summary>
        /// Id of the Department's Employee
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Navigation property of the Department's Employee
        /// </summary>
        public EmployeeDto Employee { get; set; }

        /// <summary>
        /// Compares two Departments
        /// </summary>
        /// <param name="otherDto">The other Department, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (DepartmentDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && EmployeeId == other.EmployeeId && Employee.Equals(other.Employee);
        }

        /// <summary>
        /// Creates a copy of the current Department
        /// </summary>
        /// <returns>A copy of the Department</returns>
        public override LookupDtoBase Copy()
        {
            return new DepartmentDto
            {
                Employee = Employee?.Copy(),
                Name = Name,
                Id = Id,
                EmployeeId = EmployeeId,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the Departments properties by another LookupDtoBase
        /// The other Dto is also a DepartmentDto
        /// </summary>
        /// <param name="sourceDto">The other Department</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (DepartmentDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            EmployeeId = source.EmployeeId;
            Employee.UpdateByDto(source.Employee);
            Locked = source.Locked;
        }

        /// <summary>
        /// Checks if the DepartmentDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az osztály nevét kötelező megadni!");
            }
        }

        /// <summary>
        /// Turns the data of the object into a string
        /// Used for logging
        /// </summary>
        /// <returns>The string form of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{{ Id = {Id}");
            sb.Append($", Name = {Name}");
            sb.Append($", EmployeeId = {EmployeeId}");
            sb.Append($", Employee = {Employee.ToString()}");
            sb.Append($", Locked = {Locked}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
