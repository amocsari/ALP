using System;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single location
    /// </summary>
    public class LocationDto: LookupDtoBase
    {
        /// <summary>
        /// Compares two locations
        /// </summary>
        /// <param name="otherDto">The other location, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (LocationDto) otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        /// <summary>
        /// Creates a copy of the current location
        /// </summary>
        /// <returns>A copy of the location</returns>
        public override LookupDtoBase Copy()
        {
            return new LocationDto
            {
                Name = Name,
                Id = Id,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the locations properties by another LookupDtoBase
        /// The other Dto is also a locationDto
        /// </summary>
        /// <param name="sourceDto">The other location</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            Name = sourceDto.Name;
            Id = sourceDto.Id;
            Locked = sourceDto.Locked;
        }

        /// <summary>
        /// Checks if the LocationDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("A telephely nevét kötelező megadni!");
            }
        }
    }
}
