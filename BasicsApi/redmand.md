
//EFCore DB First 步骤
        //第一步：Install-Package Microsoft.EntityFrameworkCore.SqlServer -version 2.1.1

        //第二步：Install-Package Microsoft.EntityFrameworkCore.Tools -version 2.1.1

        //第三部：Install-Package Microsoft.EntityFrameworkCore.Design -version 2.1.1

        //第四部：Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design

        //第五步：Scaffold-DbContext -Connection "Data Source=139.159.252.186;Initial Catalog=DB;User ID=sa;Password=sa186!@#;MultipleActiveResultSets=true" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir "EntityModel"

        //第六部更新实体:Scaffold-DbContext -Connection "Data Source=139.159.252.186;Initial Catalog=DB;User ID=sa;Password=sa186!@#;MultipleActiveResultSets=true" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir "EntityModel" -Force
//或者添加Nuget2.0.1;
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=test;User ID=sa;Password=123;MultipleActiveResultSets=true" "Microsoft.EntityFrameworkCore.SqlServer" -o Models


-o 输出目录(-OutputDir)
-f 覆盖现有文件(-Force),数据库更新时会用到
-t 指定表名(-Tables)
如果中间出错，请先确保你的工程文件可以编译成功，并使用-f参数覆盖现有文件
 Scaffold-DbContext "Server=127.0.0.1;port=3306;Database=test;uid=root;pwd=123;Character Set=utf8;" Pomelo.EntityFrameworkCore.MySql -o Models
dotnet ef dbcontext scaffold "Server=127.0.0.1;port=3306;Database=test;uid=root;pwd=123;Character Set=utf8;" Pomelo.EntityFrameworkCore.MySql -o Models
