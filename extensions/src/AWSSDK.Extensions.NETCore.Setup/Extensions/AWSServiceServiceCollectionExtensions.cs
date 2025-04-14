/*
 * Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using Amazon.Runtime;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add Amazon service clients
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class AWSServiceServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddAWSService<T>(this IServiceCollection collection, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            return AddAWSService<T>(collection, options: null, lifetime);
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="options">The AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddAWSService<T>(this IServiceCollection collection, AWSOptions options, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), sp => sp.CreateServiceClient<T>(options), lifetime);
            collection.Add(descriptor);
            return collection;
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="optionsFunc">A func that resolves the AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddAWSService<T>(this IServiceCollection collection, Func<IServiceProvider, AWSOptions> optionsFunc, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), sp => sp.CreateServiceClient<T>(optionsFunc(sp)), lifetime);
            collection.Add(descriptor);
            return collection;
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework if the service type hasn't already been registered.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton,
        /// the default, then the same instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddAWSService<T>(this IServiceCollection collection, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            return TryAddAWSService<T>(collection, options: null, lifetime);
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework if the service type hasn't already been registered.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton,
        /// the default, then the same instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="options">The AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddAWSService<T>(this IServiceCollection collection, AWSOptions options, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), sp => sp.CreateServiceClient<T>(options), lifetime);
            collection.TryAdd(descriptor);
            return collection;
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework if the service type hasn't already been registered.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton,
        /// the default, then the same instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3.</typeparam>
        /// <param name="collection"></param>
        /// <param name="optionsFunc">A func that resolves the AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddAWSService<T>(this IServiceCollection collection, Func<IServiceProvider, AWSOptions> optionsFunc, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), sp => sp.CreateServiceClient<T>(optionsFunc(sp)), lifetime);
            collection.TryAdd(descriptor);
            return collection;
        }

#if NET8_0_OR_GREATER

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            return AddKeyedAWSService<T>(collection, serviceKey, options: null, lifetime);
        }
        
        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="options">The AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, AWSOptions options, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), serviceKey, (sp, _) => sp.CreateServiceClient<T>(options), lifetime);
            collection.Add(descriptor);
            return collection;
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key. The Amazon service client is not
        /// created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="optionsFunc">A func that resolves the AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, Func<IServiceProvider, AWSOptions> optionsFunc, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), serviceKey, (sp, _) => sp.CreateServiceClient<T>(optionsFunc(sp)), lifetime);
            collection.Add(descriptor);
            return collection;
        }
        
        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key if the service type hasn't already been registered with the same key.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        { 
            return TryAddKeyedAWSService<T>(collection, serviceKey, options: null, lifetime);
        }
        
        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key if the service type hasn't already been registered with the same key.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="options">The AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, AWSOptions options, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), serviceKey, (sp, _) => sp.CreateServiceClient<T>(options), lifetime);
            collection.TryAdd(descriptor);
            return collection;
        }

        /// <summary>
        /// Adds the Amazon service client to the dependency injection framework with a key if the service type hasn't already been registered with the same key.
        /// The Amazon service client is not created until it is requested. If the ServiceLifetime property is set to Singleton, the default, then the same
        /// instance will be reused for the lifetime of the process and the object should not be disposed.
        /// </summary>
        /// <typeparam name="T">The AWS service interface, like IAmazonS3</typeparam>
        /// <param name="collection"></param>
        /// <param name="serviceKey">The key with which the service will be added in the dependency injection framework.</param>
        /// <param name="optionsFunc">A func that resolves the AWS options used to create the service client overriding the default AWS options added using AddDefaultAWSOptions.</param>
        /// <param name="lifetime">The lifetime of the service client created. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddKeyedAWSService<T>(this IServiceCollection collection, object serviceKey, Func<IServiceProvider, AWSOptions> optionsFunc, ServiceLifetime lifetime = ServiceLifetime.Singleton) where T : IAmazonService
        {
            var descriptor = new ServiceDescriptor(typeof(T), serviceKey, (sp, _) => sp.CreateServiceClient<T>(optionsFunc(sp)), lifetime);
            collection.TryAdd(descriptor);
            return collection;
        }
#endif
    }
}
