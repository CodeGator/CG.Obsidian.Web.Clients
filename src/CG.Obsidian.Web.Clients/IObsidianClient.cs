using CG.Business.Clients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Obsidian.Web.Clients
{
    /// <summary>
    /// This interface represents a client for accessing the <see cref="CG.Obsidian.Web"/>
    /// REST API.
    /// </summary>
    public interface IObsidianClient : IClient
    {
        /// <summary>
        /// This method queries the Obsidian API for any mime type with a 
        /// matching file extension.
        /// </summary>
        /// <param name="extension">The file extension to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task to perform the operation, that returns zero or more
        /// matching mime/types.</returns>
        Task<string[]> FindByExtensionsAsync(
            string extension,
            CancellationToken cancellationToken = default
            );
    }
}
