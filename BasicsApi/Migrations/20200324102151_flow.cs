using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicsApi.Migrations
{
    public partial class flow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "area",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pid = table.Column<int>(nullable: true),
                    district = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AREA", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_AREA_AREA_AREA_AREA",
                        column: x => x.pid,
                        principalTable: "area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    code = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    pid = table.Column<int>(nullable: true),
                    legal_person = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Idcard = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    phone = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    address = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    area = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    briefing = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    isdel = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_COMPANY_COM_COM_COMPANY",
                        column: x => x.pid,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "element_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    code = table.Column<string>(unicode: false, maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEMENT_TYPE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "元素类型表");

            migrationBuilder.CreateTable(
                name: "emp_group",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_GROUP", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "用户组");

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    file_path = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILES", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "文件表");

            migrationBuilder.CreateTable(
                name: "FlowClipCfg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    Show = table.Column<bool>(nullable: false),
                    R = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Rx = table.Column<double>(nullable: false),
                    Ry = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowClipCfg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowCss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertCss = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowCss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowEdgeLoopCfg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    Dist = table.Column<int>(nullable: false),
                    Clockwise = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowEdgeLoopCfg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowGroupTitle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Stroke = table.Column<string>(nullable: true),
                    Fill = table.Column<string>(nullable: true),
                    fontSize = table.Column<string>(nullable: true),
                    OffsetX = table.Column<int>(nullable: false),
                    OffsetY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowGroupTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowIcon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Show = table.Column<bool>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Img = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowIcon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowLayout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    PreventOverlap = table.Column<bool>(nullable: false),
                    LinkDistance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowLayout", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowLinkPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Top = table.Column<bool>(nullable: false),
                    Bottom = table.Column<bool>(nullable: false),
                    Left = table.Column<bool>(nullable: false),
                    Right = table.Column<bool>(nullable: false),
                    Size = table.Column<double>(nullable: false),
                    Fill = table.Column<string>(nullable: true),
                    Stroke = table.Column<string>(nullable: true),
                    LineWidth = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowLinkPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowStyle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fill = table.Column<string>(nullable: true),
                    Stroke = table.Column<string>(nullable: true),
                    LineWidth = table.Column<int>(nullable: false),
                    ShadowColor = table.Column<string>(nullable: true),
                    ShadowBlur = table.Column<string>(nullable: true),
                    ShadowOffsetX = table.Column<int>(nullable: false),
                    ShadowOffsetY = table.Column<int>(nullable: false),
                    Opacity = table.Column<double>(nullable: false),
                    Radius = table.Column<double>(nullable: false),
                    Offset = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "operation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    code = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    pid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPERATION", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "功能操作表");

            migrationBuilder.CreateTable(
                name: "page",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    url = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "页面元素表");

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    code = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    describes = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSITION", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "power",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POWER", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "company_log",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cid = table.Column<int>(nullable: false),
                    update_time = table.Column<DateTime>(type: "date", nullable: false),
                    content = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_LOG", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_COMPANY__COM_COMLO_COMPANY",
                        column: x => x.cid,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "公司变动表");

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dep_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    dep_code = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    company_id = table.Column<int>(nullable: false),
                    pid = table.Column<int>(nullable: true),
                    isdel = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_DEPARTME_DEP_COMPA_COMPANY",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DEPARTME_DEP_DEP_DEPARTME",
                        column: x => x.pid,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shareholder",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cid = table.Column<int>(nullable: true),
                    name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    proportion = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    pay_money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Idcard = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHAREHOLDER", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_SHAREHOL_COMPANY_C_COMPANY",
                        column: x => x.cid,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "股东表");

            migrationBuilder.CreateTable(
                name: "FlowGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    FlowDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowGroup_FlowData_FlowDataId",
                        column: x => x.FlowDataId,
                        principalTable: "FlowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowGroup_FlowGroupTitle_TitleId",
                        column: x => x.TitleId,
                        principalTable: "FlowGroupTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowLabelCfgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shape = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    StyleId = table.Column<int>(nullable: true),
                    AutoRotate = table.Column<bool>(nullable: false),
                    LabelCfgId = table.Column<int>(nullable: true),
                    LinkPointsId = table.Column<int>(nullable: true),
                    RefX = table.Column<double>(nullable: false),
                    RefY = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowLabelCfgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowLabelCfgs_FlowLabelCfgs_LabelCfgId",
                        column: x => x.LabelCfgId,
                        principalTable: "FlowLabelCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowLabelCfgs_FlowLinkPoints_LinkPointsId",
                        column: x => x.LinkPointsId,
                        principalTable: "FlowLinkPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowLabelCfgs_FlowStyle_StyleId",
                        column: x => x.StyleId,
                        principalTable: "FlowStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "element",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    url = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    type = table.Column<int>(nullable: false),
                    operation_id = table.Column<int>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEMENT", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ELEMENT_OPERATION_OPERATIO",
                        column: x => x.operation_id,
                        principalTable: "operation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ELEMENT_ELEMENT_E_ELEMENT_",
                        column: x => x.type,
                        principalTable: "element_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "页面元素表");

            migrationBuilder.CreateTable(
                name: "page_page",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ppage_id = table.Column<int>(nullable: false),
                    cpage_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGE_PAGE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PAGE_PAG_CPAGE_PAG_PAGE",
                        column: x => x.cpage_id,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PAGE_PAG_PPAGE_PAG_PAGE",
                        column: x => x.ppage_id,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "页面嵌套表");

            migrationBuilder.CreateTable(
                name: "role_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_FILE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_FIL_FILE_FILE_FILES",
                        column: x => x.file_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_FIL_ROLE_FILE_POWER",
                        column: x => x.id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "文件权限对照表");

            migrationBuilder.CreateTable(
                name: "role_opretion",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ope_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_OPRETION", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_OPR_OPRETION__OPERATIO",
                        column: x => x.ope_id,
                        principalTable: "operation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_OPR_ROLE_OPRE_POWER",
                        column: x => x.power_id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "功能权限对照表");

            migrationBuilder.CreateTable(
                name: "role_page",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    page_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_PAGE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_PAG_PAGE_PAGE_PAGE",
                        column: x => x.page_id,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_PAG_ROLE_PAGE_POWER",
                        column: x => x.power_id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "页面权限对照表");

            migrationBuilder.CreateTable(
                name: "role_power",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_POWER", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_POW_POWER_ROL_POWER",
                        column: x => x.power_id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_POW_ROLE_ROLE_ROLE",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "角色权限表");

            migrationBuilder.CreateTable(
                name: "usergroup_role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERGROUP_ROLE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USERGROU_USERGROUP_EMP_GROU",
                        column: x => x.group_id,
                        principalTable: "emp_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USERGROU_ROLE_GROU_ROLE",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    login_name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    login_pwd = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    idcard = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    phone = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    dep_id = table.Column<int>(nullable: false),
                    qq = table.Column<int>(nullable: true),
                    wechar = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    sex = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    img = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    isuse = table.Column<bool>(nullable: true),
                    stardate = table.Column<DateTime>(type: "date", nullable: false),
                    enddate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_USER_DEP_DEPARTME",
                        column: x => x.dep_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "员工表");

            migrationBuilder.CreateTable(
                name: "FlowEdge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shape = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    LabelCfgId = table.Column<int>(nullable: true),
                    StyleId = table.Column<int>(nullable: true),
                    LoopCfgId = table.Column<int>(nullable: true),
                    FlowDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowEdge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowEdge_FlowData_FlowDataId",
                        column: x => x.FlowDataId,
                        principalTable: "FlowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowEdge_FlowLabelCfgs_LabelCfgId",
                        column: x => x.LabelCfgId,
                        principalTable: "FlowLabelCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowEdge_FlowEdgeLoopCfg_LoopCfgId",
                        column: x => x.LoopCfgId,
                        principalTable: "FlowEdgeLoopCfg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowEdge_FlowStyle_StyleId",
                        column: x => x.StyleId,
                        principalTable: "FlowStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowGraph",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Container = table.Column<string>(nullable: true),
                    Width = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    FitView = table.Column<bool>(nullable: false),
                    GroupByTypes = table.Column<bool>(nullable: false),
                    GroupTypes = table.Column<string>(nullable: true),
                    AutoPaint = table.Column<bool>(nullable: false),
                    MinZoom = table.Column<double>(nullable: false),
                    MaxZoom = table.Column<double>(nullable: false),
                    DefaultNodeId = table.Column<int>(nullable: true),
                    DefaultEdgeId = table.Column<int>(nullable: true),
                    LayoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowGraph", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowGraph_FlowLabelCfgs_DefaultEdgeId",
                        column: x => x.DefaultEdgeId,
                        principalTable: "FlowLabelCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowGraph_FlowLabelCfgs_DefaultNodeId",
                        column: x => x.DefaultNodeId,
                        principalTable: "FlowLabelCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowGraph_FlowLayout_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "FlowLayout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowNode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<string>(nullable: true),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Shape = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LabelCfgId = table.Column<int>(nullable: true),
                    StyleId = table.Column<int>(nullable: true),
                    LinkPointsId = table.Column<int>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    ClipCfgId = table.Column<int>(nullable: true),
                    IconId = table.Column<int>(nullable: true),
                    Direction = table.Column<string>(nullable: true),
                    InnerR = table.Column<int>(nullable: false),
                    FlowDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowClipCfg_ClipCfgId",
                        column: x => x.ClipCfgId,
                        principalTable: "FlowClipCfg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowData_FlowDataId",
                        column: x => x.FlowDataId,
                        principalTable: "FlowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowIcon_IconId",
                        column: x => x.IconId,
                        principalTable: "FlowIcon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowLabelCfgs_LabelCfgId",
                        column: x => x.LabelCfgId,
                        principalTable: "FlowLabelCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowLinkPoints_LinkPointsId",
                        column: x => x.LinkPointsId,
                        principalTable: "FlowLinkPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowNode_FlowStyle_StyleId",
                        column: x => x.StyleId,
                        principalTable: "FlowStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "element_element",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pelement_id = table.Column<int>(nullable: false),
                    celement_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEMENT_ELEMENT", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ELEMENT__CELEMENT__ELEMENT",
                        column: x => x.celement_id,
                        principalTable: "element",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ELEMENT__PELEMENT__ELEMENT",
                        column: x => x.pelement_id,
                        principalTable: "element",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "元素嵌套表");

            migrationBuilder.CreateTable(
                name: "page_element",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    pag_id = table.Column<int>(nullable: true),
                    page_id = table.Column<int>(nullable: false),
                    element_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGE_ELEMENT", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PAGE_ELE_REFERENCE_ELEMENT",
                        column: x => x.id,
                        principalTable: "element",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PAGE_ELE_REFERENCE_PAGE",
                        column: x => x.pag_id,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "页面元素对照表");

            migrationBuilder.CreateTable(
                name: "role_element",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    element_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_ELEMENT", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_ELE_ELEMENT_E_ELEMENT",
                        column: x => x.element_id,
                        principalTable: "element",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_ELE_ROLE_ELEM_POWER",
                        column: x => x.power_id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "页面元素权限对照表");

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pid = table.Column<int>(nullable: true),
                    text = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    i18n = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    icon = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    pageId = table.Column<int>(nullable: true),
                    pagepageid = table.Column<int>(nullable: true),
                    externalLink = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    target = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    badge = table.Column<int>(nullable: true),
                    badgeDot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    badgeStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    disabled = table.Column<bool>(nullable: true),
                    hide = table.Column<bool>(nullable: true),
                    hideInBreadcrumb = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    acl = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    shortcut = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    shortcutRoot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    link = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    reuse = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_MENU_PAGE_MENU_PAGE",
                        column: x => x.pageId,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MENU_PAGES_MEN_PAGE_PAG",
                        column: x => x.pagepageid,
                        principalTable: "page_page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MENU_MENU_MENU_MENU",
                        column: x => x.pid,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "菜单表");

            migrationBuilder.CreateTable(
                name: "emp_role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_ROLE", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EMP_ROLE_EMP_EMPRO_EMPLOYEE",
                        column: x => x.emp_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EMP_ROLE_ROLE_EMPR_ROLE",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employe_position",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(nullable: false),
                    pos_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYE_POSITION", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EMPLOYE__EMP_EMPPO_EMPLOYEE",
                        column: x => x.emp_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EMPLOYE__POS_EMPPO_POSITION",
                        column: x => x.pos_id,
                        principalTable: "position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_usergroup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(nullable: true),
                    group_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_USERGROUP", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USER_USE_USER_USER_EMPLOYEE",
                        column: x => x.emp_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_USE_USERGROUP_EMP_GROU",
                        column: x => x.group_id,
                        principalTable: "emp_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户组用户对照表");

            migrationBuilder.CreateTable(
                name: "FlowG6",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowGraphId = table.Column<int>(nullable: true),
                    FlowDataId = table.Column<int>(nullable: true),
                    FlowCssId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowG6", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowG6_FlowCss_FlowCssId",
                        column: x => x.FlowCssId,
                        principalTable: "FlowCss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowG6_FlowData_FlowDataId",
                        column: x => x.FlowDataId,
                        principalTable: "FlowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowG6_FlowGraph_FlowGraphId",
                        column: x => x.FlowGraphId,
                        principalTable: "FlowGraph",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "page_elements",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    page_id = table.Column<int>(nullable: false),
                    elements_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGE_ELEMENTS", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PAGE_ELE_PAGEELEME_ELEMENT_",
                        column: x => x.elements_id,
                        principalTable: "element_element",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PAGE_ELE_PAGEELEME_PAGE",
                        column: x => x.page_id,
                        principalTable: "page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "元素嵌套与页面对照表");

            migrationBuilder.CreateTable(
                name: "role_menu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menu_id = table.Column<int>(nullable: false),
                    power_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_MENU", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ROLE_MEN_MENU_ROLE_MENU",
                        column: x => x.menu_id,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_MEN_ROLE_MENU_POWER",
                        column: x => x.power_id,
                        principalTable: "power",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "菜单权限对照表");

            migrationBuilder.CreateTable(
                name: "FlowFun",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunName = table.Column<string>(nullable: true),
                    FunParameter = table.Column<string>(nullable: true),
                    FunBody = table.Column<string>(nullable: true),
                    FlowG6Id = table.Column<int>(nullable: true),
                    FlowG6Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowFun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowFun_FlowG6_FlowG6Id",
                        column: x => x.FlowG6Id,
                        principalTable: "FlowG6",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowFun_FlowG6_FlowG6Id1",
                        column: x => x.FlowG6Id1,
                        principalTable: "FlowG6",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowRegisterBehavior",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    FlowG6Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowRegisterBehavior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowRegisterBehavior_FlowG6_FlowG6Id",
                        column: x => x.FlowG6Id,
                        principalTable: "FlowG6",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowShapeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowShapeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowShapeOptions_FlowFun_DrawId",
                        column: x => x.DrawId,
                        principalTable: "FlowFun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowRegisterEdge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShapeType = table.Column<string>(nullable: true),
                    ShapeOptionsId = table.Column<int>(nullable: true),
                    FlowG6Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowRegisterEdge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowRegisterEdge_FlowG6_FlowG6Id",
                        column: x => x.FlowG6Id,
                        principalTable: "FlowG6",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowRegisterEdge_FlowShapeOptions_ShapeOptionsId",
                        column: x => x.ShapeOptionsId,
                        principalTable: "FlowShapeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_area_pid",
                table: "area",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_company_pid",
                table: "company",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_company_log_cid",
                table: "company_log",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "IX_department_company_id",
                table: "department",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_pid",
                table: "department",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_element_operation_id",
                table: "element",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_element_type",
                table: "element",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_element_element_celement_id",
                table: "element_element",
                column: "celement_id");

            migrationBuilder.CreateIndex(
                name: "IX_element_element_pelement_id",
                table: "element_element",
                column: "pelement_id");

            migrationBuilder.CreateIndex(
                name: "IX_emp_role_emp_id",
                table: "emp_role",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_emp_role_role_id",
                table: "emp_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_employe_position_emp_id",
                table: "employe_position",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_employe_position_pos_id",
                table: "employe_position",
                column: "pos_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_dep_id",
                table: "employee",
                column: "dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowEdge_FlowDataId",
                table: "FlowEdge",
                column: "FlowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowEdge_LabelCfgId",
                table: "FlowEdge",
                column: "LabelCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowEdge_LoopCfgId",
                table: "FlowEdge",
                column: "LoopCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowEdge_StyleId",
                table: "FlowEdge",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowFun_FlowG6Id",
                table: "FlowFun",
                column: "FlowG6Id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowFun_FlowG6Id1",
                table: "FlowFun",
                column: "FlowG6Id1");

            migrationBuilder.CreateIndex(
                name: "IX_FlowG6_FlowCssId",
                table: "FlowG6",
                column: "FlowCssId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowG6_FlowDataId",
                table: "FlowG6",
                column: "FlowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowG6_FlowGraphId",
                table: "FlowG6",
                column: "FlowGraphId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGraph_DefaultEdgeId",
                table: "FlowGraph",
                column: "DefaultEdgeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGraph_DefaultNodeId",
                table: "FlowGraph",
                column: "DefaultNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGraph_LayoutId",
                table: "FlowGraph",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroup_FlowDataId",
                table: "FlowGroup",
                column: "FlowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroup_TitleId",
                table: "FlowGroup",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowLabelCfgs_LabelCfgId",
                table: "FlowLabelCfgs",
                column: "LabelCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowLabelCfgs_LinkPointsId",
                table: "FlowLabelCfgs",
                column: "LinkPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowLabelCfgs_StyleId",
                table: "FlowLabelCfgs",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_ClipCfgId",
                table: "FlowNode",
                column: "ClipCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_FlowDataId",
                table: "FlowNode",
                column: "FlowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_IconId",
                table: "FlowNode",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_LabelCfgId",
                table: "FlowNode",
                column: "LabelCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_LinkPointsId",
                table: "FlowNode",
                column: "LinkPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowNode_StyleId",
                table: "FlowNode",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRegisterBehavior_FlowG6Id",
                table: "FlowRegisterBehavior",
                column: "FlowG6Id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRegisterEdge_FlowG6Id",
                table: "FlowRegisterEdge",
                column: "FlowG6Id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRegisterEdge_ShapeOptionsId",
                table: "FlowRegisterEdge",
                column: "ShapeOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowShapeOptions_DrawId",
                table: "FlowShapeOptions",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_menu_pageId",
                table: "menu",
                column: "pageId");

            migrationBuilder.CreateIndex(
                name: "IX_menu_pagepageid",
                table: "menu",
                column: "pagepageid");

            migrationBuilder.CreateIndex(
                name: "IX_menu_pid",
                table: "menu",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_page_element_pag_id",
                table: "page_element",
                column: "pag_id");

            migrationBuilder.CreateIndex(
                name: "IX_page_elements_elements_id",
                table: "page_elements",
                column: "elements_id");

            migrationBuilder.CreateIndex(
                name: "IX_page_elements_page_id",
                table: "page_elements",
                column: "page_id");

            migrationBuilder.CreateIndex(
                name: "IX_page_page_cpage_id",
                table: "page_page",
                column: "cpage_id");

            migrationBuilder.CreateIndex(
                name: "IX_page_page_ppage_id",
                table: "page_page",
                column: "ppage_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_element_element_id",
                table: "role_element",
                column: "element_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_element_power_id",
                table: "role_element",
                column: "power_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_file_file_id",
                table: "role_file",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_menu_menu_id",
                table: "role_menu",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_menu_power_id",
                table: "role_menu",
                column: "power_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_opretion_ope_id",
                table: "role_opretion",
                column: "ope_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_opretion_power_id",
                table: "role_opretion",
                column: "power_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_page_page_id",
                table: "role_page",
                column: "page_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_page_power_id",
                table: "role_page",
                column: "power_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_power_power_id",
                table: "role_power",
                column: "power_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_power_role_id",
                table: "role_power",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_shareholder_cid",
                table: "shareholder",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "IX_user_usergroup_emp_id",
                table: "user_usergroup",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_usergroup_group_id",
                table: "user_usergroup",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_usergroup_role_group_id",
                table: "usergroup_role",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_usergroup_role_role_id",
                table: "usergroup_role",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "area");

            migrationBuilder.DropTable(
                name: "company_log");

            migrationBuilder.DropTable(
                name: "emp_role");

            migrationBuilder.DropTable(
                name: "employe_position");

            migrationBuilder.DropTable(
                name: "FlowEdge");

            migrationBuilder.DropTable(
                name: "FlowGroup");

            migrationBuilder.DropTable(
                name: "FlowNode");

            migrationBuilder.DropTable(
                name: "FlowRegisterBehavior");

            migrationBuilder.DropTable(
                name: "FlowRegisterEdge");

            migrationBuilder.DropTable(
                name: "page_element");

            migrationBuilder.DropTable(
                name: "page_elements");

            migrationBuilder.DropTable(
                name: "role_element");

            migrationBuilder.DropTable(
                name: "role_file");

            migrationBuilder.DropTable(
                name: "role_menu");

            migrationBuilder.DropTable(
                name: "role_opretion");

            migrationBuilder.DropTable(
                name: "role_page");

            migrationBuilder.DropTable(
                name: "role_power");

            migrationBuilder.DropTable(
                name: "shareholder");

            migrationBuilder.DropTable(
                name: "user_usergroup");

            migrationBuilder.DropTable(
                name: "usergroup_role");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "FlowEdgeLoopCfg");

            migrationBuilder.DropTable(
                name: "FlowGroupTitle");

            migrationBuilder.DropTable(
                name: "FlowClipCfg");

            migrationBuilder.DropTable(
                name: "FlowIcon");

            migrationBuilder.DropTable(
                name: "FlowShapeOptions");

            migrationBuilder.DropTable(
                name: "element_element");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "power");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "emp_group");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "FlowFun");

            migrationBuilder.DropTable(
                name: "element");

            migrationBuilder.DropTable(
                name: "page_page");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "FlowG6");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "element_type");

            migrationBuilder.DropTable(
                name: "page");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "FlowCss");

            migrationBuilder.DropTable(
                name: "FlowData");

            migrationBuilder.DropTable(
                name: "FlowGraph");

            migrationBuilder.DropTable(
                name: "FlowLabelCfgs");

            migrationBuilder.DropTable(
                name: "FlowLayout");

            migrationBuilder.DropTable(
                name: "FlowLinkPoints");

            migrationBuilder.DropTable(
                name: "FlowStyle");
        }
    }
}
