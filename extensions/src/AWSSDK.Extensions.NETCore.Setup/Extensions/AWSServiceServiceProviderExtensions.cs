using System;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AWSSDK.Extensions.NETCore.Setup;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class AWSServiceServiceProviderExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static object CreateServiceClient<T>(this IServiceProvider sp, AWSOptions options = null) where T : IAmazonService
        {
            var factory = sp.GetService<IClientFactory>() ?? sp.CreateDefaultClientFactory(options);
            return factory.CreateServiceClient<T>();
        }
    }
}