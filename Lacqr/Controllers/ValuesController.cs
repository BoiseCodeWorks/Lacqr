using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lacqr.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("{randoString}/XZ")]
        public IActionResult XZ(string randoString)
        {
            return View();
        }
    }
}
