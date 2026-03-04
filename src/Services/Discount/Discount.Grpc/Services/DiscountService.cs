using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if(coupon is null)
                coupon = new Coupon { ProductName = "No discount", Amount = 0, Description = "No discount available" };

            logger.LogInformation("[DiscountService] == [GetDiscount] --> Discount retrieved for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object data"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("[DiscountService] == [CreateDiscount] --> Discount created for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object data"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("[DiscountService] == [UpdateDiscount] --> Discount updated for ID: {Id}, ProductName: {ProductName}, Amount: {Amount}", coupon.Id, coupon.ProductName, coupon.Amount);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("[DiscountService] == [DeleteDiscount] --> Discount deleted for ProductName: {ProductName}", coupon.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
