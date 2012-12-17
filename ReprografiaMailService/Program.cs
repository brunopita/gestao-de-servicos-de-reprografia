using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Threading;
using Reprografia.Data;
using Reprografia.Models;
using ReprografiaMailService.Models;
using System.Collections.Generic;
using Omu.ValueInjecter;

namespace ReprografiaMailService
{
    static class Program
    {
        static private long interval = 1000; //12 * 60 * 60 * 1000; //intervalo de 12h
        static private SmtpClient mailClient;

        static private ResourceManager rm = new ResourceManager(typeof(ReprografiaMailService.Properties.Resources));

        static void Main(string[] args)
        {
            while (true)
            {
                Work();
                Thread.Sleep(new TimeSpan(interval));
            }
        }

        static void Work()
        {
            PreencherFila();
            using (ReprografiaContext db = new ReprografiaContext())
            {
                var emails = db.Emails.Where(m => !m.Enviado);
                EnviarEmails(emails);
                db.SaveChanges();
            }
        }

        static private void EnviarEmails(IEnumerable<EmailCobrancaAvaliacao> emails)
        {
            string templateText = rm.GetString("MailTemplate");

            foreach (var email in emails)
            {
                MailViewModel viewModel = MapViewModel(email);
                if (EnviarEmail(templateText, viewModel))
                {
                    email.EnviadoEm = DateTime.Now;
                    email.Enviado = true;
                }
            }
        }

        static private SmtpClient GetSmtpClient()
        {
            return new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("reprografia115@gmail.com", "Senai115")
            };
        }

        static private bool EnviarEmail(string templateText, MailViewModel viewModel)
        {
            if (String.IsNullOrEmpty(templateText))
                throw new ArgumentException("templateText is null or empty.", "templateText");
            if (viewModel == null)
                throw new ArgumentNullException("email", "email is null.");

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.Sender = new MailAddress("reprografia115@gmail.com");
            message.From = new MailAddress("reprografia115@gmail.com");
            message.To.Add(new MailAddress(viewModel.AvaliacaoSolicitacaoUserEmail));

            message.Subject = string.Format("Lembrete para avaliação da Solicitação Reprográfica {0}/{1}",
                viewModel.AvaliacaoSolicitacaoAno,
                viewModel.AvaliacaoSolicitacaoSeq);

            string templateResult = RazorEngine.Razor.Parse(templateText, viewModel);
            message.Body = templateResult;

            try
            {
                MailClient.Send(message);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }

        private static MailViewModel MapViewModel(EmailCobrancaAvaliacao email)
        {
            var viewModel = new MailViewModel();
            //viewModel.Email = email.Email;
            //viewModel.Ano = email.Avaliacao.Solicitacao.Ano;
            //viewModel.Seq = email.Avaliacao.Solicitacao.Seq;
            //viewModel.DataLimite = email.Avaliacao.DataLimite;
            //viewModel.FullName = email.Avaliacao.Solicitacao.User.FullName;
            //viewModel.IdAvaliacao = email.Avaliacao.Id;
            //viewModel.IdSolicitacao = email.Avaliacao.Solicitacao.Id;
            viewModel.InjectFrom<FlatLoopValueInjection>(email);
            return viewModel;
        }

        static private IEnumerable<EmailCobrancaAvaliacao> PreencherFila()
        {
            List<EmailCobrancaAvaliacao> result = new List<EmailCobrancaAvaliacao>();
            using (var db = new ReprografiaContext())
            {
                //Colocar na fila de emails avaliacoes com menos de 2 dias ate o prazo final
                //DateTime dateFilter = DateTime.Now.AddDays(100);
                DateTime dateFilter = DateTime.Now.AddDays(2);
                var avaliacoesPrestesAExpirar = db.Avaliacoes
                    .Where(a => a.DataLimite <= dateFilter);

                foreach (var avaliacao in avaliacoesPrestesAExpirar)
                {
                    EmailCobrancaAvaliacao email = new EmailCobrancaAvaliacao(avaliacao);
                    result.Add(email);
                    db.Emails.Add(email);
                }
                db.SaveChanges();
            }
            return result;
        }

        public static SmtpClient MailClient
        {
            get
            {
                if (mailClient == null)
                {
                    mailClient = GetSmtpClient();
                }
                return mailClient;
            }
        }
                
    }
}
