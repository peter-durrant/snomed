namespace HDD.Snomed
{
   public class FocusConcept : ConceptReference
   {
      public FocusConcept(ulong sctId) : base(sctId)
      {
      }

      public FocusConcept(ulong sctId, string term) : base(sctId, term)
      {
      }

      public FocusConcept(ConceptReference concept) : base(concept)
      {
      }
   }
}