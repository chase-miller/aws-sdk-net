using System;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AWSSDK.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup.Impl;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ClientFactoryServiceProviderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="optionsOverride"></param>
        /// <param name="credentialsOverride"></param>
        /// <param name="clientConfigFactoryOverride"></param>
        /// <returns></returns>
        public static IClientFactory CreateDefaultClientFactory(
            this IServiceProvider sp,
            AWSOptions optionsOverride = null,
            AWSCredentials credentialsOverride = null,
            IClientConfigFactory clientConfigFactoryOverride = null)
        {
            var logger = sp.GetService<ILogger>();
            var awsOptions = optionsOverride ?? sp.GetService<AWSOptions>() ?? new AWSOptions();
            var credentials = credentialsOverride ?? sp.GetService<AWSCredentials>() ?? sp.CreateDefaultAWSCredentials(awsOptions);
            var clientConfigFactory = clientConfigFactoryOverride ?? sp.GetService<IClientConfigFactory>() ?? new DefaultClientConfigFactory();

            return new DefaultClientFactory(awsOptions, credentials, logger, clientConfigFactory);
        }
    }
}