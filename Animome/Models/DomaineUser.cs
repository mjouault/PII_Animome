﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class DomaineUser
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Domaine Domaine {get;set;}
    }
}