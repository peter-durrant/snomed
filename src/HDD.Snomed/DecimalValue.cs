using System;
using System.Globalization;

namespace HDD.Snomed
{
   public class DecimalValue : ConcreteValue<decimal>
   {
      public DecimalValue(decimal value) : base(value)
      {
      }

      public override string ToString()
      {
         return "#" + Value;
      }

      public override void ToStringFormatted(Output output)
      {
         output.Append("#");
         output.Append(Value.ToString(CultureInfo.InvariantCulture));
      }

      public static decimal Parse(string value)
      {
         if ((value == null) || (value.Length <= 1) || (value[0] != '#'))
         {
            throw new ArgumentException();
         }
         return decimal.Parse(value.Substring(1));
      }
   }
}