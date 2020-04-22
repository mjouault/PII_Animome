using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using Microsoft.AspNetCore.Authorization;

namespace Animome.Controllers
{
    [Authorize(Roles = "Admin")] //Gestion des paramètre uniquement par un administrateur
    public class ParametresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
