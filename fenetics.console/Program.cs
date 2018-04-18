using Newtonsoft.Json;
using System;

namespace fenetics.console
{
  class Program
  {
    static void Main(string[] args)
    {
      var results = new fenetics.core.fenetic("Shayne");
      Console.WriteLine(JsonConvert.SerializeObject(results));


      var results2 = new fenetics.core.fenetic("Testing-This!Thing$ Again");
      Console.WriteLine(JsonConvert.SerializeObject(results2));

      Console.ReadLine();
    }
  }
}
