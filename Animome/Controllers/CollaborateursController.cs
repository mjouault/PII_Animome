using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Animome.Data;
using Animome.Models;
using Microsoft.EntityFrameworkCore;

namespace Animome.Controllers
{
    //[Authorize]
    public class CollaborateursController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CollaborateursController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) { _context = context; _userManager = userManager; }
        // GET: /Collaborateurs/

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // 
        // GET: /Collaborateurs/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}