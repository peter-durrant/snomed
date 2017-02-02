namespace HDD.Snomed
{
   public static class StringHelper
   {
      public static string RemoveTermDelimiter(this string term)
      {
         term = term.Trim();
         if (term.StartsWith("|") && term.EndsWith("|") && (term.Length > 1))
         {
            term = term.Substring(1, term.Length - 2);
            term = term.Trim();
         }
         return term;
      }
   }
}