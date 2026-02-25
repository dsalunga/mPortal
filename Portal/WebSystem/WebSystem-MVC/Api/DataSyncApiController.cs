using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy WCF DataSync.svc handler service.
    /// Requires authentication for data synchronization operations.
    /// </summary>
    [ApiController]
    [Route("api/datasync")]
    [Authorize]
    public class DataSyncApiController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public DataSyncApiController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("user")]
        public IActionResult GetObject()
        {
            return Ok(new WebUser());
        }

        [HttpGet("users/{objectId:int}")]
        public IActionResult GetObjectList(int objectId)
        {
            switch (objectId)
            {
                case WebObjects.WebUser:
                    return Ok(WebUser.GetList().ToList<WebUser>());
            }

            return Ok((object)null);
        }

        [HttpGet("bindings")]
        public IActionResult GetBindings()
        {
            var bindings = new List<WebSiteIdentity>();
            bindings.AddRange(WebSiteIdentity.Provider.GetList());
            return Ok(bindings);
        }

        [HttpGet("files")]
        public IActionResult GetFiles([FromQuery] string relativePath = "~", [FromQuery] bool recursive = true)
        {
            var items = new List<FileSyncInfo>();
            var rootPath = _env.ContentRootPath;
            var folder = FileHelper.Combine(rootPath, relativePath.Replace("~", ""), Path.DirectorySeparatorChar);

            var rootDir = new DirectoryInfo(folder);
            if (rootDir.Exists)
            {
                var files = rootDir.EnumerateFiles();
                foreach (var fileInfo in files)
                {
                    var file = new FileSyncInfo();
                    file.RelativePath = fileInfo.Name;
                    file.DateModified = fileInfo.LastWriteTime;
                    file.Size = fileInfo.Length;
                    items.Add(file);
                }
            }

            return Ok(items);
        }

        [HttpGet("user/{userId:int}/complete")]
        public IActionResult GetUserComplete(int userId)
        {
            var user = WebUser.Get(userId);
            return Ok(user != null ? new WebUserContainer(user, RemoteItemTypes.REMOTE) : null);
        }

        [HttpPost("user/complete")]
        public IActionResult SetUserComplete([FromBody] WebUserContainer container)
        {
            if (container != null && container.User != null
                && (container.ItemType == RemoteItemTypes.REMOTE || container.ItemType == RemoteItemTypes.IDENTICAL))
            {
                var existingGroups = new List<WebGroup>();
                var existingAddresses = new List<WebAddress>();

                var user = WebUser.Get(container.User.UserName);
                if (user == null)
                {
                    user = container.User;
                    user.Id = -1;
                    user.Update();
                }
                else
                {
                    existingAddresses.AddRange(user.Addresses);
                    existingGroups.AddRange(user.Groups);
                }

                foreach (var remoteGroup in container.Groups)
                {
                    if (existingGroups.Find(i => i.Name.Equals(remoteGroup.Name, StringComparison.InvariantCultureIgnoreCase)) == null)
                        user.AddToGroup(remoteGroup.Name);
                }

                foreach (var remoteAddress in container.Addresses)
                {
                    if (existingAddresses.Find(i => i.Tag.Equals(remoteAddress.Tag, StringComparison.InvariantCultureIgnoreCase)) == null)
                    {
                        remoteAddress.Id = -1;
                        remoteAddress.RecordId = user.Id;
                        remoteAddress.Update();
                    }
                }
            }

            return Ok();
        }
    }
}
