﻿using HR_Management.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Contract.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}
