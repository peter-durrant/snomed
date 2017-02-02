using System;
using System.ComponentModel;

namespace HDD.Snomed
{
   public static class EnumsHelper
   {
      public static string GetDescription(this Enum value)
      {
         var type = value.GetType();
         var name = Enum.GetName(type, value);
         if (name != null)
         {
            var field = type.GetField(name);
            if (field != null)
            {
               var attr =
                  System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
               if (attr != null)
               {
                  return attr.Description;
               }
            }
         }
         return null;
      }

      public static string ToStringOutput(this DefinitionStatus definitionStatus)
      {
         switch (definitionStatus)
         {
            case DefinitionStatus.None:
               return "";
            case DefinitionStatus.EquivalentTo:
               return "===";
            case DefinitionStatus.SubTypeOf:
               return "<<<";
            default:
               throw new InvalidEnumArgumentException(nameof(definitionStatus), (int) definitionStatus,
                  typeof(DefinitionStatus));
         }
      }
   }
}