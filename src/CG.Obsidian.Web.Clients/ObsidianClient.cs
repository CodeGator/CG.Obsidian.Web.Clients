using CG.Business.Clients;
using CG.Obsidian.Web.Clients.Options;
using CG.Validations;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Obsidian.Web.Clients
{
    /// <summary>
    /// This is a default implementation of the <see cref="IObsidianClient"/>
    /// interface.
    /// </summary>
    public class ObsidianClient : 
        ClientBase<ObsidianClientOptions>, 
        IObsidianClient
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to an HTTP client factory.
        /// </summary>
        protected IHttpClientFactory HttpClientFactory { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="ObsidianClient"/>
        /// class/
        /// </summary>
        /// <param name="options">The options to use with the client.</param>
        /// <param name="httpClientFactory">The HTTP client factory to use with
        /// the client.</param>
        public ObsidianClient(
            IOptions<ObsidianClientOptions> options,
            IHttpClientFactory httpClientFactory
            ) : base(options)
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(httpClientFactory, nameof(httpClientFactory));

            // Save the references.
            HttpClientFactory = httpClientFactory;
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public virtual async Task<string[]> FindByExtensionsAsync(
            string extension,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Validate the parameters before attempting to use them.
                Guard.Instance().ThrowIfNullOrEmpty(extension, nameof(extension));

                // Create the HTTP client.
                var httpClient = HttpClientFactory.CreateClient();

                // Set the base address.
                httpClient.BaseAddress = new Uri(Options.Value.BaseAddress);

                // Make the call.
                var response = await httpClient.PostAsync(
                    "api/MimeTypes",
                    new StringContent(
                        extension.StartsWith('"') && extension.EndsWith('"') 
                            ? $"{extension}" : $"\"{extension}\"",
                        Encoding.UTF8, 
                        "application/json"
                        ),
                    cancellationToken
                    ).ConfigureAwait(false);

                // Did we fail?
                response.EnsureSuccessStatusCode();

                // Read the content from the response.
                var json = await response.Content.ReadAsStringAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Deserialize the JSON.
                var mimeTypes = JsonSerializer.Deserialize<string[]>(json);

                // Return the results.
                return mimeTypes;
            }
            catch(Exception ex)
            {
                // Provide better context for the error.
                throw new ClientException(
                    message: $"Failed to query for mime type(s) by file extension!",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
