using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TarefasAcademicas.DataAccess.Model
{
    public class Tarefas
    {
        [Key]

        public Guid Id { get; set; }

        public string Tarefa { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data Ínicio")]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data Final")]
        public DateTime DataFinal { get; set; }

        public string Categoria { get; set; }

        public string Status { get; set; }

        public string StatusClasse { get; set; }

        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}