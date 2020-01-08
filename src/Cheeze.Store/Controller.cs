using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cheeze.Store
{
    [Route("api/store")]
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cheese>), (int)HttpStatusCode.OK)]
        public Task<ActionResult<IEnumerable<Cheese>>> Get()
        {
            var result = new[]
            {
                new Cheese
                {
                    Id = Guid.Parse("1468841a-5fbe-41c5-83b3-ab136b7ae70c"),
                    Name = "API Cheese"
                }
            };

            return Task.FromResult<ActionResult<IEnumerable<Cheese>>>(Ok(result));
        }
    }
}