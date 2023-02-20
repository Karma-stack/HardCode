using HardCode.WebAPI.Entities;

namespace HardCode.WebAPI.Interfaces
{
    public interface IAsyncRepository<T> : IBaseAsyncRepository<T, int>
        where T : IEntity<int>
    {

    }
}
