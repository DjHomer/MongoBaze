using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class BusPreduzece
    {

        public ObjectId Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string GradPredstavnistva { get; set; }

        public IList<ObjectId> Voznje { get; set; }

        public IList<ObjectId> Komentari { get; set; }

        public BusPreduzece()
        {
            Voznje = new List<ObjectId>();
            Komentari = new List<ObjectId>();
        }
    }
}
