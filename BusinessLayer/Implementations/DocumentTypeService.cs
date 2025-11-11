using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.GeneralRepository;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer.Implementations
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CreateDocumentTypeDto>> GetAllDocumentTypeAsync()

        {
            try
            {
                var documentTypes = await _unitOfWork.Repository<DocumentType>().GetAllAsync();
                Log.Information("Fetched all DocumentType successfully.");
                return documentTypes.Select(d => MapToDto(d));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all DocumentType .");
                throw; // Optionally rethrow to let API layer handle the exception
            }

        }

        public async Task<CreateDocumentTypeDto> GetDocumentTypeByIdAsync(int id)
        {
            try
            {

                var log = await _unitOfWork.Repository<DocumentType>().GetByIdAsync(id);
                if (log == null)
                {
                    Log.Warning("DocumentType with Id {Id} not found.", id);
                    return null;
                }
                Log.Information("Fetched DocumentType with Id {Id}.", id);
                return log == null ? null : MapToDto(log);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching DocumentType with Id {Id}.", id);
                throw;
            }


        }

        public async Task<IEnumerable<CreateDocumentTypeDto>> SearchDocumentTypeAsync(string? documentTypeName,
        bool? isActive)
        {

            try
            {
                Log.Information("Starting DocumentType search with filters: {@Filters}", new
                {
                    DocumentTypeName = documentTypeName,
                    IsActive = isActive
                });

                var allDocumentTypes = await _unitOfWork.Repository<DocumentType>().GetAllAsync();
                var query = allDocumentTypes.AsQueryable().AsNoTracking();



                if (!string.IsNullOrWhiteSpace(documentTypeName))
                {
                    query = query.Where(c => c.DocumentTypeName != null && c.DocumentTypeName.Contains(documentTypeName) && (c.IsActive == true || c.IsActive == false));
                }




                var results = query.ToList();
                var mappedResults = results.Select(c => MapToDto(c));
                Log.Information("DocumentType search returned {Count} results", mappedResults.Count());
                return mappedResults;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during DocumentType search with filters: {@Filters}", new
                {

                });

                return Enumerable.Empty<CreateDocumentTypeDto>();
            }
        }

        public async Task<ResponseDto<CreateDocumentTypeDto>> CreateDocumentTypeAsync(CreateDocumentTypeDto dto, int createdBy)
        {
            try
            {
                // Check for existing record with same key fields
                var existing = _unitOfWork.Repository<DocumentType>().Query().FirstOrDefault(x =>
                        x.DocumentTypeName == dto.DocumentTypeName);

                if (existing != null)
                {
                    Log.Warning("Duplicate DocumentTypes detected for DocumentTypeName: {DocumentTypeName}.", dto.DocumentTypeName);
                    //throw new InvalidOperationException("Duplicate DocumentTypeName entry already exists.");
                    return new ResponseDto<CreateDocumentTypeDto>
                    {
                        Status = "duplicate",
                        Message = $"An  DocumentType named '{dto.DocumentTypeName}' already exists.",
                        Data = MapToDto(existing)
                    };
                }
                var entity = new DocumentType
                {
                    DocumentTypeName = dto.DocumentTypeName,
                    IsActive = dto.Audit.IsActive ?? true,
                    CreatedBy = createdBy,
                    CreatedDate = DateTime.Now


                };

                var created = await _unitOfWork.Repository<DocumentType>().AddAsync(entity);
                //var created = await _unitOfWork.Repository<DocumentType>().AddAsync(entity);
                Log.Information("Created new DocumentType with Id {Id}.", created.DocumentTypeId);
                await _unitOfWork.CompleteAsync();

                return new ResponseDto<CreateDocumentTypeDto>
                {
                    Status = "created",
                    Message = $"DocumentType '{dto.DocumentTypeName}' created successfully.",
                    Data = MapToDto(created)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating new DocumentType.");
                throw;
            }




        }

        public async Task<UpdateDocumentTypeDto?> UpdateDocumentTypeAsync(int id, UpdateDocumentTypeDto dto,int modifiedBy)

        {
            try
            {
                // Fetch existing entity to ensure it exists
                var existing = await _unitOfWork.Repository<DocumentType>().GetByIdAsync(id); ;
                if (existing == null)
                    throw new KeyNotFoundException($"DocumentType with ID {dto.DocumentTypeId} not found.");


                existing.DocumentTypeName = dto.DocumentTypeName;
                existing.ModifiedBy = modifiedBy;
                existing.ModifiedAt = DateTime.UtcNow;

                // Save changes
                var updated = await _unitOfWork.Repository<DocumentType>().UpdateAsync(existing); 

                Log.Information("Updated DocumentType with ID {Id}", updated.DocumentTypeId);
                await _unitOfWork.CompleteAsync();
                return MapToUpdateDto(updated);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating DocumentType with ID {Id}", dto.DocumentTypeId);
                throw;
            }
            

        }

        public async Task<bool> DeleteDocumentTypeAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<DocumentType>().GetByIdAsync(id);
                var deleted = await _unitOfWork.Repository<DocumentType>().SoftDeleteAsync(id);
                if (deleted)
                {

                    Log.Information("DocumentType with Id {Id} Inactive successfully", id);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }

                else
                {
                    Log.Warning("DocumentType with Id {Id} not found", id);

                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting DocumentType with Id {Id}", id);
                return false; // Optionally re-throw if you want exception to bubble up
            }
            
            
          
        }



        public async Task<(int insertedCount, int updatedCount)> BulkUploadAsync(IFormFile file, int createdBy)
        {
            if (file == null || file.Length == 0)
            {
                Log.Warning("Bulk upload attempted with no file.");
                return (0, 0);
            }

            string filePath = string.Empty;

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    Log.Information("Uploaded file saved temporarily at {FilePath}", filePath);
                }

                int insertCount = 0;
                int updateCount = 0;
                int skippedDuplicates = 0;

                var newTypes = new List<DocumentType>();
                var existingTypes = (await _unitOfWork.Repository<DocumentType>().GetAllAsync()).ToList();
                var existingNames = existingTypes
                    .Select(t => t.DocumentTypeName.ToLower().Trim())
                    .ToHashSet();

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (extension == ".xls" || extension == ".xlsx")
                {
                    using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                    using var reader = ExcelReaderFactory.CreateReader(stream);

                    bool isHeaderSkipped = false;
                    do
                    {
                        while (reader.Read())
                        {
                            if (!isHeaderSkipped)
                            {
                                isHeaderSkipped = true;
                                continue;
                            }

                            var name = reader.GetValue(1)?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(name))
                            {
                                Log.Warning("Skipped empty row in Excel sheet.");
                                continue;
                            }

                            var existingType = existingTypes
                                .FirstOrDefault(t => t.DocumentTypeName.ToLower() == name.ToLower());

                            if (existingType != null)
                            {
                                existingType.ModifiedBy = createdBy;
                                existingType.ModifiedAt = DateTime.UtcNow;
                                existingType.IsActive = true;

                                updateCount++;
                                Log.Information("Updated existing Document type '{Name}'.", name);
                            }
                            else
                            {
                                if (newTypes.Any(t => t.DocumentTypeName.ToLower() == name.ToLower()))
                                {
                                    skippedDuplicates++;
                                    Log.Warning("Duplicate Document type '{Name}' skipped in upload file.", name);
                                    continue;
                                }

                                var newType = new DocumentType
                                {
                                    DocumentTypeName = name,
                                    IsActive = true,
                                    CreatedBy = createdBy,
                                    CreatedDate = DateTime.UtcNow
                                };

                                newTypes.Add(newType);
                                insertCount++;
                                Log.Information("Added new Document type '{Name}' to insert list.", name);
                            }
                        }
                    } while (reader.NextResult());

                    if (newTypes.Any())
                    {

                        await _unitOfWork.Repository<DocumentType>().BulkCreateAsync(newTypes);
                        Log.Information("Inserted {Count} new Document types into database.", newTypes.Count);
                    }

                    Log.Information("Bulk upload completed: {Inserted} inserted, {Updated} updated, {Skipped} duplicates skipped.",
                        insertCount, updateCount, skippedDuplicates);
                }
                else
                {
                    throw new NotSupportedException("Unsupported file format. Please upload XLS or XLSX.");
                }
                await _unitOfWork.CompleteAsync();
                return (insertCount, updateCount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing the Excel file.");
                throw;
            }

            finally
            {
                //  Cleanup: delete the file
                try
                {
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        Log.Information("Temporary file deleted: {FilePath}", filePath);
                    }
                }
                catch (Exception deleteEx)
                {
                    Log.Warning(deleteEx, "Failed to delete temporary file: {FilePath}", filePath);
                }
            }

        }
  
       


        // Simple mapper



        private CreateDocumentTypeDto MapToDto(DocumentType d)
        {
            return new CreateDocumentTypeDto
            {
                DocumentTypeId = d.DocumentTypeId,
                DocumentTypeName = d.DocumentTypeName,
                Audit = new CreationInfoDto
                {
                    IsActive = d.IsActive,
                    CreatedBy = (int)d.CreatedBy,
                    CreatedDate = d.CreatedDate
                }

            };
        }

        private UpdateDocumentTypeDto MapToUpdateDto(DocumentType d)
        {
            return new UpdateDocumentTypeDto
            {
                DocumentTypeId = d.DocumentTypeId,
                DocumentTypeName = d.DocumentTypeName,
                Audit = new ModificationInfoDto
                {
                    
                    ModifiedBy = (int)d.ModifiedBy,
                    ModifiedAt = d.ModifiedAt
                }
            };
        }

    }
    }
