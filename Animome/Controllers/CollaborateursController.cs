using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Animome.Data;

namespace Animome.Controllers
{
    //[Authorize]
    public class CollaborateursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaborateursController(ApplicationDbContext context) { _context = context; }
        // GET: /Collaborateurs/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /Collaborateurs/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}