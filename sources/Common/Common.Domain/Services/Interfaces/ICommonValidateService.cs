using System.Threading.Tasks;

namespace Common.Domain.Services.Interfaces
{
    public interface ICommonValidateService
    {
        Task<bool> Validate<Dto>(Dto dto);
    }
}