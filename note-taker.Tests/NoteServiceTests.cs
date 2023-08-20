using Xunit;
using FakeItEasy;
using note_taker.Services;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        [Fact]
        public void AddNote_ShouldCallIFileServiceMethodsCorrectly()
        {
            // Arrange
            var fakeFileService = A.Fake<IFileService>();
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).Returns("[]");
            A.CallTo(() => fakeFileService.WriteAllText(A<string>.Ignored, A<string>.Ignored));
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).Returns(true);
            
            var noteService = new NoteService(fakeFileService);

            // Act
            var result = noteService.AddNote("Test Note");

            // Assert
            Assert.True(result);
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeFileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_ShouldCallIFileServiceMethodsCorrectly()
        {
            // Arrange
            var fakeFileService = A.Fake<IFileService>();
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).Returns("[{\"Id\": 1, \"Text\": \"Test Note\", \"Timestamp\": \"2022-01-01T00:00:00\", \"NoteStatus\": 0}]");
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).Returns(true);
            
            var noteService = new NoteService(fakeFileService);

            // Act
            var result = noteService.PrintNotes();

            // Assert
            Assert.Single(result);
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateNoteStatus_ShouldCallIFileServiceMethodsCorrectly()
        {
            // Arrange
            var fakeFileService = A.Fake<IFileService>();
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).Returns("[{\"Id\": 1, \"Text\": \"Test Note\", \"Timestamp\": \"2022-01-01T00:00:00\", \"NoteStatus\": 0}]");
            A.CallTo(() => fakeFileService.WriteAllText(A<string>.Ignored, A<string>.Ignored));
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).Returns(true);
            
            var noteService = new NoteService(fakeFileService);

            // Act
            var result = noteService.UpdateNoteStatus(1, Note.Status.Closed);

            // Assert
            Assert.True(result);
            A.CallTo(() => fakeFileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeFileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeFileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}

