using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public abstract class LookupDtoBase: IEquatable<LookupDtoBase>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }

        public bool Equals(LookupDtoBase other)
        {
            return Id == other.Id && Name == other.Name && Locked == other.Locked;
        }
    }
}
