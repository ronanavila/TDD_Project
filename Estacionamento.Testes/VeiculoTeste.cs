using Estacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace Estacionamento.Testes
{
    public class VeiculoTeste :IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTester;

        public VeiculoTeste(ITestOutputHelper _saidaConsoleTester)
        {
            SaidaConsoleTester = _saidaConsoleTester;
            SaidaConsoleTester.WriteLine("Cosntrutor invocado");
            veiculo =  new Veiculo();
        }

        [Fact(DisplayName = "TestaVeiculoAcelerarComParametro10")]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFreiarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Frear(10);

            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void FichadeInformacaoDoVeiculo()
        {
            //Arrange
            //var veiculo = new Veiculo();

            veiculo.Proprietario = "André Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do Veículo:", dados);
        }

        [Fact]
        public void TestaNomeProprietarioDoVeiculoMenosdeTresCaracteres()
        {
            //Arrange
            string nomeProprietraio = "Ab";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietraio)
                );

        }

        [Fact]
        public void TestaMensagemExecaodoQuartoCaracterDaPlaca()
        {
            //Arrange
            string placa = "ASDF8888";

            //Act           
            var mensagem = Assert.Throws<System.FormatException>(
              
                () => new Veiculo().Placa = placa
                );

            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);

        }

        public void Dispose()
        {
            SaidaConsoleTester.WriteLine("Dispose Invocado");
        }

    }
}