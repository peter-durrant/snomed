using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDD.Snomed.Test
{
   [TestClass]
   public class ExpressionTests
   {
      [TestMethod]
      public void Expression_SimpleWithTerm()
      {
         // 6.2 Simple Expression
         const string expressionStr = "73211009|diabetes mellitus|";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression = new Expression(new FocusConcept(73211009, "diabetes mellitus"));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_SimpleWithoutTerm()
      {
         // 6.2 Simple Expression
         const string expressionStr = "73211009";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression = new Expression(new FocusConcept(73211009));
         Assert.AreEqual("73211009", expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_MultipleFocusConcepts()
      {
         // 6.3 Multiple Focus Concepts
         const string expressionStr = "421720008|spray dose form| + 7946007|drug suspension|";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression = new Expression(new List<FocusConcept>
         {
            new FocusConcept(421720008, "spray dose form"),
            new FocusConcept(7946007, "drug suspension")
         });
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_MultipleFocusConceptsOneTerm()
      {
         // 6.3 Multiple Focus Concepts
         const string expressionStr = "421720008 + 7946007|drug suspension|";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression = new Expression(new List<FocusConcept>
         {
            new FocusConcept(421720008),
            new FocusConcept(7946007, "drug suspension")
         });
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_SingleQualifyingAttribute()
      {
         // 6.4 Expressions with Refinements
         var expressionStr = "83152002|oophorectomy|:" + Environment.NewLine +
                             "	405815000|procedure device| = 122456005|laser device|";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(83152002, "oophorectomy"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(405815000, "procedure device",
                        ExpressionValue.AsFocusConcept(122456005, "laser device")))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_SingleQualifyingAttributeAsList()
      {
         // 6.4 Expressions with Refinements
         var expressionStr = "182201002|hip joint|:" + Environment.NewLine + "	272741003|laterality| = 24028007|right|";
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(182201002, "hip joint"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(272741003, "laterality",
                           ExpressionValue.AsFocusConcept(24028007, "right"))
                     })));

         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_MultipleQualifyingAttributes()
      {
         // 6.4 Expressions with Refinements
         var sb = new StringBuilder();
         sb.Append("71388002|procedure|:" + Environment.NewLine);
         sb.Append("	405815000|procedure device| = 122456005|laser device|," + Environment.NewLine);
         sb.Append("	260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 15497006|ovarian structure|");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(71388002, "procedure"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(405815000, "procedure device",
                           ExpressionValue.AsFocusConcept(122456005, "laser device")),
                        new Attribute(260686004, "method",
                           ExpressionValue.AsFocusConcept(129304002, "excision - action")),
                        new Attribute(405813007, "procedure site - direct",
                           ExpressionValue.AsFocusConcept(15497006, "ovarian structure"))
                     })));

         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("65801008|excision|:" + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 66754008|appendix structure|," + Environment.NewLine);
         sb.Append("	260870009|priority| = 25876001|emergency|");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(65801008, "excision"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(405813007, "procedure site - direct",
                           ExpressionValue.AsFocusConcept(66754008, "appendix structure")),
                        new Attribute(260870009, "priority",
                           ExpressionValue.AsFocusConcept(25876001, "emergency"))
                     })));

         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("313056006|epiphysis of ulna|:" + Environment.NewLine);
         sb.Append("	272741003|laterality| = 7771000|left|");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(313056006, "epiphysis of ulna"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(272741003, "laterality",
                        ExpressionValue.AsFocusConcept(7771000, "left")))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_MultipleFocusConceptsMultipleQualifyingAttributes()
      {
         // 6.4 Expressions with Refinements
         var sb = new StringBuilder();
         sb.Append("119189000|ulna part| + 312845000|epiphysis of upper limb|:" + Environment.NewLine);
         sb.Append("	272741003|laterality| = 7771000|left|");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new List<FocusConcept>
               {
                  new FocusConcept(119189000, "ulna part"),
                  new FocusConcept(312845000, "epiphysis of upper limb")
               },
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(272741003, "laterality",
                        ExpressionValue.AsFocusConcept(7771000, "left")))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_AttributeGroups()
      {
         // 6.5 Expressions with Attribute Groups
         var sb = new StringBuilder();
         sb.Append("71388002|procedure|:" + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 15497006|ovarian structure|}," + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 31435000|fallopian tube structure|}");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(71388002, "procedure"),
               new AttributeGroupRefinement(
                  new List<AttributeGroup>
                  {
                     new AttributeGroup(
                        new AttributeSet(
                           new List<IAttribute>
                           {
                              new Attribute(260686004, "method",
                                 ExpressionValue.AsFocusConcept(129304002, "excision - action")),
                              new Attribute(405813007, "procedure site - direct",
                                 ExpressionValue.AsFocusConcept(15497006, "ovarian structure"))
                           })),
                     new AttributeGroup(
                        new AttributeSet(
                           new List<IAttribute>
                           {
                              new Attribute(260686004, "method",
                                 ExpressionValue.AsFocusConcept(129304002, "excision - action")),
                              new Attribute(405813007, "procedure site - direct",
                                 ExpressionValue.AsFocusConcept(31435000, "fallopian tube structure"))
                           }))
                  }));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("71388002|procedure|:" + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 20837000|structure of right ovary|," + Environment.NewLine);
         sb.Append("	424226004|using device| = 122456005|laser device|}," + Environment.NewLine);
         sb.Append("	{260686004|method| = 261519002|diathermy excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 113293009|structure of left fallopian tube|}");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(71388002, "procedure"),
               new AttributeGroupRefinement(
                  new List<AttributeGroup>
                  {
                     new AttributeGroup(
                        new AttributeSet(
                           new List<IAttribute>
                           {
                              new Attribute(260686004, "method",
                                 ExpressionValue.AsFocusConcept(129304002, "excision - action")),
                              new Attribute(405813007, "procedure site - direct",
                                 ExpressionValue.AsFocusConcept(20837000, "structure of right ovary")),
                              new Attribute(424226004, "using device",
                                 ExpressionValue.AsFocusConcept(122456005, "laser device"))
                           })),
                     new AttributeGroup(
                        new AttributeSet(
                           new List<IAttribute>
                           {
                              new Attribute(260686004, "method",
                                 ExpressionValue.AsFocusConcept(261519002, "diathermy excision - action")),
                              new Attribute(405813007, "procedure site - direct",
                                 ExpressionValue.AsFocusConcept(113293009, "structure of left fallopian tube"))
                           }))
                  }));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("71388002|procedure|:" + Environment.NewLine);
         sb.Append("	{260686004|method| = 129304002|excision - action|," + Environment.NewLine);
         sb.Append("	405813007|procedure site - direct| = 15497006|ovarian structure|}");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(71388002, "procedure"),
               new AttributeGroupRefinement(
                  new List<AttributeGroup>
                  {
                     new AttributeGroup(
                        new AttributeSet(
                           new List<IAttribute>
                           {
                              new Attribute(260686004, "method",
                                 ExpressionValue.AsFocusConcept(129304002, "excision - action")),
                              new Attribute(405813007, "procedure site - direct",
                                 ExpressionValue.AsFocusConcept(15497006, "ovarian structure"))
                           }))
                  }));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_NestedRefinements()
      {
         // 6.6 Expressions with Nested Refinements
         var sb = new StringBuilder();
         sb.Append("373873005|pharmaceutical / biologic product|:" + Environment.NewLine);
         sb.Append("	411116001|has dose form| = " + Environment.NewLine);
         sb.Append("		(421720008|spray dose form| + 7946007|drug suspension|)");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(373873005, "pharmaceutical / biologic product"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(411116001, "has dose form",
                           new NestedExpressionValue(
                              new List<FocusConcept>
                              {
                                 new FocusConcept(421720008, "spray dose form"),
                                 new FocusConcept(7946007, "drug suspension")
                              }))
                     })));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("397956004|prosthetic arthroplasty of the hip|:" + Environment.NewLine);
         sb.Append("	363704007|procedure site| = " + Environment.NewLine);
         sb.Append("		(24136001|hip joint structure|:" + Environment.NewLine);
         sb.Append("			272741003|laterality| = 7771000|left|)");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(397956004, "prosthetic arthroplasty of the hip"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(363704007, "procedure site",
                           new NestedExpressionValue(
                              new SubExpression(
                                 new FocusConcept(24136001, "hip joint structure"),
                                 new AttributeSetRefinement(
                                    new AttributeSet(
                                       new List<IAttribute>
                                       {
                                          new Attribute(272741003, "laterality",
                                             ExpressionValue.AsFocusConcept(7771000, "left"))
                                       })))))
                     })));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_NestedRefinementsWithContextualWrapper()
      {
         // 6.6 Expressions with Nested Refinements
         var sb = new StringBuilder();
         sb.Append("397956004|prosthetic arthroplasty of the hip|:" + Environment.NewLine);
         sb.Append("	405814001|procedure site - indirect| = " + Environment.NewLine);
         sb.Append("		(24136001|hip joint structure|:" + Environment.NewLine);
         sb.Append("			272741003|laterality| = 7771000|left|)," + Environment.NewLine);
         sb.Append("			{363699004|direct device| = 304120007|total hip replacement prosthesis|," + Environment.NewLine);
         sb.Append("			260686004|method| = 425362007|surgical insertion - action|}");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(397956004, "prosthetic arthroplasty of the hip"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(405814001, "procedure site - indirect",
                           new NestedExpressionValue(
                              new SubExpression(
                                 new FocusConcept(24136001, "hip joint structure"),
                                 new AttributeSetRefinement(
                                    new AttributeSet(
                                       new List<IAttribute>
                                       {
                                          new Attribute(272741003, "laterality",
                                             ExpressionValue.AsFocusConcept(7771000, "left"))
                                       })))))
                     }),
                  new AttributeGroup(
                     new AttributeSet(
                        new List<IAttribute>
                        {
                           new Attribute(363699004, "direct device",
                              ExpressionValue.AsFocusConcept(304120007, "total hip replacement prosthesis")),
                           new Attribute(260686004, "method",
                              ExpressionValue.AsFocusConcept(425362007, "surgical insertion - action"))
                        }))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

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
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var nestedExpression = expression;
         expression =
            new Expression(
               new FocusConcept(243796009, "situation with explicit context"),
               new AttributeGroupRefinement(
                  new AttributeGroup(
                     new AttributeSet(
                        new List<IAttribute>
                        {
                           new Attribute(408730004, "procedure context",
                              ExpressionValue.AsFocusConcept(385658003, "done")),
                           new Attribute(408731000, "temporal context",
                              ExpressionValue.AsFocusConcept(410512000, "current or specified")),
                           new Attribute(408732007, "subject relationship context",
                              ExpressionValue.AsFocusConcept(410604004, "subject of record")),
                           new Attribute(363589002, "associated procedure",
                              new NestedExpressionValue(nestedExpression.SubExpression))
                        })
                  )
               ));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_ConcreteValues()
      {
         // 6.7 Expressions with Concrete Values
         var sb = new StringBuilder();
         sb.Append("27658006|amoxicillin|:" + Environment.NewLine);
         sb.Append("	411116001|has dose form| = 385049006|capsule|," + Environment.NewLine);
         sb.Append("	{127489000|has active ingredient| = 372687004|amoxicillin|," + Environment.NewLine);
         sb.Append("	111115|has basis of strength| = " + Environment.NewLine);
         sb.Append("		(111115|amoxicillin only|:" + Environment.NewLine);
         sb.Append("			111115|strength magnitude| = #500," + Environment.NewLine);
         sb.Append("			111115|strength unit| = 258684004|mg|)}");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(27658006, "amoxicillin"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(411116001, "has dose form",
                        ExpressionValue.AsFocusConcept(385049006, "capsule"))),
                  new AttributeGroup(
                     new AttributeSet(
                        new List<IAttribute>
                        {
                           new Attribute(127489000, "has active ingredient",
                              ExpressionValue.AsFocusConcept(372687004, "amoxicillin")),
                           new Attribute(111115, "has basis of strength",
                              new NestedExpressionValue(
                                 new SubExpression(
                                    new FocusConcept(111115, "amoxicillin only"),
                                    new AttributeSetRefinement(
                                       new AttributeSet(
                                          new List<IAttribute>
                                          {
                                             new Attribute(111115, "strength magnitude",
                                                new DecimalValue(500)),
                                             new Attribute(111115, "strength unit",
                                                ExpressionValue.AsFocusConcept(258684004, "mg"))
                                          })))))
                        }))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("91143003|albuterol|:" + Environment.NewLine);
         sb.Append("	411116001|has dose form| = 385023001|oral solution|," + Environment.NewLine);
         sb.Append("	{127489000|has active ingredient| = 372897005|albuterol|," + Environment.NewLine);
         sb.Append("	111115|has basis of strength| = " + Environment.NewLine);
         sb.Append("		(111115|albuterol only|:" + Environment.NewLine);
         sb.Append("			111115|strength magnitude| = #0.083," + Environment.NewLine);
         sb.Append("			111115|strength unit| = 118582008|%|)}");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(91143003, "albuterol"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(411116001, "has dose form",
                        ExpressionValue.AsFocusConcept(385023001, "oral solution"))),
                  new AttributeGroup(
                     new AttributeSet(
                        new List<IAttribute>
                        {
                           new Attribute(127489000, "has active ingredient",
                              ExpressionValue.AsFocusConcept(372897005, "albuterol")),
                           new Attribute(111115, "has basis of strength",
                              new NestedExpressionValue(
                                 new SubExpression(
                                    new FocusConcept(111115, "albuterol only"),
                                    new AttributeSetRefinement(
                                       new AttributeSet(
                                          new List<IAttribute>
                                          {
                                             new Attribute(111115, "strength magnitude",
                                                new DecimalValue(0.083M)),
                                             new Attribute(111115, "strength unit",
                                                ExpressionValue.AsFocusConcept(118582008, "%"))
                                          })))))
                        }))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("322236009|paracetamol 500mg tablet|:" + Environment.NewLine);
         sb.Append("	111115|trade name| = \"PANADOL\"");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(322236009, "paracetamol 500mg tablet"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(111115, "trade name",
                        new StringValue("PANADOL")))));
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_DefinitionStatus()
      {
         // 6.8 Expressions with a Definition Status
         var sb = new StringBuilder();
         sb.Append("=== 46866001|fracture of lower limb| + 428881005|injury of tibia|:" + Environment.NewLine);
         sb.Append("	116676008|associated morphology| = 72704001|fracture|," + Environment.NewLine);
         sb.Append("	363698007|finding site| = 12611008|bone structure of tibia|");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new List<FocusConcept>
               {
                  new FocusConcept(46866001, "fracture of lower limb"),
                  new FocusConcept(428881005, "injury of tibia")
               },
               new AttributeSetRefinement(
                  new AttributeSet(
                     new List<IAttribute>
                     {
                        new Attribute(116676008, "associated morphology",
                           ExpressionValue.AsFocusConcept(72704001, "fracture")),
                        new Attribute(363698007, "finding site",
                           ExpressionValue.AsFocusConcept(12611008, "bone structure of tibia"))
                     })),
               DefinitionStatus.EquivalentTo);
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());

         sb = new StringBuilder();
         sb.Append("<<< 73211009|diabetes mellitus|:" + Environment.NewLine);
         sb.Append("	363698007|finding site| = 113331007|endocrine system|");
         expressionStr = sb.ToString();
         parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         expression =
            new Expression(
               new FocusConcept(73211009, "diabetes mellitus"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(363698007, "finding site",
                        ExpressionValue.AsFocusConcept(113331007, "endocrine system")))),
               DefinitionStatus.SubTypeOf);
         Assert.AreEqual(expressionStr, expression.ToString());
         Assert.AreEqual(parsedExpression.ToString(), expression.ToString());
         Console.WriteLine(expression.ToString());
      }

      [TestMethod]
      public void Expression_WithValidWhiteSpace_ParseSuccess()
      {
         var formattedSb = new StringBuilder();
         formattedSb.Append("=== 91143003|albuterol|:" + Environment.NewLine);
         formattedSb.Append("	411116001|has dose form| = 385023001|oral solution|," + Environment.NewLine);
         formattedSb.Append("	{127489000|has active ingredient| = 372897005|albuterol|," + Environment.NewLine);
         formattedSb.Append("	111115|has basis of strength| = " + Environment.NewLine);
         formattedSb.Append("		(111115|albuterol only|:" + Environment.NewLine);
         formattedSb.Append("			111115|strength magnitude| = #0.083," + Environment.NewLine);
         formattedSb.Append("			111115|strength unit| = 118582008|%|)}");
         var formattedExpressionStr = formattedSb.ToString();
         var parsedFormattedExpression = LexerProgram.ParseSnomed(formattedExpressionStr);

         var sb = new StringBuilder();
         sb.Append("   === 91143003   | albuterol  | : " + Environment.NewLine);
         sb.Append("	411116001 | has dose form   |=385023001   | oral solution |," + Environment.NewLine);
         sb.Append("	{   127489000  |   has active ingredient| =372897005|albuterol   |  ,   " + Environment.NewLine);
         sb.Append("	111115   |has basis of strength  | = " + Environment.NewLine);
         sb.Append("		(  111115  | albuterol only|:" + Environment.NewLine);
         sb.Append("			111115 | strength magnitude| = #0.083  , " + Environment.NewLine);
         sb.Append("			111115|   strength unit| = 118582008|  %  |)}");
         var expressionStr = sb.ToString();
         var parsedExpression = LexerProgram.ParseSnomed(expressionStr);

         var expression =
            new Expression(
               new FocusConcept(91143003, "albuterol"),
               new AttributeSetRefinement(
                  new AttributeSet(
                     new Attribute(411116001, "has dose form",
                        ExpressionValue.AsFocusConcept(385023001, "oral solution"))),
                  new AttributeGroup(
                     new AttributeSet(
                        new List<IAttribute>
                        {
                           new Attribute(127489000, "has active ingredient",
                              ExpressionValue.AsFocusConcept(372897005, "albuterol")),
                           new Attribute(111115, "has basis of strength",
                              new NestedExpressionValue(
                                 new SubExpression(
                                    new FocusConcept(111115, "albuterol only"),
                                    new AttributeSetRefinement(
                                       new AttributeSet(
                                          new List<IAttribute>
                                          {
                                             new Attribute(111115, "strength magnitude",
                                                new DecimalValue(0.083M)),
                                             new Attribute(111115, "strength unit",
                                                ExpressionValue.AsFocusConcept(118582008, "%"))
                                          })))))
                        }))),
               DefinitionStatus.EquivalentTo);
         Assert.AreEqual(formattedExpressionStr, expression.ToString());
         Assert.AreEqual(parsedFormattedExpression.ToString(), expression.ToString());
         Assert.AreEqual(parsedFormattedExpression.ToString(), parsedExpression.ToString());
         Console.WriteLine(expression.ToString());
      }
   }
}