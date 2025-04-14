using System;
using Amazon.Runtime;
using AWSSDK.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        /// <returns></returns>
        public static AWSCredentials CreateDefaultAWSCredentials(this IServiceProvider sp)
        {
            var credentialsFactory = sp.GetService<IAWSCredentialsFactory>() ?? sp.CreateDefaultCredentialsFactory();
            return credentialsFactory.Create();
        }
    }
}