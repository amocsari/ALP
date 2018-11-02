namespace Model.Dto
{
    public class FloorDto : LookupDtoBase
    {
        public int BuildingId { get; set; }

        public BuildingDto Building { get; set; }

        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (FloorDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && BuildingId == other.BuildingId && Building.Equals(other.Building);
        }

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

        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (FloorDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            BuildingId = source.BuildingId;
            Building.UpdateByDto(source.Building);
            Locked = source.Locked;
        }
    }
}
