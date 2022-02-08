using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
   public class BusPreduzeceDTO
    {
        public string Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string GradPredstavnistva { get; set; }

        public IList<string> Voznje { get; set; }

        public IList<string> Komentari { get; set; }

        public BusPreduzeceDTO()
        {
            Voznje = new List<string>();
            Komentari = new List<string>();
        }

    }
}
