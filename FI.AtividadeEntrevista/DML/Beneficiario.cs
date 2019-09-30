using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de Beneficiario que representa o registo na tabela Beneficiario do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public long IDCLIENTE { get; set; }
     
    }    
}
