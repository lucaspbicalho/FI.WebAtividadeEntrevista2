using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!bo.ValidaCPF(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Digite um CPF válido.");
            }
            if (bo.VerificarExistencia(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("CPF já existente.");
            }
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                
                model.Id = bo.Incluir(new Cliente()
                {                    
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });

           
                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult IncluirBeneficiario(Beneficiario model)
        {
            BoBeneficiario bo = new BoBeneficiario();
            BoCliente boCliente = new BoCliente();
            var cliente = boCliente.Listar().Where(whe => whe.CPF==model.Id).ToList();
            model.IDCLIENTE = cliente.FirstOrDefault().Id;
            model.Id = null;
            if (!boCliente.ValidaCPF(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Digite um CPF válido.");
            }
            if (bo.VerificarExistencia(model.CPF,model.IDCLIENTE))
            {
                Response.StatusCode = 400;
                return Json("CPF já cadastrado para este Beneficiário.");
            }
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                
                model.Id = bo.Incluir(new Beneficiario()
                {                    
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IDCLIENTE = model.IDCLIENTE
                });

           
                return Json("Cadastro efetuado com sucesso");
            }
        }

    [HttpPost]
    public JsonResult getBeneficiarios(Beneficiario model)
    {
      BoBeneficiario bo = new BoBeneficiario();
      BoCliente boCliente = new BoCliente();
      var cliente = boCliente.Listar().Where(whe => whe.CPF == model.Id).ToList();
      if (cliente == null || cliente.Count() == 0)
      {
        return Json("");
      }
      model.IDCLIENTE = cliente.FirstOrDefault().Id;
      model.Id = null;

      var listaBeneficios = bo.Listar().Where(whe => whe.IDCLIENTE == model.IDCLIENTE).ToList();

      return Json(listaBeneficios);
    }

    [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(model.Id);
            if (!bo.ValidaCPF(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Digite um CPF válido.");
            }
            if (cliente.CPF != model.CPF)
            {           
              if (bo.VerificarExistencia(model.CPF))
              {
                  Response.StatusCode = 400;
                  return Json("CPF já existente.");
              }
            }
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });
                               
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    CPF = cliente.CPF,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}