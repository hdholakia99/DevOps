// <copyright file="ServiceCollectionExtensions.cs" company="MNX Global Logistics">
// Copyright (c) MNX Global Logistics. All rights reserved.
// </copyright>
// <summary>Service Collection Extension Class for resolve dependency.</summary>

namespace DevOps.Api
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MNX.ConnectEcho.Common.Models;

    /// <summary>
    /// Partial static Class ServiceCollectionExtensions : Class includes extension method related to service.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Dependency Injection.
        /// </summary>
        /// <param name="services">collection of services.</param>
        public static void ResolveDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(HttpClient), typeof(HttpClient));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<LoggingInfo>();
            services.AddScoped<CurrentUserContext>();
        }
    }
}
