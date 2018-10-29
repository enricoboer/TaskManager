using System;
using System.Collections.Generic;

namespace TarefasAcademicas.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Adicionar(TEntity obj);

        TEntity Atualizar(TEntity obj);

        TEntity ObterPorId(Guid id);

        IEnumerable<TEntity> ObterTodos();

        void Deletar(Guid id);
    }
}