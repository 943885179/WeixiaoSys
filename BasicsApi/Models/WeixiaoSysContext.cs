using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BasicsApi.Models
{
    public partial class WeixiaoSysContext : DbContext
    {
        public WeixiaoSysContext()
        {
        }

        public WeixiaoSysContext(DbContextOptions<WeixiaoSysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyLog> CompanyLog { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Element> Element { get; set; }
        public virtual DbSet<ElementElement> ElementElement { get; set; }
        public virtual DbSet<ElementType> ElementType { get; set; }
        public virtual DbSet<EmpGroup> EmpGroup { get; set; }
        public virtual DbSet<EmpRole> EmpRole { get; set; }
        public virtual DbSet<EmployePosition> EmployePosition { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageElement> PageElement { get; set; }
        public virtual DbSet<PageElements> PageElements { get; set; }
        public virtual DbSet<PagePage> PagePage { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Power> Power { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleElement> RoleElement { get; set; }
        public virtual DbSet<RoleFile> RoleFile { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<RoleOpretion> RoleOpretion { get; set; }
        public virtual DbSet<RolePage> RolePage { get; set; }
        public virtual DbSet<RolePower> RolePower { get; set; }
        public virtual DbSet<Shareholder> Shareholder { get; set; }
        public virtual DbSet<UserUsergroup> UserUsergroup { get; set; }
        public virtual DbSet<UsergroupRole> UsergroupRole { get; set; }
        public virtual DbSet<FlowGraph> FlowGraph { get; set; }
        public virtual DbSet<FlowGroup> FlowGroup { get; set; }
        public virtual DbSet<FlowEdge> FlowEdge { get; set; }
        public virtual DbSet<FlowClipCfg> FlowClipCfg { get; set; }
        public virtual DbSet<FlowCss> FlowCss { get; set; }
        public virtual DbSet<FlowEdgeLoopCfg> FlowEdgeLoopCfg { get; set; }
        public virtual DbSet<FlowFun> FlowFun { get; set; }
        public virtual DbSet<FlowGroupTitle> FlowGroupTitle { get; set; }
        public virtual DbSet<FlowIcon> FlowIcon { get; set; }
        public virtual DbSet<FlowLabelCfgs> FlowLabelCfgs { get; set; }
        public virtual DbSet<FlowLayout> FlowLayout { get; set; }
        public virtual DbSet<FlowNode> FlowNode { get; set; }
        public virtual DbSet<FlowStyle> FlowStyle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbContextOptionsBuilder = optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WeixiaoSys;User ID=sa;Password=123;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_AREA")
                    .IsClustered(false);

                entity.ToTable("area");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnName("district")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_AREA_AREA_AREA_AREA");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_COMPANY")
                    .IsClustered(false);

                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Briefing)
                    .HasColumnName("briefing")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Isdel).HasColumnName("isdel");

                entity.Property(e => e.LegalPerson)
                    .IsRequired()
                    .HasColumnName("legal_person")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_COMPANY_COM_COM_COMPANY");
            });

            modelBuilder.Entity<CompanyLog>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_COMPANY_LOG")
                    .IsClustered(false);

                entity.ToTable("company_log");

                entity.HasComment("公司变动表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("date");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.CompanyLog)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPANY__COM_COMLO_COMPANY");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_DEPARTMENT")
                    .IsClustered(false);

                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.DepCode)
                    .IsRequired()
                    .HasColumnName("dep_code")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasColumnName("dep_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isdel).HasColumnName("isdel");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEPARTME_DEP_COMPA_COMPANY");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_DEPARTME_DEP_DEP_DEPARTME");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ELEMENT")
                    .IsClustered(false);

                entity.ToTable("element");

                entity.HasComment("页面元素表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId)
                    .HasColumnName("operation_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Element)
                    .HasForeignKey(d => d.OperationId)
                    .HasConstraintName("FK_ELEMENT_OPERATION_OPERATIO");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Element)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENT_ELEMENT_E_ELEMENT_");
            });

            modelBuilder.Entity<ElementElement>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ELEMENT_ELEMENT")
                    .IsClustered(false);

                entity.ToTable("element_element");

                entity.HasComment("元素嵌套表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CelementId).HasColumnName("celement_id");

                entity.Property(e => e.PelementId).HasColumnName("pelement_id");

                entity.HasOne(d => d.Celement)
                    .WithMany(p => p.ElementElementCelement)
                    .HasForeignKey(d => d.CelementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENT__CELEMENT__ELEMENT");

                entity.HasOne(d => d.Pelement)
                    .WithMany(p => p.ElementElementPelement)
                    .HasForeignKey(d => d.PelementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENT__PELEMENT__ELEMENT");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ELEMENT_TYPE")
                    .IsClustered(false);

                entity.ToTable("element_type");

                entity.HasComment("元素类型表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpGroup>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_EMP_GROUP")
                    .IsClustered(false);

                entity.ToTable("emp_group");

                entity.HasComment("用户组");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpRole>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_EMP_ROLE")
                    .IsClustered(false);

                entity.ToTable("emp_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmpRole)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMP_ROLE_EMP_EMPRO_EMPLOYEE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EmpRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMP_ROLE_ROLE_EMPR_ROLE");
            });

            modelBuilder.Entity<EmployePosition>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_EMPLOYE_POSITION")
                    .IsClustered(false);

                entity.ToTable("employe_position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.PosId).HasColumnName("pos_id");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployePosition)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYE__EMP_EMPPO_EMPLOYEE");

                entity.HasOne(d => d.Pos)
                    .WithMany(p => p.EmployePosition)
                    .HasForeignKey(d => d.PosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYE__POS_EMPPO_POSITION");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_EMPLOYEE")
                    .IsClustered(false);

                entity.ToTable("employee");

                entity.HasComment("员工表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Enddate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasColumnName("idcard")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Isuse).HasColumnName("isuse");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasColumnName("login_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPwd)
                    .IsRequired()
                    .HasColumnName("login_pwd")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Qq).HasColumnName("qq");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Stardate)
                    .HasColumnName("stardate")
                    .HasColumnType("date");

                entity.Property(e => e.Wechar)
                    .HasColumnName("wechar")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_USER_DEP_DEPARTME");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_FILES")
                    .IsClustered(false);

                entity.ToTable("files");

                entity.HasComment("文件表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasColumnName("file_path")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_MENU")
                    .IsClustered(false);

                entity.ToTable("menu");

                entity.HasComment("菜单表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Acl)
                    .HasColumnName("acl")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Badge).HasColumnName("badge");

                entity.Property(e => e.BadgeDot)
                    .HasColumnName("badgeDot")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BadgeStatus)
                    .HasColumnName("badgeStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.ExternalLink)
                    .HasColumnName("externalLink")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Hide).HasColumnName("hide");

                entity.Property(e => e.HideInBreadcrumb)
                    .HasColumnName("hideInBreadcrumb")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.I18n)
                    .HasColumnName("i18n")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.Pagepageid).HasColumnName("pagepageid");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Reuse)
                    .HasColumnName("reuse")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shortcut)
                    .HasColumnName("shortcut")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortcutRoot)
                    .HasColumnName("shortcutRoot")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Target)
                    .HasColumnName("target")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("FK_MENU_PAGE_MENU_PAGE");

                entity.HasOne(d => d.Pagepage)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.Pagepageid)
                    .HasConstraintName("FK_MENU_PAGES_MEN_PAGE_PAG");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_MENU_MENU_MENU_MENU");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_OPERATION")
                    .IsClustered(false);

                entity.ToTable("operation");

                entity.HasComment("功能操作表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Pid).HasColumnName("pid");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_PAGE")
                    .IsClustered(false);

                entity.ToTable("page");

                entity.HasComment("页面元素表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageElement>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_PAGE_ELEMENT")
                    .IsClustered(false);

                entity.ToTable("page_element");

                entity.HasComment("页面元素对照表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ElementId).HasColumnName("element_id");

                entity.Property(e => e.PagId).HasColumnName("pag_id");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PageElement)
                    .HasForeignKey<PageElement>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAGE_ELE_REFERENCE_ELEMENT");

                entity.HasOne(d => d.Pag)
                    .WithMany(p => p.PageElement)
                    .HasForeignKey(d => d.PagId)
                    .HasConstraintName("FK_PAGE_ELE_REFERENCE_PAGE");
            });

            modelBuilder.Entity<PageElements>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_PAGE_ELEMENTS")
                    .IsClustered(false);

                entity.ToTable("page_elements");

                entity.HasComment("元素嵌套与页面对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ElementsId).HasColumnName("elements_id");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.HasOne(d => d.Elements)
                    .WithMany(p => p.PageElements)
                    .HasForeignKey(d => d.ElementsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAGE_ELE_PAGEELEME_ELEMENT_");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageElements)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAGE_ELE_PAGEELEME_PAGE");
            });

            modelBuilder.Entity<PagePage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_PAGE_PAGE")
                    .IsClustered(false);

                entity.ToTable("page_page");

                entity.HasComment("页面嵌套表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CpageId).HasColumnName("cpage_id");

                entity.Property(e => e.PpageId).HasColumnName("ppage_id");

                entity.HasOne(d => d.Cpage)
                    .WithMany(p => p.PagePageCpage)
                    .HasForeignKey(d => d.CpageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAGE_PAG_CPAGE_PAG_PAGE");

                entity.HasOne(d => d.Ppage)
                    .WithMany(p => p.PagePagePpage)
                    .HasForeignKey(d => d.PpageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAGE_PAG_PPAGE_PAG_PAGE");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_POSITION")
                    .IsClustered(false);

                entity.ToTable("position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Describes)
                    .HasColumnName("describes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Power>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_POWER")
                    .IsClustered(false);

                entity.ToTable("power");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE")
                    .IsClustered(false);

                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleElement>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_ELEMENT")
                    .IsClustered(false);

                entity.ToTable("role_element");

                entity.HasComment("页面元素权限对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ElementId).HasColumnName("element_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.RoleElement)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_ELE_ELEMENT_E_ELEMENT");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.RoleElement)
                    .HasForeignKey(d => d.PowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_ELE_ROLE_ELEM_POWER");
            });

            modelBuilder.Entity<RoleFile>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_FILE")
                    .IsClustered(false);

                entity.ToTable("role_file");

                entity.HasComment("文件权限对照表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.RoleFile)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_FIL_FILE_FILE_FILES");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.RoleFile)
                    .HasForeignKey<RoleFile>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_FIL_ROLE_FILE_POWER");
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_MENU")
                    .IsClustered(false);

                entity.ToTable("role_menu");

                entity.HasComment("菜单权限对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_MEN_MENU_ROLE_MENU");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.PowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_MEN_ROLE_MENU_POWER");
            });

            modelBuilder.Entity<RoleOpretion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_OPRETION")
                    .IsClustered(false);

                entity.ToTable("role_opretion");

                entity.HasComment("功能权限对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OpeId).HasColumnName("ope_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.Ope)
                    .WithMany(p => p.RoleOpretion)
                    .HasForeignKey(d => d.OpeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_OPR_OPRETION__OPERATIO");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.RoleOpretion)
                    .HasForeignKey(d => d.PowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_OPR_ROLE_OPRE_POWER");
            });

            modelBuilder.Entity<RolePage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_PAGE")
                    .IsClustered(false);

                entity.ToTable("role_page");

                entity.HasComment("页面权限对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.RolePage)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_PAG_PAGE_PAGE_PAGE");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.RolePage)
                    .HasForeignKey(d => d.PowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_PAG_ROLE_PAGE_POWER");
            });

            modelBuilder.Entity<RolePower>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ROLE_POWER")
                    .IsClustered(false);

                entity.ToTable("role_power");

                entity.HasComment("角色权限表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.RolePower)
                    .HasForeignKey(d => d.PowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_POW_POWER_ROL_POWER");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePower)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_POW_ROLE_ROLE_ROLE");
            });

            modelBuilder.Entity<Shareholder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_SHAREHOLDER")
                    .IsClustered(false);

                entity.ToTable("shareholder");

                entity.HasComment("股东表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayMoney)
                    .HasColumnName("pay_money")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Proportion)
                    .HasColumnName("proportion")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Shareholder)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK_SHAREHOL_COMPANY_C_COMPANY");
            });

            modelBuilder.Entity<UserUsergroup>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_USER_USERGROUP")
                    .IsClustered(false);

                entity.ToTable("user_usergroup");

                entity.HasComment("用户组用户对照表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.UserUsergroup)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_USER_USE_USER_USER_EMPLOYEE");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserUsergroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_USE_USERGROUP_EMP_GROU");
            });

            modelBuilder.Entity<UsergroupRole>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_USERGROUP_ROLE")
                    .IsClustered(false);

                entity.ToTable("usergroup_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UsergroupRole)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERGROU_USERGROUP_EMP_GROU");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsergroupRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERGROU_ROLE_GROU_ROLE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
