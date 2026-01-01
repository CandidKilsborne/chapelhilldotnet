# Event CMS Documentation

## Overview

This Content Management System (CMS) provides a secure administrative interface for managing events in the Chapel Hill .NET & Azure Meetup website. The CMS is built with Blazor WebAssembly and is fully compatible with Azure Static Web Apps.

## Features

- **Secure Authentication**: Username and password protected admin area
- **Event Management**: Complete CRUD (Create, Read, Update, Delete) operations for events
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile devices
- **Data Persistence**: Events are stored locally in the browser (LocalStorage)
- **User-Friendly Interface**: Clean, intuitive design with confirmation dialogs for destructive actions

## Getting Started

### Accessing the CMS

1. Navigate to `/admin/login` in your browser
2. Enter the admin credentials:
   - **Username**: `admin`
   - **Password**: `Chapel2024!`
3. Click "Sign In"

### Managing Events

#### Viewing Events

After logging in, you'll see the Event Management dashboard (`/admin/events`) which displays:
- A table of all events sorted by date
- Event details: Title, Description, Date, Time, Location, and Attendees
- Action buttons: Edit and Delete for each event

#### Creating a New Event

1. Click the **"+ Add New Event"** button in the top right
2. Fill in the event details:
   - **Title** (required): Name of the event
   - **Description** (optional): Detailed information about the event
   - **Date** (required): When the event will occur
   - **Time** (optional): Specific time (e.g., "6:30 PM - 9:00 PM")
   - **Location** (required): Where the event will be held
   - **Expected Attendees** (optional): Number of anticipated attendees
3. Click **"Create Event"** to save
4. Click **"Cancel"** to discard changes and return to the event list

#### Editing an Event

1. Find the event you want to edit in the event list
2. Click the **"Edit"** link in the Actions column
3. Update the event details as needed
4. Click **"Update Event"** to save changes
5. Click **"Cancel"** to discard changes and return to the event list

#### Deleting an Event

1. Find the event you want to delete in the event list
2. Click the **"Delete"** button in the Actions column
3. A confirmation dialog will appear asking you to confirm the deletion
4. Click **"Delete"** to permanently remove the event
5. Click **"Cancel"** to close the dialog without deleting

### Logging Out

- Click the **"Logout"** button in the top right corner of any admin page
- You will be redirected to the login page
- Your session will be cleared

## Technical Details

### Architecture

The CMS is built using:
- **Blazor WebAssembly**: Client-side framework for building interactive web UIs
- **LocalStorage**: Browser-based storage for event data persistence
- **Service Layer**: Clean separation of concerns with interfaces and implementations

### Data Storage

- Events are stored in the browser's LocalStorage
- Data persists across browser sessions
- Each browser/device maintains its own copy of the data

### Security

- Simple authentication using LocalStorage tokens
- Admin routes are protected and redirect to login if not authenticated
- Confirmation dialogs prevent accidental deletions

## Production Deployment Recommendations

For production use with Azure Static Web Apps, consider these enhancements:

### Authentication
- **Azure AD B2C**: Implement enterprise-grade authentication
- **Azure Static Web Apps Authentication**: Use built-in authentication providers
- **Multi-Factor Authentication**: Add an extra layer of security

### Data Persistence
- **Azure Cosmos DB**: Replace LocalStorage with a cloud database
- **Azure Functions**: Create serverless API endpoints for CRUD operations
- **Azure SQL Database**: Alternative relational database option

### Additional Features
- **Image Upload**: Use Azure Blob Storage for event images
- **Email Notifications**: Send emails when events are created/updated
- **Event Categories**: Add categorization and tagging
- **Search & Filter**: Implement advanced search capabilities
- **Audit Logging**: Track all admin actions for security

## Troubleshooting

### Can't Log In
- Verify you're using the correct credentials: `admin` / `Chapel2024!`
- Clear your browser's LocalStorage and try again
- Check browser console for JavaScript errors

### Events Not Saving
- Ensure LocalStorage is enabled in your browser
- Check if you have sufficient storage space
- Verify form validation passes (all required fields filled)

### Events Disappeared
- Events are stored per browser/device
- Clearing browser data will remove events
- Consider implementing cloud storage for production

## Support

For issues or questions:
- Check the project repository for updates
- Review the source code in `/chapelhilldotnet.web/Pages/Admin/`
- Contact the development team

## Version History

- **v1.0.0** (2026-01-01): Initial CMS implementation
  - Basic authentication
  - Event CRUD operations
  - LocalStorage persistence
  - Responsive UI
