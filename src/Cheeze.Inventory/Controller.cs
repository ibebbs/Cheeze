using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cheeze.Inventory
{
    [Route("api/inventory")]
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        private static readonly Random Random = new Random();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(uint), (int)HttpStatusCode.OK)]
        public Task<ActionResult<uint>> Get(Guid id)
        {
            return Task.FromResult<ActionResult<uint>>(Ok((uint)Random.Next(10)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Available>), (int)HttpStatusCode.OK)]
        public Task<ActionResult<IEnumerable<Available>>> Post([FromBody] Request request)
        {
            var available = request.Ids
                .Select(id => new Available { Id = id, Quantity = (uint)Random.Next(10) }) 
                .ToArray();
            
            return Task.FromResult<ActionResult<IEnumerable<Available>>>(Ok(available));
        }
    }
}