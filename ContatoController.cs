using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteMVCAngular.Models;

namespace TesteMVCAngular.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        public ActionResult Index()
        {
            return View();
            
        }

        public JsonResult Get_AllContato()
        {
            using ( BdContatoEntities Obj = new BdContatoEntities())
            {
                List<Contato> cont = Obj.Contato.ToList();
                return Json(cont, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Pegar Contato por Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public JsonResult Get_ContatoById(string Id)
        {
            using (BdContatoEntities Obj = new BdContatoEntities())
            {
                int ContId = int.Parse(Id);
                return Json(Obj.Contato.Find(ContId), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Pegar Contato por Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public JsonResult Get_ContatoByNome(string Nome, string Telefone, string Empresa)
        {
            using (BdContatoEntities Obj = new BdContatoEntities())
            {
                var contPesq = Obj.Contato.Where(x => x.Nome == Nome || x.Telefone == Telefone || x.Empresa == Empresa).ToList();

                return Json(contPesq, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Inserir Novo Contato  
        /// </summary>  
        /// <param name="Contatos"></param>  
        /// <returns></returns>  
        public string Insert_Contato(Contato Contatos)
        {
            if (Contatos != null)
            {
                using (BdContatoEntities Obj = new BdContatoEntities())
                {
                    Obj.Contato.Add(Contatos);
                    Obj.SaveChanges();
                    return "Contato adicionado com sucesso";
                }
            }
            else
            {
                return "Contato não inserido! Tente Novamente!";
            }
        }
        /// <summary>  
        /// Delete Contato   
        /// </summary>  
        /// <param name="cont"></param>  
        /// <returns></returns>  
        public string Delete_Contato(Contato cont)
        {
            if (cont != null)
            {
                using (BdContatoEntities Obj = new BdContatoEntities())
                {
                    var Cont_ = Obj.Entry(cont);
                    if (Cont_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.Contato.Attach(cont);
                        Obj.Contato.Remove(cont);
                    }
                    Obj.SaveChanges();
                    return "Contato deletado com Sucesso";
                }
            }
            else
            {
                return "Contato não Deletado! Tente Novamente";
            }
        }
        /// <summary>  
        /// Atualizar Contato  
        /// </summary>  
        /// <param name="Cont"></param>  
        /// <returns></returns>  
        public string Update_Contato(Contato Cont)
        {
            if (Cont != null)
            {
                using (BdContatoEntities Obj = new BdContatoEntities())
                {
                    var Cont_ = Obj.Entry(Cont);
                    Contato contObj = Obj.Contato.Where(x => x.IdContato == Cont.IdContato).FirstOrDefault();
                    contObj.Nome = Cont.Nome;
                    contObj.Telefone = Cont.Telefone;
                    contObj.Endereco = Cont.Endereco;
                    contObj.Email = Cont.Email;
                    contObj.Empresa = Cont.Empresa;
                    Obj.SaveChanges();
                    return "Contato atualizado com Sucesso";
                }
            }
            else
            {
                return "Contato não atualizado, tente Novamente!";
            }
        }
    }
}