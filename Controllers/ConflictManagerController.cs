using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawnMowingService.Models; // Update this with the actual namespace of your models
using LawnMowingService.Data; // Ensure this matches your project structure
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization; // Add this line

namespace LawnMowingService.Controllers
{
    public class ConflictManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public ConflictManagerController(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ConflictManagementDashboard
        public async Task<IActionResult> ConflictManagementDashboard()
        {
            var userId = HttpContext.Session.GetString("UserID");
            var user = await _userManager.FindByEmailAsync(userId);

            // Retrieve all operators from the Operators table
            var operators = await _context.Operators
                .Include(o => o.Bookings) // Include bookings if needed
                .ToListAsync();

            // Retrieve bookings that are associated with the operators
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Machine)
                .Include(b => b.Operator)
                .ToListAsync(); // Get all relevant bookings asynchronously

            ViewBag.Operators = operators;
            return View(bookings);
        }


        // POST: AssignOperator
        [HttpPost]
        [Route("ConflictManager/AssignOperator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignOperator(int bookingId, int operatorId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.OperatorId = operatorId;
                booking.Status = "Assigned"; // Change status to Assigned
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ConflictManagementDashboard");
        }

        // New method to mark booking as completed
  [HttpPost]
[Route("ConflictManager/CompleteBooking")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CompleteBooking(int bookingId)
{
    var booking = await _context.Bookings.FindAsync(bookingId);
    if (booking != null)
    {
        booking.Status = "Completed"; // Change status to Completed
        await _context.SaveChangesAsync();
    }

    // Redirect to the operator dashboard
    return RedirectToAction("Dashboard", "Operator");
}

        public async Task<IActionResult> ManageConflicts(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Machine)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Find an alternative machine
            var availableMachines = await _context.Machines
                .Where(m => !_context.Bookings.Any(b => b.MachineId == m.Id && b.Date == booking.Date))
                .ToListAsync();

            ViewBag.AvailableMachines = availableMachines;
            return View(booking);
        }

        public async Task<IActionResult> Dashboard()
        {
            var operatorId = GetCurrentOperatorId(); // Get the current operator's ID
            var operatorMachineId = _context.Operators
                .Where(o => o.Id == operatorId)
                .Select(o => o.MachineId)
                .FirstOrDefault();

            var bookings = await _context.Bookings
                .Include(b => b.Customer) // Ensure Customer is included
                .Include(b => b.Machine)
                .Where(b => b.MachineId == operatorMachineId && b.Status == "Assigned") // Filter by the operator's machine ID and status
                .ToListAsync();

            ViewBag.Operators = await _context.Operators.ToListAsync(); // Ensure operators are available for assignment
            return View(bookings);
        }

        private int GetCurrentOperatorId()
        {
            // Implement logic to retrieve the current operator's ID
            // This could be from the User claims or session, depending on your authentication setup
            return 1; // Placeholder for demonstration; replace with actual logic
        }
    }
}
