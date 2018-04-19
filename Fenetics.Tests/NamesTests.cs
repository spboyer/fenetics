using System;
using fenetics.core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fenetics.Tests
{
  [TestClass]
  public class NamesTests
  {

    private fenetic _fenetic;

    public NamesTests()
    {
    }

    [TestMethod]
    public void Fenetic_WordPropertyGetsSet()
    {
      _fenetic = new fenetic("BOYER");
      Assert.AreEqual("BOYER", _fenetic.Word);
    }

    [TestMethod]
    public void Fenetic_ReturnsObject()
    {
      _fenetic = new fenetic("BOYER");
      Assert.IsInstanceOfType(_fenetic, typeof(fenetic));
    }

    [TestMethod]
    public void Soundex_ShouldReturnExpectedValue()
    {
      _fenetic = new fenetic("BOYER");
      Assert.IsTrue(Array.IndexOf<string>(_fenetic.Soundex, "B600") >= 0);
    }

    [TestMethod]
    public void CleanWord_ShouldRemoveSpecialChars()
    {
      var word = "Testing-This!Thing$ Again";
      var expected = "TESTINGTHISTHING AGAIN";
      _fenetic = new fenetic(word);

      Assert.IsTrue(string.Equals(_fenetic.Cleansed, expected, StringComparison.CurrentCultureIgnoreCase));
    }

    [DataTestMethod]
    [DataRow("One Thousand Eight Hundred Eighty Five")]
    [DataRow("Fifty")]
    [DataRow("Nine Hundred")]
    [DataRow("Zero")]
    public void ConvertsStringsToNumbers(string number)
    {
      _fenetic = new fenetic(number);

      var val = _fenetic.ToLong(number);
      long o;

      Assert.IsTrue(long.TryParse(val.ToString(), out o));

    }

    [DataTestMethod]
    [DataRow(11000, "eleven thousand")]
    [DataRow(1, "one")]
    [DataRow(352, "three hundred and fifty-two")]
    [DataRow(1885, "one thousand eight hundred and eighty-five")]
    [DataRow(900, "nine hundred")]
    public void ConvertsNumbersToStrings(long number, string value)
    {
      _fenetic = new fenetic(value);

      Assert.AreEqual(value, _fenetic.NumberToWords(number));
    }

  }
}
