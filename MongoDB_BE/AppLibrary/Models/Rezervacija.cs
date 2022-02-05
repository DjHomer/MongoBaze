using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Rezervacija
    {
        public ObjectId Id { get; set; }
        public string Sifra_Rezervacije { get; set; }
        public ObjectId Putnik { get; set; }
    }
}
