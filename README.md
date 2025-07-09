# Chapel Hill .NET User Group Website

A modern Blazor WebAssembly application for the Chapel Hill .NET User Group community.

## Project Overview

This is a static website built with Blazor WebAssembly and styled with Tailwind CSS. The site showcases:

- **Events**: Upcoming and past .NET community events
- **Organizers**: Community leaders and their information
- **About**: Information about the user group
- **Modern UI**: Dark/light mode toggle and responsive design

## Technology Stack

- **.NET 9.0**: Latest .NET framework
- **Blazor WebAssembly**: Client-side web application framework
- **Tailwind CSS**: Utility-first CSS framework
- **Blazicons**: Icon libraries (FontAwesome, Lucide, Devicon)

## Project Structure

```
chapelhilldotnet.web/
├── Components/          # Reusable Blazor components
│   ├── EventCard.razor     # Event display component
│   └── OrganizerCard.razor # Organizer profile component
├── Data/               # Static data classes
│   ├── EventsList.cs      # Event data
│   └── OrganizersList.cs  # Organizer data
├── Layout/             # Layout and navigation components
│   ├── MainLayout.razor   # Main page layout
│   ├── NavMenu.razor      # Navigation menu
│   ├── Footer.razor       # Site footer
│   └── DarkModeToggle.razor # Theme switcher
├── Models/             # Data models
│   ├── Event.cs          # Event model
│   ├── Organizer.cs      # Organizer model
│   └── Feature.cs        # Feature model
├── Pages/              # Page components
│   ├── Home.razor        # Homepage
│   ├── Events.razor      # Events listing
│   ├── About.razor       # About page
│   └── Error.razor       # Error page
└── wwwroot/            # Static assets
    ├── css/              # Stylesheets
    ├── images/           # Images and assets
    └── index.html        # Entry point
```

## Development Guidelines

### Coding Standards
- Follow Microsoft C# coding conventions
- Use PascalCase for public members
- Use camelCase for private fields and local variables
- Prefer explicit types over `var` for clarity
- Use meaningful component and method names

### Blazor Component Patterns
- Keep components focused and single-purpose
- Use `@code` blocks for component logic
- Implement proper parameter binding with `[Parameter]`
- Use CSS isolation for component-specific styles
- Follow the Container/Presenter pattern where applicable

### Styling Conventions
- Use Tailwind utility classes for styling
- Implement responsive design with Tailwind breakpoints
- Support both dark and light themes
- Maintain consistent spacing and typography

### Data Management
- Store static data in dedicated classes in the `Data` folder
- Use strongly-typed models in the `Models` folder
- Implement proper data validation where needed

## Key Features

### Dark Mode Support
The application includes a comprehensive dark mode toggle that:
- Switches between light and dark themes
- Persists user preference in localStorage
- Uses Tailwind's dark mode utilities

### Responsive Design
- Mobile-first approach
- Tailwind responsive utilities
- Optimized for various screen sizes

### Performance Optimizations
- Blazor WebAssembly for client-side execution
- Minimal bundle size
- Optimized images and assets

## Development Commands

```bash
# Build the project
dotnet build

# Run in development mode
dotnet run

# Publish for production
dotnet publish -c Release
```

## Contributing

When contributing to this project:

1. Follow the established coding conventions
2. Ensure responsive design principles
3. Test both light and dark themes
4. Maintain component isolation
5. Update documentation as needed

## File Naming Conventions

- **Components**: PascalCase with `.razor` extension
- **Models**: PascalCase with `.cs` extension
- **Pages**: PascalCase with `.razor` extension
- **CSS Files**: kebab-case with `.css` extension
- **Images**: lowercase with underscores

This structure and these conventions help maintain code quality and make the project more maintainable for the community.
