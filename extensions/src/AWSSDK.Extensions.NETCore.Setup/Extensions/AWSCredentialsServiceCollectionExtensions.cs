using System;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class AWSCredentialsServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentials(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(AWSCredentials), sp => sp.CreateDefaultAWSCredentials(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentials(
            this IServiceCollection collection,
            AWSCredentials credentials)
        {
            collection.Add(new ServiceDescriptor(typeof(AWSCredentials), credentials));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentialsFunc"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentials(
            this IServiceCollection collection,
            Func<IServiceProvider, AWSCredentials> credentialsFunc,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(AWSCredentials), credentialsFunc, lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentials(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(AWSCredentials), sp => sp.CreateDefaultAWSCredentials(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentials(
            this IServiceCollection collection,
            AWSCredentials credentials)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(AWSCredentials), credentials));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentialsFunc"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentials(
            this IServiceCollection collection,
            Func<IServiceProvider, AWSCredentials> credentialsFunc,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(AWSCredentials), credentialsFunc, lifetime));
            return collection;
        }
    }
}