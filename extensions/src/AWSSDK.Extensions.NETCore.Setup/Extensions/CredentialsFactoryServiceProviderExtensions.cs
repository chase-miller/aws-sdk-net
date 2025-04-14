using System;
using Amazon.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup.Impl;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class CredentialsFactoryServiceProviderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static IAWSCredentialsFactory CreateDefaultCredentialsFactory(this IServiceProvider sp)
        {
            var options = sp.GetService<AWSOptions>() ?? new AWSOptions();
            return new DefaultAWSCredentialsFactory(options, sp.GetService<ILogger>());
        }
    }
}