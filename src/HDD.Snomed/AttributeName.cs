namespace HDD.Snomed
{
   public class AttributeName : ConceptReference
   {
      public AttributeName(ulong sctId) : base(sctId)
      {
      }

      public AttributeName(ulong sctId, string term) : base(sctId, term)
      {
      }

      public AttributeName(ConceptReference concept) : base(concept)
      {
      }
   }
}