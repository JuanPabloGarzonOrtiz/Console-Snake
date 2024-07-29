//Juego de Culebrita 
class Metodos
{
    //Metodos
    public void Titulo(ref int Ancho, ref string titulo)
    {
        int cantidad_Espacio = ((Ancho - titulo.Length) / 2) + titulo.Length;
        Console.WriteLine(titulo.PadLeft(cantidad_Espacio));
    }

    public void juego(ref int Ancho, ref int Alto, ref string titulo, ref int velocidad_inicial)
    {
        //Variables Locales
        int largo_Serpiente = 10;
        int ubicacion_EjeX = (Ancho - largo_Serpiente) / 2; int ubicacion_EjeY = (Alto / 2);
        bool mensaje_Error = false;
        int posicionX = 0; int posicionY = 0;
        int movimiento_Automatico = new Random().Next(0, 4);
        bool pantalla_Juego = true;

        //Ubicacion Inicial
        List<(int x, int y)> posicion_Serpiente = new List<(int x, int y)>();
        for (int i = 0; i < largo_Serpiente; i++)
            posicion_Serpiente.Add((ubicacion_EjeX + i, ubicacion_EjeY));


        //Primer posicionamiento de comida
        (int Posicion_Comida_EjeX, int Posicion_comida_ejeY) = new Metodos().posicion_Comida(ref Ancho, ref Alto);
        try
        {
            while (pantalla_Juego)
            {
                Ancho = Console.WindowWidth; Alto = Console.WindowHeight;
                Console.Clear();
                //Impresio Componenetes
                new Metodos().Titulo(ref Ancho, ref titulo);
                Console.WriteLine();

                Console.WriteLine(new string('-', Ancho));
                for (int i = 0; i < (Alto / 1.5); i++)
                    Console.WriteLine($"|{"".PadRight(Ancho - 2)}|");
                Console.WriteLine(new string('-', Ancho));

                Console.WriteLine($"\nPresione \"S\" para salir \tPresione \"P\" para parar \tPuntos:{largo_Serpiente - 10}");

                if (mensaje_Error == true)
                {
                    Console.WriteLine("El carácter ingresado no esta dentro de las \nDesplácese por medio de las flechas");
                    Console.ReadKey();
                    mensaje_Error = false;
                }

                //Nueva Comida
                Console.SetCursorPosition(Posicion_Comida_EjeX, Posicion_comida_ejeY);
                Console.WriteLine("*");
                if (posicionX == Posicion_Comida_EjeX && posicionY == Posicion_comida_ejeY)
                {
                    (Posicion_Comida_EjeX, Posicion_comida_ejeY) = new Metodos().posicion_Comida(ref Ancho, ref Alto);
                    posicion_Serpiente.Add((ubicacion_EjeX + 1, ubicacion_EjeY));
                    largo_Serpiente += 1;
                }

                foreach (var posicion in posicion_Serpiente)
                {
                    Console.SetCursorPosition(posicion.x, posicion.y);
                    Console.Write("*");
                    posicionX = posicion.x; posicionY = posicion.y;

                    //Perdida de Juego
                    if (posicion.x <= 0 || posicion.x >= (Ancho - 1) || posicion.y <= 2 || posicion.y >= (Alto / 1.5) + 3)
                    {
                        while (true)
                        {
                            Thread.Sleep(1000); Console.Clear();

                            Console.SetCursorPosition(0, (Alto - 3) / 2);
                            Console.WriteLine(("Haz Perdido").PadLeft(((Ancho - 11) / 2) + 11));

                            string puntacion = $"Tu puntación es de {largo_Serpiente - 10}";
                            int espacio_Puntacion = ((Ancho - puntacion.Length) / 2) + puntacion.Length;
                            Console.WriteLine((puntacion).PadLeft(espacio_Puntacion));

                            Console.WriteLine(("Presiona la tecla S para continuar").PadLeft(((Ancho - 39) / 2) + 39));

                            ConsoleKeyInfo tecla_Salida = (Console.ReadKey(true));
                            if (tecla_Salida.Key == ConsoleKey.S)
                                return;
                        }
                    }
                }
                //Movimineto de Serpiente
                for (int i = posicion_Serpiente.Count - 1; i > 0; i--)
                    posicion_Serpiente[i] = posicion_Serpiente[i - 1];

                //Moviminetos
                if (movimiento_Automatico == 0)
                    posicion_Serpiente[0] = (posicion_Serpiente[0].x, posicion_Serpiente[0].y - 1);

                else if (movimiento_Automatico == 1)
                    posicion_Serpiente[0] = (posicion_Serpiente[0].x, posicion_Serpiente[0].y + 1);

                else if (movimiento_Automatico == 2)
                    posicion_Serpiente[0] = (posicion_Serpiente[0].x - 1, posicion_Serpiente[0].y);

                else if (movimiento_Automatico == 3)
                    posicion_Serpiente[0] = (posicion_Serpiente[0].x + 1, posicion_Serpiente[0].y);

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.RightArrow:
                            {
                                posicion_Serpiente[0] = (posicion_Serpiente[0].x + 1, posicion_Serpiente[0].y);
                                movimiento_Automatico = 3;
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                posicion_Serpiente[0] = (posicion_Serpiente[0].x - 1, posicion_Serpiente[0].y);
                                movimiento_Automatico = 2;
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                posicion_Serpiente[0] = (posicion_Serpiente[0].x, posicion_Serpiente[0].y - 1);
                                movimiento_Automatico = 0;
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                posicion_Serpiente[0] = (posicion_Serpiente[0].x, posicion_Serpiente[0].y + 1);
                                movimiento_Automatico = 1;
                                break;
                            }
                        case ConsoleKey.S:
                            {
                                pantalla_Juego = false;
                                break;
                            }
                        case ConsoleKey.P:
                            {
                                Console.ReadKey();
                                break;
                            }
                        default:
                            {
                                mensaje_Error = true;
                                break;
                            }
                    }
                }
                if (largo_Serpiente <= 15)
                    Thread.Sleep(velocidad_inicial);

                else if (largo_Serpiente <= 20)
                    Thread.Sleep(velocidad_inicial - (velocidad_inicial / 3));

                else if (largo_Serpiente <= 25)
                    Thread.Sleep(velocidad_inicial - (velocidad_inicial / 3));
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            new Metodos().juego(ref Ancho, ref Alto, ref titulo, ref velocidad_inicial);
        }
    }
    //Posicion de Comida
    public (int, int) posicion_Comida(ref int Ancho, ref int Alto)
    {
        int Posicion_Comida_EjeX = (new Random()).Next(1, Ancho - 2);
        int Posicion_comida_ejeY = (new Random()).Next(3, Convert.ToInt32((Alto / 1.5) + 2));
        return (Posicion_Comida_EjeX, Posicion_comida_ejeY);
    }
}
class Program
{
    static void Main()
    { 
        string titulo = "La Culebrita";
        int velocidad_inicial = 200;
        Metodos metodo = new Metodos();
        while (true)
        {
            //Tamaño de Pantalla
            int Ancho = Console.WindowWidth;  int Alto = Console.WindowHeight;
            Thread monitor_Tamaño_Thread = new Thread(() => new Program().Monitor_Tamaño(Ancho, Alto));
            monitor_Tamaño_Thread.Start();

            Console.Clear();
            metodo.Titulo(ref Ancho, ref titulo);

            Console.WriteLine("\n1. Jugar \n2. Configuración \n3. Salir");
            Console.Write("\nEscribe la acción que quieras hacer:"); 

            switch (Console.ReadLine().ToLower().Trim())
            {
                case "jugar":
                    {
                        Console.WriteLine("Vamos a jugar");
                        Thread.Sleep(1000);
                        metodo.juego(ref Ancho, ref Alto, ref titulo, ref velocidad_inicial);
                        break;
                    }
                case "configuracion":
                    {
                        Console.Clear();
                        int velocidad = 0;
                        Console.Write($"Bienvenido a la Configuración\nLa velocidad inicial esta en {velocidad_inicial}\nIngrese la velociad inicial que desea para jugar:");
                        int.TryParse(Console.ReadLine(), out velocidad);
                        velocidad_inicial = velocidad > 0 ? velocidad : velocidad_inicial;
                        Console.WriteLine($"La velocidad inicial quedo en {velocidad_inicial}");
                        Console.ReadKey();
                        break;
                    }
                case "salir":
                    {
                        System.Environment.Exit(0);
                        break;
                    }
            }
        }
    }
    public void Monitor_Tamaño(int Ancho, int Alto)
    {
        while (true)
        {
            if (Console.WindowWidth != Ancho || Console.WindowHeight != Alto)
            {
                Console.Clear();
                Console.WriteLine("\nEl tamaño de la pantalla se cambio se cambio");
                Ancho = Console.WindowWidth; Alto = Console.WindowHeight;
                Thread.Sleep(1000); Console.Clear();
                Console.WriteLine("Presiona Cualquier tecla para continuar");
                Thread.Sleep(1000); Console.ReadKey(true);
            }
            Thread.Sleep(500);
        }
    }
}