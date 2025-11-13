using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Constants;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Orders.Commands
{
    public interface ICheckoutCommand
    {
        Task<OrderDto> Execute(CheckoutDto checkoutDto, long userId);
    }

    public class CheckoutCommand : ICheckoutCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CheckoutCommand(IProductRepository productRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderDto> Execute(CheckoutDto checkoutDto, long userId)
        {
            var variantIds = checkoutDto.Items.Select(ids => ids.ProductVariantId).ToList();

            var variants = await _productRepository.GetVariantsByIdsAsync(variantIds);

            decimal subTotal = 0;
            var orderItems = new List<OrderProduct>();

            foreach (var item in checkoutDto.Items)
            {
                var variant = variants.FirstOrDefault(x => x.Id == item.ProductVariantId);

                if (variant == null)
                    throw new KeyNotFoundException($"Product variant with ID {item.ProductVariantId} not found.");

                if (variant.StockAmount < item.Quantity)
                    throw new InvalidOperationException($"Not enough stock for {variant.SKU}.");

                subTotal += variant.Price * item.Quantity;

                orderItems.Add(new OrderProduct(
                    item.Quantity,
                    variant.Price,
                    0,
                    variant.Id
                ));

                variant.RemoveStock(item.Quantity);
            }

            var order = new Order(0, subTotal, 0, userId, OrderStatus.PendingPayment);

            orderItems.ForEach(order.AddOrderItem);

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<OrderDto>(order);
        }
    }
}