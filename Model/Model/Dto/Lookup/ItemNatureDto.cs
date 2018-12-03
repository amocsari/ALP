using System;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single ItemNature
    /// </summary>
    public class ItemNatureDto : LookupDtoBase
    {
        /// <summary>
        /// Compares two ItemNatures
        /// </summary>
        /// <param name="otherDto">The other ItemNature, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemNatureDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        /// <summary>
        /// Creates a copy of the current ItemNature
        /// </summary>
        /// <returns>A copy of the ItemNature</returns>
        public override LookupDtoBase Copy()
        {
            return new ItemNatureDto
            {
                Name = Name,
                Id = Id,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the ItemNatures properties by another LookupDtoBase
        /// The other Dto is also a ItemNatureDto
        /// </summary>
        /// <param name="sourceDto">The other ItemNature</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            Name = sourceDto.Name;
            Id = sourceDto.Id;
            Locked = sourceDto.Locked;
        }

        /// <summary>
        /// Checks if the ItemNatureDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az eszközjelleg nevét kötelező megadni!");
            }
        }
    }
}
