using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Animome.Models;
using Microsoft.AspNetCore.Authorization;
using Animome.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Animome.ViewModels;

namespace Animome.Controllers
{
    public class ParametresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
