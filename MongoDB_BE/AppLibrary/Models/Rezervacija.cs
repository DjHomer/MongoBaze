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
        public int BrSedista { get; set; }
        public byte[] Legitimacija { get; set; }
        public byte[] Covid19Test { get; set; }
        public string Status { get; set; }
        public string Sifra_Rezervacije { get; set; }
        public float Cena { get; set; }
        public IList<ObjectId> Niz_Usluga { get; set; }
        public ObjectId Putnik { get; set; }
        public ObjectId Voznja { get; set; }
        public ObjectId Prtljag { get; set; }

        public static explicit operator Rezervacija(ObjectId o)
        {
            throw new NotImplementedException();
        }
    }
}
