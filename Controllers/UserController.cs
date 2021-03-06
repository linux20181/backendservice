﻿using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Controllers.Infrastructure;
using API.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using API.Utils;
using API.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{

    [ApiController]

    public class UserController : ControllerBase
    {
        public IUserService userService;
        public ApplicationDbContext context;
        public UserController(IUserService _userService)
        {
            userService = _userService;
            context = new FactoryContext().Init();
        }
        [HttpGet("/api/User/{id}")]
        public User GetById(string id)
        {
            User user = new User();

            if (id == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                user = userService.GetById(new Guid(id));
            }
            return user;
        }
        [Authorize]
        [HttpPost("/api/GetAllUser")]
        public PagingDataSource<IEnumerable<User>> GetAllExpand(GetAllUser request)
        {
            var result = new PagingDataSource<IEnumerable<User>> { Total = 0 };
            result.Data = userService.GetAll(request.Expand).AsQueryable().ParseOData(request).ToList();
            result.Total = result.Data.Count();
            return result;
        }
        [Authorize]
        [HttpPost("/api/Register")]
        public void Register(User user)
        {
            User _user = new User();
            try
            {
                var hashsalt = Utils.Utils.EncryptPassword(user.Password);
                user.Password = hashsalt.Hash;
                user.Salt = hashsalt.Salt;
                userService.Create(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPost("/api/User")]
        public void Create(User user)
        {
            try
            {
                User _user = new User();
                if (user.Id != Guid.Empty)
                {
                    userService.Update(user);
                }
                else
                {
                    var hashsalt = Utils.Utils.EncryptPassword(user.Password);
                    user.Password = hashsalt.Hash;
                    user.Salt = hashsalt.Salt;
                    userService.Create(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("/api/Login")]
        public string Login(User user)
        {
            User _user = new User();
            try
            {
                _user = context.User.FirstOrDefault(u => u.UserName == user.UserName);
                if (_user != null)
                {

                    var results = Utils.Utils.VerifyPassword(user.Password, _user.Salt, _user.Password);
                    if (results)
                    {
                        return Authetication.GenerateJsonWebToken(_user);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpDelete("/api/User/{id}")]
        public void Remove(string id)
        {
            User user = new User();
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                user = userService.GetById(new Guid(id));
                userService.Delete(user);
            }
        }
    }

    public class GetAllUser : ODataRequest
    {
        public string[] Expand { set; get; }
    }
}



