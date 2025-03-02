using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFIssueTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEventType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", maxLength: 100000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", maxLength: 100000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueComment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueComment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetricsType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    DimensionPropertyNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordReset",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValidationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordReset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTaskJob",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Error = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTaskJob", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTaskStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTaskStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTaskType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTaskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemValueType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemValueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotificationConfig",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipientEmails = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NotificationGroupId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificationConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailNotificationConfig_NotificationGroup_NotificationGroupId",
                        column: x => x.NotificationGroupId,
                        principalTable: "NotificationGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationGroupReference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NotificationGroupId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuditEventTypeId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationGroupReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationGroupReference_AuditEventType_AuditEventTypeId",
                        column: x => x.AuditEventTypeId,
                        principalTable: "AuditEventType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationGroupReference_NotificationGroup_NotificationGroupId",
                        column: x => x.NotificationGroupId,
                        principalTable: "NotificationGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectComponent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectComponent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemTaskParameter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SystemValueTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SystemTaskJobId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTaskParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemTaskParameter_SystemTaskJob_SystemTaskJobId",
                        column: x => x.SystemTaskJobId,
                        principalTable: "SystemTaskJob",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SystemTaskParameter_SystemValueType_SystemValueTypeId",
                        column: x => x.SystemValueTypeId,
                        principalTable: "SystemValueType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditEvent_AuditEventType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AuditEventType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditEvent_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectComponentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AssignedUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_IssueStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "IssueStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_IssueType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "IssueType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_ProjectComponent_ProjectComponentId",
                        column: x => x.ProjectComponentId,
                        principalTable: "ProjectComponent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_User_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditEventParameter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SystemValueTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AuditEventId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEventParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditEventParameter_AuditEvent_AuditEventId",
                        column: x => x.AuditEventId,
                        principalTable: "AuditEvent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditEventParameter_SystemValueType_SystemValueTypeId",
                        column: x => x.SystemValueTypeId,
                        principalTable: "SystemValueType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentReference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentReference_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentReference_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagReference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagReference_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagReference_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReference_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReference_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditEvent_CreatedUserId",
                table: "AuditEvent",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEvent_TypeId",
                table: "AuditEvent",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEventParameter_AuditEventId",
                table: "AuditEventParameter",
                column: "AuditEventId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEventParameter_SystemValueTypeId",
                table: "AuditEventParameter",
                column: "SystemValueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentReference_DocumentId",
                table: "DocumentReference",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentReference_IssueId",
                table: "DocumentReference",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificationConfig_NotificationGroupId",
                table: "EmailNotificationConfig",
                column: "NotificationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssignedUserId",
                table: "Issue",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CreatedUserId",
                table: "Issue",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectComponentId",
                table: "Issue",
                column: "ProjectComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectId",
                table: "Issue",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_StatusId",
                table: "Issue",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_TypeId",
                table: "Issue",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroupReference_AuditEventTypeId",
                table: "NotificationGroupReference",
                column: "AuditEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroupReference_NotificationGroupId",
                table: "NotificationGroupReference",
                column: "NotificationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComponent_ProjectId",
                table: "ProjectComponent",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTaskParameter_SystemTaskJobId",
                table: "SystemTaskParameter",
                column: "SystemTaskJobId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTaskParameter_SystemValueTypeId",
                table: "SystemTaskParameter",
                column: "SystemValueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TagReference_IssueId",
                table: "TagReference",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TagReference_TagId",
                table: "TagReference",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReference_IssueId",
                table: "UserReference",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReference_UserId",
                table: "UserReference",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEventParameter");

            migrationBuilder.DropTable(
                name: "ContentTemplate");

            migrationBuilder.DropTable(
                name: "DocumentReference");

            migrationBuilder.DropTable(
                name: "EmailNotificationConfig");

            migrationBuilder.DropTable(
                name: "IssueComment");

            migrationBuilder.DropTable(
                name: "MetricsType");

            migrationBuilder.DropTable(
                name: "NotificationGroupReference");

            migrationBuilder.DropTable(
                name: "PasswordReset");

            migrationBuilder.DropTable(
                name: "SystemTaskParameter");

            migrationBuilder.DropTable(
                name: "SystemTaskStatus");

            migrationBuilder.DropTable(
                name: "SystemTaskType");

            migrationBuilder.DropTable(
                name: "TagReference");

            migrationBuilder.DropTable(
                name: "UserReference");

            migrationBuilder.DropTable(
                name: "AuditEvent");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "NotificationGroup");

            migrationBuilder.DropTable(
                name: "SystemTaskJob");

            migrationBuilder.DropTable(
                name: "SystemValueType");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "AuditEventType");

            migrationBuilder.DropTable(
                name: "IssueStatus");

            migrationBuilder.DropTable(
                name: "IssueType");

            migrationBuilder.DropTable(
                name: "ProjectComponent");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
