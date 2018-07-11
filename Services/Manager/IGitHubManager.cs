using Services.Model;
using System.Threading.Tasks;

namespace Services.Manager
{
    public interface IGitHubManager
    {
        Task<UserRepos> GetUserGitHubRepo(string username, int page, int per_page);
        Task<UserRepo> GetPullRequetsByRepoName(string username, string repoName);
    }
}
