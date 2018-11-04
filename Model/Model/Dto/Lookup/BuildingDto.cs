namespace Common.Model.Dto
{
    public class BuildingDto : LookupDtoBase
    {
        public int LocationId { get; set; }

        public LocationDto Location { get; set; }

        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (BuildingDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && LocationId == other.LocationId && Location.Equals(other.Location);
        }

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

        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (BuildingDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            LocationId = source.LocationId;
            Location.UpdateByDto(source.Location);
            Locked = source.Locked;
        }
    }
}
