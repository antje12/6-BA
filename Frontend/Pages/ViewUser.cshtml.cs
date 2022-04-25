using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Frontend.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Pages;

public class ViewUserModel : LayoutModel
{
    private readonly ILogger<ViewUserModel> _logger;
    
    private readonly IUserService _userService;
    private readonly IJobService _jobService;
    private readonly IContractService _contractService;
    private readonly IReviewService _reviewService;

    public Dictionary<Type, bool> ServiceStatus { get; private set; }

    public User? Client { get; private set; }
    public IEnumerable<Job> Jobs { get; private set; }
    public IEnumerable<Contract> OpenContracts { get; private set; }
    public IEnumerable<Contract> ClosedContracts { get; private set; }
    public IEnumerable<Review> ReviewsAsClient { get; private set; }
    public IEnumerable<Review> ReviewsAsProvider { get; private set; }
    
    public ViewUserModel(ILogger<ViewUserModel> logger,
        IJobService jobService,
        IUserService userService,
        IContractService contractService,
        IReviewService reviewService)
    {
        _logger = logger;
        _jobService = jobService;
        _userService = userService;
        _contractService = contractService;
        _reviewService = reviewService;
        ServiceStatus = new Dictionary<Type, bool>();
    }
    
    public IActionResult OnGet()
    {
        Instantiate();
        if (!SessionLoggedIn) return RedirectToPage("Login");
        
        Client = _userService.GetUserById(new Guid(HttpContext.Session.GetString(SessionIdKey)));
        Jobs = _jobService.ListJobsByUser(Client.Id);
        
        var openContracts = new List<Contract>();
        var closedContracts = new List<Contract>();
        foreach (var contract in _contractService.ListContracts(Client.Id))
        {
            if(contract.ContractState == State.Open) openContracts.Add(contract);
            else closedContracts.Add(contract);
        }
        OpenContracts = openContracts;
        ClosedContracts = closedContracts;
        
        ReviewsAsClient = _reviewService.ListReviews(Client.Id, ReviewType.Client);
        ReviewsAsProvider = _reviewService.ListReviews(Client.Id, ReviewType.Provider);

        return Page();
    }
}