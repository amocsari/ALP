using System;

namespace Model
{
    public class LocationDto: IEquatable<LocationDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(LocationDto other)
        {
            return Id == other.Id && Name == other.Name;
        }
    }
}
