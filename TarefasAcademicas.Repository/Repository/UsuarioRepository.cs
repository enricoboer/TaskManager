using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TarefasAcademicas.DataAccess.Context;
using TarefasAcademicas.DataAccess.Model;

namespace TarefasAcademicas.DataAccess.Repository
{
    public class UsuarioRepository : ContextAccess, IRepository<Usuario>
    {
        public Usuario Adicionar(Usuario usuario)
        {
            var consultResult = ObterPorId(usuario.UsuarioId);

            if (consultResult == null)
            {
                var query = @"INSERT INTO Usuario(UsuarioId, Nome) VALUES(@usuarioId,@nome)";

                var parameters = new DynamicParameters();
                parameters.Add("@usuarioId", usuario.UsuarioId);
                parameters.Add("@nome", usuario.Nome);

                connection.Execute(query, parameters);

                return usuario;
            }

            return null;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            var query = @"UPDATE Usuario " +
                        " SET Nome = @nome " +
                        " WHERE UsuarioId = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@nome", usuario.Nome);
            parameters.Add("@id", usuario.UsuarioId);

            connection.Execute(query, parameters);

            return usuario;
        }

        public Usuario ObterPorId(Guid id)
        {
            var query = @"SELECT * FROM Usuario WHERE UsuarioId = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var result = connection.Query<Usuario>(query, parameters);

            return result?.FirstOrDefault();
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            var query = @"SELECT * FROM Usuario";

            var result = connection.Query<Usuario>(query);

            return result?.ToList();
        }

        public void Deletar(Guid id)
        {
            var query = @"DELETE FROM Usuario WHERE UsuarioId = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            connection.Execute(query, parameters);
        }
    }
}