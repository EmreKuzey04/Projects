﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services.InMemoryCache
{
    public interface ICachableQuery
    {
        string CacheKey { get; }
        double CacheTime { get; }

    }
}
