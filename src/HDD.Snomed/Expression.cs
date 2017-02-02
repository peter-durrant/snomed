using System.Collections.Generic;

namespace HDD.Snomed
{
   public class Expression
   {
      public Expression(FocusConcept focusConcept, IRefinement refinement = null,
         DefinitionStatus definitionStatus = DefinitionStatus.None)
      {
         DefinitionStatus = definitionStatus;
         SubExpression = new SubExpression(focusConcept, refinement);
      }

      public Expression(IEnumerable<FocusConcept> focusConcepts, IRefinement refinement = null,
         DefinitionStatus definitionStatus = DefinitionStatus.None)
      {
         DefinitionStatus = definitionStatus;
         SubExpression = new SubExpression(focusConcepts, refinement);
      }

      public Expression(SubExpression subExpression, DefinitionStatus definitionStatus = DefinitionStatus.None)
      {
         DefinitionStatus = definitionStatus;
         SubExpression = subExpression;
      }

      public DefinitionStatus DefinitionStatus { get; }
      public SubExpression SubExpression { get; }

      public override string ToString()
      {
         var output = new Output();
         var definitionStatus = DefinitionStatus.ToStringOutput();
         output.Append(definitionStatus);
         if (definitionStatus.Length > 0)
         {
            output.Append(" ");
         }
         SubExpression.ToStringFormatted(output);
         return output.ToString();
      }
   }
}