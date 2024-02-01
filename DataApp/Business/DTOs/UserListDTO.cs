﻿using Business.Interfaces;

namespace Business.DTOs;

public class UserListDTO : IUserListDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
