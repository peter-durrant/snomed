using System.Collections.Generic;

namespace HDD.Snomed
{
   public class ExpressionValue : SubExpression, IAttributeValue
   {
      protected ExpressionValue(FocusConcept focusConcept) : base(focusConcept, null)
      {
      }

      protected ExpressionValue(IEnumerable<FocusConcept> focusConcepts) : base(focusConcepts)
      {
      }

      protected ExpressionValue(SubExpression subExpression) : base(subExpression)
      {
      }

      public ExpressionValue(ConceptReference concept) : base(concept)
      {
      }

      public static ExpressionValue AsFocusConcept(ulong sctId)
      {
         return new ExpressionValue(new FocusConcept(sctId));
      }

      public static ExpressionValue AsFocusConcept(ulong sctId, string term)
      {
         return new ExpressionValue(new FocusConcept(sctId, term));
      }
   }

   public class NestedExpressionValue : ExpressionValue
   {
      public NestedExpressionValue(FocusConcept focusConcept) : base(focusConcept)
      {
      }

      public NestedExpressionValue(IEnumerable<FocusConcept> focusConcepts) : base(focusConcepts)
      {
      }

      public NestedExpressionValue(SubExpression subExpression) : base(subExpression)
      {
      }

      public override string ToString()
      {
         return "(" + base.ToString() + ")";
      }

      public override void ToStringFormatted(Output output)
      {
         output.BreakAndIndent();
         output.Append("(");
         base.ToStringFormatted(output);
         output.Append(")");
      }
   }
}