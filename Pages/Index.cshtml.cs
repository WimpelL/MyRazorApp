using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;
using MyRazorApp.Models;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Item> Items { get; set; } = new();

    [BindProperty]
    public string NewItem { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        Items = await _db.Items.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewItem))
        {
            _db.Items.Add(new Item { Name = NewItem });
            await _db.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
