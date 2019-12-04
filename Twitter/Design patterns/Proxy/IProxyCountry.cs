using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.Design_patterns.Proxy
{
    public interface IProxyCountry
    {
        List<CountryObject> country();
    }
}
