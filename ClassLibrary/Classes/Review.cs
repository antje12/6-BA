﻿namespace ClassLibrary.Classes;

public class Review
{
    public Review(Guid id, Guid contractId, Guid creatorId, Guid targetId, string comment, int rating, ReviewType type)
    {
        Id = id;
        ContractId = contractId;
        CreatorId = creatorId;
        TargetId = targetId;
        Comment = comment;
        Rating = rating;
        Type = type;
    }
    
    public Review()
    {
        Id = Guid.Empty;
        ContractId = Guid.Empty;
        CreatorId = Guid.Empty;
        TargetId = Guid.Empty;
        Comment = String.Empty;
        Rating = 0;
        Type = ReviewType.All;
    }

    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Guid CreatorId { get; set; }
    public Guid TargetId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public ReviewType Type { get; set; }
}

public enum ReviewType
{
    Client,
    Provider,
    All
}