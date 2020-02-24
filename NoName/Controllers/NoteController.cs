using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoName.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
        }

        //NoteBox Main Page
        public IActionResult Index()
        {
            return View();
        }

        //Note Sending Page
        public IActionResult Send()
        {
            return View();
        }

        //Note Received Page
        public IActionResult Received()
        {
            return View();
        }

        //Note Delete Function
        public IActionResult Delete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
