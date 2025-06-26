using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalMvc.Models
{
    public class LocacaoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório")]
        public int IdUsuario { get; set; }

        public int IdPropostaLocador { get; set; }
        public int Classe { get; set; }
        public int IdLocador { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Início")]
        public DateTime DtInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Término")]
        public DateTime DtFim { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data da Situação")]
        public DateTime DtSituacao { get; set; }

        [Display(Name = "Usuário da Situação")]
        public int IdUsuarioSituacao { get; set; }
    }
}