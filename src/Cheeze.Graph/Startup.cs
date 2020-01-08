using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.Extensions.Options;

namespace Cheeze.Graph
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<Cheeze.Store.Client.IStoreClient, Cheeze.Store.Client.StoreClient>(
                (serviceProvider, httpClient) => httpClient.BaseAddress = serviceProvider.GetRequiredService<IOptions<Store.Configuration>>().Value.BaseAddress
            );

            services.AddHttpClient<Cheeze.Inventory.Client.IInventoryClient, Cheeze.Inventory.Client.InventoryClient>(
                (serviceProvider, httpClient) => httpClient.BaseAddress = serviceProvider.GetRequiredService<IOptions<Inventory.Configuration>>().Value.BaseAddress
            );

            // this enables you to use DataLoader in your resolvers.
            services.AddDataLoaderRegistry();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseWebSockets()
                .UseGraphQL()
                .UsePlayground()
                .UseVoyager();
        }
    }
}
