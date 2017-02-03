using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.Snomed.Console
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var expression = "  73211009";
         ParseExpression(expression);

         expression = "73211009|diabetes mellitus|";
         ParseExpression(expression);

         expression = "73211009|diabetes mellitus|";
         ParseExpression(expression);

         expression = " 73211009 | diabetes mellitus | ";
         ParseExpression(expression);

         expression = "73211009 + 31435000";
         ParseExpression(expression);

         expression = "73211009+ 31435000";
         ParseExpression(expression);

         expression = "73211009 +31435000";
         ParseExpression(expression);

         expression = "73211009 + 31435000";
         ParseExpression(expression);

         expression = "73211009 + 31435000 + 91143003";
         ParseExpression(expression);

         expression = "73211009|diabetes mellitus| + 31435000|fallopian tube structure| + 91143003|albuterol|";
         ParseExpression(expression);

         expression = "73211009|diabetes mellitus|:73211009=212";
         ParseExpression(expression);

         var sb = new StringBuilder();
         sb.Append("71388002|procedure|:" + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 15497006|ovarian structure|}," + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 31435000|fallopian tube structure|}");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("373873005|pharmaceutical / biologic product|:" + Environment.NewLine);
         sb.Append("	411116001|has dose form| = " + Environment.NewLine);
         sb.Append("		(421720008|spray dose form| + 7946007|drug suspension|)");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("397956004|prosthetic arthroplasty of the hip|:" + Environment.NewLine);
         sb.Append("	405814001|procedure site - indirect| = " + Environment.NewLine);
         sb.Append("		(24136001|hip joint structure|:" + Environment.NewLine);
         sb.Append("			272741003|laterality| = 7771000|left|)," + Environment.NewLine);
         sb.Append("			{363699004|direct device| = 304120007|total hip replacement prosthesis|," + Environment.NewLine);
         sb.Append("			260686004|method| = 425362007|surgical insertion - action|}");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("243796009|situation with explicit context|:" + Environment.NewLine);
         sb.Append("	{408730004|procedure context| = 385658003|done|," + Environment.NewLine);
         sb.Append("	408731000|temporal context| = 410512000|current or specified|," + Environment.NewLine);
         sb.Append("	408732007|subject relationship context| = 410604004|subject of record|," + Environment.NewLine);
         sb.Append("	363589002|associated procedure| = " + Environment.NewLine);
         sb.Append("		(397956004|prosthetic arthroplasty of the hip|:" + Environment.NewLine);
         sb.Append("			405814001|procedure site - indirect| = " + Environment.NewLine);
         sb.Append("				(24136001|hip joint structure|:" + Environment.NewLine);
         sb.Append("					272741003|laterality| = 7771000|left|)," + Environment.NewLine);
         sb.Append("					{363699004|direct device| = 304120007|total hip replacement prosthesis|," + Environment.NewLine);
         sb.Append("					260686004|method| = 425362007|surgical insertion - action|})}");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("111115|albuterol only|:" + Environment.NewLine);
         sb.Append(" 111115|strength magnitude| = #0.083");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("27658006|amoxicillin|:" + Environment.NewLine);
         sb.Append("	411116001|has dose form| = 385049006|capsule|," + Environment.NewLine);
         sb.Append("	{127489000|has active ingredient| = 372687004|amoxicillin|," + Environment.NewLine);
         sb.Append("	111115|has basis of strength| = " + Environment.NewLine);
         sb.Append("		(111115|amoxicillin only|:" + Environment.NewLine);
         sb.Append("			111115|strength magnitude| = #500," + Environment.NewLine);
         sb.Append("			111115|strength unit| = 258684004|mg|)}");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("  322236009|paracetamol 500mg tablet|:" + Environment.NewLine);
         sb.Append("	111115|trade name| = \"PANADOL\"");
         expression = sb.ToString();
         ParseExpression(expression);

         sb = new StringBuilder();
         sb.Append("<<< 322236009|paracetamol 500mg tablet|:" + Environment.NewLine);
         sb.Append("	111115|trade name| = \"PANADOL\"  ");
         expression = sb.ToString();
         ParseExpression(expression);
      }

      private static void ParseExpression(string expression)
      {
         System.Console.ForegroundColor = ConsoleColor.White;
         System.Console.WriteLine("Original expression:");
         System.Console.ForegroundColor = ConsoleColor.Gray;
         System.Console.WriteLine($"{expression}");
         System.Console.WriteLine();

         var f = LexerProgram.ParseSnomed(expression);
         System.Console.ForegroundColor = ConsoleColor.White;
         System.Console.WriteLine("Formatted expression:");
         System.Console.ForegroundColor = ConsoleColor.Yellow;
         System.Console.WriteLine($"{f}");
         System.Console.WriteLine();

         if (f.DefinitionStatus != DefinitionStatus.None)
         {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("Definition status: ");
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine($"{f.DefinitionStatus}");
         }
         foreach (var focusConcept in f.SubExpression.FocusConcepts)
         {
            WriteFocusConcept(focusConcept);
         }

         if (f.SubExpression.Refinement != null)
         {
            WriteRefinement(f.SubExpression.Refinement);
         }
         System.Console.ForegroundColor = ConsoleColor.White;
         System.Console.WriteLine();
      }

      private static void WriteRefinement(IRefinement refinement)
      {
         System.Console.ForegroundColor = ConsoleColor.White;
         System.Console.Write("Refinement: ");
         System.Console.ForegroundColor = ConsoleColor.Cyan;
         System.Console.WriteLine($"{refinement}");

         var attributeGroupRefinement = refinement as AttributeGroupRefinement;
         var attributeSetRefinement = refinement as AttributeSetRefinement;

         if ((attributeGroupRefinement == null) && (attributeSetRefinement == null))
         {
            throw new InvalidOperationException("Unrecognised refinement");
         }

         if (attributeGroupRefinement != null)
         {
            foreach (var attributeGroup in attributeGroupRefinement.AttributeGroups)
            {
               var attributeSet = attributeGroup.AttributeSet;
               ParseAttributeSet(attributeSet);
            }
         }
         else if (attributeSetRefinement != null)
         {
            if (attributeSetRefinement.AttributeGroups != null)
            {
               ParseAttributeGroups(attributeSetRefinement.AttributeGroups);
            }
            ParseAttributeSet(attributeSetRefinement.AttributeSet);
         }
      }

      private static void WriteFocusConcept(FocusConcept focusConcept)
      {
         System.Console.ForegroundColor = ConsoleColor.White;
         System.Console.Write("Focus concept: ");
         System.Console.ForegroundColor = ConsoleColor.Cyan;
         System.Console.Write($"{focusConcept.SctId}");
         if (!string.IsNullOrEmpty(focusConcept.Term))
         {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(", term: ");
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write($"{focusConcept.Term}");
         }
         System.Console.WriteLine();
      }

      private static void ParseAttributeSet(AttributeSet attributeSet)
      {
         foreach (var attribute in attributeSet.Attributes)
         {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write($"{attribute.Name}");
            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine($" {attribute.Value}");

            var expressionValue = attribute.Value as ExpressionValue; // TODO consider NestedExpressionValue
            if (expressionValue == null)
            {
               return;
            }

            foreach (var focusConcept in expressionValue.FocusConcepts)
            {
               WriteFocusConcept(focusConcept);
            }
            if (expressionValue.Refinement != null)
            {
               WriteRefinement(expressionValue.Refinement);
            }
         }
      }

      private static void ParseAttributeGroups(IEnumerable<AttributeGroup> attributeGroups)
      {
         foreach (var attributeGroup in attributeGroups)
         {
            ParseAttributeSet(attributeGroup.AttributeSet);
         }
      }
   }
}