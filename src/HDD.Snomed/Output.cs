using System;
using System.Text;

namespace HDD.Snomed
{
   public class Output
   {
      private int _indentation;
      private readonly StringBuilder _output = new StringBuilder();

      public void Append(string str)
      {
         _output.Append(str);
      }

      public void AppendAndIndent(string str)
      {
         _indentation++;
         AppendAndBreak(str);
      }

      public void AppendAndBreak(string str)
      {
         _output.Append(str);
         _output.Append(Environment.NewLine);
         _output.Append(new string('\t', _indentation));
      }

      public void BreakAndIndent()
      {
         _output.Append(Environment.NewLine);
         _indentation++;
         _output.Append(new string('\t', _indentation));
      }

      public override string ToString()
      {
         return _output.ToString();
      }
   }
}