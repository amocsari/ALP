namespace Common.Model.Dto
{
    public class ItemStateDto : LookupDtoBase
    {
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemStateDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        public override LookupDtoBase Copy()
        {
            return new ItemStateDto
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
