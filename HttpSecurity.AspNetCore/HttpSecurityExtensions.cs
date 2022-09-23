﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HttpSecurity.AspNetCore;


/// <summary>
/// Static extensions.
/// </summary>
public static class HttpSecurityExtensions
{
    /// <summary>
    /// Adds the http security headers service as a scoped service.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddHttpsSecurityHeaders(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        return serviceCollection.AddScoped<HttpSecurityService>();
    }


    /// <summary>
    /// Adds the http security headers service as a scoped service.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddHttpsSecurityHeaders(this IServiceCollection serviceCollection, Action<HttpSecurityOptions> configureOptions)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        HttpSecurityOptions options = new();

        configureOptions.Invoke(options);

        return serviceCollection.AddScoped(serviceProvider => new HttpSecurityService(options));
    }


    /// <summary>
    /// Adds security headers and policies to the context response. Place this immediately after <see cref="HttpsPolicyBuilderExtensions.UseHttpsRedirection()"/>
    /// to enable this middleware to over-ride with your values any headers that may have been set by subsequent middleware.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseHttpSecurityHeaders(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<HttpSecurityMiddleware>();
    }


    /// <summary>
    /// Adds security headers and policies to the context response. Place this immediately after <see cref="HttpsPolicyBuilderExtensions.UseHttpsRedirection()"/>
    /// to enable this middleware to over-ride with your values any headers that may have been set by subsequent middleware.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseHttpSecurityHeaders(this IApplicationBuilder app, HttpSecurityOptions options)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<HttpSecurityMiddleware>(Options.Create(options));
    }
}
