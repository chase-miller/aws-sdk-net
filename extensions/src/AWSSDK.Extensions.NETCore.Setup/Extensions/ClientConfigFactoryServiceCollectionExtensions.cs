using System;
using AWSSDK.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup.Impl;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ClientConfigFactoryServiceCollectionExtensions
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
            collection.Add(new ServiceDescriptor(typeof(IClientConfigFactory), CreateDefaultCredentialsFactory, lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientConfigFactoryFunc"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddClientConfigFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IClientConfigFactory> clientConfigFactoryFunc,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(IClientConfigFactory), clientConfigFactoryFunc, lifetime));
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
            collection.TryAdd(new ServiceDescriptor(typeof(IClientConfigFactory), CreateDefaultCredentialsFactory, lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clientConfigFactoryFunc"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddClientConfigFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IClientConfigFactory> clientConfigFactoryFunc,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IClientConfigFactory), clientConfigFactoryFunc, lifetime));
            return collection;
        }

        private static IClientConfigFactory CreateDefaultCredentialsFactory(IServiceProvider sp)
        {
            return new DefaultClientConfigFactory();
        }
    }
}