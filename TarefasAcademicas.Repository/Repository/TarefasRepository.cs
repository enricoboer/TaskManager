using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TarefasAcademicas.DataAccess.Context;
using TarefasAcademicas.DataAccess.Model;

namespace TarefasAcademicas.DataAccess.Repository
{
    public class TarefasRepository : ContextAccess, IRepository<Tarefas>
    {
        public Tarefas Adicionar(Tarefas tarefa)
        {
            var query = @"INSERT INTO Tarefas(Id,Tarefas, Data_Inicio, Data_Final, Categoria, UsuarioId, Status) VALUES(@id,@tarefa,@datainicio,@datafinal,@categoria,@usuarioId,@status)";

            var parameters = new DynamicParameters();
            parameters.Add("@id", Guid.NewGuid());
            parameters.Add("@tarefa", tarefa.Tarefa);
            parameters.Add("@datainicio", tarefa.DataInicio);
            parameters.Add("@datafinal", tarefa.DataFinal);
            parameters.Add("@categoria", tarefa.Categoria);
            parameters.Add("@usuarioId", tarefa.UsuarioId);
            if (tarefa.DataInicio > tarefa.DataFinal)
            {
                parameters.Add("@status", 1) ;
            }
            else if (tarefa.DataInicio == tarefa.DataFinal)
            {
                parameters.Add("@status", 2);
            }
            else
            {
                parameters.Add("@status", 3);
            }

            connection.Execute(query, parameters);

            return tarefa;
        }

        public Tarefas Atualizar(Tarefas tarefa)
        {
            var query = @"UPDATE Tarefas " +
                        " SET Tarefas = @tarefa, " +
                        " Data_Inicio = @datainicio, " +
                        " Data_Final = @datafinal," +
                        " Categoria = @categoria"+
                        " WHERE ID = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@tarefa", tarefa.Tarefa);
            parameters.Add("@datainicio", tarefa.DataInicio.ToString("dd/MM/yyyy"));
            parameters.Add("@datafinal", tarefa.DataFinal.ToString("dd/MM/yyyy"));
            parameters.Add("@categoria", tarefa.Categoria);
            parameters.Add("@id", tarefa.Id);

            connection.Execute(query, parameters);

            return tarefa;
        }

        public Tarefas ObterPorId(Guid id)
        {
            var query = @"SELECT ID as Id, Tarefas as Tarefa, Data_Inicio as DataInicio, Data_Final as DataFinal, Categoria as Categoria " +
                        " FROM Tarefas WHERE ID = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var result = connection.Query<Tarefas>(query, parameters);

            return result?.First();
        }

        public IEnumerable<Tarefas> ObterTodos()
        {
            var query = @"SELECT ID as Id, Tarefas as Tarefa, Data_Inicio as DataInicio, Data_Final as DataFinal, Categoria as Categoria FROM Tarefas";

            var result = connection.Query<Tarefas>(query);

            return result?.ToList();
        }

        public void Deletar(Guid id)
        {
            var query = @"DELETE FROM Tarefas WHERE ID = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            connection.Execute(query, parameters);
        }

        public IEnumerable<Tarefas> ObterPorUsuarioId(Guid usuarioId)
        {
            var query = @"SELECT ID as Id, Tarefas as Tarefa, Data_Inicio as DataInicio, Data_Final as DataFinal, Categoria as Categoria "+
                         " FROM Tarefas WHERE UsuarioId = @usuarioId";

            var parameters = new DynamicParameters();
            parameters.Add("@usuarioId", usuarioId);

            var result = connection.Query<Tarefas>(query, parameters);

            return result?.ToList();
        }

        public int ObterNumeroTotalTarefas(Guid usuarioId)
        {
            var query = @"SELECT COUNT(*) FROM Tarefas WHERE UsuarioId = @usuarioId";

            var result = connection.Query<int>(query, new
            {
                @usuarioId= usuarioId
            });

            return result.First();
        }

        public int ObterNumeroTotalTarefasForadoPrazo(Guid usuarioId)
        {

            var query = @"SELECT COUNT(*) FROM Tarefas WHERE UsuarioId = @usuarioId and Status = 1";

            var result = connection.Query<int>(query, new
            {
                @usuarioId = usuarioId
            });

            return result.First();
        }

        public int ObterNumeroTotalTarefasDentrodoPrazo(Guid usuarioId)
        {

            var query = @"SELECT COUNT(*) FROM Tarefas WHERE UsuarioId = @usuarioId and Status = 3";

            var result = connection.Query<int>(query, new
            {
                @usuarioId = usuarioId
            });

            return result.First();
        }


        public int ObterNumeroTotalTarefasIgualPrazo(Guid usuarioId)
        {

            var query = @"SELECT COUNT(*) FROM Tarefas WHERE UsuarioId = @usuarioId and Status = 2";

            var result = connection.Query<int>(query, new
            {
                @usuarioId = usuarioId
            });

            return result.First();
        }



    }
}