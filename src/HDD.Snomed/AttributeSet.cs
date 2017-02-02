using System.Collections.Generic;
using System.Linq;

namespace HDD.Snomed
{
   public class AttributeSet
   {
      public AttributeSet()
      {
         Attributes = new List<IAttribute>();
      }

      public AttributeSet(IAttribute attribute)
      {
         Attributes = new List<IAttribute> {attribute};
      }

      public AttributeSet(IEnumerable<IAttribute> attributes)
      {
         Attributes = attributes.ToList();
      }

      public List<IAttribute> Attributes { get; }

      public override string ToString()
      {
         var output = "";
         foreach (var attribute in Attributes)
         {
            if (output.Length > 0)
            {
               output += ", ";
            }
            output += attribute;
         }
         return output;
      }

      public void ToStringFormatted(Output output)
      {
         var first = true;
         foreach (var attribute in Attributes)
         {
            if (first)
            {
               first = false;
            }
            else
            {
               output.AppendAndBreak(",");
            }
            attribute.ToStringFormatted(output);
         }
      }
   }
}