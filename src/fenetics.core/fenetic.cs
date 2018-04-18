using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace fenetics.core
{
  public class fenetic
  {
    public string Word { get; set; }
    public string[] Soundex { get; set; }
    public string[] Metaphone { get; set; }
    public string[] DoubleMetaphone { get; set; }
    public string[] Caverphone { get; set; }
    public string[] MatchRatingApproach { get; set; }
    public string[] Variations { get; set; }
    public string Cleansed { get; set; }

    public fenetic(string word)
    {
      Word = word;
      GetKeys();
    }

    private void GetKeys()
    {
      Soundex = new Phonix.Soundex().BuildKeys(Word);
      Metaphone = new Phonix.Metaphone().BuildKeys(Word);
      DoubleMetaphone = new Phonix.DoubleMetaphone().BuildKeys(Word);
      Caverphone = new Phonix.CaverPhone().BuildKeys(Word);
      MatchRatingApproach = new Phonix.MatchRatingApproach().BuildKeys(Word);
      Cleansed = CleanWord();
    }

    private string CleanWord()
    {
      var result = Word;
      var special = " ~`!@#$%^&*()_-+=|\\}]{[':;?/>.<,\"";
      for (int i = 0; i < special.Length; i++)
      {
        result = result.Replace(special.Substring(i, 1), "");
      }
      return result.ToUpper();
    }

    private static readonly string[] unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
    private static readonly string[] tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    private static Dictionary<string, long> numberTable =
    new Dictionary<string, long>
        {{"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},
        {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9},
        {"ten",10},{"eleven",11},{"twelve",12},{"thirteen",13},
        {"fourteen",14},{"fifteen",15},{"sixteen",16},
        {"seventeen",17},{"eighteen",18},{"nineteen",19},{"twenty",20},
        {"thirty",30},{"forty",40},{"fifty",50},{"sixty",60},
        {"seventy",70},{"eighty",80},{"ninety",90},{"hundred",100},
        {"thousand",1000},{"million",1000000},{"billion",1000000000},
        {"trillion",1000000000000},{"quadrillion",1000000000000000},
        {"quintillion",1000000000000000000}};

    public static string NumberToWords(long number)
    {
      if (number == 0)
        return "zero";

      if (number < 0)
        return "minus " + NumberToWords(Math.Abs(number));

      string words = "";

      if ((number / 1000000000000000000) > 0)
      {
        words += NumberToWords(number / 1000000000000000000) + " quintillion ";
        number %= 1000000000000000000;
      }

      if ((number / 1000000000000000) > 0)
      {
        words += NumberToWords(number / 1000000000000000) + " quadrillion ";
        number %= 1000000000000000;
      }

      if ((number / 1000000000000) > 0)
      {
        words += NumberToWords(number / 1000000000000) + " trillion ";
        number %= 1000000000000;
      }

      if ((number / 1000000000) > 0)
      {
        words += NumberToWords(number / 1000000000) + " billion ";
        number %= 1000000000;
      }

      if ((number / 1000000) > 0)
      {
        words += NumberToWords(number / 1000000) + " million ";
        number %= 1000000;
      }

      if ((number / 1000) > 0)
      {
        words += NumberToWords(number / 1000) + " thousand ";
        number %= 1000;
      }

      if ((number / 100) > 0)
      {
        words += NumberToWords(number / 100) + " hundred ";
        number %= 100;
      }

      if (number > 0)
      {
        if (words != "")
          words += "and ";

        if (number < 20)
          words += unitsMap[number];
        else
        {
          words += tensMap[number / 10];
          if ((number % 10) > 0)
            words += "-" + unitsMap[number % 10];
        }
      }

      return words.Replace("  ", " ");
    }

    public static long ToLong(string numberString)
    {
      var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
           .Select(m => m.Value.ToLowerInvariant())
           .Where(v => numberTable.ContainsKey(v))
           .Select(v => numberTable[v]);
      long acc = 0, total = 0L;
      foreach (var n in numbers)
      {
        if (n >= 1000)
        {
          total += (acc * n);
          acc = 0;
        }
        else if (n >= 100)
        {
          acc *= n;
        }
        else acc += n;
      }
      return (total + acc) * (numberString.StartsWith("minus",
            StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
    }
  }
}
