using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class PrtljagDTO
    {
        public string  Id { get; set; }
        public int Kolicina { get; set; }
        public bool PostojiPrtljag { get; set; }

        public string Rezervacija { get; set; }
    }
}
