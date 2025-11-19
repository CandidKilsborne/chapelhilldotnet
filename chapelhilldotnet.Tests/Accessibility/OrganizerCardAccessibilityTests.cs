using Bunit;
using chapelhilldotnet.web.Components;
using chapelhilldotnet.web.Models;
using Xunit;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for OrganizerCard component to ensure WCAG 2.1 AA compliance
/// </summary>
public class OrganizerCardAccessibilityTests : TestContext
{
    [Fact]
    public void OrganizerCard_UsesSemanticArticleElement()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            TwitterUrl = "https://twitter.com/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var article = cut.Find("article");
        Assert.NotNull(article);
    }

    [Fact]
    public void OrganizerCard_ImageHasAltText()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "Jane Smith",
            Bio = "DevOps Specialist",
            ImageUrl = "https://example.com/jane.jpg"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var img = cut.Find("img");
        Assert.Equal("Jane Smith", img.GetAttribute("alt"));
    }

    [Fact]
    public void OrganizerCard_SocialLinksHaveAriaLabels()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            LinkedInUrl = "https://linkedin.com/in/johndoe",
            GitHubUrl = "https://github.com/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var markup = cut.Markup;
        Assert.Contains($"aria-label=\"{organizer.Name} on LinkedIn\"", markup);
        Assert.Contains($"aria-label=\"{organizer.Name} on GitHub\"", markup);
    }

    [Fact]
    public void OrganizerCard_SocialNavHasAriaLabel()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            LinkedInUrl = "https://linkedin.com/in/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var nav = cut.Find("nav");
        Assert.Contains($"{organizer.Name}'s social media profiles", nav.GetAttribute("aria-label"));
    }

    [Fact]
    public void OrganizerCard_ExternalLinksHaveSecurityAttributes()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            LinkedInUrl = "https://linkedin.com/in/johndoe",
            GitHubUrl = "https://github.com/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var links = cut.FindAll("a");
        foreach (var link in links)
        {
            Assert.Equal("_blank", link.GetAttribute("target"));
            Assert.Equal("noopener noreferrer", link.GetAttribute("rel"));
        }
    }

    [Fact]
    public void OrganizerCard_IconsHaveAriaHidden()
    {
        // Arrange
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            LinkedInUrl = "https://linkedin.com/in/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var markup = cut.Markup;
        Assert.Contains("aria-hidden=\"true\"", markup);
    }

    [Fact]
    public void OrganizerCard_OnlyRendersProvidedSocialLinks()
    {
        // Arrange - Only LinkedIn provided
        var organizer = new Organizer
        {
            Id = 1,
            Name = "John Doe",
            Bio = "Software Engineer",
            ImageUrl = "https://example.com/image.jpg",
            LinkedInUrl = "https://linkedin.com/in/johndoe"
        };

        // Act
        var cut = RenderComponent<OrganizerCard>(parameters => parameters
            .Add(p => p.Organizer, organizer));

        // Assert
        var links = cut.FindAll("a");
        Assert.Single(links); // Only one link should be rendered
        Assert.Contains("LinkedIn", links[0].GetAttribute("aria-label"));
    }
}
