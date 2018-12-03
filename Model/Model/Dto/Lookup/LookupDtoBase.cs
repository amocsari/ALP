using System;

namespace Common.Model.Dto
{
    /// <summary>
    /// Used to store the date of a single lookup data
    /// </summary>
    public abstract class LookupDtoBase: IEquatable<LookupDtoBase>
    {
        /// <summary>
        /// Id of the stored lookup data
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the stored Lookup data
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Locked status of the stored lookup data
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Compares two LookupDatas
        /// </summary>
        /// <param name="otherDto">The other LookupData, which this is compared to.</param>
        /// <returns>The result of the comparison</returns>
        public abstract bool Equals(LookupDtoBase other);

        /// <summary>
        /// Creates a copy of the current LookupData
        /// </summary>
        /// <returns>A copy of the LookupData</returns>
        public abstract LookupDtoBase Copy();

        /// <summary>
        /// Updates the values of the LookupDatas properties by another LookupDtoBase
        /// </summary>
        /// <param name="sourceDto">The other LookupData</param>
        public abstract void UpdateByDto(LookupDtoBase sourceDto);

        /// <summary>
        /// Checks if the dto contains only valid data
        /// </summary>
        public abstract void Validate();
    }
}
