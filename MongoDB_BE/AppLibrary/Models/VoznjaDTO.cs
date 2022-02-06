using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class VoznjaDTO
    {
        public string Id { get; set; }

        public string PolazniGrad { get; set; }

        public string DolazniGrad { get; set; }

        public string TipVoznje { get; set; }

        public DateTime DatumVoznje { get; set; }

        public int BrojSedista { get; set; }

        public int BrojPreostalihSedista { get; set; }

        public IList<string> ListaRezervacija { get; set; }

        public string BusPreduzece { get; set; }

        public VoznjaDTO()
        {
            ListaRezervacija = new List<string>();
        }
    }
}
