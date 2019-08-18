using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMessageEntity
    {
        bool Msgflag { get; set; }
        object Msgvalue { get; set; }
    }
}
