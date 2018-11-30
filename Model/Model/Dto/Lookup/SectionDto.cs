﻿namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single Section
    /// </summary>
    public class SectionDto : LookupDtoBase
    {
        /// <summary>
        /// Id of the Section's Department
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Id of the Section's Floor
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// Navigation property of the Section's Department
        /// </summary>
        public DepartmentDto Department { get; set; }

        /// <summary>
        /// Navigation property of the Section's Floor
        /// </summary>
        public FloorDto Floor { get; set; }

        /// <summary>
        /// Compares two Sections
        /// </summary>
        /// <param name="otherDto">The other Section, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (SectionDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked
                   && DepartmentId == other.DepartmentId && Department.Equals(other.Department)
                   && FloorId == other.FloorId && Floor.Equals(other.Floor);
        }

        /// <summary>
        /// Creates a copy of the current Section
        /// </summary>
        /// <returns>A copy of the Section</returns>
        public override LookupDtoBase Copy()
        {
            return new SectionDto
            {
                Department = (DepartmentDto)Department?.Copy(),
                Floor = (FloorDto)Floor?.Copy(),
                Name = Name,
                Id = Id,
                DepartmentId = DepartmentId,
                FloorId = FloorId,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the Sections properties by another LookupDtoBase
        /// The other Dto is also a SectionDto
        /// </summary>
        /// <param name="sourceDto">The other Section</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (SectionDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            DepartmentId = source.DepartmentId;
            FloorId = source.FloorId;
            Department.UpdateByDto(source.Department);
            Floor.UpdateByDto(source.Floor);
            Locked = source.Locked;
        }
    }
}