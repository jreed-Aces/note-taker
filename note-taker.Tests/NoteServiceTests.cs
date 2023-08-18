using Xunit;
using FakeItEasy;
using note_taker.Services;
using System.Collections.Generic;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        [Fact]
        public void AddNote_CallsCorrectMethodsAndReturnsTrue()
        {
            // Arrange
            var fileService = A.Fake<IFileService>();
            var noteService = new NoteService(fileService);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).Returns("[]");

            // Act
            var result = noteService.AddNote("Test note");

            // Assert
            Assert.True(result);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_CallsCorrectMethodsAndReturnsNotes()
        {
            // Arrange
            var fileService = A.Fake<IFileService>();
            var noteService = new NoteService(fileService);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).Returns("[]");

            // Act
            var result = noteService.PrintNotes();

            // Assert
            Assert.IsType<List<Note>>(result);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateNoteStatus_CallsCorrectMethodsAndReturnsTrue()
        {
            // Arrange
            var fileService = A.Fake<IFileService>();
            var noteService = new NoteService(fileService);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).Returns("[{\"Id\":1,\"Content\":\"Test note\",\"NoteStatus\":\"Open\"}]");

            // Act
            var result = noteService.UpdateNoteStatus(1, Note.Status.Completed);

            // Assert
            Assert.True(result);
            A.CallTo(() => fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}

