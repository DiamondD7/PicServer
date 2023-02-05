using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PicWorldServer.Data;
using PicWorldServer.Model;
using System.Security.Cryptography;
using System.Text;

namespace PicWorldServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [ActionName("GetAllUserData")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserData()
        {
            return await _context.User.ToListAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetaUserData")]
        public async Task<ActionResult<User>> GetaUserData(int id)
        {
            try
            {
                var findUserId = await _context.User.FindAsync(id);
                if (findUserId == null)
                {
                    return NotFound();
                }
                return findUserId;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUserWithPassword")]
        public async Task<ActionResult<User>> GetUserWithPassword([FromQuery]string password)
        {
            var hashedPassword = HashPassword(password);
            var loginUser = _context.User.FirstOrDefault(x => x.Password == hashedPassword);
            if(loginUser == null)
            {
                return NotFound("No users found");
            }

            return loginUser;
        }

        [HttpPost]
        [ActionName("AddUserData")]
        public async Task<ActionResult<User>> AddUserData(User user)
        {
            try
            {
                var HashedPW = HashPassword(user.Password);
                var userNew = new User();
                userNew.UserName = user.UserName;
                userNew.FirstName = user.FirstName;
                userNew.LastName = user.LastName;
                userNew.Posts = user.Posts;
                userNew.Bio = user.Bio;
                userNew.Email = user.Email;
                userNew.Password = HashedPW;
                userNew.ProfilePicture = user.ProfilePicture;


                _context.User.Add(userNew);
                await _context.SaveChangesAsync();
                return Ok("Added");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        [ActionName("UpdateUserData")]
        public async Task<IActionResult> UpdateUserData(int id, User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest();
                }

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteUserData")]
        public async Task<ActionResult<User>> DeleteUserData(int id)
        {
            try
            {
                var findUserId = await _context.User.FindAsync(id);
                if(findUserId == null)
                {
                    return NotFound();
                }

                _context.User.Remove(findUserId);
                await _context.SaveChangesAsync();
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

       
    }
}
