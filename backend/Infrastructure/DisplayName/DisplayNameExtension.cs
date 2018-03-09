using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Infrastructure.DisplayName
{
    public static class DisplayNameExtension
    {
        public static string GetDisplayName<TEnum>(this TEnum @enum)
        {
            return @enum.GetType().GetMember(@enum.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;
        }
    }
}