namespace HDD.Snomed
{
   public abstract class ConcreteValue<T> : IAttributeValue
   {
      protected ConcreteValue(T value)
      {
         Value = value;
      }

      protected T Value { get; private set; }

      public abstract void ToStringFormatted(Output output);
   }
}