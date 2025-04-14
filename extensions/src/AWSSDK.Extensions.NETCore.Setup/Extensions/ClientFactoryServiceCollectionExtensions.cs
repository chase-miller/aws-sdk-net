using System;
using AWSSDK.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ClientFactoryServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddClientConfigFactory(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(IClientConfigFactory), sp => sp.CreateDefaultClientFactory(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddClientConfigFactory(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IClientConfigFactory), sp => sp.CreateDefaultClientFactory(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientFactory"></param>
        /// <returns></returns>
        public static IServiceCollection AddClientConfigFactory(
            this IServiceCollection collection,
            IClientFactory clientFactory)
        {
            collection.Add(new ServiceDescriptor(typeof(IClientConfigFactory), clientFactory));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientFactory"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddClientConfigFactory(
            this IServiceCollection collection,
            IClientFactory clientFactory)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IClientConfigFactory), clientFactory));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientFactoryFunc"></param>
        /// <returns></returns>
        public static IServiceCollection AddClientConfigFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IClientFactory> clientFactoryFunc)
        {
            collection.Add(new ServiceDescriptor(typeof(IClientConfigFactory), clientFactoryFunc));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientFactoryFunc"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddClientConfigFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IClientFactory> clientFactoryFunc)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IClientConfigFactory), clientFactoryFunc));
            return collection;
        }
    }
}