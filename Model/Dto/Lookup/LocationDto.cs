using System;

namespace Model.Dto
{
    public class LocationDto: LookupDtoBase
    {
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (LocationDto) otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        public override LookupDtoBase Copy()
        {
            return new LocationDto
            {
                Name = Name,
                Id = Id,
                Locked = Locked
            };
        }

        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            Name = sourceDto.Name;
            Id = sourceDto.Id;
            Locked = sourceDto.Locked;
        }
    }
}
