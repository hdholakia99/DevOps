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
    /// Operation Related to Order UserController
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class PortalController : ControllerBase
    {
        private readonly LoggingInfo loggingInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalController"/> class.
        /// </summary>
        /// <param name="loggingInfo">instance of applicationLogInfo.</param>
        public PortalController(LoggingInfo loggingInfo)
        {
            this.loggingInfo = loggingInfo;
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
                loggingInfo.APICallStartTime = DateTime.UtcNow;
                Response<string> responseDto = new Response<string>();
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                loggingInfo.RequestStatus = RequestStatus.Success.ToString();
                loggingInfo.APICallEndTime = DateTime.UtcNow;
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
