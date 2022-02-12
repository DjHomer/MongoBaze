using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class RezervacijaDTO
    {
        public ObjectId Id { get; set; }
        public int BrSedista { get; set; }
        public string Legitimacija { get; set; }
        public string Covid19Test{ get; set; }
        public string Status { get; set; }
        public string Sifra_Rezervacije { get; set; }
        public float Cena { get; set; }
        public IList<ObjectId> Niz_Usluga { get; set; }
        public IList<string> Usluge { get; set; }
        public string Putnik { get; set; }
        public string Voznja { get; set; }
        public string Prtljag { get; set; }

        public int KolicinaPrt { get; set; }
        public bool PostojiPrt { get; set; }
    }
}
