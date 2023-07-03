using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public static class EnumExtension
    {
        public static T? GetAttributeOfType<T>(this Enum ValueEnum) where T : System.Attribute
        {
            var type = ValueEnum.GetType();
            var memInfo = type.GetMember(ValueEnum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDescription(this Enum valorEnum)
        {
            return valorEnum?.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
        }
    }
}