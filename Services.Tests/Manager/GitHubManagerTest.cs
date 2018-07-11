using Moq;
using PRHawkDemo.Tests;
using Services.Manager;
using Services.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Services.Tests.Manager
{
    public class GitHubManagerTest : BaseTestClass
    {
        private Mock<IHttpHandler> _mockHttpHandler;

        public GitHubManagerTest()
        {
            _mockHttpHandler = Mock.Get(Container.Resolve<IHttpHandler>());
        }

        [Fact]
        public async Task Test_GetUserGitHubRepo_Success()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.OK;
            
            message.Content = new StringContent(GetUserReposData());
            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);
            
            var result = await githubManager.GetUserGitHubRepo("SomePath", 1, 1);

            // Validate the result
            Assert.NotNull(result);
            Assert.NotNull(result.Repos);
            Assert.Equal(result.Repos.Count, 1);
        }

        [Fact]
        public async Task Test_GetUserGitHubRepo_NotFound()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.NotFound;
            
            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);

            var result = await githubManager.GetUserGitHubRepo("SomePath", 1, 1);

            // Validate the result
            Assert.NotNull(result);
            Assert.Null(result.Repos);
        }

        [Fact]
        public async Task Test_GetUserGitHubRepo_InternalServerError()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.InternalServerError;

            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);

            var result = await githubManager.GetUserGitHubRepo("SomePath", 1, 1);

            // Validate the result
            Assert.Null(result);
        }

        [Fact]
        public async Task Test_GetPullRequetsByRepoName_Success()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.OK;

            message.Content = new StringContent(GetPRsData());
            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);

            var result = await githubManager.GetPullRequetsByRepoName("SomePath", "test");

            // Validate the result
            Assert.NotNull(result);
            Assert.NotNull(result.PullRequests);
            Assert.Equal(result.PullRequests.Count, 1);
        }

        [Fact]
        public async Task Test_GetPullRequetsByRepoName_NotFound()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.NotFound;

            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);

            var result = await githubManager.GetPullRequetsByRepoName("SomePath", "test");

            // Validate the result
            Assert.NotNull(result);
            Assert.Null(result.PullRequests);
        }

        [Fact]
        public async Task Test_GetPullRequetsByRepoName_InternalServerError()
        {
            var githubManager = new GitHubManager(_mockHttpHandler.Object);

            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.InternalServerError;

            var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletion.SetResult(message);
            _mockHttpHandler.Setup(n => n.GetAsync(It.IsAny<Uri>())).Returns(taskCompletion.Task);

            var result = await githubManager.GetPullRequetsByRepoName("SomePath", "test");

            // Validate the result
            Assert.Null(result);
        }



        private string GetUserReposData()
        {
            return "[{\"id\":138426699,\"node_id\":\"MDEwOlJlcG9zaXRvcnkxMzg0MjY2OTk=\",\"name\":\"TESTREPO\",\"full_name\":\"tharakeswararaogoru/TESTREPO\",\"owner\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"private\":false,\"html_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"description\":\"TEST\",\"fork\":false,\"url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO\",\"forks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/forks\",\"keys_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/keys{/key_id}\",\"collaborators_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/collaborators{/collaborator}\",\"teams_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/teams\",\"hooks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/hooks\",\"issue_events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/events{/number}\",\"events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/events\",\"assignees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/assignees{/user}\",\"branches_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/branches{/branch}\",\"tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/tags\",\"blobs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/blobs{/sha}\",\"git_tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/tags{/sha}\",\"git_refs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/refs{/sha}\",\"trees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/trees{/sha}\",\"statuses_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/statuses/{sha}\",\"languages_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/languages\",\"stargazers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/stargazers\",\"contributors_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contributors\",\"subscribers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscribers\",\"subscription_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscription\",\"commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/commits{/sha}\",\"git_commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/commits{/sha}\",\"comments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/comments{/number}\",\"issue_comment_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/comments{/number}\",\"contents_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contents/{+path}\",\"compare_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/compare/{base}...{head}\",\"merges_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/merges\",\"archive_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/{archive_format}{/ref}\",\"downloads_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/downloads\",\"issues_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues{/number}\",\"pulls_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls{/number}\",\"milestones_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/milestones{/number}\",\"notifications_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/notifications{?since,all,participating}\",\"labels_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/labels{/name}\",\"releases_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/releases{/id}\",\"deployments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/deployments\",\"created_at\":\"2018-06-23T19:01:43Z\",\"updated_at\":\"2018-06-23T19:04:07Z\",\"pushed_at\":\"2018-06-23T19:22:00Z\",\"git_url\":\"git://github.com/tharakeswararaogoru/TESTREPO.git\",\"ssh_url\":\"git@github.com:tharakeswararaogoru/TESTREPO.git\",\"clone_url\":\"https://github.com/tharakeswararaogoru/TESTREPO.git\",\"svn_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"homepage\":null,\"size\":1,\"stargazers_count\":0,\"watchers_count\":0,\"language\":null,\"has_issues\":true,\"has_projects\":true,\"has_downloads\":true,\"has_wiki\":true,\"has_pages\":false,\"forks_count\":0,\"mirror_url\":null,\"archived\":false,\"open_issues_count\":1,\"license\":null,\"forks\":0,\"open_issues\":1,\"watchers\":0,\"default_branch\":\"master\"}]";
        }

        private string GetPRsData()
        {
            return "[{\"url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1\",\"id\":196917051,\"node_id\":\"MDExOlB1bGxSZXF1ZXN0MTk2OTE3MDUx\",\"html_url\":\"https://github.com/tharakeswararaogoru/TESTREPO/pull/1\",\"diff_url\":\"https://github.com/tharakeswararaogoru/TESTREPO/pull/1.diff\",\"patch_url\":\"https://github.com/tharakeswararaogoru/TESTREPO/pull/1.patch\",\"issue_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/1\",\"number\":1,\"state\":\"open\",\"locked\":false,\"title\":\"Update README.md\",\"user\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"body\":\"\",\"created_at\":\"2018-06-23T19:22:01Z\",\"updated_at\":\"2018-06-23T19:22:01Z\",\"closed_at\":null,\"merged_at\":null,\"merge_commit_sha\":\"d59e02caaa25def0dd4deb7cb5eef8a01af69640\",\"assignee\":null,\"assignees\":[],\"requested_reviewers\":[],\"requested_teams\":[],\"labels\":[],\"milestone\":null,\"commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1/commits\",\"review_comments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1/comments\",\"review_comment_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/comments{/number}\",\"comments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/1/comments\",\"statuses_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/statuses/91985fd379ea71329447dfe8cab326b0d9f5043f\",\"head\":{\"label\":\"tharakeswararaogoru:TESTBRANCH\",\"ref\":\"TESTBRANCH\",\"sha\":\"91985fd379ea71329447dfe8cab326b0d9f5043f\",\"user\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"repo\":{\"id\":138426699,\"node_id\":\"MDEwOlJlcG9zaXRvcnkxMzg0MjY2OTk=\",\"name\":\"TESTREPO\",\"full_name\":\"tharakeswararaogoru/TESTREPO\",\"owner\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"private\":false,\"html_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"description\":\"TEST\",\"fork\":false,\"url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO\",\"forks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/forks\",\"keys_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/keys{/key_id}\",\"collaborators_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/collaborators{/collaborator}\",\"teams_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/teams\",\"hooks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/hooks\",\"issue_events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/events{/number}\",\"events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/events\",\"assignees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/assignees{/user}\",\"branches_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/branches{/branch}\",\"tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/tags\",\"blobs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/blobs{/sha}\",\"git_tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/tags{/sha}\",\"git_refs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/refs{/sha}\",\"trees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/trees{/sha}\",\"statuses_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/statuses/{sha}\",\"languages_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/languages\",\"stargazers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/stargazers\",\"contributors_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contributors\",\"subscribers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscribers\",\"subscription_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscription\",\"commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/commits{/sha}\",\"git_commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/commits{/sha}\",\"comments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/comments{/number}\",\"issue_comment_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/comments{/number}\",\"contents_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contents/{+path}\",\"compare_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/compare/{base}...{head}\",\"merges_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/merges\",\"archive_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/{archive_format}{/ref}\",\"downloads_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/downloads\",\"issues_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues{/number}\",\"pulls_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls{/number}\",\"milestones_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/milestones{/number}\",\"notifications_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/notifications{?since,all,participating}\",\"labels_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/labels{/name}\",\"releases_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/releases{/id}\",\"deployments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/deployments\",\"created_at\":\"2018-06-23T19:01:43Z\",\"updated_at\":\"2018-06-23T19:04:07Z\",\"pushed_at\":\"2018-06-23T19:22:00Z\",\"git_url\":\"git://github.com/tharakeswararaogoru/TESTREPO.git\",\"ssh_url\":\"git@github.com:tharakeswararaogoru/TESTREPO.git\",\"clone_url\":\"https://github.com/tharakeswararaogoru/TESTREPO.git\",\"svn_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"homepage\":null,\"size\":1,\"stargazers_count\":0,\"watchers_count\":0,\"language\":null,\"has_issues\":true,\"has_projects\":true,\"has_downloads\":true,\"has_wiki\":true,\"has_pages\":false,\"forks_count\":0,\"mirror_url\":null,\"archived\":false,\"open_issues_count\":1,\"license\":null,\"forks\":0,\"open_issues\":1,\"watchers\":0,\"default_branch\":\"master\"}},\"base\":{\"label\":\"tharakeswararaogoru:master\",\"ref\":\"master\",\"sha\":\"8d6efefd53b53d965128e39950b05ba8c51ad5ef\",\"user\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"repo\":{\"id\":138426699,\"node_id\":\"MDEwOlJlcG9zaXRvcnkxMzg0MjY2OTk=\",\"name\":\"TESTREPO\",\"full_name\":\"tharakeswararaogoru/TESTREPO\",\"owner\":{\"login\":\"tharakeswararaogoru\",\"id\":38439383,\"node_id\":\"MDQ6VXNlcjM4NDM5Mzgz\",\"avatar_url\":\"https://avatars1.githubusercontent.com/u/38439383?v=4\",\"gravatar_id\":\"\",\"url\":\"https://api.github.com/users/tharakeswararaogoru\",\"html_url\":\"https://github.com/tharakeswararaogoru\",\"followers_url\":\"https://api.github.com/users/tharakeswararaogoru/followers\",\"following_url\":\"https://api.github.com/users/tharakeswararaogoru/following{/other_user}\",\"gists_url\":\"https://api.github.com/users/tharakeswararaogoru/gists{/gist_id}\",\"starred_url\":\"https://api.github.com/users/tharakeswararaogoru/starred{/owner}{/repo}\",\"subscriptions_url\":\"https://api.github.com/users/tharakeswararaogoru/subscriptions\",\"organizations_url\":\"https://api.github.com/users/tharakeswararaogoru/orgs\",\"repos_url\":\"https://api.github.com/users/tharakeswararaogoru/repos\",\"events_url\":\"https://api.github.com/users/tharakeswararaogoru/events{/privacy}\",\"received_events_url\":\"https://api.github.com/users/tharakeswararaogoru/received_events\",\"type\":\"User\",\"site_admin\":false},\"private\":false,\"html_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"description\":\"TEST\",\"fork\":false,\"url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO\",\"forks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/forks\",\"keys_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/keys{/key_id}\",\"collaborators_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/collaborators{/collaborator}\",\"teams_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/teams\",\"hooks_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/hooks\",\"issue_events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/events{/number}\",\"events_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/events\",\"assignees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/assignees{/user}\",\"branches_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/branches{/branch}\",\"tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/tags\",\"blobs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/blobs{/sha}\",\"git_tags_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/tags{/sha}\",\"git_refs_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/refs{/sha}\",\"trees_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/trees{/sha}\",\"statuses_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/statuses/{sha}\",\"languages_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/languages\",\"stargazers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/stargazers\",\"contributors_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contributors\",\"subscribers_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscribers\",\"subscription_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/subscription\",\"commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/commits{/sha}\",\"git_commits_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/git/commits{/sha}\",\"comments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/comments{/number}\",\"issue_comment_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/comments{/number}\",\"contents_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/contents/{+path}\",\"compare_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/compare/{base}...{head}\",\"merges_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/merges\",\"archive_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/{archive_format}{/ref}\",\"downloads_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/downloads\",\"issues_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues{/number}\",\"pulls_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls{/number}\",\"milestones_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/milestones{/number}\",\"notifications_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/notifications{?since,all,participating}\",\"labels_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/labels{/name}\",\"releases_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/releases{/id}\",\"deployments_url\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/deployments\",\"created_at\":\"2018-06-23T19:01:43Z\",\"updated_at\":\"2018-06-23T19:04:07Z\",\"pushed_at\":\"2018-06-23T19:22:00Z\",\"git_url\":\"git://github.com/tharakeswararaogoru/TESTREPO.git\",\"ssh_url\":\"git@github.com:tharakeswararaogoru/TESTREPO.git\",\"clone_url\":\"https://github.com/tharakeswararaogoru/TESTREPO.git\",\"svn_url\":\"https://github.com/tharakeswararaogoru/TESTREPO\",\"homepage\":null,\"size\":1,\"stargazers_count\":0,\"watchers_count\":0,\"language\":null,\"has_issues\":true,\"has_projects\":true,\"has_downloads\":true,\"has_wiki\":true,\"has_pages\":false,\"forks_count\":0,\"mirror_url\":null,\"archived\":false,\"open_issues_count\":1,\"license\":null,\"forks\":0,\"open_issues\":1,\"watchers\":0,\"default_branch\":\"master\"}},\"_links\":{\"self\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1\"},\"html\":{\"href\":\"https://github.com/tharakeswararaogoru/TESTREPO/pull/1\"},\"issue\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/1\"},\"comments\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/issues/1/comments\"},\"review_comments\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1/comments\"},\"review_comment\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/comments{/number}\"},\"commits\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/pulls/1/commits\"},\"statuses\":{\"href\":\"https://api.github.com/repos/tharakeswararaogoru/TESTREPO/statuses/91985fd379ea71329447dfe8cab326b0d9f5043f\"}},\"author_association\":\"OWNER\"}]";
        }
    }
}
