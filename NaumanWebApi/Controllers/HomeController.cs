using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraxManUC001.Models;

namespace DraxManUC001.Controllers {
    public class HomeController : Controller {
        DomainModel dm = new DomainModel();
        public ActionResult Index() {
            return View();
        }

        public ActionResult ListaProdotti() {
            return View();
        }
        public ActionResult Dettaglio() {
            return View();
        }
        public ActionResult Ricerca() {
            return View();
        }
        public ActionResult Detail(int id) {
            ViewBag.Prodotto = dm.Search(id);
            if (ViewBag.Prodotto != null) {
                return View("Dettaglio");
            } else {
                ViewBag.Message = "Prodotto non torvato";
                return View("Ricerca");
            }

        }
        [HttpPost]
        public ActionResult Ricerca(string id, string descrizione) {
            int codice;
            if (id != "" && int.TryParse(id, out codice)) {
                Prodotto prodotto = dm.Search(codice);
                if (prodotto == null) {
                    ViewBag.Message = $"La ricerca con il seguente codice {codice} non ha prodotto alcun risultato";
                    return View();
                }
                ViewBag.Prodotto = prodotto;
                return View("Dettaglio");
            } else if (descrizione != "") {
                List<Prodotto> prodotti = dm.Search(descrizione);
                if (prodotti.Count == 0) {
                    ViewBag.Message = "Non è stato trovato alcun prodotto che corrisponda alla descrizione";
                    return View("Ricerca");
                }
                ViewBag.Prodotti = prodotti;
                return View("ListaProdotti");
            } else {
                ViewBag.Message = "Inserire almeno un parametro di ricerca";
                return View();
            }
        }
        public ActionResult AddToCar(int? qta, int id) {
            Prodotto prodotto = dm.Search(id);
            if (prodotto != null) {
                if (qta == null || qta <= 0) {
                    ViewBag.Message = $"Inserire la quantita deve essere maggiore di 1 ";
                    ViewBag.Prodotto = prodotto;
                    return View("Dettaglio");
                }
                List<Prodotto> prodotti = Session["products"] as List<Prodotto>;
                if (prodotti == null) {
                    prodotti = new List<Prodotto>();
                }
                if (prodotti.Contains(prodotto)) {
                    prodotti[prodotti.IndexOf(prodotto)].QtaOrdine += (int)qta;
                } else {
                    Prodotto aggiunto = new Prodotto { Id = id, Descrizione = prodotto.Descrizione, QtaOrdine = (int)qta };
                    prodotti.Add(aggiunto);
                }
                Session["products"] = prodotti;
                ViewBag.Message = "Elemento aggiunto al carrello";
            } else
                ViewBag.Message = "Prodotto non è stato trovato ";
            return View("Ricerca");
        }
        public ActionResult Carrello() {
            List<Prodotto> list = Session["products"] as List<Prodotto>;
            if (list != null && list.Count > 0) {
                ViewBag.Prodotti = list;
            } else
                ViewBag.Message = "Non ci sono ordini";
            return View();
        }
        public ActionResult SpedisciOrdine() {
            List<Prodotto> list = Session["products"] as List<Prodotto>;
            if (list != null && list.Count > 0) {
                dm.SpedisciOrdine(list);
                ViewBag.Message = "Ordini spediti";
                Session["products"] = null;
            } else
                ViewBag.Message = "Non ci sono ordini";
            return View("Ricerca");
        }
        public ActionResult PulisciCarrello() {
            List<Prodotto> prodotti = Session["products"] as List<Prodotto>;
            if (prodotti == null) {
                ViewBag.Message = "Il carrello è vuoto";
                return View("Ricerca");
            }
            prodotti = null;
            Session["products"] = prodotti;
            ViewBag.MessageBox = "Il carrello è stato pulito";
            return View("Ricerca");
        }
    }
}