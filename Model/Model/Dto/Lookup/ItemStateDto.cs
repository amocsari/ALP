using System;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single ItemState
    /// </summary>
    public class ItemStateDto : LookupDtoBase
    {
        /// <summary>
        /// Compares two ItemStates
        /// </summary>
        /// <param name="otherDto">The other ItemState, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemStateDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        /// <summary>
        /// Creates a copy of the current ItemState
        /// </summary>
        /// <returns>A copy of the ItemState</returns>
        public override LookupDtoBase Copy()
        {
            return new ItemStateDto
            {
                Name = Name,
                Id = Id,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the ItemStates properties by another LookupDtoBase
        /// The other Dto is also a ItemStateDto
        /// </summary>
        /// <param name="sourceDto">The other ItemState</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            Name = sourceDto.Name;
            Id = sourceDto.Id;
            Locked = sourceDto.Locked;
        }

        /// <summary>
        /// Checks if the ItemStateDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az eszközállapot nevét kötelező megadni!");
            }
        }
    }
}
