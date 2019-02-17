using System;
using System.Text;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single itemType
    /// </summary>
    public class ItemTypeDto : LookupDtoBase
    {
        /// <summary>
        /// Id of the ItemType's ItemNature
        /// </summary>
        public int ItemNatureId { get; set; }

        /// <summary>
        /// Navigation property of the ItemType's ItemNature
        /// </summary>
        public ItemNatureDto ItemNature { get; set; }

        /// <summary>
        /// Compares two itemTypes
        /// </summary>
        /// <param name="otherDto">The other itemType, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemTypeDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && ItemNatureId == other.ItemNatureId;
        }

        /// <summary>
        /// Creates a copy of the current itemType
        /// </summary>
        /// <returns>A copy of the itemType</returns>
        public override LookupDtoBase Copy()
        {
            return new ItemTypeDto
            {
                ItemNature = (ItemNatureDto)ItemNature?.Copy(),
                Name = Name,
                Id = Id,
                ItemNatureId = ItemNatureId,
                Locked = Locked
            };
        }

        /// <summary>
        /// Updates the values of the itemTypes properties by another LookupDtoBase
        /// The other Dto is also a itemTypeDto
        /// </summary>
        /// <param name="sourceDto">The other itemType</param>
        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (ItemTypeDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            ItemNatureId = source.ItemNatureId;
            Locked = source.Locked;
        }

        /// <summary>
        /// Checks if the ItemTypeDto contains only valid data
        /// </summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Az eszköztípus nevét kötelező megadni!");
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
            sb.Append($", ItemNatureId = {ItemNatureId}");
            sb.Append($", Building = {ItemNature?.ToString()}");
            sb.Append($", Locked = {Locked}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
