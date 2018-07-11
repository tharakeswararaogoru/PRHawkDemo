using System;
using Unity;
using Services.Manager;
using Moq;
using Xunit;
using PRHawkDemo.Controllers;
using Services.Model;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PRHawkDemo.Tests.Controllers
{
    /// <summary>
    /// UserController Test methods
    /// </summary>
    public class UserControllerTest : BaseTestClass
    {
        private Mock<IGitHubManager> _mockGitHubManager;

        public UserControllerTest()
        {
            _mockGitHubManager = Mock.Get(Container.Resolve<IGitHubManager>());
        }

        [Fact]
        public async Task Test_Index_Success()
        {
            var userController = new UserController(_mockGitHubManager.Object);

            // Get the test data
            UserRepos repos = new UserRepos();
            UserRepo repo = new UserRepo
            {
                name = "test"
            };
            repos.Repos = new System.Collections.Generic.List<UserRepo>();
            repos.Repos.Add(repo);
            var taskCompletion = new TaskCompletionSource<UserRepos>();
            taskCompletion.SetResult(repos);

            _mockGitHubManager.Setup(m => m.GetUserGitHubRepo(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(taskCompletion.Task);

            var result = await userController.Index("SomePath");

            // Validate the result
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            if (viewResult != null)
            {
                Assert.Equal(viewResult.ViewName, "Index");
            }
        }
        [Fact]
        public async Task Test_Index_Exception()
        {
            var userController = new UserController(_mockGitHubManager.Object);

            // Get the test data
            UserRepos repos = new UserRepos();
            UserRepo repo = new UserRepo
            {
                name = "test"
            };
            repos.Repos = new System.Collections.Generic.List<UserRepo>();
            repos.Repos.Add(repo);
            var taskCompletion = new TaskCompletionSource<UserRepos>();
            taskCompletion.SetResult(repos);

            _mockGitHubManager.Setup(m => m.GetUserGitHubRepo(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            var result = await userController.Index("SomePath");

            // Validate the result
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            if (viewResult != null)
            {
                Assert.Equal(viewResult.ViewName, "Error");
            }
        }

        [Fact]
        public async Task Test_Index_InternalError()
        {
            var userController = new UserController(_mockGitHubManager.Object);

            // Get the test data
            UserRepos repos = new UserRepos();
            UserRepo repo = new UserRepo
            {
                name = "test"
            };
            repos.Repos = new System.Collections.Generic.List<UserRepo>();
            repos.Repos.Add(repo);
            var taskCompletion = new TaskCompletionSource<UserRepos>();
            taskCompletion.SetResult(null);

            _mockGitHubManager.Setup(m => m.GetUserGitHubRepo(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(taskCompletion.Task);

            var result = await userController.Index("SomePath");

            // Validate the result
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            if (viewResult != null)
            {
                Assert.Equal(viewResult.ViewName, "Error");
            }
        }

        [Fact]
        public async Task Test_Index_Missing_UserName()
        {
            var userController = new UserController(_mockGitHubManager.Object);

            var result = await userController.Index("");

            // Validate the result
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            if (viewResult != null)
            {
                Assert.Equal(viewResult.ViewName, "Error");
            }
        }
    }
}
