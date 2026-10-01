using Learning_ASP.NET_MVC_.Data;
using Learning_ASP.NET_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using Learning_ASP.NET_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP.NET_MVC_.Controllers;

[Authorize]
public class PostController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
    
    public PostController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    // READ
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(int? categoryId)
    {
        var postQuery = _context.Posts
            .Include(p => p.Category)
            .AsNoTracking()
            .AsQueryable();
        if (categoryId.HasValue)
        {
            postQuery = postQuery.Where(p => p.CategoryId == categoryId);
        }
        var post = await postQuery.ToListAsync();
        ViewBag.CategoryList = await _context.Categories
            .AsNoTracking()
            .ToListAsync();
        return View(post);
    }
    
    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var postViewModels = new PostViewModels();
        postViewModels.CategoryList = await _context.Categories
            .AsNoTracking()
            .Select(c =>
            new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name

            }).ToListAsync();
        return View(postViewModels);
    }
    
    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Create(PostViewModels postViewModels)
    {
        if (ModelState.IsValid)
        {
            if (postViewModels.ImageFiles != null)
            {
                var inputFilesExtension = Path.GetExtension(postViewModels.ImageFiles.FileName).ToLower();
                bool isAllowed = _allowedExtensions.Contains(inputFilesExtension);
            
                if (!isAllowed)
                {
                    ModelState.AddModelError("","Invalid image format, allow for .jpg, .jpeg, .png");
                
                    // ត្រូវទាញ Category មកវិញមុននឹង Return View
                    postViewModels.CategoryList = await _context.Categories
                        .AsNoTracking()
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                        .ToListAsync();
                    
                    return View(postViewModels);
                }
                postViewModels.PostData!.FeaturedImage = await UploadFileToFolder(postViewModels.ImageFiles);
            }
            await _context.Posts.AddAsync(postViewModels.PostData!);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        postViewModels.CategoryList = await _context.Categories
            .AsNoTracking()
            .Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToListAsync();
        return View(postViewModels);
        
    }

    private async Task<string> UploadFileToFolder(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return string.Empty; 
        }

        var inputFileExtension = Path.GetExtension(file.FileName);
        var fileName = Guid.NewGuid().ToString() + inputFileExtension;
        var wwwRootPath = _webHostEnvironment.WebRootPath;
        var imageFolderPath = Path.Combine(wwwRootPath, "images");
    
        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        var filePath = Path.Combine(imageFolderPath, fileName);
    
        try
        {
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error Uploading Images: " + ex.Message);
            return string.Empty; 
        }
    
        return "/images/" + fileName;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound("Not Found");
        }
        var post = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
        
    }
    
    [HttpPost]
    [Authorize]
    public JsonResult AddComment([FromBody]Comment comment)
    {
        comment.CommentDate = DateTime.Now;
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return Json(new
        {
            username =  comment.Username,
            commentDate = comment.CommentDate.ToString("MMM dd, yyyy HH:mm"),
            content = comment.Content
        });
    }

    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound("Not Found");
        }
        var postFromDb = await _context.Posts.FirstOrDefaultAsync(p=>p.Id == id);
        if (postFromDb == null)
        {
            return NotFound();
        }

        EditViewModels editViewModels = new EditViewModels
        {
            PostData = postFromDb,
            CategoryList = _context.Categories.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
        };
        return View(editViewModels);
    }

    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Edit(EditViewModels editViewModels)
    {
        if (!ModelState.IsValid)
        {
            return View(editViewModels);
        }
        var postFromDb = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p=>p.Id == editViewModels.PostData.Id);
        if (postFromDb == null)
        {
            return NotFound();
        }

        if (editViewModels.ImageFiles != null)
        {
            var inputFilesExtension = Path.GetExtension(editViewModels.ImageFiles.FileName).ToLower();
            bool isAllowed = _allowedExtensions.Contains(inputFilesExtension);
            if (!isAllowed)
            {
                ModelState.AddModelError("","Invalid image format, allow for .jpg, jpeg, .png");
                return  View(editViewModels);
            }
            var existFilesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images",Path.GetFileName(postFromDb.FeaturedImage!));
            if (System.IO.File.Exists(existFilesPath))
            {
                System.IO.File.Delete(existFilesPath);
            }

            editViewModels.PostData!.FeaturedImage = await UploadFileToFolder(editViewModels.ImageFiles);
        }
        else
        {
            editViewModels.PostData!.FeaturedImage =  postFromDb.FeaturedImage;
        }

        _context.Posts.Update(editViewModels.PostData);
        await _context.SaveChangesAsync();
        return  RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (postFromDb == null)
        {
            return NotFound("Not Found");
        }
        return View(postFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirm(int? id)
    {
        var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (postFromDb == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrWhiteSpace(postFromDb.FeaturedImage))
        {
            var existFilesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images",Path.GetFileName(postFromDb.FeaturedImage!));
            if (System.IO.File.Exists(existFilesPath))
            {
                System.IO.File.Delete(existFilesPath);
            }
        }
        _context.Posts.Remove(postFromDb);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
        
    
}