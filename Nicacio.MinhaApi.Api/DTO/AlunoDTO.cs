using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nicacio.MinhaApi.Api.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do aluno é obrigatório")]
        [StringLength(maximumLength:20, MinimumLength = 2, ErrorMessage = ("O nome do aluno deve conter entre 2 e 20 caracteres"))]
        public string Nome { get; set; }
        [MaxLength(100, ErrorMessage = "O endereço deve conter até 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A mensalidade é obrigatoria")]
        [Range(0.01, 9999.99, ErrorMessage = "A Mensalidade deve estar entre R$ 0,01 e R$ 9999,99")]
        public decimal Mensalidade { get; set; }
    }
}