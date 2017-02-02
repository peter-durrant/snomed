using System.Collections.Generic;
using System.Linq;

namespace HDD.Snomed
{
   public class AttributeSetRefinement : IRefinement
   {
      public AttributeSetRefinement()
      {
         AttributeSet = new AttributeSet();
         AttributeGroups = new List<AttributeGroup>();
      }

      public AttributeSetRefinement(AttributeSet attributeSet)
      {
         AttributeSet = attributeSet;
      }

      public AttributeSetRefinement(AttributeSet attributeSet, AttributeGroup attributeGroup)
      {
         AttributeSet = attributeSet;
         AttributeGroups = new List<AttributeGroup> {attributeGroup};
      }

      public AttributeSetRefinement(AttributeSet attributeSet, IEnumerable<AttributeGroup> attributeGroups)
      {
         AttributeSet = attributeSet;
         AttributeGroups = attributeGroups.ToList();
      }

      public AttributeSet AttributeSet { get; }
      public List<AttributeGroup> AttributeGroups { get; }

      public void ToStringFormatted(Output output)
      {
         AttributeSet.ToStringFormatted(output);
         if (AttributeGroups != null)
         {
            output.AppendAndBreak(",");
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
      }

      public override string ToString()
      {
         if (AttributeGroups != null)
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

            return AttributeSet + ", " + output;
         }
         return AttributeSet.ToString();
      }
   }
}