namespace HDD.Snomed
{
   public class Attribute : IAttribute
   {
      public Attribute(ulong sctId, IAttributeValue value)
      {
         Name = new AttributeName(sctId);
         Value = value;
      }

      public Attribute(ulong sctId, string term, IAttributeValue value)
      {
         Name = new AttributeName(sctId, term);
         Value = value;
      }

      public Attribute(ConceptReference concept, IAttributeValue value)
      {
         Name = new AttributeName(concept);
         Value = value;
      }

      public AttributeName Name { get; }
      public IAttributeValue Value { get; }

      public void ToStringFormatted(Output output)
      {
         Name.ToStringFormatted(output);
         output.Append(" = ");
         Value.ToStringFormatted(output);
      }

      public override string ToString()
      {
         return $"{Name} = {Value}";
      }
   }
}