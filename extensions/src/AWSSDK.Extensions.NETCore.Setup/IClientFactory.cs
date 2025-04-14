using Amazon.Runtime;

namespace AWSSDK.Extensions.NETCore.Setup
{
    /// <summary>
    ///
    /// </summary>
    public interface IClientFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IAmazonService CreateServiceClient<T>() where T : IAmazonService;
    }
}