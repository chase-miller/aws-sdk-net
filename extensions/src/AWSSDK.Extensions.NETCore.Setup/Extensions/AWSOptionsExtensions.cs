using System;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AWSSDK.Extensions.NETCore.Setup.Impl;

namespace AWSSDK.Extensions.NETCore.Setup
{
    /// <summary>
    ///
    /// </summary>
    public static class AWSOptionsExtensions
    {
        /// <summary>
        /// Create a service client for the specified service interface using the options set in this instance.
        /// For example if T is set to IAmazonS3 then the AmazonS3ServiceClient which implements IAmazonS3 is created
        /// and returned.
        /// </summary>
        /// <typeparam name="T">The service interface that a service client will be created for.</typeparam>
        /// <returns>The service client that implements the service interface.</returns>
        [Obsolete("Prefer creating a service client via one of the IServiceCollection or IServiceProvider extensions.")]
        public static T CreateServiceClient<T>(this AWSOptions options, IAWSCredentialsFactory credentialsFactory = null, IClientConfigFactory clientConfigFactory = null)
            where T : class, IAmazonService
        {
            credentialsFactory = credentialsFactory ?? new DefaultAWSCredentialsFactory(options);
            clientConfigFactory = clientConfigFactory ?? new DefaultClientConfigFactory();
            var clientFactory = new DefaultClientFactory(options, credentialsFactory, null, clientConfigFactory);

            return clientFactory.CreateServiceClient<T>() as T;
        }
    }
}