namespace Common.Model.Dto
{
    public class ItemNatureDto : LookupDtoBase
    {
        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemNatureDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }

        public override LookupDtoBase Copy()
        {
            return new ItemNatureDto
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
