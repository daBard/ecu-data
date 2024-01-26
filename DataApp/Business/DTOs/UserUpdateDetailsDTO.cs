﻿namespace Business.DTOs;

public class UserUpdateDetailsDTO
{
    public Guid Guid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
