namespace Stripe.Service;
public interface IStripeServiceWrapper
{
    PaymentIntent Create(PaymentIntentCreateOptions createOptions, RequestOptions requestOptions = null);

    Task<PaymentIntent> CreateAsync(PaymentIntentCreateOptions createOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);

}

public class StripeServiceWrapper(IStripeClient client) : IStripeServiceWrapper
{
    public PaymentIntent Create(PaymentIntentCreateOptions createOptions, RequestOptions requestOptions = null)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentIntent> CreateAsync(PaymentIntentCreateOptions createOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}