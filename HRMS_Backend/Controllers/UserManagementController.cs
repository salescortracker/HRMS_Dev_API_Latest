using BusinessLayer.DTOs;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IRegionService _regionService;
        private readonly IUserService _userService;
        private readonly IMenuMasterService _menuService;
        private readonly IRoleMasterService _roleService;
        private readonly IMenuRoleService _menuRoleService;
        private readonly IEmployeeFamilyService _employeeFamilyService;
        private readonly IEmployeeEmergencyContactService _emergencyContactService;
        private readonly IEmployeeImmigrationService _employeeImmigrationService;
        public UserManagementController(ICompanyService companyService, IRegionService regionService, IUserService userService
            , IMenuMasterService menuService, IRoleMasterService roleService, IMenuRoleService menuRoleService, IEmployeeFamilyService employeeFamilyService, IEmployeeEmergencyContactService emergencyContactService, IEmployeeImmigrationService employeeImmigrationService)
        {
            _companyService = companyService;
            _regionService = regionService;
            _userService = userService;
            _menuService = menuService;
            _roleService = roleService;
            _menuRoleService = menuRoleService;
            _employeeFamilyService = employeeFamilyService;
            _emergencyContactService = emergencyContactService;
            _employeeImmigrationService = employeeImmigrationService;
        }
        #region Company Details
        /// <summary>
        /// Retrieves a list of all companies.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch all companies from the data
        /// source.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a collection of companies.  Returns an HTTP 200 status code with
        /// the list of companies if successful.</returns>
        [HttpGet]
        [Route("GetCompany")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }
        /// <summary>
        /// Retrieves a company by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch the company details. Ensure
        /// the <paramref name="id"/> corresponds to a valid company record.</remarks>
        /// <param name="id">The unique identifier of the company to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the company data if found; otherwise, a <see
        /// cref="NotFoundResult"/> if the company does not exist.</returns>
        [HttpGet]
        [Route("GetCompanyById")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }
        /// <summary>
        /// Searches for companies based on the specified filter criteria.
        /// </summary>
        /// <remarks>The filter object must be structured according to the requirements of the underlying
        /// search service. Ensure that the filter contains valid criteria to avoid unexpected results.</remarks>
        /// <param name="filter">An object containing the filter criteria for the search. The structure and fields of the filter object
        /// depend on the implementation of the search service.</param>
        /// <returns>An <see cref="IActionResult"/> containing the search results. The result is a collection of companies that
        /// match the specified filter criteria.</returns>
        [HttpPost]
        [Route("GetCompanySearch")]
        public async Task<IActionResult> Search([FromBody] object filter)
        {
            var companies = await _companyService.SearchCompaniesAsync(filter);
            return Ok(companies);
        }
        /// <summary>
        /// Creates a new company and returns the created resource with its location.
        /// </summary>
        /// <remarks>This method uses the HTTP POST verb to create a new company. The created resource's
        /// URI is included in the response.</remarks>
        /// <param name="dto">The data transfer object containing the details of the company to create.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> containing the details of the created company and the URI of the
        /// resource.</returns>
        [HttpPost]
        [Route("SaveCompany")]
        public async Task<IActionResult> Create([FromBody] CompanyDto dto)
        {
            var company = await _companyService.AddCompanyAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = company.CompanyId }, company);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdateCompany/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyDto dto)
        {
            var updated = await _companyService.UpdateCompanyAsync(id, dto);
            return Ok(updated);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("DeleteCompany/{id}")]
       
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion
        #region Region Details
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRegion")]
        public async Task<IActionResult> GetRegion()
        {
            var regions = await _regionService.GetAllRegionsAsync();
            return Ok(regions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRegionById")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            var region = await _regionService.GetRegionByIdAsync(id);
            if (region == null) return NotFound();
            return Ok(region);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRegion")]
        public async Task<IActionResult> SaveRegion([FromBody] object model)
        {
            var region = await _regionService.AddRegionAsync(model);
            return CreatedAtAction(nameof(GetRegionById), new { id = region.RegionID }, region);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateRegion/{id}")]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] object model)
        {
            var region = await _regionService.UpdateRegionAsync(id, model);
            return Ok(region);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRegion/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var result = await _regionService.DeleteRegionAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRegionSearch")]
        public async Task<IActionResult> GetRegionSearch([FromBody] object filter)
        {
            var regions = await _regionService.SearchRegionsAsync(filter);
            return Ok(regions);
        }
        #endregion
        #region User Details
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        #endregion
        #region Menu Master Details
        /// <summary>
        /// Get all menus
        /// </summary>
        [HttpGet("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus()
       {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        /// <summary>
        /// Get menu by ID
        /// </summary>
        [HttpGet("GetMenuById/{id:int}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
                return NotFound(new { message = "Menu not found" });

            return Ok(menu);
        }

        /// <summary>
        /// Search menus dynamically by MenuName, ParentMenuID, IsActive, etc.
        /// </summary>
        [HttpPost("SearchMenus")]
        public async Task<IActionResult> SearchMenus([FromBody] object filter)
        {
            var results = await _menuService.SearchMenusAsync(filter);
            return Ok(results);
        }

        /// <summary>
        /// Create a new menu
        /// </summary>
        [HttpPost("CreateMenu")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Example: retrieve CreatedBy from token later if you have authentication
            var createdBy = 1; // placeholder for now
            var menu = await _menuService.AddMenuAsync(dto, createdBy);
            return CreatedAtAction(nameof(GetMenuById), new { id = menu.MenuID }, menu);
        }

        /// <summary>
        /// Update an existing menu
        /// </summary>
        [HttpPost("UpdateMenu/{id:int}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modifiedBy = 1; // placeholder for now
            try
            {
                var updatedMenu = await _menuService.UpdateMenuAsync(id, dto, modifiedBy);
                return Ok(updatedMenu);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete menu by ID
        /// </summary>
        [HttpPost("DeleteMenu/{id:int}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var deleted = await _menuService.DeleteMenuAsync(id);
            if (!deleted)
                return NotFound(new { message = "Menu not found" });

            return NoContent();
        }

        /// <summary>
        /// Get all active menus
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveMenus()
        {
            var activeMenus = await _menuService.GetActiveMenusAsync();
            return Ok(activeMenus);
        }
        #endregion
        #region Role Details
        // ✅ GET: api/RoleMaster
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // ✅ GET: api/RoleMaster/{id}
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            return Ok(role);
        }

        // ✅ POST: api/RoleMaster
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRole = await _roleService.AddRoleAsync(dto);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.RoleId }, createdRole);
        }

        // ✅ PUT: api/RoleMaster/{id}
        [HttpPost("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(id, dto);
                return Ok(updatedRole);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // ✅ DELETE: api/RoleMaster/{id}
        [HttpPost("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var deleted = await _roleService.DeleteRoleAsync(id);
            if (!deleted)
                return NotFound(new { message = "Role not found or already deleted" });

            return Ok(new { message = "Role deleted successfully" });
        }

        // ✅ POST: api/RoleMaster/search
        [HttpPost("search")]
        public async Task<IActionResult> SearchRoles(
            [FromBody] object filter,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isDescending = false)
        {
            var roles = await _roleService.SearchRolesAsync(filter, pageNumber, pageSize, sortBy, isDescending);
            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = roles.Count(),
                Data = roles
            });
        }
        #endregion
        #region menurolemaster
        /// <summary>
        /// Assign permissions for multiple roles.
        /// </summary>
        [HttpPost("AssignMultipleRoles")]
        public async Task<IActionResult> AssignPermissionsToMultipleRoles([FromBody] List<RolePermissionRequestDto> rolePermissions)
        {
            if (rolePermissions == null || !rolePermissions.Any())
                return BadRequest("Role permissions data cannot be empty.");

            var success = await _menuRoleService.AssignPermissionsToMultipleRolesAsync(rolePermissions);
            return success ? Ok(new { Message = "Permissions assigned successfully." }) : StatusCode(500, "Failed to assign permissions.");
        }

        /// <summary>
        /// Get permissions for multiple roles.
        /// </summary>
        [HttpPost("GetPermissionsForMultipleRoles")]
        public async Task<IActionResult> GetPermissionsForMultipleRoles([FromBody] List<int> roleIds)
        {
            if (roleIds == null || !roleIds.Any())
                return BadRequest("Role IDs list cannot be empty.");

            var result = await _menuRoleService.GetPermissionsForMultipleRolesAsync(roleIds);
            return Ok(result);
        }
        /// <summary>
        /// Assign permissions for a single role.
        /// </summary>
        [HttpPost("assign-permissions/{roleId}")]
        public async Task<IActionResult> AssignPermissionsToRole(int roleId, [FromBody] List<MenuRoleDto> permissions)
        {
            if (permissions == null || !permissions.Any())
                return BadRequest("Permissions list cannot be empty.");

            var success = await _menuRoleService.AssignPermissionsToRoleAsync(roleId, permissions);
            if (success)
                return Ok(new { message = "Permissions assigned successfully." });

            return StatusCode(500, "Failed to assign permissions.");
        }

        /// <summary>
        /// Get all assigned permissions for a role.
        /// </summary>
        [HttpGet("get-permissions/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRole(int roleId)
        {
            var result = await _menuRoleService.GetPermissionsByRoleAsync(roleId);
            return Ok(result);
        }
        #endregion

        #region Employee Family Details

        /// <summary>
        /// Retrieves a list of all employee family details.
        /// </summary>
        /// <remarks>
        /// This method performs an asynchronous operation to fetch all employee family details
        /// from the data source.
        /// </remarks>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a collection of employee family records. 
        /// Returns an HTTP 200 status code with the list of records if successful.
        /// </returns>
        [HttpGet("GetAllEmployeeFamilyDetails")]
        public async Task<IActionResult> GetAllEmployeeFamilyDetails()
        {
            var result = await _employeeFamilyService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an employee family record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee family record to retrieve.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the employee family data if found; 
        /// otherwise, a <see cref="NotFoundResult"/> if the record does not exist.
        /// </returns>
        [HttpGet("GetEmployeeFamilyDetailById/{id}")]
        public async Task<IActionResult> GetEmployeeFamilyDetailById(int id)
        {
            var record = await _employeeFamilyService.GetByIdAsync(id);
            if (record == null)
                return NotFound();
            return Ok(record);
        }

        /// <summary>
        /// Creates a new employee family record.
        /// </summary>
        /// <param name="model">The data transfer object containing the details of the employee family record to create.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> containing the details of the created record.
        /// </returns>
        [HttpPost("CreateEmployeeFamilyDetail")]
        public async Task<IActionResult> CreateEmployeeFamilyDetail([FromBody] EmployeeFamilyDetail model)
        {
            var result = await _employeeFamilyService.AddAsync(model);
            return CreatedAtAction(nameof(GetEmployeeFamilyDetailById), new { id = result.FamilyId }, result);
        }

        /// <summary>
        /// Updates an existing employee family record.
        /// </summary>
        /// <param name="id">The unique identifier of the record to update.</param>
        /// <param name="model">The updated employee family details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the updated record details if successful; 
        /// otherwise, a <see cref="BadRequestResult"/> if the IDs do not match.
        /// </returns>
        [HttpPut("UpdateEmployeeFamilyDetail/{id}")]
        public async Task<IActionResult> UpdateEmployeeFamilyDetail(int id, [FromBody] EmployeeFamilyDetail model)
        {
            if (id != model.FamilyId)
                return BadRequest("Family ID mismatch.");

            var result = await _employeeFamilyService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Deletes an employee family record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the record to delete.</param>
        /// <returns>
        /// Returns an HTTP 204 (No Content) status if successful; otherwise, HTTP 404 (Not Found).
        /// </returns>
        [HttpDelete("DeleteEmployeeFamilyDetail/{id}")]
        public async Task<IActionResult> DeleteEmployeeFamilyDetail(int id)
        {
            var deleted = await _employeeFamilyService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        #endregion

        #region Employee Emergency Contact Details

        /// <summary>
        /// Retrieves all employee emergency contact records.
        /// </summary>
        [HttpGet("GetAllEmployeeEmergencyContacts")]
        public async Task<IActionResult> GetAllEmployeeEmergencyContacts()
        {
            var contacts = await _emergencyContactService.GetAllAsync();
            return Ok(contacts);
        }

        /// <summary>
        /// Retrieves a specific emergency contact by its ID.
        /// </summary>
        [HttpGet("GetEmployeeEmergencyContactById/{id:int}")]
        public async Task<IActionResult> GetEmployeeEmergencyContactById(int id)
        {
            var contact = await _emergencyContactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound(new { Message = "Emergency contact not found." });

            return Ok(contact);
        }

        /// <summary>
        /// Retrieves emergency contacts by Employee ID.
        /// </summary>
        [HttpGet("GetEmergencyContactsByEmployeeId/{employeeId:int}")]
        public async Task<IActionResult> GetEmergencyContactsByEmployeeId(int employeeId)
        {
            var contacts = await _emergencyContactService.GetByEmployeeIdAsync(employeeId);
            return Ok(contacts);
        }

        /// <summary>
        /// Creates a new employee emergency contact record.
        /// </summary>
        [HttpPost("CreateEmployeeEmergencyContact")]
        public async Task<IActionResult> CreateEmployeeEmergencyContact([FromBody] EmployeeEmergencyContactDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emergencyContactService.AddAsync(model);
            return CreatedAtAction(nameof(GetEmployeeEmergencyContactById), new { id = result.EmergencyContactId }, result);
        }

        /// <summary>
        /// Updates an existing employee emergency contact record.
        /// </summary>
        [HttpPut("UpdateEmployeeEmergencyContact/{id:int}")]
        public async Task<IActionResult> UpdateEmployeeEmergencyContact(int id, [FromBody] EmployeeEmergencyContactDTO model)
        {
            if (id != model.EmergencyContactId)
                return BadRequest(new { Message = "EmergencyContactId mismatch." });

            try
            {
                var updated = await _emergencyContactService.UpdateAsync(model);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an employee emergency contact record by ID.
        /// </summary>
        [HttpDelete("DeleteEmployeeEmergencyContact/{id:int}")]
        public async Task<IActionResult> DeleteEmployeeEmergencyContact(int id)
        {
            var deleted = await _emergencyContactService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Emergency contact not found." });

            return NoContent();
        }

        #endregion

        #region Employee Immigration

        /// <summary>
        /// Retrieves a list of all employee immigration records.
        /// </summary>
        /// <remarks>
        /// This method performs an asynchronous operation to fetch all employee immigration records
        /// from the data source.
        /// </remarks>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a collection of employee immigration records.
        /// Returns an HTTP 200 status code with the list of records if successful.
        /// </returns>
        [HttpGet("GetAllEmployeeImmigrations")]
        public async Task<IActionResult> GetAllEmployeeImmigrations()
        {
            var result = await _employeeImmigrationService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an employee immigration record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee immigration record to retrieve.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the employee immigration data if found;
        /// otherwise, a <see cref="NotFoundResult"/> if the record does not exist.
        /// </returns>
        [HttpGet("GetEmployeeImmigrationById/{id}")]
        public async Task<IActionResult> GetEmployeeImmigrationById(int id)
        {
            var record = await _employeeImmigrationService.GetByIdAsync(id);
            if (record == null)
                return NotFound();
            return Ok(record);
        }

        /// <summary>
        /// Creates a new employee immigration record.
        /// </summary>
        /// <param name="model">The data transfer object containing the details of the employee immigration record to create.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> containing the details of the created record.
        /// </returns>
        [HttpPost("CreateEmployeeImmigration")]
        public async Task<IActionResult> CreateEmployeeImmigration([FromBody] EmployeeImmigration model)
        {
            var result = await _employeeImmigrationService.CreateAsync(model);
            return CreatedAtAction(nameof(GetEmployeeImmigrationById), new { id = result.ImmigrationId }, result);
        }

        /// <summary>
        /// Updates an existing employee immigration record.
        /// </summary>
        /// <param name="id">The unique identifier of the record to update.</param>
        /// <param name="model">The updated employee immigration details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the updated record details if successful;
        /// otherwise, a <see cref="BadRequestResult"/> if the IDs do not match.
        /// </returns>
        [HttpPut("UpdateEmployeeImmigration/{id}")]
        public async Task<IActionResult> UpdateEmployeeImmigration(int id, [FromBody] EmployeeImmigration model)
        {
            if (id != model.ImmigrationId)
                return BadRequest("Immigration ID mismatch.");

            var result = await _employeeImmigrationService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Deletes an employee immigration record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the record to delete.</param>
        /// <returns>
        /// Returns an HTTP 204 (No Content) status if successful; otherwise, HTTP 404 (Not Found).
        /// </returns>
        [HttpDelete("DeleteEmployeeImmigration/{id}")]
        public async Task<IActionResult> DeleteEmployeeImmigration(int id)
        {
            var deleted = await _employeeImmigrationService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        #endregion
    }
}

