using System;

namespace HDD.Snomed
{
   public class IntegerValue : ConcreteValue<int>
   {
      public IntegerValue(int value) : base(value)
      {
      }

      public override string ToString()
      {
         return Value.ToString();
      }

      public override void ToStringFormatted(Output output)
      {
         output.Append("#");
         output.Append(Value.ToString());
      }

      public static int Parse(string value)
      {
         if ((value == null) || (value.Length <= 1) || (value[0] != '#'))
         {
            throw new ArgumentException();
         }
         return int.Parse(value.Substring(1));
      }
   }
}