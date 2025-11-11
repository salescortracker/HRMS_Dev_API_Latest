using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        private readonly IDocumentTypeService _documentTypeService;
        public UserManagementController(ICompanyService companyService, IRegionService regionService, IUserService userService
            , IMenuMasterService menuService, IRoleMasterService roleService, IMenuRoleService menuRoleService, IDocumentTypeService documentTypeService)
        {
            _companyService = companyService;
            _regionService = regionService;
            _userService = userService;
            _menuService = menuService;
            _roleService = roleService;
            _menuRoleService = menuRoleService;
            _documentTypeService = documentTypeService;
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

        #region DocumentType
        /// <summary>
        /// Retrieves a list of all documentTypes.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch all documentTypes from the data
        /// source.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a collection of documentTypes.  Returns an HTTP 200 status code with
        /// the list of documentTypes if successful.</returns>
        [HttpGet]
        [Route("GetDocumentType")]
        public async Task<IActionResult> GetAllDocumentTypes()
        {
            try
            {
                var documentTypes = await _documentTypeService.GetAllDocumentTypeAsync();
                Log.Information("Fetched all DocumentType successfully from controller.");
                return Ok(documentTypes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all DocumentTypes in controller.");
                return StatusCode(500, "Internal server error");
            }
            
            
        }
        /// <summary>
        /// Retrieves a documnetType by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch the DocumentType details. Ensure
        /// the <paramref name="id"/> corresponds to a valid documentType record.</remarks>
        /// <param name="id">The unique identifier of the documentType to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the documentType data if found; otherwise, a <see
        /// cref="NotFoundResult"/> if the documentType does not exist.</returns>
        [HttpGet]
        [Route("GetDocumentTypeById")]
        public async Task<IActionResult> GetDocumentTypeById(int id)
        {
            try
            {
                var log = await _documentTypeService.GetDocumentTypeByIdAsync(id);
                if (log == null)
                {
                    Log.Warning("DocumentType with Id {Id} not found in controller.", id);
                    return NotFound($"DocumentType with Id {id} not found.");
                }
                Log.Information("Fetched DocumentType with Id {Id} from controller.", id);
                return Ok(log);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching DocumentType with Id {Id} in controller.", id);
                return StatusCode(500, "Internal server error");
            }
            
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
        [Route("GetdocumentTypeSearch")]
        public async Task<IActionResult> Search([FromQuery] string? documentTypeName, [FromQuery] bool? IsActive)
        {
            try
            {
                Log.Information("Received search request for DocumentType");

                var results = await _documentTypeService.SearchDocumentTypeAsync(
                   documentTypeName, IsActive

                );

                if (results == null || !results.Any())
                    return NotFound(new { Message = "No  DocumentType found for the specified filters." });

                return Ok(results);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while searching DocumentType");
                return StatusCode(500, new { Message = "An error occurred while searching DocumentTypes." });
            }
            
        }
        /// <summary>
        /// Creates a new documentType and returns the created resource with its location.
        /// </summary>
        /// <remarks>This method uses the HTTP POST verb to create a new documentType. The created resource's
        /// URI is included in the response.</remarks>
        /// <param name="dto">The data transfer object containing the details of the documentType to create.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> containing the details of the created documentType and the URI of the
        /// resource.</returns>
        [HttpPost]
        [Route("SavedocumentType")]
        public async Task<IActionResult> CreateDocumentType([FromBody] CreateDocumentTypeDto dto, [FromQuery] int createdby)
        {
            try
            {
                var created = await _documentTypeService.CreateDocumentTypeAsync(dto,createdby);
                var DocumentTypeId = created.Data.DocumentTypeId; // Accessing the Id from the ResponseDto
                var status = created.Status;
                var data = created.Data;

                if (status == "created")
                {
                    Log.Information("Created new DocumentType with Id {Id} in controller.", data.DocumentTypeId);
                    return CreatedAtAction(nameof(GetById), new { id = data.DocumentTypeId }, created);
                }

                if (status == "duplicate")
                {
                    Log.Warning("Duplicate DocumentType detected for DocumentTypeName: {DocumentTypeName}.", data.DocumentTypeName);
                    return Conflict(created); // HTTP 409 for duplicates
                }

                // Optional: handle unexpected status
                Log.Error("Unexpected status '{Status}' returned from CreateAsync.", status);
                return StatusCode(500, new { message = "Unexpected response from service." });


            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating  DocumentType in controller.");
                return StatusCode(500, "Internal server error");
            }
            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdatedocumentType/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDocumentTypeDto dto,int modifiedby)
        {
            try
            {
                var updated = await _documentTypeService.UpdateDocumentTypeAsync(id,dto, modifiedby);
                Log.Information("Updated DocumentType with Id {Id} in controller.", dto.DocumentTypeId);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating DocumentType with Id {Id} in controller.", dto.DocumentTypeId);
                return StatusCode(500, "Internal server error");
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("DeletedocumentType/{id}")]

        public async Task<IActionResult> DeleteDocumentType(int id)
        {

            try
            {
                var deleted = await _documentTypeService.DeleteDocumentTypeAsync(id);
                if (!deleted)
                {
                    Log.Warning("DocumentType with Id {Id} not found for deletion in controller.", id);
                    return NotFound($"DocumentType with Id {id} not found for deletion.");
                    
                }

                Log.Information("Deleted DocumentType with Id {Id} in controller.", id);
                return Ok($"DocumentType with Id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting DocumentType with Id {Id} in controller.", id);
                return StatusCode(500, "Internal server error");
            }
            
        }

        /// <summary>
        /// Bulk upload Document types from Excel
        /// </summary>
        /// <param name="request">Form data containing the Excel file</param>
        /// <param name="createdBy">User ID performing the upload</param>
        [HttpPost("BulkUpload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> BulkUpload([FromForm] BulkUploadRequest request, int createdBy)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file uploaded.");


            try
            {
                (int insertedCount, int updatedCount)? result = await _documentTypeService.BulkUploadAsync(request.File, createdBy);
                if (result == null)
                {
                    Log.Warning("File {File} not found for bulk upload.", request.File.FileName);
                    return NotFound("File not found or empty.");
                }

                Log.Information("Bulk upload completed in controller.");
                return Ok(new
                {
                    Message = "Bulk upload completed successfully.",
                    Inserted = result.Value.insertedCount,
                    Updated = result.Value.updatedCount
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during bulk upload with file {File}.", request.File.FileName);
                return StatusCode(500, "Internal server error");
            }
        }
            #endregion

        }
}
