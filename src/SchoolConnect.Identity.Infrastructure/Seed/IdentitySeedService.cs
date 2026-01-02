using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Enums;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Infrastructure.Seed;

public class IdentitySeedService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _roleRepository;

    public IdentitySeedService(
        IPermissionRepository permissionRepository,
        IRoleRepository roleRepository)
    {
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
    }

    public async Task SeedAsync()
    {
        await SeedPermissionsAsync();
        await SeedRolesAsync();
    }

    private async Task SeedPermissionsAsync()
    {
        var existingPermissions = await _permissionRepository.GetAllAsync();
        if (existingPermissions.Any()) return;

        var permissions = new List<Permission>
        {
            // Users
            new("users.view", "View Users", "View user information", PermissionCategory.Users),
            new("users.create", "Create Users", "Create new users", PermissionCategory.Users),
            new("users.update", "Update Users", "Update user information", PermissionCategory.Users),
            new("users.delete", "Delete Users", "Delete users", PermissionCategory.Users),
            
            // Roles
            new("roles.view", "View Roles", "View role information", PermissionCategory.Roles),
            new("roles.create", "Create Roles", "Create new roles", PermissionCategory.Roles),
            new("roles.update", "Update Roles", "Update role information", PermissionCategory.Roles),
            new("roles.delete", "Delete Roles", "Delete roles", PermissionCategory.Roles),
            
            // Institutes
            new("institutes.view", "View Institutes", "View institute information", PermissionCategory.Institutes),
            new("institutes.create", "Create Institutes", "Create new institutes", PermissionCategory.Institutes),
            new("institutes.update", "Update Institutes", "Update institute information", PermissionCategory.Institutes),
            new("institutes.delete", "Delete Institutes", "Delete institutes", PermissionCategory.Institutes),
            
            // Centres
            new("centres.view", "View Centres", "View centre information", PermissionCategory.Centres),
            new("centres.create", "Create Centres", "Create new centres", PermissionCategory.Centres),
            new("centres.update", "Update Centres", "Update centre information", PermissionCategory.Centres),
            new("centres.delete", "Delete Centres", "Delete centres", PermissionCategory.Centres),
            
            // Students
            new("students.view", "View Students", "View student information", PermissionCategory.Students),
            new("students.create", "Create Students", "Create new students", PermissionCategory.Students),
            new("students.update", "Update Students", "Update student information", PermissionCategory.Students),
            new("students.delete", "Delete Students", "Delete students", PermissionCategory.Students),
            
            // Classes
            new("classes.view", "View Classes", "View class information", PermissionCategory.Classes),
            new("classes.create", "Create Classes", "Create new classes", PermissionCategory.Classes),
            new("classes.update", "Update Classes", "Update class information", PermissionCategory.Classes),
            new("classes.delete", "Delete Classes", "Delete classes", PermissionCategory.Classes),
            
            // Lessons
            new("lessons.view", "View Lessons", "View lesson information", PermissionCategory.Lessons),
            new("lessons.create", "Create Lessons", "Create new lessons", PermissionCategory.Lessons),
            new("lessons.update", "Update Lessons", "Update lesson information", PermissionCategory.Lessons),
            new("lessons.delete", "Delete Lessons", "Delete lessons", PermissionCategory.Lessons),
            
            // Assessments
            new("assessments.view", "View Assessments", "View assessment information", PermissionCategory.Assessments),
            new("assessments.create", "Create Assessments", "Create new assessments", PermissionCategory.Assessments),
            new("assessments.grade", "Grade Assessments", "Grade student assessments", PermissionCategory.Assessments),
            
            // Reports
            new("reports.view", "View Reports", "View report information", PermissionCategory.Reports),
            new("reports.generate", "Generate Reports", "Generate new reports", PermissionCategory.Reports)
        };

        foreach (var permission in permissions)
        {
            await _permissionRepository.AddAsync(permission);
        }
    }

    private async Task SeedRolesAsync()
    {
        var existingRoles = await _roleRepository.GetAllAsync();
        if (existingRoles.Any()) return;

        var allPermissions = await _permissionRepository.GetAllAsync();
        
        // SuperAdmin - All permissions
        var superAdmin = new Role("SuperAdmin", "Has full system access", true);
        foreach (var perm in allPermissions)
        {
            superAdmin.AddPermission(perm.Id);
        }
        await _roleRepository.AddAsync(superAdmin);

        // SchoolAdmin - Institute and Centre management
        var schoolAdmin = new Role("SchoolAdmin", "School administrator with institute management", true);
        var schoolAdminPerms = allPermissions.Where(p => 
            p.Category == PermissionCategory.Institutes ||
            p.Category == PermissionCategory.Centres ||
            p.Category == PermissionCategory.Students ||
            p.Category == PermissionCategory.Users ||
            p.Category == PermissionCategory.Classes);
        foreach (var perm in schoolAdminPerms)
        {
            schoolAdmin.AddPermission(perm.Id);
        }
        await _roleRepository.AddAsync(schoolAdmin);

        // Teacher - Class and lesson management
        var teacher = new Role("Teacher", "Teacher with class and assessment management", true);
        var teacherPerms = allPermissions.Where(p => 
            p.Category == PermissionCategory.Classes ||
            p.Category == PermissionCategory.Lessons ||
            p.Category == PermissionCategory.Assessments ||
            p.Code.Contains(".view"));
        foreach (var perm in teacherPerms)
        {
            teacher.AddPermission(perm.Id);
        }
        await _roleRepository.AddAsync(teacher);

        // Parent - View only for student data
        var parent = new Role("Parent", "Parent with view access to student information", true);
        var parentPerms = allPermissions.Where(p => p.Code.Contains(".view"));
        foreach (var perm in parentPerms)
        {
            parent.AddPermission(perm.Id);
        }
        await _roleRepository.AddAsync(parent);

        // Student - Limited view access
        var student = new Role("Student", "Student with limited access", true);
        var studentPerms = allPermissions.Where(p => 
            p.Code == "lessons.view" || 
            p.Code == "assessments.view");
        foreach (var perm in studentPerms)
        {
            student.AddPermission(perm.Id);
        }
        await _roleRepository.AddAsync(student);
    }
}
