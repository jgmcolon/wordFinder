using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordFinder
{
    public interface IWordFinder
    {
       
        public IEnumerable<string> Find(IEnumerable<string> wordstream);
        
    }
}
