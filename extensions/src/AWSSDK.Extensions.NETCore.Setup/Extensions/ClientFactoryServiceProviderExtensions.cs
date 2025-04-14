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
    public static class ClientFactoryServiceProviderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="optionsOverride"></param>
        /// <param name="credentialsFactoryOverride"></param>
        /// <param name="clientConfigFactoryOverride"></param>
        /// <returns></returns>
        public static IClientFactory CreateDefaultClientFactory(
            this IServiceProvider sp,
            AWSOptions optionsOverride = null,
            IAWSCredentialsFactory credentialsFactoryOverride = null,
            IClientConfigFactory clientConfigFactoryOverride = null)
        {
            var logger = sp.GetService<ILogger>();
            var awsOptions = optionsOverride ?? sp.GetService<AWSOptions>() ?? new AWSOptions();
            var credentialsFactory = credentialsFactoryOverride ?? sp.GetService<IAWSCredentialsFactory>() ?? new DefaultAWSCredentialsFactory(awsOptions, logger);
            var clientConfigFactory = clientConfigFactoryOverride ?? sp.GetService<IClientConfigFactory>() ?? new DefaultClientConfigFactory();

            return new DefaultClientFactory(awsOptions, credentialsFactory, logger, clientConfigFactory);
        }
    }
}