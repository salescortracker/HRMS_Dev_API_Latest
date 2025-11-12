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
        private readonly IEmployeeEducationService _employeeEducationService;
        private readonly IEmployeeCertificationService _employeeCertificationService;
        private readonly IEmployeeDocumentService _employeeDocumentService;
        private readonly IWebHostEnvironment _env;

        public UserManagementController(ICompanyService companyService, IRegionService regionService, IUserService userService
            , IMenuMasterService menuService, IRoleMasterService roleService, IMenuRoleService menuRoleService, IEmployeeEducationService employeeEducationService, IEmployeeCertificationService employeeCertificationService, IEmployeeDocumentService employeeDocumentService, IWebHostEnvironment env)
        {
            _companyService = companyService;
            _regionService = regionService;
            _userService = userService;
            _menuService = menuService;
            _roleService = roleService;
            _menuRoleService = menuRoleService;
            _employeeEducationService = employeeEducationService;

            _employeeCertificationService = employeeCertificationService;
            _employeeDocumentService = employeeDocumentService;
            _env = env;

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

        #region Employee Education

        /// <summary>
        /// Get all education records for a specific employee
        /// </summary>
        [HttpGet("employee/{employeeId}/education")]
        public async Task<IActionResult> GetEmployeeEducations(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest(new { message = "Invalid employeeId" });

            var data = await _employeeEducationService.GetByEmployeeIdAsync(employeeId);

            if (data == null || !data.Any())
                return NotFound(new { message = "No education records found for this employee" });

            return Ok(data);
        }

        /// <summary>
        /// Get employee education by EducationId
        /// </summary>
        [HttpGet("education/{id}")]
        public async Task<IActionResult> GetEmployeeEducationById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid education id" });

            var data = await _employeeEducationService.GetByIdAsync(id);

            if (data == null)
                return NotFound(new { message = "Education record not found" });

            return Ok(data);
        }

        /// <summary>
        /// Add new employee education
        /// </summary>
        [HttpPost("education")]
        public async Task<IActionResult> SaveEmployeeEducation([FromForm] EmployeeEducationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Handle optional certificate file upload
            string webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "Uploads", "EducationCertificates");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (model.CertificateFile != null && model.CertificateFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{model.CertificateFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.CertificateFile.CopyToAsync(stream);
                }

                model.CertificateFilePath = Path.Combine("Uploads", "EducationCertificates", fileName)
                    .Replace("\\", "/");
            }

            model.CreatedBy = 0;

            var newId = await _employeeEducationService.AddAsync(model);

            return CreatedAtAction(nameof(GetEmployeeEducationById), new { id = newId },
                new { message = "Saved successfully", id = newId });
        }

        /// <summary>
        /// Update employee education
        /// </summary>
        [HttpPut("education/{id}")]
        public async Task<IActionResult> UpdateEmployeeEducation(int id, [FromForm] EmployeeEducationDto model)
        {
            if (id != model.EducationId)
                return BadRequest("ID mismatch");

            // File update logic
            string webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "Uploads", "EducationCertificates");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (model.CertificateFile != null && model.CertificateFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{model.CertificateFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.CertificateFile.CopyToAsync(stream);
                }

                model.CertificateFilePath = Path.Combine("Uploads", "EducationCertificates", fileName)
                    .Replace("\\", "/");
            }

            model.ModifiedBy ??= 0;

            var result = await _employeeEducationService.UpdateAsync(model);

            if (!result)
                return NotFound("Education record not found");

            return Ok(new { message = "Updated successfully" });
        }

        /// <summary>
        /// Delete employee education
        /// </summary>
        [HttpDelete("education/{id}")]
        public async Task<IActionResult> DeleteEmployeeEducation(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid id" });

            var result = await _employeeEducationService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = "Record not found or already deleted" });

            return Ok(new { message = "Deleted successfully" });
        }
        // Mode Of Study Dropdown

        [HttpGet("employeeeducation/modeofstudy")]
        public async Task<IActionResult> GetModeOfStudyList()
        {
            var result = await _employeeEducationService.GetModeOfStudyListAsync();

            if (result == null || !result.Any())
                return NotFound(new { message = "No active mode of study found" });

            return Ok(result);
        }


        #endregion

        #region Employee Certification

        /// <summary>
        /// Get all certification records for a specific employee
        /// </summary>
        [HttpGet("employee/{employeeId}/certifications")]
        public async Task<IActionResult> GetEmployeeCertifications(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest(new { message = "Invalid employeeId" });

            var data = await _employeeCertificationService.GetEmployeeCertificationsAsync(employeeId);

            if (data == null || !data.Any())
                return NotFound(new { message = "No certification records found for this employee" });

            return Ok(data);
        }

        /// <summary>
        /// Get employee certification by CertificationId
        /// </summary>
        [HttpGet("certification/{id}")]
        public async Task<IActionResult> GetCertificationById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid certification id" });

            var data = await _employeeCertificationService.GetCertificationByIdAsync(id);

            if (data == null)
                return NotFound(new { message = "Certification record not found" });

            return Ok(data);
        }

        /// <summary>
        /// Add new employee certification (with file upload)
        /// </summary>
        [HttpPost("certification")]
        public async Task<IActionResult> SaveEmployeeCertification([FromForm] EmployeeCertificationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "Uploads", "Certifications");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (model.DocumentFile != null && model.DocumentFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{model.DocumentFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.DocumentFile.CopyToAsync(stream);
                }

                model.DocumentPath = Path.Combine("Uploads", "Certifications", fileName).Replace("\\", "/");
            }

            model.CreatedBy ??= 0; // ✅ fix for int?
            model.CreatedDate = DateTime.Now;

            var newId = await _employeeCertificationService.CreateCertificationAsync(model);

            return CreatedAtAction(nameof(GetCertificationById), new { id = newId },
                new { message = "Saved successfully", id = newId });
        }

        /// <summary>
        /// Update employee certification (with file upload)
        /// </summary>
        [HttpPut("certification/{id}")]
        public async Task<IActionResult> UpdateEmployeeCertification(int id, [FromForm] EmployeeCertificationDto model)
        {
            if (id != model.CertificationId)
                return BadRequest("ID mismatch");

            string webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "Uploads", "Certifications");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (model.DocumentFile != null && model.DocumentFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{model.DocumentFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.DocumentFile.CopyToAsync(stream);
                }

                model.DocumentPath = Path.Combine("Uploads", "Certifications", fileName).Replace("\\", "/");
            }

            model.ModifiedBy ??= 0; // ✅ fix

            var result = await _employeeCertificationService.UpdateCertificationAsync(id, model);

            if (!result)
                return NotFound("Certification record not found");

            return Ok(new { message = "Updated successfully" });
        }

        /// <summary>
        /// Delete employee certification
        /// </summary>
        [HttpDelete("certification/{id}")]
        public async Task<IActionResult> DeleteEmployeeCertification(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid id" });

            var result = await _employeeCertificationService.DeleteCertificationAsync(id);

            if (!result)
                return NotFound(new { message = "Record not found or already deleted" });

            return Ok(new { message = "Deleted successfully" });
        }
        /// <summary>
        /// Get all certification types (e.g., Technical, Professional, Language)
        /// </summary>
        [HttpGet("certification/types")]
        public async Task<IActionResult> GetCertificationTypes()
        {
            var types = await _employeeCertificationService.GetCertificationTypesAsync();

            if (types == null || !types.Any())
                return NotFound(new { message = "No certification types found" });

            return Ok(types);
        }


        #endregion


        #region Employee Documents

        /// <summary>
        /// Get all documents for an employee
        /// </summary>
        [HttpGet("employee/{employeeId}/documents")]
        public async Task<IActionResult> GetAllDocumentsAsync(int employeeId)
        {
            var result = await _employeeDocumentService.GetDocumentsByEmployeeAsync(employeeId);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific document by ID
        /// </summary>
        [HttpGet("GetDocumentById/{id}")]
        public async Task<IActionResult> GetDocumentByIdAsync(int id)
        {
            var result = await _employeeDocumentService.GetDocumentByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Document not found" });

            return Ok(result);
        }
        [HttpPost("AddDocument")]
        public async Task<IActionResult> AddDocumentAsync([FromForm] EmployeeDocumentDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Invalid data" });

            // ✅ Safe path combine
            string webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "Uploads", "EmployeeDocuments");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string? relativePath = null;

            if (dto.DocumentFile != null && dto.DocumentFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.DocumentFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.DocumentFile.CopyToAsync(stream);
                }

                relativePath = Path.Combine("Uploads", "EmployeeDocuments", fileName).Replace("\\", "/");
            }

            dto.FilePath = relativePath;
            dto.CreatedBy ??= 0;

            var documentId = await _employeeDocumentService.AddDocumentAsync(dto);
            return Ok(new { message = "Document added successfully", documentId });
        }

        /// <summary>
        /// Update an existing document
        /// </summary>
        [HttpPut("UpdateDocument/{id}")]
        public async Task<IActionResult> UpdateDocumentAsync(int id, [FromForm] EmployeeDocumentDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Invalid data" });

            dto.Id = id;

            if (dto.DocumentFile != null && dto.DocumentFile.Length > 0)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "Uploads", "EmployeeDocuments");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string fileName = $"{Guid.NewGuid()}_{dto.DocumentFile.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.DocumentFile.CopyToAsync(stream);
                }

                dto.FilePath = Path.Combine("Uploads", "EmployeeDocuments", fileName).Replace("\\", "/");
            }

            // ✅ Apply numeric fallback
            dto.ModifiedBy ??= 0;

            var success = await _employeeDocumentService.UpdateDocumentAsync(dto);
            if (!success)
                return NotFound(new { message = "Document not found" });

            return Ok(new { message = "Document updated successfully" });
        }


        /// <summary>
        /// Delete a document by ID
        /// </summary>
        [HttpDelete("DeleteDocument/{id}")]
        public async Task<IActionResult> DeleteDocumentAsync(int id)
        {
            var success = await _employeeDocumentService.DeleteDocumentAsync(id);
            if (!success)
                return NotFound(new { message = "Document not found" });

            return Ok(new { message = "Document deleted successfully" });
        }
        [HttpGet("document/types")]
        public async Task<IActionResult> GetAllDocumentTypesAsync()
        {
            var result = await _employeeDocumentService.GetAllDocumentTypesAsync();
            return Ok(result);
        }

        #endregion
    }
}
