using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;


namespace BusinessLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly DataAccessLayer.DBContext.HRMSContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataAccessLayer.DBContext.HRMSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<DataAccessLayer.DBContext.User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<DataAccessLayer.DBContext.User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<DataAccessLayer.DBContext.User> CreateUserAsync(DataAccessLayer.DBContext.User user)
        {
            user.CreatedDate = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Send Welcome Email after user creation
            await SendWelcomeEmailAsync(user);

            return user;
        }

        public async Task<DataAccessLayer.DBContext.User?> UpdateUserAsync(int id, DataAccessLayer.DBContext.User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return null;

            existingUser.FullName = updatedUser.FullName;
            existingUser.Email = updatedUser.Email;
            existingUser.RoleId = updatedUser.RoleId;
           // existingUser.IsActive = updatedUser.IsActive;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task SendWelcomeEmailAsync(DataAccessLayer.DBContext.User user)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    ),
                    EnableSsl = true,
                };

                string subject = "Welcome to HRMS – Your Login Details";
                string body = $@"
Hi {user.FullName},<br/><br/>
Welcome to <b>Cortracker HRMS</b>! 🎉<br/><br/>
Your HRMS account has been created.<br/><br/>
<b>Login URL:</b> https://ha.cortracker360.com<br/>
<b>Username:</b> {user.Email}<br/>
<b>Password:</b> {user.PasswordHash}<br/><br/>
Please update your password after your first login.<br/><br/>
Regards,<br/>HR Team
";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:From"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }
    }
}

