using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using Kame.Management.Core.Entity;
using Kame.Management.Core.Services;
using Kame.Core.Entity.Log;

namespace Kame.Management.Api.Controllers
{
    [ApiController]
    [Route("api/deployproject")]
    public class DeployProjectController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public DeployProjectController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet("getbyname/{projectName}")]
        [Authorize("user")]
        public ActionResult<dynamic> GetByName(string projectName)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                return null;
            }

            IKameDbContext dbContext = (IKameDbContext)HttpContext.RequestServices.GetService(typeof(IKameDbContext));
            DeployConfig deployConfig = dbContext.FindDeployConfigByName(projectName);

            if (deployConfig == null)
            {
                return null;
            }
            else
            {
                return deployConfig.DeployProject;
            }
        }

        [HttpGet("getbyid/{projectId}")]
        [Authorize("user")]
        public ActionResult<dynamic> GetById(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                return null;
            }

            IKameDbContext dbContext = (IKameDbContext)HttpContext.RequestServices.GetService(typeof(IKameDbContext));
            DeployConfig deployConfig = dbContext.FindDeployConfigByName(projectId);

            if (deployConfig == null)
            {
                return null;
            }
            else
            {
                return deployConfig.DeployProject;
            }
        }

        [HttpPost("executionLogById/{projectId}")]
        [Authorize("user")]
        public ActionResult<dynamic> PostExecutionLogById(string projectId, [FromBody] DeployLogXML log)
        {
            IKameDbContext dbContext = (IKameDbContext)HttpContext.RequestServices.GetService(typeof(IKameDbContext));

            this.SaveExecutionLog(dbContext, dbContext.FindDeployConfigById(projectId), log);

            return null;
        }

        [HttpPost("executionLogByName/{projectName}")]
        [Authorize("user")]
        public ActionResult<dynamic> PostExecutionLogByName(string projectName, [FromBody] DeployLogXML log)
        {
            IKameDbContext dbContext = (IKameDbContext)HttpContext.RequestServices.GetService(typeof(IKameDbContext));

            this.SaveExecutionLog(dbContext, dbContext.FindDeployConfigByName(projectName), log);

            return null;
        }

        private void SaveExecutionLog(IKameDbContext dbContex, DeployConfig deployConfig, DeployLogXML log)
        {
            log.ProjectId = deployConfig.Id;
            dbContex.CheckDeployExecutionLogTable(true);
            dbContex.SaveDeployExecution(log);
        }
    }
}
