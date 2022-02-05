﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class PutnikDTO
    {
        public ObjectId Id { get; set; }
        public String Jmbg { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public String Email { get; set; }
        public String Broj_Telefona { get; set; }
        public List<Rezervacija> Rezervacije { get; set; }


    }
}
