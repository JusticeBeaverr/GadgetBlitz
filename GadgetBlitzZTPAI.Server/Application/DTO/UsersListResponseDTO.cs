namespace GadgetBlitzZTPAI.Server.Application.DTO
{
    public class UsersListResponseDTO
    {
        public List<UserResponseDTO> Items { get; set; }

        public UsersListResponseDTO(List<UserResponseDTO> items)
        {
            Items = items;
        }
    }
}
