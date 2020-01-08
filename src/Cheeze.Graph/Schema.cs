using AutoMapper;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeze.Graph
{
    public static class Schema
    {
        private static readonly IMapper Mapper;

        static Schema()
        {
            var mapping = new MapperConfiguration(
                configuration =>
                {
                    configuration.CreateMap<Cheeze.Store.Client.Cheese, Cheese>()
                        .ForMember(cheese => cheese.Available, options => options.Ignore());
                }
            );

            Mapper = mapping.CreateMapper();
        }
        private static async Task<IReadOnlyDictionary<Guid, int>> FetchInventory(this Cheeze.Inventory.Client.IInventoryClient inventoryClient, IReadOnlyList<Guid> cheeses)
        {
            var response = await inventoryClient.PostAsync(new Cheeze.Inventory.Client.Request { Ids = cheeses.ToArray() });

            return cheeses
                .GroupJoin(response, id => id, available => available.Id, (id, available) => (Id: id, Available: available.Select(a => a.Quantity).FirstOrDefault()))
                .ToDictionary(tuple => tuple.Id, tuple => tuple.Available);
        }

        private static async Task<int> ResolveInventory(this IResolverContext context)
        {
            var dataLoader = context.BatchDataLoader<Guid, int>(
                "availableById",
                context.Service<Cheeze.Inventory.Client.IInventoryClient>().FetchInventory);

            return await dataLoader.LoadAsync(context.Parent<Cheese>().Id, context.RequestAborted);
        }

        private static async Task<IEnumerable<Cheese>> ResolveCheeses(this IResolverContext context)
        {
            var results = await context.Service<Cheeze.Store.Client.IStoreClient>().GetAsync();

            return results.Select(source => Mapper.Map<Cheeze.Store.Client.Cheese, Cheese>(source));
        }

        public static ISchemaBuilder Build()
        {
            return SchemaBuilder.New()
                .AddQueryType(
                    typeDescriptor => typeDescriptor
                        .Field("Cheese")
                            .Resolver(context => context.ResolveCheeses()))
                .AddObjectType<Cheese>(
                    cheese => cheese
                        .Field(f => f.Available)
                            .Resolver(context => context.ResolveInventory()))
                .ModifyOptions(o => o.RemoveUnreachableTypes = true);
        }
    }
}