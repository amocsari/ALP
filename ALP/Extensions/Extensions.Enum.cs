using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace ALP.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the description attribute value of an enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}
