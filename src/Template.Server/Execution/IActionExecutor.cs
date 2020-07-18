using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Models;

namespace Template.Server.Execution
{
    public interface IActionExecutor
    {
        Task<TResult> Execute<TResult>(AbstractAction<TResult> action, CancellationToken cancellationToken = default);
    }
}
