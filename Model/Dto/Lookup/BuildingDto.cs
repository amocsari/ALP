using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class BuildingDto: LookupDtoBase, IEquatable<BuildingDto>
    {
        public int LocationId { get; set; }

        public LocationDto Location { get; set; }

        public bool Equals(BuildingDto other)
        {
            return base.Equals(other) && LocationId == other.LocationId;
        }
    }
}
