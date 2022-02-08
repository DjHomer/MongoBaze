using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class Voznja
    {
        public ObjectId Id { get; set; }

        public string PolazniGrad { get; set; }

        public string DolazniGrad { get; set; }

        public float CenaVoznje { get; set; }

        public string TipVoznje { get; set; }

        public DateTime DatumVoznje { get; set; }

        public int BrojSedista { get; set; }

        public int BrojPreostalihSedista { get; set; }

        public IList<ObjectId> ListaRezervacija { get; set; }

        public ObjectId BusPreduzece { get; set; }

        public Voznja()
        {
            ListaRezervacija = new List<ObjectId>();
        }
    }
}
