using DraxManUC001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NaumanWebApi.Controllers {
    public class ValuesController : ApiController {
        DomainModel dm = new DomainModel();
        // GET api/values
        public IEnumerable<Prodotto> Get() {
            return dm.Search("");
        }

        // GET api/values/5
        public Prodotto Get(int id) {
            return dm.Search(id);
        }

        // POST api/values
        public void Post([FromBody]string value) {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }
    }
}
