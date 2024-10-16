# Lawn-Mowing Service README

## Project Overview
The Lawn-Mowing Service application allows customers to easily book lawn care services. The system consists of three main roles: Customer, Admin (Conflict Manager), and Operator.

## User Workflow
### Registration & Login
Customers can register an account and log in to the system.

### Creating a Booking
Once logged in, customers can create a booking for lawn care services.

### Admin Role
The Admin (Conflict Manager) receives incoming bookings and assigns them to available Operators.

### Operator Role
Operators receive notifications of assigned bookings. After completing the service, they update the booking status to "Completed."

### Status Updates
Both the Admin and Customers can view the status of their bookings on their respective dashboards.

## Project Structure
- **Frontend**: User interface for customers, Admin, and Operators.
- **Backend**: Manages user authentication, booking creation, status updates, and notifications.
- **Database**: Stores user data, bookings, and status updates.

## How the Project Runs
### Setup
- Ensure all dependencies are installed.
- Run database migrations to set up the required tables.

### Configuration
Admin and Operator login details are stored in the `appsettings.json` file. Modify this file to set the appropriate credentials.

### Running the Application
- Start the server using the appropriate command (e.g., `dotnet run` for a .NET application).
- Access the application through your web browser at `http://localhost:[port].`

## Admin and Operator Login Details
The default credentials for the Admin and Operator roles are specified in the `appsettings.json` file.

  "AdminUser": {
    "Email": "admin@lms.com",
    "Password": "AdminTest123!",
    "Name": "Admin User"
  },
  "OperatorUser1": {
    "Email": "operator1@lms.com",
    "Password": "Operator1Test123!",
    "Name": "Operator One"
  },
  "OperatorUser2": {
    "Email": "operator2@lms.com",
    "Password": "Operator2Test123!",
    "Name": "Operator Two"
  },
Note: Make sure to change these credentials before deploying the application to ensure security.

Questions or Feedback
If you have any questions or need further assistance, please don't hesitate to reach out. Your feedback is valuable for improving the application!
