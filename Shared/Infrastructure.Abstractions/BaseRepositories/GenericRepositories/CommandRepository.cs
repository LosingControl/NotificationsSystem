using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.BaseRepositories.GenericRepositories
{
    /// <summary>
    /// Реализация репозитория для команд, CUD операций. Generic класс для DI
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public class CommandRepository<TEntity> : BaseCommandRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : class
    {
        public CommandRepository(DbContext context) : base(context)
        { }
    }
}
