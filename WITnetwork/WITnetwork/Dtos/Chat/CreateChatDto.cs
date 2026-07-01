using System.ComponentModel.DataAnnotations;

namespace WITnetwork.Dtos;

public record CreateChatDto (
    List<Guid> ParticipantsIds,
    string Name,
    bool IsGroup = false
);