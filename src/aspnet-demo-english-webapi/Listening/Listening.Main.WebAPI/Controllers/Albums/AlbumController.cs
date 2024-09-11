
using Listening.Main.WebAPI.Controllers.Albums.ViewModels;

namespace Listening.Main.WebAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IListeningRepository _repository;
    private readonly IMemoryCacheHelper _cacheHelper;
    public AlbumController(IListeningRepository repository, IMemoryCacheHelper cacheHelper)
    {
        this._repository = repository;
        this._cacheHelper = cacheHelper;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<AlbumVM>> FindById([RequiredGuid] Guid id)
    {
        var album = await _cacheHelper.GetOrCreateAsync($"AlbumController.FindById.{id}",
           async (e) => AlbumVM.Create(await _repository.GetAlbumByIdAsync(id)));
        if (album == null)
        {
            return NotFound();
        }
        return album;
    }

    [HttpGet]
    [Route("{categoryId}")]
    public async Task<ActionResult<AlbumVM[]>> FindByCategoryId([RequiredGuid] Guid categoryId)
    {
        //写到单独的local函数的好处是避免回调中代码太复杂
        Task<Album[]> FindDataAsync()
        {
            return _repository.GetAlbumsByCategoryIdAsync(categoryId);
        }
        var task = _cacheHelper.GetOrCreateAsync($"AlbumController.FindByCategoryId.{categoryId}",
            async (e) => AlbumVM.Create(await FindDataAsync()));
        return await task;
    }
}