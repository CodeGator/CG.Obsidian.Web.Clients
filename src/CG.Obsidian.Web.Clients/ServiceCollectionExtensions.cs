using CG.DataProtection;
using CG.Obsidian.Web.Clients;
using CG.Obsidian.Web.Clients.Options;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CG.Obsidian.Web
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type, for the CG.Obsidian library.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a client for accessing the CG.Obsidian.Web REST API.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for
        /// the operation.</param>
        /// <param name="configuration">The configuration to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/> parameter,
        /// for chaining calls together.</returns>
        public static IServiceCollection AddObsidianClients(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the options.
            serviceCollection.ConfigureOptions<ObsidianClientOptions>(
                DataProtector.Instance(),
                configuration
                );

            // Configure the standard HTTP client.
            serviceCollection.AddHttpClient();

            // Register the clients.
            serviceCollection.Add<IObsidianClient, ObsidianClient>(serviceLifetime);

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
