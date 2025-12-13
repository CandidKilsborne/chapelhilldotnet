using chapelhilldotnet.web.Models;

namespace chapelhilldotnet.web.Data;

public static class OrganizersList
{
    public static List<Organizer> Organizers { get; } =
    [
        new()
        {
            Id = 1,
            Name = "This Could Be You!",
            Bio =
                "We're always looking for passionate community members who are eager to help organize events and grow our group. If you're interested, let us know!",
            ImageUrl = "/images/organizers/undraw_profile-pic_fatv.png",
            TwitterUrl = "#",
            LinkedInUrl = "#",
            GitHubUrl = "#"
        },
        new()
        {
            Id = 2,
            Name = "Aaron Piotrowski",
            Bio = "Software Developer / Cloud Architect / Community Organizer / Entrepreneur",
            ImageUrl = "/images/organizers/aaron_piotrowski.jpg",
            TwitterUrl = "#",
            LinkedInUrl = "https://www.linkedin.com/in/aaronpiotrowski/",
            GitHubUrl = "https://github.com/CandidKilsborne"
        },
        new()
        {
            Id = 3,
            Name = "This Could Be You!",
            Bio =
                "We're always looking for passionate community members who are eager to help organize events and grow our group. If you're interested, let us know!",
            ImageUrl = "/images/organizers/undraw_male-avatar_zkzx.png",
            TwitterUrl = "#",
            LinkedInUrl = "#",
            GitHubUrl = "#"
        }
    ];
}