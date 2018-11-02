namespace Model.Dto
{
    public class ItemTypeDto : LookupDtoBase
    {
        public int ItemNatureId { get; set; }

        public ItemNatureDto ItemNature { get; set; }

        public override bool Equals(LookupDtoBase otherDto)
        {
            var other = (ItemTypeDto)otherDto;

            return Id == other.Id && Name == other.Name && Locked == other.Locked && ItemNatureId == other.ItemNatureId && ItemNature.Equals(other.ItemNature);
        }

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

        public override void UpdateByDto(LookupDtoBase sourceDto)
        {
            var source = (ItemTypeDto)sourceDto;
            Id = source.Id;
            Name = source.Name;
            ItemNatureId = source.ItemNatureId;
            ItemNature.UpdateByDto(source.ItemNature);
            Locked = source.Locked;
        }
    }
}
