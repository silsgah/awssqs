﻿namespace Customers.Api.Contracts.Messages;

public class CusomerCreated
{
    public required Guid Id { get; init; } = Guid.NewGuid();

    public required string GitHubUsername { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required DateTime DateOfBirth { get; init; }
}


public class CusomerUpdated
{
    public required Guid Id { get; init; } = Guid.NewGuid();

    public required string GitHubUsername { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required DateTime DateOfBirth { get; init; }
}

public class CustomerDeleted
{
    public required Guid Id { get; init; }
}

