﻿using ClassLibrary.Classes;
using Moq;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using ThrowawayDb.MySql;
using UserService.Classes;
using UserService.Controllers;
using UserService.Data_Providers;
using UserService.Interfaces;

namespace UserService.Tests;

[TestFixture]
public class Test
{
    private ThrowawayDatabase database;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        database = ThrowawayDatabase.Create(
            "root",
            "root",
            "localhost"
        );

        using var con = new MySqlConnection(database.ConnectionString);
        con.Open();
        var sql = @"CREATE TABLE User (
                      ID CHAR(36) PRIMARY KEY,
                      Email NVARCHAR(500) NOT NULL,
                      PasswordSalt NVARCHAR(500) NOT NULL,
                      PasswordHash NVARCHAR(500) NOT NULL,
                      Firstname NVARCHAR(500) NOT NULL,
                      Lastname NVARCHAR(500) NOT NULL,
                      PhoneNumber NVARCHAR(500) NOT NULL,
                      IsServiceProvider BIT NOT NULL
                    );";
        using var cmd = new MySqlCommand(sql, con);
        cmd.ExecuteNonQuery();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        database.Dispose();
    }
    
    [SetUp]
    public void Setup()
    {
        database.CreateSnapshot();
    }

    [TearDown]
    public void Cleanup()
    {
        database.RestoreSnapshot();
    }

    [Test]
    public void CreateUserTest()
    {
        var input = new UserCreator(Guid.NewGuid(), "email", "first name", "last name", "12345678", false, "password");

        var logger = new Mock<ILogger<UserServiceController>>();
        var dataProvider = new MySQLDataProvider();
        dataProvider.setConnectionString(database.ConnectionString);

        var service = new UserServiceController(logger.Object, dataProvider);
        var output = service.CreateUser(input);

        Assert.AreSame(input, output);
    }

    [Test]
    public void ValidateUserTest()
    {
        var user = new UserCreator(Guid.NewGuid(), "email", "first name", "last name", "12345678", false, "password");
        var input = new LoginRequest(user.Email, user.Password);
        
        var logger = new Mock<ILogger<UserServiceController>>();
        var dataProvider = new MySQLDataProvider();
        dataProvider.setConnectionString(database.ConnectionString);

        var service = new UserServiceController(logger.Object, dataProvider);
        service.CreateUser(user);
        var output = service.ValidateUser(input);

        Assert.AreEqual(user.Id, output.Id);
        Assert.AreEqual(user.Email, output.Email);
        Assert.AreEqual(user.FirstName, output.FirstName);
        Assert.AreEqual(user.LastName, output.LastName);
        Assert.AreEqual(user.PhoneNumber, output.PhoneNumber);
        Assert.AreEqual(user.IsServiceProvider, output.IsServiceProvider);
    }
    
    [Test]
    public void ChangePasswordTest()
    {
        var user = new UserCreator(Guid.NewGuid(), "email", "first name", "last name", "12345678", false, "password");
        var input = new PasswordRequest(user.Id, user.Password, "more secret");
        
        var logger = new Mock<ILogger<UserServiceController>>();
        var dataProvider = new MySQLDataProvider();
        dataProvider.setConnectionString(database.ConnectionString);

        var service = new UserServiceController(logger.Object, dataProvider);
        service.CreateUser(user);
        input.UserId = user.Id;
        var output = service.ChangePassword(input);

        Assert.AreEqual(true, output);
    }
}