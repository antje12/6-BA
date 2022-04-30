using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Frontend.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Frontend.Pages;

public class ListJobsModel : LayoutModel
{
    private readonly ILogger<ListJobsModel> _logger;
    
    private readonly IJobService _jobService;
    private readonly IUserService _userService;
    
    public Dictionary<Type, bool> ServiceStatus { get; private set; }

    public IEnumerable<Job> Jobs { get; private set; }
    
    public IEnumerable<User> Clients { get; private set; }
    
    [BindProperty]
    public Filter CustomFilter { get; set; }

    public Filter Filter { get; set; }
    public SelectList Categories { get; set; }
    

    public ListJobsModel(ILogger<ListJobsModel> logger,
        IJobService jobService,
        IUserService userService)
    {
        _logger = logger;
        _jobService = jobService;
        _userService = userService;
        ServiceStatus = new Dictionary<Type, bool>();
    }
    
    public IActionResult OnGet()
    {
        return OnGetCategory(null);
    }

    public IActionResult OnGetCategory(Guid? categoryId)
    {
        Instantiate();
        
        Filter = new Filter();
        
        Jobs = _jobService.ListJobs(Filter);
        Categories = new SelectList(_jobService.ListCategories(), nameof(Category.Id), nameof(Category.Name));
        if (categoryId != null)
        {
            //TODO: Should be done before listing jobs, but nvm, we dont use this
            CustomFilter.CategoryId = categoryId;
        }
        
        var clientIds = Jobs.Select(job => job.ClientId).ToList();
        Clients = _userService.ListUsersByIDs(clientIds);
        
        return Page();
    }

    public IActionResult OnPost()
    {
        Instantiate();
        
        Jobs = _jobService.ListJobs(CustomFilter);
        Categories = new SelectList(_jobService.ListCategories(), nameof(Category.Id), nameof(Category.Name));

        var clientIds = Jobs.Select(job => job.ClientId).ToList();
        Clients = _userService.ListUsersByIDs(clientIds);

        return Page();
    }
}