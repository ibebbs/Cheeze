using System;
using System.Collections.Generic;
using System.Linq;

namespace Cheeze.Inventory
{
    public class Request
    {
        public IEnumerable<Guid> Ids { get; set; } = Enumerable.Empty<Guid>();
    }
}