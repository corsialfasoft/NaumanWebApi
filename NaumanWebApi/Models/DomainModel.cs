using NaumanWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraxManUC001.Models {
	interface IDomainModel{
		Prodotto Search(int id);
		List<Prodotto> Search(string des);
		void SpedisciOrdine(List<Prodotto> prodotti);

	}

	public class Prodotto {
		public int Id{ get;set;}
		public string Descrizione{ get;set;}
		public int Giacenza{ get;set;}
		public int QtaOrdine{ get;set;}
	}

	public class DomainModel : IDomainModel {
		RICHIESTEEntities db = null;
		public Prodotto Search(int id) {
			using(db= new RICHIESTEEntities()){
				ProdottiSet prodotto = db.ProdottiSet.Find(id);
				if(prodotto!=null){
					return new Prodotto{ Id=prodotto.Id,Descrizione=prodotto.descrizione,Giacenza=prodotto.quantita};
				}
				return null;
			}
		}

		public List<Prodotto> Search(string des) {
			using (db = new RICHIESTEEntities()) {
				var query = from pro in db.ProdottiSet
							where pro.descrizione.Contains(des)
							select pro;
				List<Prodotto> prodotti = new List<Prodotto>();
 				foreach(ProdottiSet prodotto in query)
					prodotti.Add(new Prodotto { Id = prodotto.Id, Descrizione = prodotto.descrizione, Giacenza = prodotto.quantita });
				return prodotti;
			}
		}

		public void SpedisciOrdine(List<Prodotto> prodotti) {
			using (db = new RICHIESTEEntities()) {
				RichiesteSet richiesta = new RichiesteSet{data=DateTime.Now};
				db.RichiesteSet.Add(richiesta);
				foreach(Prodotto prodotto in prodotti){
					ProdottiSet ps = db.ProdottiSet.Find(prodotto.Id);
					if(ps!=null){ 
						RichiesteProdotti rp =new RichiesteProdotti{RichiesteSet = richiesta, ProdottiSet = ps, quantita= prodotto.QtaOrdine};
						db.RichiesteProdotti.Add(rp);
						db.SaveChanges();
					}
				}

			}
		}
	}
}