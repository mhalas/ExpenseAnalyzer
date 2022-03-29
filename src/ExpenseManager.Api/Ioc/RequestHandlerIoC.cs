using Api.HandlerRequests;
using Api.HandlerResponses;
using DataAccess.Contracts.Model;
using DataAccess.Contracts.Shared;
using Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Api.Ioc
{
    public static class RequestHandlerIoC
    {
        public static void AddGenericHandlers(this IServiceCollection services)
        {
            AddHandlersForEntity<UserCategory>(services);
            AddHandlersForEntity<UserCategoryValue>(services);
            AddHandlersForEntity<UserConfiguration>(services);
        }

        private static void AddHandlersForEntity<TEntity>(IServiceCollection services)
            where TEntity : class, IId
        {
            AddSingleHandler<GetItemListHandler<TEntity>, GetItemsRequest<TEntity>, IEnumerable<TEntity>>(services);
            AddSingleHandler<GetSingleItemHandler<TEntity>, GetSingleItemRequest<TEntity>, TEntity>(services);
            AddSingleHandler<RemoveItemHandler<TEntity>, RemoveItemRequest<TEntity>, GeneralResponse>(services);
        }

        private static void AddSingleHandler<THandler, TRequest, TResponse>(IServiceCollection services)
            where THandler : class, IRequestHandler<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
        {
            services.AddTransient<IRequestHandler<TRequest, TResponse>, THandler>();
        }
    }
}
