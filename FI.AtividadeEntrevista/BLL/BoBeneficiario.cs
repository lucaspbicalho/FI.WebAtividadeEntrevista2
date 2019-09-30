using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        public string Incluir(DML.Beneficiario Beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Incluir(Beneficiario);
        }

        /// <summary>
        /// Altera um Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        public void Alterar(DML.Beneficiario Beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Alterar(Beneficiario);
        }

        /// <summary>
        /// Consulta o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Listar()
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long Id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarExistencia(CPF,Id);
        }

        /// <summary>
        /// ValidaCPF
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool ValidaCPF(string CPF)
        {
            string valor = CPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;
            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }
    }
}
