﻿using Isopoh.Cryptography.Argon2;
using WebAppAspLayered.DAL.Repositories;
using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.DL.Enums;

namespace WebAppAspLayered.BLL.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Register(User user)
    {
        if (_userRepository.ExistByEmail(user.Email))
        {
            throw new Exception($"User with email {user.Email} already exists");
        }

        user.Role = UserRole.User;
        user.Password = Argon2.Hash(user.Password);

        _userRepository.Add(user);
    }

    public User Login(string email, string password)
    {
        User? user = _userRepository.GetUserByEmail(email) ?? throw new Exception($"User with email {email} not found");

        if (!Argon2.Verify(user.Password, password))
        {
            throw new Exception($"Wrong password");
        }

        return user;
    }
}
