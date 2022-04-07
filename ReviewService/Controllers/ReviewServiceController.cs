using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Interfaces;

namespace ReviewService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewServiceController : ControllerBase, IReviewService
{
    private readonly ILogger<ReviewServiceController> _logger;
    private readonly IDataProvider _dataProvider;

    public ReviewServiceController(ILogger<ReviewServiceController> logger, IDataProvider dataProvider)
    {
        _logger = logger;
        _dataProvider = dataProvider;
    }

    [HttpGet("Get")]
    public bool Get()
    {
        return true;
    }

    [HttpPost("CreateReview")]
    public Review? CreateReview([FromBody]Review review)
    {
        return _dataProvider.Create(review);
    }

    [HttpGet("GetReviewById/{id}")]
    public Review? GetReviewById(Guid id)
    {
        return _dataProvider.Get(id);
    }

    [HttpPost("ListReviews/{id}")]
    public IEnumerable<Review> ListReviews(Guid userId, [FromBody]ReviewType type)
    {
        var reviews = _dataProvider.List(userId);
        switch (type)
        {
            case ReviewType.All:
                return reviews;
            default:
                return reviews.Where(review => review.Type == type).ToList();
        }
    }

    [HttpPost("GetRating/{id}")]
    public decimal GetRating(Guid userId, [FromBody]ReviewType type)
    {
        var reviews = _dataProvider.List(userId);
        switch (type)
        {
            case ReviewType.All:
                break;
            default:
                reviews = reviews.Where(review => review.Type == type).ToList();
                break;
        }
        
        if (reviews.Any())
        {
            var total = reviews.Sum(x => x.Rating);
            var score = (decimal) total / reviews.Count;
            return Math.Round(score, 2);
        }

        return 0;
    }

    [HttpPut("UpdateReview/{id}")]
    public Review? UpdateReview(Review review)
    {
        return _dataProvider.Update(review);
    }

    [HttpDelete("DeleteReview/{id}")]
    public bool DeleteReview(Guid id)
    {
        return _dataProvider.Delete(id);
    }
}
