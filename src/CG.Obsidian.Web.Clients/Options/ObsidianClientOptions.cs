using CG.Business.Clients.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Obsidian.Web.Clients.Options
{
    /// <summary>
    /// This class represents configuration settings for an Obsidian client.
    /// </summary>
    public class ObsidianClientOptions : ClientOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the base address for the REST API.
        /// </summary>
        [Required]
        public string BaseAddress { get; set; }

        #endregion
    }
}
