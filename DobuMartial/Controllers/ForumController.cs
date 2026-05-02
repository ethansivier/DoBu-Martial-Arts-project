using DobuMartial_project.Data;
using DobuMartial_project.Models;
using DobuMartial_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Configuration;

namespace DobuMartial_project.Controllers
{
    public class ForumController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DBInfoGrabber _dbGrabber;
        private readonly ApplicationDbContext _context;

        public ForumController(UserManager<User> userManager, DBInfoGrabber dbGrabber, ApplicationDbContext context)
        {
            _userManager = userManager;
            _dbGrabber = dbGrabber;
            _context = context;
        }
        public IActionResult ErrorRedirect(string error, string action, string controller, string fragment = "")
        {
            TempData["Error"] = error;
            return RedirectToAction(action, controller, fragment);
        }
        public async Task<IActionResult> Index(ForumModel model)
        {
            model.Posts = await _dbGrabber.GetAllDBForumPosts();
            return View(model);
        }

        public async Task<IActionResult> CreatePost(ForumModel model)
        {
            ForumPost post = new ForumPost();
            post.PostTitle = model.PostTitle;
            post.PostBody = model.PostBody;

            User? idUser = await _userManager.GetUserAsync(HttpContext.User);
            if (idUser == null) { return ErrorRedirect("Could not find you.", "Index", "Forum"); }
            User? dbUser = await _dbGrabber.GetDBUser(idUser);
            if (dbUser == null) { return ErrorRedirect("Could not find you.", "Index", "Forum"); }

            post.Owner = dbUser;

            _context.ForumPosts.Add(post);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PostView(PostViewModel model, int postId)
        {
            model.Post = await _dbGrabber.GetDBForumPost(postId);
            if (model.Post == null) { return ErrorRedirect("Could not find the post in our database", "Index", "Forum"); }

            User? idUser = await _userManager.GetUserAsync(HttpContext.User);
            if (idUser != null) 
            {
                User? dbUser = await _dbGrabber.GetDBUser(idUser);
                if (dbUser != null) 
                {
                    model.UserId = idUser.Id;
                }
            }

            return View(model);
        }

        public async Task<IActionResult> PostComment(PostViewModel model, int postId)
        {
            model.Post = await _dbGrabber.GetDBForumPost(postId);
            if (model.Post == null) { return ErrorRedirect("Could not find the post", "Index", "Forum"); }
            ForumComment comment = new ForumComment();
            User? idUser = await _userManager.GetUserAsync(HttpContext.User);
            if (idUser == null) { return ErrorRedirect("Could not find you.", "Index", "Forum"); }

            comment.Comment = model.Comment;
            comment.UserId = idUser.Id;
            model.Post.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("PostView", "Forum", new { postID = postId });
        }

        public async Task<IActionResult> PostRemove(int postId)
        {
            ForumPost? post = await _dbGrabber.GetDBForumPost(postId);
            if (post == null) { return ErrorRedirect("Could not find the post", "Index", "Forum"); }
            _context.ForumPosts.Remove(post);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Successfully removed post.";
            return RedirectToAction("Index", "Forum");
        }

        public async Task EditComment(int commentId, string newText, int postId)
        {
            ForumPost? post = await _dbGrabber.GetDBForumPost(postId);
            if (post == null) { return; }
            ForumComment? comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) { return; }
            comment.Comment = newText;
            await _context.SaveChangesAsync();

        }

        public async Task<IActionResult> RemoveComment(int commentId, int postId)
        {
            ForumPost? post = await _dbGrabber.GetDBForumPost(postId);
            if (post == null) { return ErrorRedirect("Could not successfully find the post", "Index", "Forum"); }
            ForumComment? comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) { return ErrorRedirect("Could not successfully find the comment", "Index", "Forum"); }
            post.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("PostView", "Forum", new { postID = postId });
        }
    }
}
