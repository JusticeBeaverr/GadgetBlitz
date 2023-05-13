using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using MediatR;

namespace GadgetBlitzZTPAI.Server.Application.Commands
{
    public record AddSmartphoneCommand(SmartphoneDTO smartphoneDto) : IRequest<Smartphone>
    {
    }

    public class AddSmartphoneCommandHandler : IRequestHandler<AddSmartphoneCommand, Smartphone>
    {
        private readonly ISmartphoneRepository _smartphoneRepository;

        private readonly IMapper _mapper;
        public AddSmartphoneCommandHandler(ISmartphoneRepository smartphoneRepository, IMapper mapper)
        {
            _smartphoneRepository = smartphoneRepository;
            _mapper = mapper;
        }
        public async Task<Smartphone> Handle(AddSmartphoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var smartphone = await _smartphoneRepository.GetByName(request.smartphoneDto.Name);
                if(smartphone == null)
                {
                    var newSmartphone = Smartphone.Create(request.smartphoneDto.Name, request.smartphoneDto.RAM, request.smartphoneDto.Memory, request.smartphoneDto.ScreenDiagonal, request.smartphoneDto.Resolution, request.smartphoneDto.CamerasCount, request.smartphoneDto.AVGPrice, request.smartphoneDto.PhotoUrl);
                    newSmartphone.AddCameras(_mapper.Map<List<Camera>>(request.smartphoneDto.Cameras));
                    newSmartphone.AddColors(_mapper.Map<List<Color>>(request.smartphoneDto.Colors));

                    await _smartphoneRepository.AddOrReplaceSmartphoneAsync(newSmartphone);

                    return newSmartphone;
                }
                else
                {
                    smartphone.Modify(request.smartphoneDto.Name, request.smartphoneDto.RAM, request.smartphoneDto.Memory, request.smartphoneDto.ScreenDiagonal, request.smartphoneDto.Resolution, request.smartphoneDto.CamerasCount, request.smartphoneDto.AVGPrice, request.smartphoneDto.PhotoUrl);
                    smartphone.ReplaceCameras(_mapper.Map<List<Camera>>(request.smartphoneDto.Cameras));
                    smartphone.ReplaceColors(_mapper.Map<List<Color>>(request.smartphoneDto.Colors));

                    await _smartphoneRepository.AddOrReplaceSmartphoneAsync(smartphone);

                    return smartphone;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
