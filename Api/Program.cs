// <copyright file="Program.cs" company="MNX Global Logistics">
// Copyright (c) MNX Global Logistics. All rights reserved.
// </copyright>
// <summary> Starting point of Api</summary>

namespace DevOps.Api
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Class Program : Class where we create Web host application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Calls method to build  web host app.
        /// </summary>
        /// <param name="args">arguments. </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method which creates webhost builder and Setup IIS itegration, Kestrel.
        /// </summary>
        /// <param name="args">argument.</param>
        /// <returns>Returns the Web Host buider.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
