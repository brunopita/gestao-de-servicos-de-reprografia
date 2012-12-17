using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reprografia.Models;
using Reprografia.Data;
using Reprografia.BusinessLogic;

namespace Reprografia.Controllers
{
    public class AvaliacaoController : Controller
    {
        private ReprografiaContext db = new ReprografiaContext();

        //
        // GET: /Avaliacao/Details/5

        public ViewResult Details(int id)
        {
            Avaliacao avaliacao = db.Avaliacoes.Find(id);
            return View(avaliacao);
        }

        //
        // GET: /Avaliacao/Edit/5

        public ActionResult Edit(int id)
        {
            Avaliacao avaliacao = db.Avaliacoes
                .Include("ItemsAvaliacao")
                .Include("Solicitacao")
                .First(a => a.Id == id);

            if (avaliacao.Avaliado)
            {
                return RedirectToAction("Index", "Solicitacao");
            }
            return View(avaliacao);
        }

        //
        // POST: /Avaliacao/Edit/5

        [HttpPost]
        public ActionResult Edit(Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacao).State = EntityState.Modified;

                avaliacao.Avaliado = true;
                avaliacao.DataAvaliado = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index", "Solicitacao");
            }
            return View(avaliacao);
        }

        //
        // GET: /Avaliacao/Delete/5

        public ActionResult Delete(int id)
        {
            Avaliacao avaliacao = db.Avaliacoes.Find(id);
            return View(avaliacao);
        }

        //
        // POST: /Avaliacao/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Avaliacao avaliacao = db.Avaliacoes.Find(id);
            db.Avaliacoes.Remove(avaliacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult XL(string id)
        {
            string[] anoSeq = id.Split('-');
            int ano = int.Parse(anoSeq[0]);
            int seq = int.Parse(anoSeq[1]);
            Solicitacao solicitacao = db.Solicitacoes
                .Include("Avaliacao")
                .First(s => s.Ano == ano && s.Seq == seq);
            string resultPath = AvaliacaoBL.EscreverXl(solicitacao.Avaliacao, Server.MapPath("~"));

            return new FilePathResult(resultPath, "application/vnd.ms-excel");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}