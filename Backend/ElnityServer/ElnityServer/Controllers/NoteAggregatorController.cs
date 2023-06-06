using ElnityServer.Authorization.CustomAttributes;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.NoteAggregator.Request;
using ElnityServerBLL.Dto.NoteAggregator.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElnityServer.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteAggregatorController : ControllerBase
    {
        private INoteAggregator _noteAggregator;

        public NoteAggregatorController(INoteAggregator noteAggregator)
        {
            _noteAggregator = noteAggregator;
        }


        [Authorize]
        [HttpPost("add-note")]
        public async Task<ActionResult<bool>> AddNote([FromForm]AddNoteRequest reqParams)
        {
            var res = await _noteAggregator.AddNoteAsync(reqParams);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("get-user-notes")]

        public async Task<ActionResult<IEnumerable<UserNoteResponse>>> GetNote(GetNoteUserRequest reqParams)
        {
            var res = await _noteAggregator.GetNoteAsync(reqParams);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("delete-note")]
        public async Task<ActionResult<bool>> DeleteNote(DeleteNoteRequest reqParams)
        {
            var res = await _noteAggregator.DeleteNoteAsync(reqParams);
            return Ok(res);
        }
    }
}
