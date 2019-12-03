using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twitter.Design_patterns.Observer
{
    public class ObserverAlerta : IObserver
    {
        private static readonly int MAX_CARACTERES = 280;
        private int num_caracteres;
        private ISubject subject;
        public static string color = "verde";
        //public static bool b = true;

        public ObserverAlerta(ISubject subject)
        {
            this.subject = subject;
            subject.RegistrarObserver(this);
        }

        public void Update(object o)
        {
            // Comprobamos el tipo del objeto recibido como parametro
            int[] arrayInt = null;
            if (o.GetType() == typeof(int[]))
                arrayInt = (int[])o;

            // Si es del tipo esperado (int[]) y del tamano esperado (1), actualizamos los
            // atributos
            if ((arrayInt != null) && (arrayInt.Length == 1))
            {
                num_caracteres = arrayInt[0];
                // Comprobamos que los caracteres no exceden el limite
                ComprobarTweet();
            }
        }

        private void ComprobarTweet()
        {
            if(num_caracteres == 0)
            {

            }else if (num_caracteres <= MAX_CARACTERES)
            {
                color = "verde";
                //b = true;
                MessageBox.Show($"NO HA SUPERADO EL NUMERO LÍMITE DE CARACTERES : {num_caracteres}/{MAX_CARACTERES}");
            }
            else if (num_caracteres > MAX_CARACTERES)
            {
                color = "rojo";
                //b = false;
                MessageBox.Show($"HA SUPERADO EL NUMERO LÍMITE DE CARACTERES : {num_caracteres}/{MAX_CARACTERES}");
            }
        }

    }
}
