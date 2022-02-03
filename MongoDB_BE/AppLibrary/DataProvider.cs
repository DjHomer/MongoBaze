using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using AppLibrary.Models;

namespace AppLibrary
{
   public class DataProvider
    {
        public static void KreirajBusPreduzece(BusPreduzece buspreduzece)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<BusPreduzece>("busPreduzece");
            collection.InsertOne(buspreduzece);
        }


        public static IList<BusPreduzeceDTO> VratiBusPreduzeca()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<BusPreduzece> BusPreduzeca = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => true).ToList<BusPreduzece>();

            IList<BusPreduzeceDTO> BusPreduzeceDTO = new List<BusPreduzeceDTO>();
            foreach (var b in BusPreduzeca)
            {
                BusPreduzeceDTO pom = new BusPreduzeceDTO();
                pom.Id = b.Id.ToString();
                pom.Naziv = b.Naziv;
                pom.GodinaOsnivanja = b.GodinaOsnivanja;
                pom.GradPredstavnistva = b.GradPredstavnistva;

                foreach (var let in b.Voznje)
                {
                    pom.Voznje.Add(let.ToString());
                }

                foreach (var k in b.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }

                BusPreduzeceDTO.Add(pom);
            }

            return BusPreduzeceDTO;
        }

        public static BusPreduzeceDTO VratiBusPreduzece(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            BusPreduzece b = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();


            BusPreduzeceDTO pom = new BusPreduzeceDTO();

            if (b != null)
            {

                pom.Id = b.Id.ToString();
                pom.GodinaOsnivanja = b.GodinaOsnivanja;
                pom.GradPredstavnistva = b.GradPredstavnistva;
                pom.Naziv = b.Naziv;
                foreach (var let in b.Voznje)
                {
                    pom.Voznje.Add(let.ToString());
                }

                foreach (var k in b.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }
            }
            return pom;


        }

        public static void AzurirajBusPreduzece(string id, BusPreduzeceDTOUpdate busPreduzeceDTOUpdate)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<BusPreduzece> busPreduzeceCollection = db.GetCollection<BusPreduzece>("busPreduzece");

            BusPreduzece busPreduzece = busPreduzeceCollection.Find(a => a.Id == new ObjectId(id)).FirstOrDefault();

            if (busPreduzece != null)
            {
                busPreduzece.Naziv = busPreduzeceDTOUpdate.Naziv;
                busPreduzece.GodinaOsnivanja = busPreduzeceDTOUpdate.GodinaOsnivanja;
                busPreduzece.GradPredstavnistva = busPreduzeceDTOUpdate.GradPredstavnistva;

                busPreduzeceCollection.ReplaceOne(x => x.Id == new ObjectId(id), busPreduzece);
            }
        }

        public static void ObrisiBusPreduzece(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<BusPreduzece> busPreduzeceCollection = db.GetCollection<BusPreduzece>("busPreduzece");

            BusPreduzece busPreduzeceZaBrisanje = busPreduzeceCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            if (busPreduzeceZaBrisanje != null)
            {
                foreach (var voznja in busPreduzeceZaBrisanje.Voznje)
                {
                  //  DataProvider.ObrisiVoznju(voznja.ToString());
                }

                busPreduzeceCollection.DeleteOne(a => a.Id == new ObjectId(id));
            }
        }

    }
}
