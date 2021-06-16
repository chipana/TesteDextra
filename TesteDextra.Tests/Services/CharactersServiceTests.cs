using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Services;
using TesteDextra.Services.Interfaces;
using Xunit;

namespace TesteDextra.Tests
{
    public class CharactersServiceTests
    {
        private readonly Mock<ICharactersRepository> charactersRepository;
        private readonly Mock<IHousesService> housesService;
        private List<House> Houses;
        private List<Character> Characters;
        private House gryffindor;
        private House slytherin;

        private Character harryPotter;
        private Character dracoMalfoy;

        public CharactersServiceTests()
        {
            charactersRepository = new Mock<ICharactersRepository>();
            housesService = new Mock<IHousesService>();
            Houses = new List<House>();
            Characters = new List<Character>();

            gryffindor = new House
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Gryffindor"
            };

            slytherin = new House
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "Slytherin"
            };

            Houses.Add(gryffindor);
            Houses.Add(slytherin);

            housesService.Setup(p => p.GetHousesAsync()).ReturnsAsync(Houses);

            harryPotter = new Character
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                House = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Harry Potter",
                Patronus = "Deer",
                Role = Models.Enums.Occupation.Student,
                School = "Hogwarts School of Witchcraft and Wizardry"
            };

            dracoMalfoy = new Character
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                House = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "Draco Malfoy",
                Patronus = "None",
                Role = Models.Enums.Occupation.Student,
                School = "Hogwarts School of Witchcraft and Wizardry"
            };

            Characters.Add(harryPotter);
            Characters.Add(dracoMalfoy);
        }

        #region Create

        [Fact]
        public async Task GivenANewCharacaterWhenCreateAsyncThenReturnsOk()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync((Character)null);
            charactersRepository.Setup(p => p.CreateAsync(harryPotter, default)).ReturnsAsync(true);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.CreateAsync(harryPotter);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.Code);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task GivenAAlreadyCreatedCharacaterWhenCreateAsyncThenReturnsError()
        {
            //Arrange
            housesService.Setup(p => p.GetHousesAsync()).ReturnsAsync(Houses);
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.CreateAsync(harryPotter);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Code);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task GivenANewCharacaterWithAnUnavailableHouseWhenCreateAsyncThenReturnsError()
        {
            //Arrange
            harryPotter.House = new Guid("00000000-0000-0000-0000-000000000003");

            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync((Character)null);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.CreateAsync(harryPotter);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Code);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task GivenANewCharacaterWithErrorWhenCreateAsyncThenReturnsError()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync((Character)null);
            charactersRepository.Setup(p => p.CreateAsync(harryPotter, default)).ReturnsAsync(false);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.CreateAsync(harryPotter);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result.Code);
            Assert.NotNull(result.Message);
        }

        #endregion

        #region Get
        public async Task GivenACharacaterWhenGetAsyncThenReturnsTheCharacter()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.GetAsync(harryPotter.Id, default);

            //Assert
            Assert.Equal(harryPotter, result);
        }
        #endregion

        #region Update

        [Fact]
        public async Task GivenACharacaterWhenUpdateAsyncThenReturnsOk()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);
            charactersRepository.Setup(p => p.UpdateAsync(harryPotter.Id, dracoMalfoy, default)).ReturnsAsync(true);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.UpdateAsync(harryPotter.Id, dracoMalfoy);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.Code);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task GivenANonexistentCharacaterToUpdateWhenUpdateAsyncThenReturnsError()
        {
            //Arrange
            housesService.Setup(p => p.GetHousesAsync()).ReturnsAsync(Houses);
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync((Character)null);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.UpdateAsync(harryPotter.Id, dracoMalfoy);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Code);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task GivenACharacaterWithAnUnavailableHouseWhenUpdateAsyncThenReturnsError()
        {
            //Arrange
            dracoMalfoy.House = new Guid("00000000-0000-0000-0000-000000000003");

            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.UpdateAsync(harryPotter.Id, dracoMalfoy);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Code);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task GivenACharacaterWithErrorWhenUpdateAsyncThenReturnsError()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);
            charactersRepository.Setup(p => p.UpdateAsync(harryPotter.Id, dracoMalfoy, default)).ReturnsAsync(false);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.UpdateAsync(harryPotter.Id, dracoMalfoy);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result.Code);
            Assert.NotNull(result.Message);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task GivenACharacaterWhenDeleteAsyncThenReturnsOk()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);
            charactersRepository.Setup(p => p.DeleteAsync(harryPotter.Id, default)).ReturnsAsync(true);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.DeleteAsync(harryPotter.Id);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.Code);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task GivenANonexistentCharacaterWhenDeleteAsyncThenReturnsError()
        {
            //Arrange
            housesService.Setup(p => p.GetHousesAsync()).ReturnsAsync(Houses);
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync((Character)null);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.DeleteAsync(harryPotter.Id);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Code);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task GivenACharacaterWithErrorWhenDeleteAsyncThenReturnsError()
        {
            //Arrange
            charactersRepository.Setup(p => p.GetAsync(harryPotter.Id, default)).ReturnsAsync(harryPotter);
            charactersRepository.Setup(p => p.DeleteAsync(harryPotter.Id, default)).ReturnsAsync(false);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.DeleteAsync(harryPotter.Id);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result.Code);
            Assert.NotNull(result.Message);
        }

        #endregion

        #region List
        public async Task GivenAListOfCharacatersWhenListAsyncThenReturnsTheList()
        {
            //Arrange
            charactersRepository.Setup(p => p.ListAsync(default)).ReturnsAsync(Characters);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.ListAsync();

            //Assert
            Assert.Equal(result, Characters);
        }

        public async Task GivenAListOfCharacatersWithFilterWhenListAsyncThenReturnsTheList()
        {
            //Arrange
            charactersRepository.Setup(p => p.ListAsync(default)).ReturnsAsync(Characters);

            CharactersService charactersService = new CharactersService(charactersRepository.Object, housesService.Object);

            //Act
            var result = await charactersService.ListAsync(gryffindor.Id);

            //Assert
            Assert.Contains(harryPotter, result);
            Assert.DoesNotContain(dracoMalfoy ,result);
        }
        #endregion
    }
}
