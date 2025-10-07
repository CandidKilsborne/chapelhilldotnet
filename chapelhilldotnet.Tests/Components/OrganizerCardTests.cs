using Bunit;
using chapelhilldotnet.web.Components;
using chapelhilldotnet.web.Models;
using Xunit;

namespace chapelhilldotnet.Tests.Components;

public class OrganizerCardTests : TestContext
{
    [Fact]
    public void OrganizerCard_RendersOrganizerName()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "John Doe",
            Bio = "Software Developer",
            ImageUrl = "/images/john.jpg",
            TwitterUrl = "https://twitter.com/johndoe",
            LinkedInUrl = "https://linkedin.com/in/johndoe",
            GitHubUrl = "https://github.com/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var nameElement = cut.Find("h3");
        Assert.Equal("John Doe", nameElement.TextContent);
    }

    [Fact]
    public void OrganizerCard_DisplaysBio()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "Jane Smith",
            Bio = "Cloud Architect",
            ImageUrl = "/images/jane.jpg",
            TwitterUrl = "",
            LinkedInUrl = "https://linkedin.com/in/janesmith",
            GitHubUrl = ""
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var bioElement = cut.Find("p");
        Assert.Equal("Cloud Architect", bioElement.TextContent);
    }

    [Fact]
    public void OrganizerCard_DisplaysImageWithCorrectSrc()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "Bob Wilson",
            Bio = "DevOps Engineer",
            ImageUrl = "/images/bob.jpg",
            TwitterUrl = "",
            LinkedInUrl = "",
            GitHubUrl = "https://github.com/bobwilson"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var imgElement = cut.Find("img");
        Assert.Equal("/images/bob.jpg", imgElement.GetAttribute("src"));
        Assert.Equal("Bob Wilson", imgElement.GetAttribute("alt"));
    }

    [Fact]
    public void OrganizerCard_ShowsLinkedInLinkWhenProvided()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "Alice Johnson",
            Bio = "Senior Developer",
            ImageUrl = "/images/alice.jpg",
            TwitterUrl = "",
            LinkedInUrl = "https://linkedin.com/in/alicejohnson",
            GitHubUrl = ""
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var linkedInLink = cut.Find("a[href='https://linkedin.com/in/alicejohnson']");
        Assert.NotNull(linkedInLink);
    }

    [Fact]
    public void OrganizerCard_ShowsGitHubLinkWhenProvided()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "Charlie Brown",
            Bio = "Full Stack Developer",
            ImageUrl = "/images/charlie.jpg",
            TwitterUrl = "",
            LinkedInUrl = "",
            GitHubUrl = "https://github.com/charliebrown"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var gitHubLink = cut.Find("a[href='https://github.com/charliebrown']");
        Assert.NotNull(gitHubLink);
    }

    [Fact]
    public void OrganizerCard_HidesTwitterLinkWhenNotProvided()
    {
        // Arrange
        var testOrganizer = new Organizer
        {
            Name = "Diana Prince",
            Bio = "Tech Lead",
            ImageUrl = "/images/diana.jpg",
            TwitterUrl = "",
            LinkedInUrl = "https://linkedin.com/in/dianaprince",
            GitHubUrl = "https://github.com/dianaprince"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, testOrganizer));

        // Assert
        var links = cut.FindAll("a");
        Assert.Equal(2, links.Count); // Only LinkedIn and GitHub links should be present
    }
}
