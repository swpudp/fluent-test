﻿using FluentTest.UserCenter.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.UserCenter.Service.Contract
{
    public interface IUserService
    {
        User GetById(string id);
    }
}
