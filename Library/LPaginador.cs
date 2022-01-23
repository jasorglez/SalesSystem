using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp_Sales_System.Library
{
    public class LPaginador<T>
    {
        //cantidad de resultados por pagina;
        private int pagi_cuantos = 10;
        //cantidad de enlaces que se mostraran como maximo en la barra de navegacion
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;
        //definimos que ira en el enlace a la pagina anterior
        private String pagi_nav_anterior = " &laquo; Anterior ";
        //definimos que ira en el enlace a la pagina siguiente
        private string pagi_nav_siguiente = "Siguiente &raquo; ";
        //definimos que ira en el enlace a la pagina siguiente
        private String pagi_nav_primera = " &laquo; Primero ";
        private string pagi_nav_ultima = "Ultimo &raquo; ";
        private String pagi_navegacion = null;

        public object[] paginador(List<T> table, int pagina, int registros, String area, String controller, 
            String action, String host)
        {
            pagi_actual = pagina == 0 ? 1 : pagina;
            if (registros >0)
            {
                pagi_cuantos = registros;
            }

            int pagi_totalReg = table.Count;
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));
            if (pagi_actual != 1)
            {
                // Si no estamos en la pagina 1 Ponemos el enlace primero
                int pagi_url = 1;  //sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" 
                    + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" 
                    + pagi_nav_primera + "</a>";

                // Si no estamos en la pagina 1 Ponemos 
                 pagi_url = pagi_actual - 1; //sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action
                    + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" 
                    + pagi_nav_anterior + " </a>";
            }
            // Si se definio la variable pagi_nav_num_enlaces
            // Calculamos el intervalo para restar y sumar a partir de la pagina actual
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            //Calculamos desde que numero de paginas se mostrara
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            //Calculamos hasta que numero de paginas se mostrara
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            //si pagi_nav desde es un numero negativo
            if (pagi_nav_desde  <1)
            {
                // Le sumamos la cantidad sobrante al final para mantener
                //el numero de enlaces que quiere mostrar
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                //Establecemos pagi_nav_desde como 1
                pagi_nav_desde = 1;
            }
            // Si pagi_nav_hasta es un numero mayor que el total de las paginas
            if (pagi_nav_hasta > pagi_totalPags)
            {
                // Le restamos la cantidad excedida al comienzo para mantener
                // el numero de enlaces que se quiere mostrar
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                // Establecemos pagi_nav_hasta como el total de paginas
                pagi_nav_hasta = pagi_totalPags;
                // Hacemos el ultimo ajuste Verificando que al cambiar pagi_nav_desde 
                // No haya quedado con un valor no valido
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for (int pagi_i= pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i ++)
            {
                //Desde pagina 1 hasta ultima pagina (pagi_totalPAgs)
                if (pagi_i == pagi_actual)
                {
                    // Si el numero de pagina es la actual (pagi_actual). Se escribe el numero, pero sin enlace y en neg
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    // Si es cualquier otro. Se escribe el enlace a dicho numero de pagina
                    pagi_navegacion += "<a class ='btn btn-default' href='" + host + "/" + controller + "/" +
                        action + "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                        pagi_i + " </a>";
                }
            }
            if (pagi_actual < pagi_totalPags)
            {
                // Si no estamos en la ultima pagina. Ponemos el enlace siguiente 
                int pagi_url = pagi_actual + 1; //sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                    action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                     pagi_nav_siguiente + " </a>";

                // Si no estamos en la ultima pagina. Ponemos el enlace Ultima 
                 pagi_url = pagi_totalPags; //sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                    action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                     pagi_nav_ultima + " </a>";
            }

            //obtencion de los registros que se mostraran en la pagina actual
            //Calculamos desde que registro se mostrara en esta pagina
            // Recordemos que el conteo empieza desde cero
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;
            //Consulta SQL. Devuelve cantidad registros empezando desde pagi_inicial

            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();


            /*
             * Generacion de la informacion sobre los registros mostrados
             * ---------------------
             */
            //Numero del primer registro de la pagina actual
            int pagi_desde = pagi_inicial + 1;
            //Numero del Ultimo registro de la pagina actual
            int pagi_hasta = pagi_inicial + pagi_cuantos;

            if (pagi_hasta > pagi_totalReg)
            {
                // Si estamos en la ultima pagina
                // El ultimo registro de la pagina actual sera igual al numero de registros
                pagi_hasta = pagi_totalReg;
            }


            String pagi_info = " del <b>" + pagi_actual + "</b> a1 <b>" + pagi_totalPags + "</b> de <b>" +
                  pagi_totalReg + "</b> <b>/" + pagi_cuantos + " </b>";
            object[] data = { pagi_info, pagi_navegacion, query };
            return data;
                
        }
    }
}
