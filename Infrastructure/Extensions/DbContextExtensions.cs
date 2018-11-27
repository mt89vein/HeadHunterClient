using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Находит все имплементации <see cref="IEntityTypeConfiguration{TEntity}" /> в указаной сборке и регистирует их в
        /// <seealso cref="ApplicationContext" />
        /// </summary>
        /// <param name="modelBuilder">Конфигуратор сущностей</param>
        /// <param name="assembly">Сборка, из которой необходимо зарегистировать маппинги</param>
        public static void ApplyAllConfigurationsFromAssembly(
            this ModelBuilder modelBuilder, Assembly assembly)
        {
            var applyGenericMethod = typeof(ModelBuilder).GetMethods().FirstOrDefault(m =>
                m.Name == "ApplyConfiguration" && m.GetParameters().First().ParameterType.GetGenericTypeDefinition() ==
                typeof(IEntityTypeConfiguration<>).GetGenericTypeDefinition());

            foreach (var type in assembly.GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            foreach (var i in type.GetInterfaces())
                if (i.IsConstructedGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(i.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(modelBuilder, new[] {Activator.CreateInstance(type)});
                    break;
                }
        }
    }
}