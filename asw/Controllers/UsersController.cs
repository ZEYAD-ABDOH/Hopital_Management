using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using asw.Data;
using asw.Model;
using asw.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Client.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly My_db_s _context;
        private readonly GetToken _getToken;
        public UsersController(My_db_s context , GetToken getToken)
        {
            _context = context;
            _getToken = getToken;   
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireRoleSuperisor")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        [HttpPost("login")]

       
        public async Task<IActionResult> login([FromBody] userlogin userlogin)
        {
            if(string.IsNullOrEmpty(userlogin.UserName)|| string.IsNullOrEmpty(userlogin.Password))
            {
                return BadRequest();
            }

            string pass= Encrypt(userlogin.Password);

            var lginuser = await _context.users.SingleOrDefaultAsync(u => u.UserName == userlogin.UserName && u.Password == userlogin.Password);
            if (lginuser == null)
            { 
                return Unauthorized("اسم المستخدم او كلمة المرور  ");

            }
            string Role;
            if (lginuser.Role== "1")
            {
                Role = "Admin";
            }
            else if (lginuser.Role == "2")
            {
                Role = "User";

            }  
            else if (lginuser.Role == "3")
            {
                Role = "Superisor";


            } else if (lginuser.Role == "4")
            {
                Role = "Sales";


            }

            else
            {

                return BadRequest("الصلاحية غير معروف");
            }
            var token = _getToken.CreateToken(userlogin.UserName, Role);
            return Ok(

                new
                {
                    m = "تم تسجيل بنجاح ",
                    Token = token,
                    Role = Role,
                }

                );
        }
        private string Encrypt(string password)
        {
            if (password==null)
            {
                throw new ArgumentException(nameof(password));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {

                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
                    
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.ID == id);
        }
    }
}
