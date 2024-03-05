using System;
using System.Globalization;

namespace Questao1
{
    class Program
    {
        private static ContaBancaria conta;
        static void Main(string[] args)
        {
            conta = InformarDadosContaBancaria();

            Console.WriteLine();
            ExibirDadosBancarios(conta,false);

            Console.WriteLine();



            Boolean valorValido = false;
            double quantia;
            while (!valorValido)
            {

                Console.Write("Entre um valor para depósito: ");
                _ = double.TryParse(Console.ReadLine().Replace('.', ','), out quantia);
                if (quantia > 0)
                {
                    conta.Deposito(quantia);
                    ExibirDadosBancarios(conta, true);
                    valorValido = true;
                }
            }
            valorValido = false;
            quantia = 0;
            while (!valorValido)
            {
                Console.WriteLine();
                Console.Write("Entre um valor para saque: ");
                _ = double.TryParse(Console.ReadLine().Replace('.', ','), out quantia);
                if (quantia > 0)
                {
                    conta.Saque(quantia);
                    ExibirDadosBancarios(conta, true);
                    valorValido = true;
                }
            }
            Console.WriteLine();
            Console.Write("Deseja alterar o nome do títular (s/n)? ");
            _ = char.TryParse(Console.ReadLine(), out char resp);

            if (resp == 's' || resp == 'S')
            {
                Console.WriteLine();
                Console.Write("Alterar Nome do Titular: ");
                conta.AlterarNomeTitular(Console.ReadLine());
                ExibirDadosBancarios(conta, true);

            }

        }

        private static void ExibirDadosBancarios(ContaBancaria conta, bool atualizado)
        {
            if (atualizado)
            {
                Console.WriteLine("Dados da conta atualizados:");
            }
            else
            {
                Console.WriteLine("Dados da conta:");
            }
            Console.WriteLine($"Conta {conta.Numero}, Titular: {conta.Titular}, Saldo: $ {string.Format("{0:0.00}", conta.Saldo)}");
        }

        private static ContaBancaria InformarDadosContaBancaria()
        {

            Console.Write("Entre o número da conta: ");
            Boolean valorValido = false;
            _ = int.TryParse(Console.ReadLine(), out int numero);
            if (numero > 0)
            {
                Console.Write("Entre o titular da conta: ");
                string titular = Console.ReadLine();
                Console.Write("Haverá depósito inicial (s/n)? ");
                _ = char.TryParse(Console.ReadLine(), out char resp);

                if (resp == 's' || resp == 'S')
                {
                    while (!valorValido)
                    {
                        Console.Write("Entre o valor de depósito inicial: ");
                        _ = double.TryParse(Console.ReadLine().Replace('.', ','), out double depositoInicial);
                        if (depositoInicial > 0)
                        {
                            conta = new ContaBancaria(numero, titular, depositoInicial);
                            valorValido = true;
                        }
                    }

                }
                else
                {
                    conta = new ContaBancaria(numero, titular);
                }
                return conta;
            }
            else
            {
                if (conta == null)
                    InformarDadosContaBancaria();
            }
            return conta;
        }
    }
}
