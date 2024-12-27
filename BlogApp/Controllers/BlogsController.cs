using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace BlogApp.Controllers
{
    public class BlogsController : MvcController
    {
        // Service injections:
        private readonly IService<Blog, BlogModel> _blogService;
        private readonly IService<User, UserModel> _userService;
        private readonly ITagService _tagService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public BlogsController(
            IService<Blog, BlogModel> blogService
            , IService<User, UserModel> userService
            , ITagService tagService

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _blogService = blogService;
            _userService = userService;
            _tagService = tagService;
            

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Blogs
        public IActionResult Index()
        {
            var list = _blogService.Query().ToList();

            ViewBag.Tags = _tagService.Query().Select(t => t.Record.Name).ToList();
            ViewBag.SelectedTags = new string[] { };

            return View(list);
        }

        // GET: Blogs with selected tags
        [HttpGet]
        public IActionResult Index([FromQuery] string[] selectedTags)
        {
            // Split any comma-separated tags into separate values
            var tagsArray = selectedTags?
                .SelectMany(t => t.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(t => t.Trim())
                .ToArray() ?? Array.Empty<string>();

            Console.WriteLine($"Processing tags: {string.Join(", ", tagsArray)}"); // Debug log

            var query = _blogService.Query();

            if (tagsArray.Length > 0)
            {
                var normalizedSelectedTags = tagsArray
                    .Select(t => t.Trim().ToLower())
                    .Distinct()
                    .ToArray();

                Console.WriteLine($"Normalized tags: {string.Join(", ", normalizedSelectedTags)}"); // Debug log

                query = query.Where(b =>
                    b.Record.BlogTags.Count(bt =>
                        normalizedSelectedTags.Contains(bt.Tag.Name.Trim().ToLower()))
                    == normalizedSelectedTags.Length);
            }

            var list = query.ToList();

            Console.WriteLine($"Found {list.Count} blogs"); // Debug log

            ViewBag.Tags = _tagService.Query().Select(t => t.Record.Name).ToList();
            ViewBag.SelectedTags = tagsArray;

            return View(list);
        }

        // GET: Blogs/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _blogService.Query()
                        .Where(q => q.Record.Id == id)
                        .Select(q => new BlogModel
                        {
                            Record = q.Record,
                            Tags = q.Record.BlogTags.Select(bt => bt.Tag.Name).ToList()
                        })
                        .SingleOrDefault();

            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["UserId"] = new SelectList(_userService.Query().Select(u => u.Record).ToList(),"Id", "UserName");
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Blogs/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                var tagNames = blog.TagNames?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(t => t.Trim())
                                         .ToList() ?? new List<string>();

                var tags = new List<Tag>();
                foreach (var tagName in tagNames)
                {
                    var tagModel = _tagService.Query().FirstOrDefault(t => t.Name == tagName);
                    Tag tag;
                    if (tagModel == null)
                    {
                        tag = new Tag { Name = tagName };
                        _tagService.Create(tag);
                    }
                    else
                    {
                        tag = tagModel.Record;
                    }
                    tags.Add(tag);
                }

                // Create the blog
                var newBlog = new Blog
                {
                    Title = blog.Title,
                    Content = blog.Content,
                    Rating = blog.Rating,
                    PublishDate = DateTime.Now,
                    UserId = blog.UserId,
                };

                newBlog.BlogTags = tags.Select(t => new BlogTag { TagId = t.Id, BlogId = newBlog.Id }).ToList();

                // Insert item service logic:
                var result = _blogService.Create(newBlog);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = newBlog.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            SetViewData();
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public IActionResult Edit(int id)
        {
            var item = _blogService.Query()
                       .Where(q => q.Record.Id == id)
                       .Select(q => new BlogModel
                       {
                           Record = q.Record,
                           Tags = q.Record.BlogTags.Select(bt => bt.Tag.Name).ToList(),
                           TagNames = string.Join(",", q.Record.BlogTags.Select(bt => bt.Tag.Name))
                       })
                       .SingleOrDefault();
            SetViewData();
            return View(item);
        }

        // POST: Blogs/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                // Update the blog record
                var existingBlog = _blogService.Query().SingleOrDefault(q => q.Record.Id == blog.Record.Id);
                if (existingBlog != null)
                {
                    existingBlog.Record.Title = blog.Title;
                    existingBlog.Record.Content = blog.Content;
                    existingBlog.Record.Rating = blog.Rating;
                    existingBlog.Record.PublishDate = blog.PublishDate;
                    existingBlog.Record.UserId = blog.UserId;

                    // Update tags
                    var tagNames = blog.TagNames?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                             .Select(t => t.Trim())
                                             .ToList() ?? new List<string>();

                    var tags = new List<Tag>();
                    foreach (var tagName in tagNames)
                    {
                        var tagModel = _tagService.Query().AsEnumerable().FirstOrDefault(t => t.Name == tagName);
                        Tag tag;
                        if (tagModel == null)
                        {
                            tag = new Tag { Name = tagName };
                            _tagService.Update(tag);
                        }
                        else
                        {
                            tag = tagModel.Record;
                        }
                        tags.Add(tag);
                    }

                    existingBlog.Record.BlogTags = tags.Select(t => new BlogTag { TagId = t.Id, BlogId = existingBlog.Record.Id }).ToList();

                    // Update item service logic:
                    var result = _blogService.Update(existingBlog.Record);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Details), new { id = blog.Record.Id });
                    }
                    ModelState.AddModelError("", result.Message);
                }
            }
            SetViewData();
            return View(blog);
        }

        // GET: Blogs/Delete/5
        [Authorize]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _blogService.Query().Where(q => q.Record.Id == id)
                       .Select(q => new BlogModel
                       {
                           Record = q.Record,
                           Tags = q.Record.BlogTags.Select(bt => bt.Tag.Name).ToList(),
                           TagNames = string.Join(",", q.Record.BlogTags.Select(bt => bt.Tag.Name))
                       })
                       .SingleOrDefault();
            return View(item);
        }

        // POST: Blogs/Delete
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _blogService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
