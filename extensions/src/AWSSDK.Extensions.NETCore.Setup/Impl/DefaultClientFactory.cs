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
using System.Reflection;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Logging;

namespace AWSSDK.Extensions.NETCore.Setup.Impl
{
    /// <summary>
    /// The factory class for creating AWS service clients from the AWS SDK for .NET.
    /// </summary>
    internal class DefaultClientFactory : IClientFactory
    {
        private readonly AWSOptions _options;
        private readonly IAWSCredentialsFactory _credentialsFactory;
        private readonly IClientConfigFactory _clientConfigFactory;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs an instance of the ClientFactory
        /// </summary>
        /// <param name="awsOptions">The AWS options used for creating service clients.</param>
        /// <param name="credentialsFactory"></param>
        /// <param name="logger"></param>
        /// <param name="clientConfigFactory"></param>
        internal DefaultClientFactory(AWSOptions awsOptions, IAWSCredentialsFactory credentialsFactory, ILogger logger, IClientConfigFactory clientConfigFactory)
        {
            _options = awsOptions ?? throw new ArgumentNullException(nameof(awsOptions));
            _credentialsFactory = credentialsFactory ?? throw new ArgumentNullException(nameof(credentialsFactory));
            _logger = logger;
            _clientConfigFactory = clientConfigFactory;
        }

        /// <summary>
        /// Creates the AWS service client that implements the service client interface.
        /// </summary>
        /// <returns>The AWS service client</returns>
        public IAmazonService CreateServiceClient<T>(AWSCredentials credentials = null)
            where T : IAmazonService
        {
            PerformGlobalConfig(_logger, _options);
            credentials = credentials ?? _credentialsFactory.Create();

            if (!string.IsNullOrEmpty(_options?.SessionRoleArn))
            {
                if (string.IsNullOrEmpty(_options?.ExternalId))
                {
                    credentials = new AssumeRoleAWSCredentials(credentials, _options.SessionRoleArn, _options.SessionName);
                }
                else
                {
                    credentials = new AssumeRoleAWSCredentials(credentials, _options.SessionRoleArn, _options.SessionName, new AssumeRoleAWSCredentialsOptions() { ExternalId = _options.ExternalId });
                }
            }

            var config = _clientConfigFactory.CreateConfig<T>(_options);
            var client = CreateClient<T>(credentials, config);
            return client as IAmazonService;
        }

        /// <summary>
        /// Performs all of the global settings that have been specified in AWSOptions.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        private static void PerformGlobalConfig(ILogger logger, AWSOptions options)
        {
            if(options?.Logging != null)
            {
                if(options.Logging.LogTo.HasValue && AWSConfigs.LoggingConfig.LogTo != options.Logging.LogTo.Value)
                {
                    AWSConfigs.LoggingConfig.LogTo = options.Logging.LogTo.Value;
                    logger?.LogDebug($"Configuring SDK LogTo: {AWSConfigs.LoggingConfig.LogTo}");
                }

                if (options.Logging.LogResponses.HasValue && AWSConfigs.LoggingConfig.LogResponses != options.Logging.LogResponses.Value)
                {
                    AWSConfigs.LoggingConfig.LogResponses = options.Logging.LogResponses.Value;
                    logger?.LogDebug($"Configuring SDK LogResponses: {AWSConfigs.LoggingConfig.LogResponses}");
                }

                if (options.Logging.LogMetrics.HasValue && AWSConfigs.LoggingConfig.LogMetrics != options.Logging.LogMetrics.Value)
                {
                    AWSConfigs.LoggingConfig.LogMetrics = options.Logging.LogMetrics.Value;
                    logger?.LogDebug($"Configuring SDK LogMetrics: {AWSConfigs.LoggingConfig.LogMetrics}");
                }

                if (options.Logging.LogResponsesSizeLimit.HasValue && AWSConfigs.LoggingConfig.LogResponsesSizeLimit != options.Logging.LogResponsesSizeLimit.Value)
                {
                    AWSConfigs.LoggingConfig.LogResponsesSizeLimit = options.Logging.LogResponsesSizeLimit.Value;
                    logger?.LogDebug($"Configuring SDK LogResponsesSizeLimit: {AWSConfigs.LoggingConfig.LogResponsesSizeLimit}");
                }
            }
        }

        /// <summary>
        /// Creates the service client using the credentials and client config.
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static AmazonServiceClient CreateClient<T>(AWSCredentials credentials, ClientConfig config)
            where T : IAmazonService
        {
#if NET8_0_OR_GREATER
            return T.CreateDefaultServiceClient(credentials, config) as AmazonServiceClient;
#else
            var clientTypeName = typeof(T).Namespace + "." + typeof(T).Name.Substring(1) + "Client";
            var clientType = typeof(T).GetTypeInfo().Assembly.GetType(clientTypeName);
            if (clientType == null)
            {
                throw new AmazonClientException($"Failed to find service client {clientTypeName} which implements {typeof(T).FullName}.");
            }

            var constructor = clientType.GetConstructor(new Type[] { typeof(AWSCredentials), config.GetType() });
            if (constructor == null)
            {
                throw new AmazonClientException($"Service client {clientTypeName} missing a constructor with parameters AWSCredentials and {config.GetType().FullName}.");
            }

            return constructor.Invoke(new object[] { credentials, config }) as AmazonServiceClient;
#endif
        }
    }
}
