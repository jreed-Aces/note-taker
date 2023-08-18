using Xunit;
using FakeItEasy;
using note_taker.Services;

namespace note_taker.Tests
{
    public class NoteServiceTests
    {
        private readonly INoteService _noteService;
        private readonly IFileService _fileService;

        public NoteServiceTests()
        {
            _fileService = A.Fake<IFileService>();
            _noteService = new NoteService(_fileService);
        }
    }
}

