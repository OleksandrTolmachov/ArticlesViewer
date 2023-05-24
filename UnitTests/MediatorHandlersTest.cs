using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.Handlers;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Moq;
using AutoFixture.Xunit2;

namespace UnitTests
{
    public class MediatorHandlersTests : AutoDataAttribute
    {
        private readonly IFixture _fixture;

        public MediatorHandlersTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().First());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Theory]
        [AutoDomainData]
        public async Task Handle_ShouldSanitizeHtml(
            [Frozen] Mock<IHtmlSanitizer> sanitizerMock,
            GetSanitizedHtmlQuery query,
            GetSanitizedHtmlHandler handler)
        {
            // Arrange
            var sanitizedHtml = _fixture.Create<string>();
            sanitizerMock.Setup(x => x.Sanitize(It.IsAny<string>(), "", null)).Returns(sanitizedHtml).Verifiable();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(sanitizedHtml);
            sanitizerMock.Verify();
        }

        [Theory]
        [AutoDomainData]
        public async Task Handle_CreateArticleCommand_Returns_Successful(
            [Frozen] Mock<UserManager<User>> userManagerMock,
            [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
            CreateArticleCommand command,
            CreateArticleHandler handler)
        {
            unitOfWorkMock.Setup(x => x.Articles.CreateAsync(It.IsAny<Article>())).Verifiable();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<User>());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<Article>(command)).Returns(new Article());

            // Act
            await handler.Handle(command, new CancellationToken());

            // Assert
            userManagerMock.Verify();
            unitOfWorkMock.Verify(x => x.Articles.CreateAsync(It.IsAny<Article>()), Times.Once);
        }
    }
}
