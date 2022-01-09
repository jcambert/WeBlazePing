using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeMediatCrud;

public interface IMediatorAction : ISender
{
    public IObservable<ValidateableResponse> OnOk{ get; }
    public IObservable<ValidateableResponse> OnError { get; }


}
public sealed class MediatorAction : IMediatorAction
{
    private readonly IMediator InternalMediator;
    //public Action<ValidateableResponse> OnError { get; set; }
    
    private ISubject<ValidateableResponse> _onOkSubject = new Subject<ValidateableResponse>();
    private ISubject<ValidateableResponse> _onErrorSubject = new Subject<ValidateableResponse>();
    public IObservable<ValidateableResponse> OnOk => _onOkSubject.AsObservable();
    public IObservable<ValidateableResponse> OnError=> _onErrorSubject.AsObservable();
    public MediatorAction(IMediator mediator)
    {
        this.InternalMediator = mediator;
    }
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return InternalMediator.CreateStream<TResponse>(request, cancellationToken);
    }
    //
    // Résumé :
    //     Create a stream via an object request to a stream handler
    //
    // Paramètres :
    //   request:
    //
    //   cancellationToken:
    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
    {
        return InternalMediator.CreateStream(request, cancellationToken);
    }
    //
    // Résumé :
    //     Asynchronously send a request to a single handler
    //
    // Paramètres :
    //   request:
    //     Request object
    //
    //   cancellationToken:
    //     Optional cancellation token
    //
    // Paramètres de type :
    //   TResponse:
    //     Response type
    //
    // Retourne :
    //     A task that represents the send operation. The task result contains the handler
    //     response
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)

    {
        TResponse res = await InternalMediator.Send<TResponse>(request, cancellationToken);
        if (res!=null && res is ValidateableResponse)
        {
            var response = res as ValidateableResponse;
            ISubject<ValidateableResponse> subject = !response.IsValidResponse ? _onErrorSubject : _onOkSubject;
            subject.OnNext(response);

        }
        return res;
    }
    //
    // Résumé :
    //     Asynchronously send an object request to a single handler via dynamic dispatch
    //
    // Paramètres :
    //   request:
    //     Request object
    //
    //   cancellationToken:
    //     Optional cancellation token
    //
    // Retourne :
    //     A task that represents the send operation. The task result contains the type
    //     erased handler response
    public async Task<object> Send(object request, CancellationToken cancellationToken = default)
    {
        object res = await InternalMediator.Send(request, cancellationToken);
        if (res!=null && res is ValidateableResponse)
        {
            var response = res as ValidateableResponse;
            ISubject<ValidateableResponse> subject = !response.IsValidResponse ? _onErrorSubject : _onOkSubject;
            subject.OnNext(response);

        }
        return res;
        
    }
}

