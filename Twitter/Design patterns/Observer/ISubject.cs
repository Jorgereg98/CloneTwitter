using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Design_patterns.Observer
{
    public interface ISubject
    {
        void RegistrarObserver(IObserver o);
        void EliminarObserver(IObserver o);
        void NotificarObservers();
    }
}
