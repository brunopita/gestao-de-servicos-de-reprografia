using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reprografia.Data;
using Reprografia.Models.ViewModels;
using Omu.ValueInjecter;
using Reprografia.BusinessLogic;

namespace Reprografia.Controllers
{
    public class AdminController : Controller
    {
        private ReprografiaContext db = new ReprografiaContext();

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var solicitacoes = db.Solicitacoes
                .Include("Avaliacao")
                .Include("User");

            //Inicializar ViewModel a partir dos modelos de dados
            List<SolicitacaoIndexModel> model = new List<SolicitacaoIndexModel>();
            foreach (var s in solicitacoes)
            {
                SolicitacaoIndexModel modelItem = new SolicitacaoIndexModel();
                modelItem.InjectFrom(s.Avaliacao, s);
                modelItem.InsertFrom(s);
                modelItem.Cancelavel = s.IsCancelavel();
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult ActionReport(int year = 0, int month = 0)
        {
            if (year == 0)
                year = DateTime.Today.Year;

            if (month == 0)
                month = DateTime.Today.Month;
            var solicitacoes = db.Solicitacoes
                .Include("User")
                .Include("Avaliacao");

            solicitacoes.Where(s => s.Ano == year && s.DataSolicitacao.Month == month);

            List<ActionReportViewModel> model = new List<ActionReportViewModel>(solicitacoes.Count());
            foreach (var s in solicitacoes)
                model.Add(new ActionReportViewModel(s));

            return View(model);
        }

    }
}
