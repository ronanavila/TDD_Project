using Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Estacionamento.Testes
{

    public class PatioTeste :IDisposable
    {
        private Veiculo veiculo;
        private Patio patio;
        private Operador operador;
        public ITestOutputHelper SaidaConsoleTester;

        public PatioTeste(ITestOutputHelper _saidaConsoleTester)
        {
            SaidaConsoleTester = _saidaConsoleTester;
            SaidaConsoleTester.WriteLine("Cosntrutor invocado");
            veiculo = new Veiculo();
            patio = new Patio();
            operador = new Operador();
            operador.Nome = "Pedro Duarte";
            patio.OperadorPatio = operador;
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoUmComVeiculo()
        {
            //Arrange
            //var patio = new Patio();
            //var veiculo = new Veiculo();

            veiculo.Proprietario = "André Silva";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";

            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);

            //Arrange
            double faturamento = patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-9999", "Verde", "Fusca")]
        [InlineData("José Silva", "ADS-1111", "Azul", "Maréa")]
        [InlineData("Maria Silva", "AAA-0000", "Preto", "Gol")]

        public void ValidaFaturamentoDoEstacionamentoVariosVeiculos(string proprietarioa, string placa,
            string cor, string modelo)
        {
            ////Arrange
            //var patio = new Patio();
            //var veiculo = new Veiculo();

            veiculo.Proprietario = proprietarioa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }


        [Theory]
        [InlineData("André Silva", "ASD-9999", "Verde", "Fusca")]
        public void LocalizaVeiculoNoPatioComBasenoIdDoTicket(string proprietarioa, string placa,
            string cor, string modelo)
        {
            //Arrange
            //var patio = new Patio();
            //var veiculo = new Veiculo();

            veiculo.Proprietario = proprietarioa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            patio.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = patio.PesquisaVeiculoPorTicket(veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionameno Alura ###", consultado.Ticket);

        }

        [Fact]
        public void AlterarDadosDoProprioVeiculo()
        {
            //Arrange
            //var patio = new Patio();
            //var veiculo = new Veiculo();

            veiculo.Proprietario = "André Silva";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";
            patio.RegistrarEntradaVeiculo(veiculo);

            var veiculoalterado = new Veiculo();

            veiculoalterado.Proprietario = "André Silva";
            veiculoalterado.Cor = "Azul";
            veiculoalterado.Modelo = "Gol";
            veiculoalterado.Placa = "ASD-9999";

            //Act
            Veiculo alterado = patio.AlteraDadosVeiculo(veiculoalterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoalterado.Cor);


        }

        public void Dispose()
        {
            SaidaConsoleTester.WriteLine("Dispose Invocado");
        }
    }


}
