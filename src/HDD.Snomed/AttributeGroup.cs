namespace HDD.Snomed
{
   public class AttributeGroup
   {
      public AttributeGroup()
      {
         AttributeSet = new AttributeSet();
      }

      public AttributeGroup(AttributeSet attributeSet)
      {
         AttributeSet = attributeSet;
      }

      public AttributeSet AttributeSet { get; }

      public override string ToString()
      {
         return AttributeSet.ToString();
      }

      public void ToStringFormatted(Output output)
      {
         AttributeSet.ToStringFormatted(output);
      }
   }
}