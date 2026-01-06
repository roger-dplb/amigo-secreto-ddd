using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace AmigoSecreto.Domain.Entities
{
    public class Gift
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}