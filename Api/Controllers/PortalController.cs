// <copyright file="PortalController.cs" company="MNX Global Logistics">
// Copyright (c) MNX Global Logistics. All rights reserved.
// </copyright>
// <summary> Operation Related to UserController Class.</summary>
namespace PortalController.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MNX.ConnectEcho.Common.Models;
    using MNX.ConnectEcho.Common.Models.Dto;
    using MNX.ConnectEcho.Common.Models.Enum;

    /// <summary>
    /// Operation Related to Order UserController.
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class PortalController : ControllerBase
    {
        private readonly LoggingInfo applicationLogInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalController"/> class.
        /// </summary>
        /// <param name="applicationLogInfo">instance of applicationLogInfo.</param>
        public PortalController(LoggingInfo applicationLogInfo)
        {
            this.applicationLogInfo = applicationLogInfo;
        }

        /// <summary>
        /// Ordered Event Processor Operation.
        /// </summary>
        /// <param name="request">jobGUID and taskGUID request param.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("PortalDetail")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                applicationLogInfo.APICallStartTime = DateTime.UtcNow;
                Response<string> responseDto = new Response<string>();
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                applicationLogInfo.RequestStatus = RequestStatus.Success.ToString();
                applicationLogInfo.APICallEndTime = DateTime.UtcNow;
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ordered Event Processor Operation.
        /// </summary>
        /// <param name="request">jobGUID and taskGUID request param.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("ViewPortalDetail")]
        public async Task<IActionResult> ViewPortalDetail()
        {
            try
            {
                applicationLogInfo.APICallStartTime = DateTime.UtcNow;
                Response<string> responseDto = new Response<string>();
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                applicationLogInfo.RequestStatus = RequestStatus.Success.ToString();
                applicationLogInfo.APICallEndTime = DateTime.UtcNow;
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}