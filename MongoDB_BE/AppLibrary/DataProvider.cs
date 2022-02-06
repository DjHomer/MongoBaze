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
        #region BusPreduzece
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

        public static void DodajBusPreduzecuVoznju(ObjectId voznjaId, ObjectId busPreduzeceId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            IMongoCollection<BusPreduzece> busPreduzeceCollection = db.GetCollection<BusPreduzece>("busPreduzece");
            BusPreduzece busPreduzece = busPreduzeceCollection.Find(x => x.Id == busPreduzeceId).FirstOrDefault();
            busPreduzece.Voznje.Add(voznjaId);
            busPreduzeceCollection.ReplaceOne(x => x.Id == busPreduzeceId, busPreduzece);
        }

        public static void ObrisiBusPreduzecuVoznju(ObjectId voznjaId, ObjectId busPreduzeceId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            IMongoCollection<BusPreduzece> busPreduzeceCollection = db.GetCollection<BusPreduzece>("busPreduzece");
            BusPreduzece busPreduzece = busPreduzeceCollection.Find(x => x.Id == busPreduzeceId).FirstOrDefault();
            busPreduzece.Voznje.Remove(voznjaId);
            busPreduzeceCollection.ReplaceOne(x => x.Id == busPreduzeceId, busPreduzece);
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
                    DataProvider.ObrisiVoznju(voznja.ToString());
                }

                busPreduzeceCollection.DeleteOne(a => a.Id == new ObjectId(id));
            }
        }
        #endregion

        #region Rezervacija

        public static void KreirajRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
        }

        public static void DodajUsluge(string id, string[] rezervacije)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");

            IList<ObjectId> retList = new List<ObjectId>();
            foreach (String s in rezervacije)
            {
                retList.Add(new ObjectId(s));
            }

            var filter = Builders<Rezervacija>.Filter.Eq(x => x.Id, new ObjectId(id));
            var update = Builders<Rezervacija>.Update.Set(x => x.Niz_Usluga, retList);
            collection.UpdateOne(filter, update);
        }

        public static ObjectId KreirajRezervaciju(Rezervacija rez)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
            collection.InsertOne(rez);

            return rez.Id;
        }

        public static Rezervacija VratiRezervaciju(string sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => x.Sifra_Rezervacije == sifra).FirstOrDefault();
        }

        public static RezervacijaDTO VratiRezervacijuId(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Rezervacija rez = db.GetCollection<Rezervacija>("rezervacije").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            IList<string> niz_usluga = new List<string>();
            foreach (ObjectId idProz in rez.Niz_Usluga)
            {
                niz_usluga.Add(DataProvider.VratiUslugu(idProz.ToString()).Naziv);
            }

            RezervacijaDTO rezDTO = new RezervacijaDTO()
            {
                Id = rez.Id,
                Status = rez.Status,
                Sifra_Rezervacije = rez.Sifra_Rezervacije,
                Cena = rez.Cena,
                Usluge = niz_usluga
            };
            return rezDTO;
        }

        public static IList<Rezervacija> VratiRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => true).ToList<Rezervacija>();
        }

        public static void ObrisiRezervaciju(String sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Rezervacija>("rezervacije").DeleteOne(x => x.Sifra_Rezervacije == sifra);
        }

        public static void AzurirajRezervaciju(String sifra, String status)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Rezervacija>.Filter.Eq(x => x.Sifra_Rezervacije, sifra);
            var update = Builders<Rezervacija>.Update.Set(x => x.Status, status);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter, update);
        }

        #endregion


        #region Voznja

        public static void KreirajVoznju(Voznja voznja)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var voznje = db.GetCollection<Voznja>("voznja");
            voznje.InsertOne(voznja);

            if (voznja.BusPreduzece != ObjectId.Empty)
            {
                DodajBusPreduzecuVoznju(voznja.Id, voznja.BusPreduzece);
            }
        }

        public static IList<VoznjaDTO> VratiSveVoznje()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Voznja> voznje = db.GetCollection<Voznja>("voznja").Find(x => true).ToList<Voznja>();
            IList<VoznjaDTO> voznjeDTO = new List<VoznjaDTO>();

            foreach (var v in voznje)
            {
                VoznjaDTO pom = new VoznjaDTO();
                pom.Id = v.Id.ToString();
                pom.PolazniGrad = v.PolazniGrad;
                pom.DolazniGrad = v.DolazniGrad;
                pom.DatumVoznje = v.DatumVoznje;
                pom.BrojSedista = v.BrojSedista;
                pom.BrojPreostalihSedista = v.BrojPreostalihSedista;
                pom.TipVoznje = v.TipVoznje;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in v.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.BusPreduzece.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.BusPreduzece = "";
                //}
               // else
                //{
                    BusPreduzece busPreduzece = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => x.Id == v.BusPreduzece).FirstOrDefault();
                    if (busPreduzece != null)
                    {
                        pom.BusPreduzece = busPreduzece.Id.ToString();
                    }
                //}

                voznjeDTO.Add(pom);
            }

            return voznjeDTO;
        }


        public static IList<VoznjaDTO> VratiSveGotoveVoznje()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Voznja> voznje = db.GetCollection<Voznja>("voznja").Find(x => (x.DatumVoznje < DateTime.Now)).ToList<Voznja>();
            IList<VoznjaDTO> voznjeDTO = new List<VoznjaDTO>();

            foreach (var v in voznje)
            {
                VoznjaDTO pom = new VoznjaDTO();
                pom.Id = v.Id.ToString();
                pom.PolazniGrad = v.PolazniGrad;
                pom.DolazniGrad = v.DolazniGrad;
                pom.DatumVoznje = v.DatumVoznje;
                pom.BrojSedista = v.BrojSedista;
                pom.BrojPreostalihSedista = v.BrojPreostalihSedista;
                pom.TipVoznje = v.TipVoznje;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in v.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.BusPreduzece.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.BusPreduzece = "";
                //}
                // else
                //{
                BusPreduzece busPreduzece = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => x.Id == v.BusPreduzece).FirstOrDefault();
                if (busPreduzece != null)
                {
                    pom.BusPreduzece = busPreduzece.Id.ToString();
                }
                //}

                voznjeDTO.Add(pom);
            }

            return voznjeDTO;
        }

        public static IList<VoznjaDTO> VratiSveAktivneVoznje()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Voznja> voznje = db.GetCollection<Voznja>("voznja").Find(x => (x.DatumVoznje > DateTime.Now)).ToList<Voznja>();
            IList<VoznjaDTO> voznjeDTO = new List<VoznjaDTO>();

            foreach (var v in voznje)
            {
                VoznjaDTO pom = new VoznjaDTO();
                pom.Id = v.Id.ToString();
                pom.PolazniGrad = v.PolazniGrad;
                pom.DolazniGrad = v.DolazniGrad;
                pom.DatumVoznje = v.DatumVoznje;
                pom.BrojSedista = v.BrojSedista;
                pom.BrojPreostalihSedista = v.BrojPreostalihSedista;
                pom.TipVoznje = v.TipVoznje;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in v.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.BusPreduzece.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.BusPreduzece = "";
                //}
                // else
                //{
                BusPreduzece busPreduzece = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => x.Id == v.BusPreduzece).FirstOrDefault();
                if (busPreduzece != null)
                {
                    pom.BusPreduzece = busPreduzece.Id.ToString();
                }
                //}

                voznjeDTO.Add(pom);
            }

            return voznjeDTO;
        }

        public static IList<Voznja> VratiSveVoznjeObjectId()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Voznja> voznje = db.GetCollection<Voznja>("voznja").Find(x => true).ToList<Voznja>();
            return voznje;
        }
        public static VoznjaDTO VratiVoznju(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Voznja v = db.GetCollection<Voznja>("voznja").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            VoznjaDTO pom = new VoznjaDTO();

            if (v != null)
            {

                pom.Id = v.Id.ToString();
                pom.PolazniGrad = v.PolazniGrad;
                pom.DolazniGrad = v.DolazniGrad;
                pom.DatumVoznje = v.DatumVoznje;
                pom.BrojSedista = v.BrojSedista;
                pom.BrojPreostalihSedista = v.BrojPreostalihSedista;
                pom.TipVoznje = v.TipVoznje;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in v.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.BusPreduzece.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.BusPreduzece = "";
                //}
                // else
                //{
                BusPreduzece busPreduzece = db.GetCollection<BusPreduzece>("busPreduzece").Find(x => x.Id == v.BusPreduzece).FirstOrDefault();
                    if (busPreduzece != null)
                    {
                        pom.BusPreduzece = busPreduzece.Id.ToString();
                    }
                //}

            }
            return pom;
        }


        public static void AzurirajVoznju(String id, VoznjaDTO voznjaDTO)
        {
            IMongoDatabase db = Session.MongoDatabase;

            Voznja pom = new Voznja();
            pom.Id = new ObjectId(id);
            pom.PolazniGrad = voznjaDTO.PolazniGrad;
            pom.DolazniGrad = voznjaDTO.DolazniGrad;
            pom.DatumVoznje = voznjaDTO.DatumVoznje;
            pom.BrojSedista = voznjaDTO.BrojSedista;
            pom.BrojPreostalihSedista = voznjaDTO.BrojPreostalihSedista;
            pom.TipVoznje = voznjaDTO.TipVoznje;



            pom.ListaRezervacija = new List<ObjectId>();
            foreach (var rez in voznjaDTO.ListaRezervacija)
            {
                pom.ListaRezervacija.Add(new ObjectId(rez));
            }
            if (voznjaDTO.BusPreduzece.Equals(""))
                pom.BusPreduzece = ObjectId.Empty;
            else
            {
                IMongoCollection<BusPreduzece> collectionBusPreduzece = db.GetCollection<BusPreduzece>("busPreduzece");
                BusPreduzece b = collectionBusPreduzece.Find(x => x.Id == new ObjectId(voznjaDTO.BusPreduzece)).FirstOrDefault();

                if (b != null)
                {

                    if (!b.Voznje.Contains(pom.Id))
                    {
                        b.Voznje.Add(pom.Id);
                        collectionBusPreduzece.ReplaceOne(x => x.Id == b.Id, b);

                    }
                    pom.BusPreduzece = new ObjectId(voznjaDTO.BusPreduzece);
                }


            }


            db.GetCollection<Voznja>("voznja").ReplaceOne(x => x.Id == new ObjectId(id), pom);
        }

        public static void ObrisiVoznju(string id) 
        {
            IMongoDatabase db = Session.MongoDatabase;

            Voznja voznja = db.GetCollection<Voznja>("voznja").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            if (voznja.BusPreduzece != ObjectId.Empty)
            {
                ObrisiBusPreduzecuVoznju(voznja.Id, voznja.BusPreduzece);
            }

            db.GetCollection<Voznja>("voznja").DeleteOne(x => x.Id == new ObjectId(id));
        }



        #endregion

        #region Komentar
        public static void KreirajKomentar(string id, Komentar komentar)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");
            komentar.BusPreduzece = new ObjectId(id);
            collection.InsertOne(komentar);
        }

        public static Komentar VratiKomentar(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Komentar komentar = db.GetCollection<Komentar>("komentari").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            return komentar;
        }

        public static List<Komentar> VratiSveKomentare()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => true).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static List<Komentar> VratiKomentareZaBusPreduzece(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => x.BusPreduzece == new ObjectId(id)).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static void AzurirajKomentar(string komentarId, String text)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Komentar>.Filter.Eq(x => x.Id, new ObjectId(komentarId));
            var update = Builders<Komentar>.Update.Set(x => x.Tekst, text);


            db.GetCollection<Komentar>("komentari").UpdateOne(filter, update);
        }

        public static void ObrisiKomentar(String komentarId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("komentari").DeleteOne(x => x.Id == new ObjectId(komentarId));
        }


        #endregion

        #region Usluga

        public static void KreirajUslugu(Usluga u)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Usluga>("usluge");
            collection.InsertOne(u);
        }

        public static void KreirajKolekcijuUsluga()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Usluga>("usluge");
        }

        public static Usluga VratiUslugu(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Usluga>("usluge").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();
        }


        public static IList<Usluga> VratiUsluge()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Usluga>("usluge").Find(x => true).ToList<Usluga>();
        }
        public static void ObrisiUslugu(ObjectId uslugaId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Usluga>("usluge").DeleteOne(x => x.Id == uslugaId);
        }
        public static void AzurirajCenuUsluge(ObjectId idUsluge, float novaCena)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Usluga>.Filter.Eq(x => x.Id, idUsluge);
            var update = Builders<Usluga>.Update.Set(x => x.Cena, novaCena);

            db.GetCollection<Usluga>("usluge").UpdateOne(filter, update);
        }

        #endregion

        #region Prtljag
        public static void KreirajPrtljag(Prtljag prtljag)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Prtljag>("prtljag");
            collection.InsertOne(prtljag);
        }
        public static void KreirajKolekcijuPrtljaga()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Prtljag>("prtljag");
        }

        public static List<Prtljag> VratiSavPrtljag()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Prtljag>("prtljag");

            List<Prtljag> prtljag = new List<Prtljag>();

            foreach (Prtljag p in collection.Find(x => true).ToList())
            {
                prtljag.Add(p);
            }

            return prtljag;
        }

        public static void AzurirajKolicinuPrtljaga(ObjectId idPr, int novaKol)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Prtljag>.Filter.Eq(x => x.Id, idPr);
            var update = Builders<Prtljag>.Update.Set(x => x.Kolicina, novaKol);

            db.GetCollection<Prtljag>("prtljag").UpdateOne(filter, update);
        }

        public static void ObrisiPrtljag(ObjectId prtljagId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Prtljag>("prtljag").DeleteOne(x => x.Id == prtljagId);
        }
        #endregion

        #region Putnik
        public static void KreirajKolekcijuPutnika()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
        }

        public static List<PutnikDTO> VratiSvePutnike()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            var rezervacijeCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<PutnikDTO> putnici = new List<PutnikDTO>();

            foreach (Putnik putnik in collection.Find(x => true).ToList())
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                if (putnik.Rezervacije != null)
                {
                    foreach (ObjectId rezId in putnik.Rezervacije)
                    {
                        rezervacije.Add(rezervacijeCollection.Find(x => x.Id == rezId).First());
                    }
                }
                PutnikDTO newPutnik = new PutnikDTO
                {
                    Id = putnik.Id,
                    Jmbg = putnik.Jmbg,
                    Ime = putnik.Ime,
                    Prezime = putnik.Prezime,
                    Email = putnik.Email,
                    Broj_Telefona = putnik.Broj_Telefona,
                    Rezervacije = rezervacije
                };
                putnici.Add(newPutnik);
            }
            return putnici;
        }



        public static List<Putnik> VratiPutnikeZaVoznju(String sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var kolekcija = db.GetCollection<Putnik>("putnici");
            var kolekcijaRezervacije = db.GetCollection<Rezervacija>("rezervacije");


            List<Putnik> putnici = new List<Putnik>();
            Rezervacija rezervacija = kolekcijaRezervacije.Find(x => x.Sifra_Rezervacije == sifra).First();
            foreach (Putnik p in kolekcija.Find(x => x.Rezervacije.Contains(rezervacija.Id)).ToList())
            {
                putnici.Add(p);
            }
            return putnici;
        }

        public static ObjectId KreirajPutnika(Putnik putnik)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            collection.InsertOne(putnik);
            return putnik.Id;
        }

        public static void DodajRezervacijuPutniku(String sifra, String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var putniciColleciton = db.GetCollection<Putnik>("putnici");
            var rezervacijaCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<ObjectId> rezervacije = new List<ObjectId>();
            ObjectId putnikId = new ObjectId();
            foreach (Putnik p in putniciColleciton.Find(x => x.Jmbg == jmbg).ToList())
            {
                putnikId = p.Id;
                rezervacije = p.Rezervacije;
                if (rezervacije == null)
                {
                    rezervacije = new List<ObjectId>();
                }
            }
            Rezervacija rezervacija = rezervacijaCollection.Find(x => x.Sifra_Rezervacije == sifra).First();
            rezervacije.Add(rezervacija.Id);

            var filter = Builders<Putnik>.Filter.Eq(x => x.Jmbg, jmbg);
            var update = Builders<Putnik>.Update.Set(x => x.Rezervacije, rezervacije);

            db.GetCollection<Putnik>("putnici").UpdateOne(filter, update);

            var filter1 = Builders<Rezervacija>.Filter.Eq(x => x.Sifra_Rezervacije, sifra);
            var update1 = Builders<Rezervacija>.Update.Set(x => x.Putnik, putnikId);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter1, update1);
        }

        public static void AzurirajPutnika(string id, PutnikDTOUpdate putnikDTOUpdate)
        {


            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<Putnik> putnikCollection = db.GetCollection<Putnik>("putnici");

            Putnik putnik = putnikCollection.Find(p => p.Id == new ObjectId(id)).FirstOrDefault();


            if (putnik != null)
            {
                putnik.Ime = putnikDTOUpdate.Ime;
                putnik.Prezime = putnikDTOUpdate.Prezime;
                putnik.Email = putnikDTOUpdate.Email;
                putnik.Broj_Telefona = putnikDTOUpdate.Broj_Telefona;

                putnikCollection.ReplaceOne(x => x.Id == new ObjectId(id), putnik);
            }
        }

        public static void ObrisiPutnika(String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Putnik>("putnici").DeleteOne(x => x.Jmbg == jmbg);
        }
        #endregion


    }
}
