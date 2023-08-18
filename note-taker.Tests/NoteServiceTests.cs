using Xunit;
using FakeItEasy;
using note_taker.Services;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        private readonly IFileService _fileService;

        public NoteServiceTests()
        {
            _fileService = A.Fake<IFileService>();
        }

        [Fact]
        public void AddNote_CallsCorrectMethodsOnFileService()
        {
            // Arrange
            var noteService = new NoteService(_fileService);

            // Act
            noteService.AddNote("Test note");

            // Assert
            A.CallTo(() => _fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PrintNotes_CallsCorrectMethodsOnFileService()
        {
            // Arrange
            var noteService = new NoteService(_fileService);
        
            // Act
            noteService.PrintNotes();
        
            // Assert
            A.CallTo(() => _fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void UpdateNoteStatus_CallsCorrectMethodsOnFileService()
        {
            // Arrange
            var noteService = new NoteService(_fileService);
        
            // Act
            noteService.UpdateNoteStatus(1, Note.Status.Closed);
        
            // Assert
            A.CallTo(() => _fileService.Exists(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.WriteAllText(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}

