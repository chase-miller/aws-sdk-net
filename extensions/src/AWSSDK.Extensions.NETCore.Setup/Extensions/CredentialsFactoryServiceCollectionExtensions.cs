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
using AWSSDK.Extensions.NETCore.Setup;
using AWSSDK.Extensions.NETCore.Setup.Impl;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add Amazon service clients
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class CredentialsFactoryServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentialsFactory(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(IAWSCredentialsFactory), sp => sp.CreateDefaultCredentialsFactory(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="implementationFactory"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentialsFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IAWSCredentialsFactory> implementationFactory,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.Add(new ServiceDescriptor(typeof(IAWSCredentialsFactory), implementationFactory, lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static IServiceCollection AddAWSCredentialsFactory(
            this IServiceCollection collection,
            IAWSCredentialsFactory credentials)
        {
            collection.Add(new ServiceDescriptor(typeof(IAWSCredentialsFactory), credentials));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentialsFactory(
            this IServiceCollection collection,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IAWSCredentialsFactory), sp => sp.CreateDefaultCredentialsFactory(), lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="implementationFactory"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentialsFactory(
            this IServiceCollection collection,
            Func<IServiceProvider, IAWSCredentialsFactory> implementationFactory,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IAWSCredentialsFactory), implementationFactory, lifetime));
            return collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddAWSCredentialsFactory(
            this IServiceCollection collection,
            IAWSCredentialsFactory credentials)
        {
            collection.TryAdd(new ServiceDescriptor(typeof(IAWSCredentialsFactory), credentials));
            return collection;
        }
    }
}
