using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // string placa = "";
            // do
            // {
            //     Console.WriteLine("Digite a placa do veículo  para estacionar:");
            //     placa = Console.ReadLine();
            // }
            // while (string.IsNullOrWhiteSpace(placa));

            // veiculos.Add(placa);
            string placa = "";
            Regex regexPlaca = new Regex(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$");

            do
            {
                Console.WriteLine("Digite a placa do veículo para estacionar sem espaço e/ou -:");
                placa = Console.ReadLine().ToUpper().Trim();

                if (!regexPlaca.IsMatch(placa))
                {
                    Console.WriteLine("Placa inválida!");
                    placa = "";
                }

            } while (string.IsNullOrWhiteSpace(placa));

            veiculos.Add(placa);
            Console.WriteLine($"Veículo com placa {placa} adicionado com sucesso!");
        }

        public void RemoverVeiculo()
        {
            string placa = "";

            do
            {
                Console.WriteLine("Digite a placa do veículo para remover:");
                placa = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(placa));

            if (!veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                return;
            }

            int horas;

            do
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
            while (!int.TryParse(Console.ReadLine(), out horas) || horas < 0);

            decimal valorTotal = precoInicial + (horas * precoPorHora);
            Console.WriteLine($"Valor total: {valorTotal:C}");

            decimal valorPago = 0;
            decimal troco = 0;
            do
            {
                Console.WriteLine("Digite o valor pago pelo cliente:");
                Console.Write("R$ ");
                while (!decimal.TryParse(Console.ReadLine(), out valorPago) || valorPago <= 0)
                {
                    Console.WriteLine("Valor inválido. Digite um valor válido:");
                }

                troco = valorPago - valorTotal;
                if (troco < 0)
                {
                    Console.WriteLine($"Restante: {-troco:C}");
                    valorTotal -= valorPago;
                }

            } while (troco < 0);

            veiculos.Remove(placa);

            Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {valorPago - troco:C}");
            Console.WriteLine($"Troco: {troco:C}");

        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");

                foreach (string veiculo in veiculos)
                    Console.WriteLine(veiculo);

            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
