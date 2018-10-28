using System;

namespace Model.Dto
{
    public class LocationDto: LookupDtoBase, IEquatable<LocationDto>
    {
        public bool Equals(LocationDto other)
        {
            return base.Equals(other);
        }
    }
}
