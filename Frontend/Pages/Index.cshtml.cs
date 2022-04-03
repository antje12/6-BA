﻿using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserService _userService;
    private readonly IJobService _jobService;
    private readonly IOfferService _offerService;
    private readonly IContractService _contractService;

    public IEnumerable<User> Users { get; private set; }
    public IEnumerable<Category> Categories { get; private set; }
    public IEnumerable<Job> Jobs { get; private set; }
    public IEnumerable<Offer>? Offers { get; private set; }
    public IEnumerable<Contract>? Contracts { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, IUserService userService, IJobService jobService, IOfferService offerService)
    {
        _logger = logger;
        _userService = userService;
        _jobService = jobService;
        _offerService = offerService;
    }

    public void OnGet()
    {
        //Users = _userService.Get();
        Categories = _jobService.ListCategories();
        var data = new Filter(
            Guid.Parse("0ebbe367-300a-4c86-9070-d6e106d7e4b9"), 
            DateTime.Now.AddDays(-7), 
            DateTime.Now.AddDays(7), 
            "5000",
            "");
        Jobs = _jobService.ListJobs(data);
        Offers = TestOffers();
        Contracts = TestContracts();
    }

    // Create test offer and load it afterwards
    IEnumerable<Offer>? TestOffers()
    {

        _offerService.CreateOffer(new Offer(
                Guid.Empty, 
                Jobs.ToArray()[0].Id, 
                Users.ToArray()[0].Id, 
                400, 
                "2 Hours", 
                DateTime.Now));
        
        return _offerService.ListOffersForJob(Jobs.ToArray()[0].Id);
    }
    
    // Create test contracts and load it afterwards
    IEnumerable<Contract>? TestContracts()
    {

        _contractService.CreateContract(new Contract(
            Guid.Empty, 
            Jobs.ToArray()[0].Id, 
            Offers.ToArray()[0].Id, 
            Users.ToArray()[0].Id, 
            Users.ToArray()[1].Id, 
            DateTime.Now,
            State.Open));
        
        return _contractService.ListContracts(Jobs.ToArray()[0].Id);
    }
}
