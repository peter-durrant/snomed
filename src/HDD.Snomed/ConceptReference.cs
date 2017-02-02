namespace HDD.Snomed
{
   public class ConceptReference
   {
      protected ConceptReference(ulong sctId)
      {
         SctId = sctId;
         Term = null;
      }

      protected ConceptReference(ulong sctId, string term)
      {
         SctId = sctId;
         Term = term.RemoveTermDelimiter();
      }

      protected ConceptReference(ConceptReference concept)
      {
         SctId = concept.SctId;
         Term = concept.Term;
      }

      public ulong SctId { get; }
      public string Term { get; }

      public override string ToString()
      {
         return string.IsNullOrEmpty(Term) ? SctId.ToString() : $"{SctId}|{Term}|";
      }

      public void ToStringFormatted(Output output)
      {
         output.Append(SctId.ToString());
         if (!string.IsNullOrEmpty(Term))
         {
            output.Append("|");
            output.Append(Term);
            output.Append("|");
         }
      }
   }
}