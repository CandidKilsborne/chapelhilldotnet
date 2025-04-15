using chapelhilldotnet.app.Models;

namespace chapelhilldotnet.app.Data;

public static class OrganizersList
{
    public static List<Organizer> Organizers { get; } =
    [
        new()
        {
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
            Name = "Aaron Piotrowski",
            Bio = "Software Developer / Cloud Architect / Community Organizer / Entrepreneur",
            ImageUrl = "/images/organizers/aaron_piotrowski.jpg",
            TwitterUrl = "#",
            LinkedInUrl = "https://www.linkedin.com/in/aaronpiotrowski/",
            GitHubUrl = "https://github.com/CandidKilsborne"
        },
        new()
        {
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