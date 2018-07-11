using System;
using System.Text;
using System.Threading.Tasks;
using Services.Model;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace Services.Manager
{
    /// <summary>
    /// GitHub Api service
    /// </summary>
    public class GitHubManager : IGitHubManager
    {
        private IHttpHandler HttpHandler;

        /// <summary>
        /// Constructor to Inject HttpClient Handler
        /// </summary>
        /// <param name="httpHandler"></param>
        public GitHubManager(IHttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }
        
        private static readonly string GITHUB_HOST = "https://api.github.com";

        /// <summary>
        /// Fetches all user repos by username, Page, per_page
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="per_page"></param>
        /// <returns></returns>
        public async Task<UserRepos> GetUserGitHubRepo(string username, int page, int per_page)
        {
            return await ExecuteUserReposRequest(username, page, per_page);
        }

        /// <summary>
        /// Fetches all the pull requests by repo name
        /// </summary>
        /// <param name="username"></param>
        /// <param name="repoName"></param>
        /// <param name="page"></param>
        /// <param name="per_page"></param>
        /// <returns></returns>
        public async Task<UserRepo> GetPullRequetsByRepoName(string username, string repoName)
        {
            return await ExecuteRepoPRsRequest(repoName, username);
        }

        private async Task<UserRepos> ExecuteUserReposRequest(string userName, int page, int per_page)
        {
            Uri baseUri = new Uri(GITHUB_HOST);
            Uri fullUri = new Uri(baseUri, "users/" + userName + "/repos?page=" + page + "&per_page=" + per_page);

            HttpResponseMessage responseMessage = await HttpHandler.GetAsync(fullUri);

            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return new UserRepos();
            }
            else if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();                      // If there is an error, this will throw
            string result = await responseMessage.Content.ReadAsStringAsync();

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(UserRepo[]));

            UserRepo[] userRepos = (UserRepo[])jsonSerializer.ReadObject(stream);

            //
            foreach (UserRepo repo in userRepos)
            {
                UserRepo repoPRs = await GetPullRequetsByRepoName(userName, repo.name);

                //Filter Open PRs and reassign
                var openPRs = from p in repoPRs.PullRequests where p.state == "open" select p;

                repo.PullRequests = openPRs.ToList();
            }
            // Order by no of PRs in the list
            userRepos = userRepos.OrderByDescending(x => x.PullRequests.Count).ToArray();

            return new UserRepos { Repos = new List<UserRepo>(userRepos) };
        }

        private async Task<UserRepo> ExecuteRepoPRsRequest(string repoName, string userName)
        {
            Uri baseUri = new Uri(GITHUB_HOST);
            Uri fullUri = new Uri(baseUri, "repos/" + userName + "/" + repoName+ "/pulls");

            HttpResponseMessage responseMessage = await HttpHandler.GetAsync(fullUri);

            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return new UserRepo();
            }
            else if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();                      // If there is an error, this will throw
            string result = await responseMessage.Content.ReadAsStringAsync();

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PullRequest[]));

            PullRequest[] pullRequests = (PullRequest[])jsonSerializer.ReadObject(stream);

            return new UserRepo { PullRequests = new List<PullRequest>(pullRequests) };
        }
    }
}
