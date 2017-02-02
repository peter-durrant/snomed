using System.Collections.Generic;
using System.Linq;

namespace HDD.Snomed
{
   public class AttributeGroupRefinement : IRefinement
   {
      public AttributeGroupRefinement(AttributeGroup attributeGroup)
      {
         AttributeGroups = new List<AttributeGroup> {attributeGroup};
      }

      public AttributeGroupRefinement(IEnumerable<AttributeGroup> attributeGroups)
      {
         AttributeGroups = attributeGroups.ToList();
      }

      public List<AttributeGroup> AttributeGroups { get; }

      public void ToStringFormatted(Output output)
      {
         var first = true;
         foreach (var attributeGroup in AttributeGroups)
         {
            if (first)
            {
               first = false;
            }
            else
            {
               output.AppendAndBreak(",");
            }
            output.Append("{");
            attributeGroup.ToStringFormatted(output);
            output.Append("}");
         }
      }

      public override string ToString()
      {
         var output = "";

         foreach (var attributeGroup in AttributeGroups)
         {
            if (output.Length > 1)
            {
               output += ", ";
            }

            output += "{" + attributeGroup + "}";
         }

         return output;
      }
   }
}