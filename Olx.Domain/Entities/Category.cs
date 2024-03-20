using Olx.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; }
    public long ParentId { get; set; }
}
