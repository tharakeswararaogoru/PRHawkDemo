using PRHawkDemo.Models;
using Services.Manager;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace PRHawkDemo.Controllers
{
    /// <summary>
    /// User controller which displays all the public repository details
    /// </summary>
    public class UserController : Controller
    {
        
        private IGitHubManager GitHubManager;
        private int PerPageCount = 2;
        private UserRepos userAvailableRepos = new UserRepos();

        /// <summary>
        /// Constructor to Inject dependent service
        /// </summary>
        /// <param name="gitHubManager"></param>
        public UserController(IGitHubManager gitHubManager)
        {
            this.GitHubManager = gitHubManager;
        }

        // GET: /user/{username}
        /// <summary>
        /// Default Action to view user repo details
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page">pagination params</param>
        /// <param name="per_page">pagination params</param>
        /// <param name="gridPage">internal parameter to handle pagination in the page</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string username, int page = 1, int per_page=2, int gridPage=1)
        {
            if (string.IsNullOrEmpty(username))
            {
                // Username is not passed
                return View("Error", new HandleErrorInfo(new Exception("Please try with valid route. like /user/{username}"), "User", "Index"));
            }
            try
            {
                IPagedList<UserRepo> model = null;
                // Fetches all user repos by username
                userAvailableRepos = await GitHubManager.GetUserGitHubRepo(username, page, per_page);
                if (userAvailableRepos == null || userAvailableRepos.Repos == null)
                {
                    return View("Error", new HandleErrorInfo(new Exception("Internal Error"), "User", "Index"));
                }

                // Pagination logic
                model = userAvailableRepos.Repos.ToPagedList(gridPage, 1);
                ViewBag.Page = page;
                ViewBag.PerPage = per_page;
                return View("Index", model);
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new Exception("Internal Error"), "User", "Index"));
            }
        }   
    }
}