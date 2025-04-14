using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

namespace AWSSDK.Extensions.NETCore.Setup
{
    /// <summary>
    ///
    /// </summary>
    public interface IClientConfigFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ClientConfig CreateConfig<T>(AWSOptions options)
            where T : IAmazonService;
    }
}