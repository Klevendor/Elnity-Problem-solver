using ElnityServerBLL.Dto.NoteAggregator.Request;
using ElnityServerBLL.Dto.NoteAggregator.Response;

namespace ElnityServerBLL.Services.Interfaces
{
    public interface INoteAggregator
    {
        public Task<bool> AddNoteAsync(AddNoteRequest reqParams);
        public Task<IEnumerable<UserNoteResponse>> GetNoteAsync(GetNoteUserRequest reqParams);
        public Task<bool> DeleteNoteAsync(DeleteNoteRequest reqParams);
    }
}
