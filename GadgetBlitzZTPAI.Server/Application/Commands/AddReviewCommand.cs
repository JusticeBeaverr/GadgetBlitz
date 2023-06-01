using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record AddReviewCommand(AddReviewDTO ReviewDto) : IRequest<Review>;
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, Review>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        public AddReviewCommandHandler(ISmartphoneRepository smartphoneRepository)
        {
            _smartphoneRepository = smartphoneRepository;
        }

        public async Task<Review> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var smartphone = await _smartphoneRepository.GetByIDAsync(request.ReviewDto.SmartphoneId);
                if (smartphone == null)
                {
                    throw new Exception("Smartphone not found"); // Wyrzuć wyjątek, jeśli nie znaleziono smartfona
                }

                var review = new Review
                {
                    Username = request.ReviewDto.Username,
                    ReviewText = request.ReviewDto.ReviewText
                };

                smartphone.AddReview(review); // Dodaj recenzję do smartfona

                await _smartphoneRepository.UpdateSmartphoneAsync(smartphone); // Zaktualizuj smartfona w repozytorium

                return review;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
