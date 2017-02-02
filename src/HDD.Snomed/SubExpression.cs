using System.Collections.Generic;

namespace HDD.Snomed
{
   public class SubExpression
   {
      public SubExpression()
      {
         FocusConcepts = new List<FocusConcept>();
      }

      public SubExpression(FocusConcept focusConcept, IRefinement refinement = null)
      {
         FocusConcepts = new List<FocusConcept>
         {
            focusConcept
         };
         Refinement = refinement;
      }

      public SubExpression(ConceptReference concept, IRefinement refinement = null)
      {
         FocusConcepts = new List<FocusConcept>
         {
            new FocusConcept(concept)
         };
         Refinement = refinement;
      }

      public SubExpression(IEnumerable<FocusConcept> focusConcepts, IRefinement refinement = null)
      {
         FocusConcepts = focusConcepts;
         Refinement = refinement;
      }

      public SubExpression(SubExpression subExpression) : this(subExpression.FocusConcepts, subExpression.Refinement)
      {
      }

      public IEnumerable<FocusConcept> FocusConcepts { get; }
      public IRefinement Refinement { get; }

      public override string ToString()
      {
         var output = "";
         foreach (var focusConcept in FocusConcepts)
         {
            if (output.Length > 0)
            {
               output += " + ";
            }
            output += focusConcept;
         }

         if (Refinement != null)
         {
            output += ": " + Refinement;
         }

         return output;
      }

      public virtual void ToStringFormatted(Output output)
      {
         output.Append(string.Join(" + ", FocusConcepts));
         if (Refinement != null)
         {
            output.AppendAndIndent(":");
            Refinement.ToStringFormatted(output);
         }
      }
   }
}