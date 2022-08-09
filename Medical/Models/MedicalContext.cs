using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Medical.Models
{
    public partial class MedicalContext : DbContext
    {
        public MedicalContext()
        {
        }

        public MedicalContext(DbContextOptions<MedicalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administarator> Administarators { get; set; }
        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<AdvertiseStatue> AdvertiseStatues { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleComment> ArticleComments { get; set; }
        public virtual DbSet<CaseRecord> CaseRecords { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ClinicDetail> ClinicDetails { get; set; }
        public virtual DbSet<ClinicRoom> ClinicRooms { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Newscategory> Newscategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Orderstate> Orderstates { get; set; }
        public virtual DbSet<OtherProductImage> OtherProductImages { get; set; }
        public virtual DbSet<Paytype> Paytypes { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RatingDoctor> RatingDoctors { get; set; }
        public virtual DbSet<RatingType> RatingTypes { get; set; }
        public virtual DbSet<Reserve> Reserves { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<ShipType> ShipTypes { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<TreatmentDetail> TreatmentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Medical;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Administarator>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("Administarator");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminAccount).HasMaxLength(50);

                entity.Property(e => e.AdminName).HasMaxLength(50);

                entity.Property(e => e.AdminPassword).HasMaxLength(50);
            });

            modelBuilder.Entity<Advertise>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK_Adverrise");

                entity.ToTable("Advertise");

                entity.Property(e => e.AdPicturePath).HasMaxLength(50);

                entity.Property(e => e.AdTitle).HasMaxLength(50);

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Advertises)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Adverrise_Administarator");

                entity.HasOne(d => d.Adstatue)
                    .WithMany(p => p.Advertises)
                    .HasForeignKey(d => d.AdstatueId)
                    .HasConstraintName("FK_Adverrise_Advertisestatue");
            });

            modelBuilder.Entity<AdvertiseStatue>(entity =>
            {
                entity.HasKey(e => e.AdstatueId)
                    .HasName("PK_Advertisestatue");

                entity.ToTable("AdvertiseStatue");

                entity.Property(e => e.Adstatue)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.ArPicturePath).HasMaxLength(50);

                entity.Property(e => e.Articeltitle).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasMaxLength(50);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Article_Administarator");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Article_Doctor");
            });

            modelBuilder.Entity<ArticleComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_ArticalComment");

                entity.ToTable("ArticleComment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleComments)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_ArticleComment_Article");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ArticleComments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_ArticleComment_Member");
            });

            modelBuilder.Entity<CaseRecord>(entity =>
            {
                entity.HasKey(e => e.CaseId);

                entity.ToTable("CaseRecord");

                entity.Property(e => e.CaseId).HasColumnName("CaseID");

                entity.Property(e => e.DiagnosticRecord).HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ReserveId).HasColumnName("ReserveID");

                entity.Property(e => e.TreatmentDetailId).HasColumnName("TreatmentDetailID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CaseRecords)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseRecord_Member");

                entity.HasOne(d => d.Reserve)
                    .WithMany(p => p.CaseRecords)
                    .HasForeignKey(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseRecord_Reserve");

                entity.HasOne(d => d.TreatmentDetail)
                    .WithMany(p => p.CaseRecords)
                    .HasForeignKey(d => d.TreatmentDetailId)
                    .HasConstraintName("FK_CaseRecord_TreatmentDetail");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClinicDetail>(entity =>
            {
                entity.ToTable("ClinicDetail");

                entity.Property(e => e.ClinicDetailId).HasColumnName("ClinicDetailID");

                entity.Property(e => e.ClinicDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ClinicDetails)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_ClinicDetail_Department");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ClinicDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_ClinicDetail_Doctor");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.ClinicDetails)
                    .HasForeignKey(d => d.PeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClinicDetail_Period");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ClinicDetails)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_ClinicDetail_ClinicRoom");
            });

            modelBuilder.Entity<ClinicRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("ClinicRoom");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomName).HasMaxLength(50);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");
            });

            modelBuilder.Entity<CouponDetail>(entity =>
            {
                entity.ToTable("CouponDetail");

                entity.Property(e => e.CouponDetailId).HasColumnName("CouponDetailID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetail_Coupon");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DeptName).HasMaxLength(50);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DoctorName).HasMaxLength(50);

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.JobTitle).HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.PicturePath).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Doctor_Department");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Doctor_Member");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("Experience");

                entity.Property(e => e.ExperienceId).HasColumnName("ExperienceID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Experience1).HasColumnName("Experience");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Experiences)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experience_Doctor");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("GenderID");

                entity.Property(e => e.Gender1)
                    .HasMaxLength(50)
                    .HasColumnName("Gender");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.BloodType).HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IcCardNo)
                    .HasMaxLength(50)
                    .HasColumnName("IC_Card_No");

                entity.Property(e => e.IdentityId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IdentityID");

                entity.Property(e => e.MemberName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK_Member_RoleType");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NewsCategoryId).HasColumnName("NewsCategoryID");

                entity.Property(e => e.NewsPicturePath).HasMaxLength(50);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_News_Administarator");

                entity.HasOne(d => d.NewsCategory)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.NewsCategoryId)
                    .HasConstraintName("FK_News_Newscategory");
            });

            modelBuilder.Entity<Newscategory>(entity =>
            {
                entity.ToTable("Newscategory");

                entity.Property(e => e.NewsCategoryId).HasColumnName("NEwsCategoryID");

                entity.Property(e => e.NewsCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStateId).HasColumnName("OrderStateID");

                entity.Property(e => e.PayTypeId).HasColumnName("PayTypeID");

                entity.Property(e => e.ShipAddress).HasMaxLength(50);

                entity.Property(e => e.ShipTypeId).HasColumnName("ShipTypeID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Order_City");

                entity.HasOne(d => d.CouponDetail)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponDetailId)
                    .HasConstraintName("FK_Order_CouponDetail");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");

                entity.HasOne(d => d.OrderState)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStateId)
                    .HasConstraintName("FK_Order_Orderstate");

                entity.HasOne(d => d.PayType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PayTypeId)
                    .HasConstraintName("FK_Order_Paytype");

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipTypeId)
                    .HasConstraintName("FK_Order_ShipType");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Orderstate>(entity =>
            {
                entity.ToTable("Orderstate");

                entity.Property(e => e.OrderStateId).HasColumnName("OrderStateID");

                entity.Property(e => e.OrderState1)
                    .HasMaxLength(50)
                    .HasColumnName("OrderState");
            });

            modelBuilder.Entity<OtherProductImage>(entity =>
            {
                entity.ToTable("OtherProductImage");

                entity.Property(e => e.OtherProductImageId).HasColumnName("OtherProductImageID");

                entity.Property(e => e.OtherProductPhoto)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OtherProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OtherProductImage_Product");
            });

            modelBuilder.Entity<Paytype>(entity =>
            {
                entity.ToTable("Paytype");

                entity.Property(e => e.PayTypeId).HasColumnName("PayTypeID");

                entity.Property(e => e.PayType1)
                    .HasMaxLength(50)
                    .HasColumnName("PayType");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("Period");

                entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

                entity.Property(e => e.PeriodDetail)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Discontinued).HasColumnName("discontinued");

                entity.Property(e => e.ProductBrandId).HasColumnName("ProductBrandID");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ProductBrand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductBrand");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.ToTable("ProductBrand");

                entity.Property(e => e.ProductBrandId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductBrandID");

                entity.Property(e => e.ProductBrandName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.ProductCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductSpecification>(entity =>
            {
                entity.ToTable("ProductSpecification");

                entity.Property(e => e.ProductSpecificationId).HasColumnName("ProductSpecificationID");

                entity.Property(e => e.ProductAppearance)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductColor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductImage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductMaterial)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSpecifications)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSpecification_Product");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            });

            modelBuilder.Entity<RatingDoctor>(entity =>
            {
                entity.ToTable("RatingDoctor");

                entity.Property(e => e.RatingDoctorId).HasColumnName("RatingDoctorID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.RatingTypeId).HasColumnName("RatingTypeID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.RatingDoctors)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RatingDoctor_Doctor");

                entity.HasOne(d => d.RatingType)
                    .WithMany(p => p.RatingDoctors)
                    .HasForeignKey(d => d.RatingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RatingDoctor_RatingType");
            });

            modelBuilder.Entity<RatingType>(entity =>
            {
                entity.ToTable("RatingType");

                entity.Property(e => e.RatingTypeId).HasColumnName("RatingTypeID");
            });

            modelBuilder.Entity<Reserve>(entity =>
            {
                entity.ToTable("Reserve");

                entity.Property(e => e.ReserveId).HasColumnName("ReserveID");

                entity.Property(e => e.ClinicDetailId).HasColumnName("ClinicDetailID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.RemarkAdmin).HasColumnName("Remark_Admin");

                entity.Property(e => e.RemarkPatient).HasColumnName("Remark_Patient");

                entity.Property(e => e.ReserveDate).HasColumnType("datetime");

                entity.Property(e => e.SequenceNumber).HasColumnName("sequence_number");

                entity.HasOne(d => d.ClinicDetail)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.ClinicDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserve_ClinicDetail1");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserve_Member");

                entity.HasOne(d => d.SourceNavigation)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.Source)
                    .HasConstraintName("FK_Reserve_Source");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK_Reserve_State");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RatingTypeId).HasColumnName("RatingTypeID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Product");

                entity.HasOne(d => d.RatingType)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.RatingTypeId)
                    .HasConstraintName("FK_Review_RatingType");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.HasKey(e => e.Role);

                entity.ToTable("RoleType");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<ShipType>(entity =>
            {
                entity.ToTable("ShipType");

                entity.Property(e => e.ShipTypeId).HasColumnName("ShipTypeID");

                entity.Property(e => e.ShipType1)
                    .HasMaxLength(50)
                    .HasColumnName("ShipType");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Product");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("Source");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.Source1)
                    .HasMaxLength(50)
                    .HasColumnName("Source");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.State1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("State");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("Treatment");

                entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.TreatmentDetailId).HasColumnName("TreatmentDetailID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Treatment_Doctor");

                entity.HasOne(d => d.TreatmentDetail)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.TreatmentDetailId)
                    .HasConstraintName("FK_Treatment_TreatmentDetail");
            });

            modelBuilder.Entity<TreatmentDetail>(entity =>
            {
                entity.ToTable("TreatmentDetail");

                entity.Property(e => e.TreatmentDetailId).HasColumnName("TreatmentDetailID");

                entity.Property(e => e.TreatmentDetail1)
                    .HasMaxLength(50)
                    .HasColumnName("TreatmentDetail");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
