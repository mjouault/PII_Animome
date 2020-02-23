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
        private readonly RoleManager<IdentityRole> _roleManager;
        public CollaborateursController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
        { _context = context;
            _userManager = userManager; 
            _roleManager = roleManager; 
        }
        // GET: /Collaborateurs/

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
                //.Include(user=>user.LesDomaineUsers)
                //.ThenInclude(lesDomainesUsers=>lesDomainesUsers.Domaine).ToListAsync());
        }

        // 
        // GET: /Collaborateurs/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        /// <summary>
        /// Acceptation d'un collaborateur
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Administrateur")]
        [HttpPost]
        public IActionResult AccepterCollaborateur()
        {
            return View();
        }
    }
}