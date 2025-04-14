using System;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AWSSDK.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup.Impl;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class AWSCredentialsServiceProviderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AWSCredentials CreateDefaultAWSCredentials(this IServiceProvider sp, AWSOptions options = null)
        {
            options = options ?? sp.GetService<AWSOptions>() ?? new AWSOptions();
            return new DefaultAWSCredentials(options, sp.GetService<ILogger>());
        }
    }
}