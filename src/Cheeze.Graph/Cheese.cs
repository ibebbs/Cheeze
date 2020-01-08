using System;

namespace Cheeze.Graph
{
    public class Cheese
    {
        public Guid Id { get; set; }

        public Uri? Uri { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Available { get; set; }
    }
}