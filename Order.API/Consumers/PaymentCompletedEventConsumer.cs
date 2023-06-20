using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<PaymentCompletedEvent> _logger;

        public PaymentCompletedEventConsumer(AppDbContext context, ILogger<PaymentCompletedEvent> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.orderId);
            
            if (order != null)
            {
                order.Status = OrderStatus.Completed;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Order Id =  {context.Message.orderId} status changed : {order.Status}");
            }
            else
            {
                _logger.LogInformation($"Order Id =  {context.Message.orderId} not found");
            }

        }
    }
}
