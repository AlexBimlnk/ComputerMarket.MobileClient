using MobileClient.Contract.Builder;

namespace MobileClient.Logic.Builder;
public interface IBuilderAccessor
{
    public Task<BuildResult> GetBuildResultAsync(RequestBuild requestBuild);
}
