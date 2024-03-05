namespace Questao1
{
    class ContaBancaria
    {

        public int Numero { get; } = 0;
        public string Titular { get; private set; } = string.Empty;
        public double Saldo { get; private set; } = 0;

        public ContaBancaria()
        {

        }
        public ContaBancaria(int numero, string titular)
        {
            this.Numero = numero;
            this.Titular = titular;
        }
        public ContaBancaria(int numero, string titular, double saldo)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.Saldo = saldo;
        }

        public double Deposito(double valor)
        {
            this.Saldo += valor;
            return this.Saldo;
        }

        public double Saque(double valor)
        {
            this.Saldo -= (valor + Constantes.Taxa_Saque);
            return this.Saldo;
        }

        public void AlterarNomeTitular(string nome)
        {
            this.Titular = string.IsNullOrEmpty(nome) ? this.Titular : nome;
        }
    }
}
