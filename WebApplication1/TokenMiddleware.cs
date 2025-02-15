﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;
    string pattern;
    public TokenMiddleware(RequestDelegate next, string pattern)
    {
        this._next = next;
        this.pattern = pattern;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
        if (token != pattern)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}