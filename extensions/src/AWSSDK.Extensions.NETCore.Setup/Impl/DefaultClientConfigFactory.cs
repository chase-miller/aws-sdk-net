using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

namespace AWSSDK.Extensions.NETCore.Setup.Impl
{
    /// <summary>
    ///
    /// </summary>
    public class DefaultClientConfigFactory : IClientConfigFactory
    {
        private static readonly Type[] EMPTY_TYPES = Array.Empty<Type>();
        private static readonly object[] EMPTY_PARAMETERS = Array.Empty<object>();

        /// <summary>
        /// Creates the ClientConfig object for the service client.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ClientConfig CreateConfig<T>(AWSOptions options)
            where T : IAmazonService
        {
#if NET8_0_OR_GREATER
            ClientConfig config = T.CreateDefaultClientConfig();
#else
            var configTypeName = typeof(T).Namespace + "." + typeof(T).Name.Substring(1) + "Config";
            var configType = typeof(T).GetTypeInfo().Assembly.GetType(configTypeName);

            var constructor = configType.GetConstructor(EMPTY_TYPES);
            ClientConfig config = constructor.Invoke(EMPTY_PARAMETERS) as ClientConfig;
#endif

            if (options == null)
            {
                options = new AWSOptions();
            }

            var defaultConfig = options.DefaultClientConfig;


            // There is intertwined logic between ServiceURL, Region and DefaultConfigurationMode
            // in the SDK. They are handled at the start together to make it easier to debug SDK behavior.
            if (!string.IsNullOrEmpty(defaultConfig.ServiceURL))
            {
                config.ServiceURL = defaultConfig.ServiceURL;
            }
            // Setting RegionEndpoint only if ServiceURL was not set, because ServiceURL value will be lost otherwise
            else if (options.Region != null)
            {
                config.RegionEndpoint = options.Region;
            }

            if (options.DefaultConfigurationMode.HasValue)
            {
                config.DefaultConfigurationMode = options.DefaultConfigurationMode.Value;
            }



            if (defaultConfig.AllowAutoRedirect.HasValue)
            {
                config.AllowAutoRedirect = defaultConfig.AllowAutoRedirect.Value;
            }
            if (defaultConfig.AuthenticationRegion != null)
            {
                config.AuthenticationRegion = defaultConfig.AuthenticationRegion;
            }
            if (defaultConfig.BufferSize.HasValue)
            {
                config.BufferSize = defaultConfig.BufferSize.Value;
            }
            if (defaultConfig.ClientAppId != null)
            {
                config.ClientAppId = defaultConfig.ClientAppId;
            }
            if (defaultConfig.DisableHostPrefixInjection.HasValue)
            {
                config.DisableHostPrefixInjection = defaultConfig.DisableHostPrefixInjection.Value;
            }
            if (defaultConfig.DisableLogging.HasValue)
            {
                config.DisableLogging = defaultConfig.DisableLogging.Value;
            }
            if (defaultConfig.DisableRequestCompression.HasValue)
            {
                config.DisableRequestCompression = defaultConfig.DisableRequestCompression.Value;
            }
            if (defaultConfig.EndpointDiscoveryCacheLimit.HasValue)
            {
                config.EndpointDiscoveryCacheLimit = defaultConfig.EndpointDiscoveryCacheLimit.Value;
            }
            if (defaultConfig.EndpointDiscoveryEnabled.HasValue)
            {
                config.EndpointDiscoveryEnabled = defaultConfig.EndpointDiscoveryEnabled.Value;
            }
            if (defaultConfig.FastFailRequests.HasValue)
            {
                config.FastFailRequests = defaultConfig.FastFailRequests.Value;
            }
            if (defaultConfig.HttpClientCacheSize.HasValue)
            {
                config.HttpClientCacheSize = defaultConfig.HttpClientCacheSize.Value;
            }
            if (defaultConfig.IgnoreConfiguredEndpointUrls.HasValue)
            {
                config.IgnoreConfiguredEndpointUrls = defaultConfig.IgnoreConfiguredEndpointUrls.Value;
            }
            if (defaultConfig.LogMetrics.HasValue)
            {
                config.LogMetrics = defaultConfig.LogMetrics.Value;
            }
            if (defaultConfig.LogResponse.HasValue)
            {
                config.LogResponse = defaultConfig.LogResponse.Value;
            }
            if (defaultConfig.MaxErrorRetry.HasValue)
            {
                config.MaxErrorRetry = defaultConfig.MaxErrorRetry.Value;
            }
            if (defaultConfig.ProgressUpdateInterval.HasValue)
            {
                config.ProgressUpdateInterval = defaultConfig.ProgressUpdateInterval.Value;
            }
            if (defaultConfig.RequestMinCompressionSizeBytes.HasValue)
            {
                config.RequestMinCompressionSizeBytes = defaultConfig.RequestMinCompressionSizeBytes.Value;
            }
            if (defaultConfig.ResignRetries.HasValue)
            {
                config.ResignRetries = defaultConfig.ResignRetries.Value;
            }
            if (defaultConfig.RetryMode.HasValue)
            {
                config.RetryMode = defaultConfig.RetryMode.Value;
            }
            if (defaultConfig.ThrottleRetries.HasValue)
            {
                config.ThrottleRetries = defaultConfig.ThrottleRetries.Value;
            }
            if (defaultConfig.Timeout.HasValue)
            {
                config.Timeout = defaultConfig.Timeout.Value;
            }
            if (defaultConfig.UseAlternateUserAgentHeader.HasValue)
            {
                config.UseAlternateUserAgentHeader = defaultConfig.UseAlternateUserAgentHeader.Value;
            }
            if (defaultConfig.UseDualstackEndpoint.HasValue)
            {
                config.UseDualstackEndpoint = defaultConfig.UseDualstackEndpoint.Value;
            }
            if (defaultConfig.UseFIPSEndpoint.HasValue)
            {
                config.UseFIPSEndpoint = defaultConfig.UseFIPSEndpoint.Value;
            }
            if (defaultConfig.UseHttp.HasValue)
            {
                config.UseHttp = defaultConfig.UseHttp.Value;
            }

            if (defaultConfig.ServiceSpecificSettings.Count > 0)
            {
                ProcessServiceSpecificSettings(config, defaultConfig.ServiceSpecificSettings);
            }

            return config;
        }

#if NET8_0_OR_GREATER
        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075",
              Justification = "The parent calling method uses the DynamicDependencyAttribute on the service client to ensure the public properties are not trimmed.")]
#endif
        private void ProcessServiceSpecificSettings(ClientConfig clientConfig, IDictionary<string, string> serviceSettings)
        {
            var singleArray = new object[1];
            var properties = clientConfig.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var kvp in serviceSettings)
            {
                var property = properties.FirstOrDefault(x => string.Equals(x.Name, kvp.Key, StringComparison.OrdinalIgnoreCase));
                if (property == null)
                    continue;

                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                try
                {
                    if (propertyType == typeof(TimeSpan))
                    {
                        singleArray[0] = TimeSpan.FromMilliseconds(Convert.ToInt64(kvp.Value));
                    }
                    else if (propertyType.IsEnum)
                    {
                        singleArray[0] = Enum.Parse(propertyType, kvp.Value, true);
                    }
                    else
                    {
                        singleArray[0] = Convert.ChangeType(kvp.Value, propertyType);
                    }

                    property.SetMethod.Invoke(clientConfig, singleArray);
                }
                catch (Exception e)
                {
                    throw new ConfigurationException($"Error reading value for property {kvp.Key}.", e)
                    {
                        PropertyName = kvp.Key,
                        PropertyValue = kvp.Value
                    };
                }
            }
        }
    }
}