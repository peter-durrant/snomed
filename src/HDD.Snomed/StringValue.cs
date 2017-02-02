using System;

namespace HDD.Snomed
{
   public class StringValue : ConcreteValue<string>
   {
      public StringValue(string value) : base(value)
      {
      }

      public override string ToString()
      {
         return "\"" + Value + "\"";
      }

      public override void ToStringFormatted(Output output)
      {
         output.Append("\"");
         output.Append(Value);
         output.Append("\"");
      }

      public static string Parse(string value)
      {
         if ((value == null) || (value.Length <= 2) || !value.StartsWith("\"") || !value.EndsWith("\""))
         {
            throw new ArgumentException();
         }
         return value.Substring(1, value.Length - 2);
      }
   }
}