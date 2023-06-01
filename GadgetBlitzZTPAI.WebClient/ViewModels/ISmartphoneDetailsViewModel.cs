using GadgetBlitzZTPAI.WebClient.Models;
using Microsoft.AspNetCore.Components;

namespace GadgetBlitzZTPAI.WebClient.ViewModels
{
    public interface ISmartphoneDetailsViewModel
    {
        [Parameter]
        public string SmartphoneId { get; set; }
        public SmartphoneModel Model { get; set; }

        Task GetSmartphoneById(string id);
        Task<bool> AddReview(AddReviewCommand addReviewCommand);

    }
}