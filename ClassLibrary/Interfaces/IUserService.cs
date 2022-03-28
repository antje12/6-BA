﻿using ClassLibrary.Classes;

namespace ClassLibrary.Interfaces;

public interface IUserService
{
    public IEnumerable<User> Get();
}