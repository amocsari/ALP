using System;
using System.Text;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single building
    /// </summary>
    public class BuildingDto : LookupDtoBase
    {
        /// <summary>
        /// Id of the building's location
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Navigation property of the building's location
        /// </summary>
        public LocationDto Location { get; set; }

        /// <summary>
        /// Compares two buildings
        /// </summary>
        /// <param name="otherDto">The other building, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (BuildingDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && LocationId == other.LocationId && Location.Equals(other.Location);
        }

        /// <summary>
        /// Creates a copy of the current building
        /// </summary>
        /// <returns>A copy of the building</returns>
        public override LookupDtoBase Copy()
        {
            return new BuildingDto
            {
                Location = (LocationDto)Location?.Copy(),
                Name = Name,
                Id = Id,
                LocationId = LocationId,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the Buildings properties by another LookupDtoBase
        /// The other Dto is also a BuildingDto
        /// </summary>
        /// <param name="sourceDto">The other building</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (BuildingDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            LocationId = source.LocationId;
            Location.UpdateByDto(source.Location);
            Locked = source.Locked;
        }

        /// <summary>
        /// Checks if the BuildingDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az épület nevét kötelező megadni!");
            }

            if (Location == null)
            {
                throw new Exception("Az épület telephelye nem lehet üres!");
            }
        }

        /// <summary>
        /// Turns the data of the object into a string
        /// Used for logging
        /// </summary>
        /// <returns>The string form of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{{ Id = {Id}, ");
            sb.Append($"Name = {Name}, ");
            sb.Append($"LocationId = {LocationId}, ");
            sb.Append($"Location = {Location.ToString()} ");
            sb.Append($"Locked = {Locked} ");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
