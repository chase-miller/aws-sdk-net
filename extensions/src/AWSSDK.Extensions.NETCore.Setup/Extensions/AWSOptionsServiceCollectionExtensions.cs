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
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add Amazon service clients
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class AWSOptionsServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the AWSOptions object to the dependency injection framework providing information
        /// that will be used to construct Amazon service clients.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="options">The default AWS options used to construct AWS service clients with.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddDefaultAWSOptions(this IServiceCollection collection, AWSOptions options)
        {
            collection.Add(new ServiceDescriptor(typeof(AWSOptions), options));
            return collection;
        }

        /// <summary>
        /// Adds the AWSOptions object to the dependency injection framework providing information
        /// that will be used to construct Amazon service clients.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="implementationFactory">The factory that creates the default AWS options.
        /// The AWS options will be used to construct AWS service clients
        /// </param>
        /// <param name="lifetime">The lifetime of the AWSOptions. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection AddDefaultAWSOptions(
            this IServiceCollection collection, 
            Func<IServiceProvider, AWSOptions> implementationFactory, 
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(AWSOptions), implementationFactory, lifetime));
            return collection;
        }

        /// <summary>
        /// Adds the AWSOptions object to the dependency injection framework providing information
        /// that will be used to construct Amazon service clients if they haven't already been registered.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="options">The default AWS options used to construct AWS service clients with.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddDefaultAWSOptions(this IServiceCollection collection, AWSOptions options)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(AWSOptions), options));
            return collection;
        }

        /// <summary>
        /// Adds the AWSOptions object to the dependency injection framework providing information
        /// that will be used to construct Amazon service clients if they haven't already been registered.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="implementationFactory">The factory that creates the default AWS options.
        /// The AWS options will be used to construct AWS service clients
        /// </param>
        /// <param name="lifetime">The lifetime of the AWSOptions. The default is Singleton.</param>
        /// <returns>Returns back the IServiceCollection to continue the fluent system of IServiceCollection.</returns>
        public static IServiceCollection TryAddDefaultAWSOptions(
            this IServiceCollection collection, 
            Func<IServiceProvider, AWSOptions> implementationFactory, 
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(AWSOptions), implementationFactory, lifetime));
            return collection;
        }
    }
}
