using System;
using System.Text;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single floor
    /// </summary>
    public class FloorDto : LookupDtoBase
    {
        /// <summary>
        /// Id of the floor's building
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// Navigation property of the floor's building
        /// </summary>
        public BuildingDto Building { get; set; }

        /// <summary>
        /// Compares two floors
        /// </summary>
        /// <param name="otherDto">The other floor, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (FloorDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && BuildingId == other.BuildingId && Building.Equals(other.Building);
        }

        /// <summary>
        /// Creates a copy of the current floor
        /// </summary>
        /// <returns>A copy of the floor</returns>
        public override LookupDtoBase Copy()
        {
            return new FloorDto
            {
                Building = (BuildingDto)Building?.Copy(),
                Name = Name,
                Id = Id,
                BuildingId = BuildingId,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the floors properties by another LookupDtoBase
        /// The other Dto is also a floor
        /// </summary>
        /// <param name="sourceDto">The other floor</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (FloorDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            BuildingId = source.BuildingId;
            Building?.UpdateByDto(source.Building);
            Locked = source.Locked;
        }

        /// <summary>
        /// Checks if the FloorDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az emelet nevét kötelező megadni!");
            }

            if (Building == null)
            {
                throw new Exception("Az épület megadása kötelező");
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
            sb.Append($", BuildingId = {BuildingId}");
            sb.Append($", Building = {Building?.ToString()}");
            sb.Append($", Locked = {Locked}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
