using Api.HandlerResponses;
using MediatR;
using System.Collections.Generic;

namespace Api.HandlerRequests
{
    public class UploadCategoriesRequest: IRequest<GeneralResponse>
    {
        public UploadCategoriesRequest(int userId, Dictionary<string, List<string>> categories)
        {
            UserId = userId;
            Categories = categories;
        }

        public int UserId { get; set; }
        public Dictionary<string, List<string>> Categories { get; set; }
    }
}
