namespace HDD.Snomed
{
   public interface IAttribute
   {
      AttributeName Name { get; }
      IAttributeValue Value { get; }
      void ToStringFormatted(Output output);
   }
}