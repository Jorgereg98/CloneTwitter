using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Design_patterns.Observer
{
    public class MedidorCaracteres : ISubject
    {
        private int num_caracteres;
        private readonly IList _suscriptores;

        public int NumeroCaracteres
        {
            get => num_caracteres;

            set
            {
                if(num_caracteres != value)
                {
                    num_caracteres = value;
                    NotificarObservers();
                }
            }
        }

        public void RegistrarObserver(IObserver o)
        {
            if (!_suscriptores.Contains(o))
                _suscriptores.Add(o);
        }

        public void EliminarObserver(IObserver o)
        {
            if (_suscriptores.Contains(o))
                _suscriptores.Remove(o);
        }

        public void NotificarObservers()
        {
            // Creamos un array con el estado del Subject
            int[] valores = { num_caracteres};

            // Recorremos todos los objetos suscritos (observers)
            foreach (var o in _suscriptores)
            {
                // Invocamos el metodo Update de cada observer, pasandole el array con el estado
                // del subject como parametro.
                // Cada observer ya hara lo que estime necesario con esa informacion.
                var observer = (IObserver)o;
                observer.Update(valores);
            }
        }

        public MedidorCaracteres(int num_caracteres)
        {
            _suscriptores = new ArrayList();
            this.num_caracteres = num_caracteres;
        }


    }
}
