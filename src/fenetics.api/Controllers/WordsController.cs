using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fenetics.core;
using Microsoft.AspNetCore.Mvc;

namespace fenetics.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WordsController : ControllerBase
  {

    [HttpGet]
    public ActionResult<string> Get()
    {
      return "hello";
    }

    // GET api/words/string
    [HttpGet("{word}")]
    public ActionResult<fenetic> Get(string word)
    {
      return new fenetic(word);
    }

  }
}
