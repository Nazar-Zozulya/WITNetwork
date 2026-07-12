using System.ComponentModel.DataAnnotations;

namespace WITnetwork.Dtos;

public record CreateChatDto (
    List<long> ParticipantsIds,
    string Name,
    bool IsGroup = false
);