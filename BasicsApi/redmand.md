
//EFCore DB First ����
        //��һ����Install-Package Microsoft.EntityFrameworkCore.SqlServer -version 2.1.1
 
        //�ڶ�����Install-Package Microsoft.EntityFrameworkCore.Tools -version 2.1.1
 
        //��������Install-Package Microsoft.EntityFrameworkCore.Design -version 2.1.1
 
        //���Ĳ���Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design
 
        //���岽��Scaffold-DbContext -Connection "Data Source=139.159.252.186;Initial Catalog=DB;User ID=sa;Password=sa186!@#;MultipleActiveResultSets=true" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir "EntityModel"
 
        //����������ʵ��:Scaffold-DbContext -Connection "Data Source=139.159.252.186;Initial Catalog=DB;User ID=sa;Password=sa186!@#;MultipleActiveResultSets=true" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir "EntityModel" -Force
//�������Nuget2.0.1;
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=test;User ID=sa;Password=123;MultipleActiveResultSets=true" "Microsoft.EntityFrameworkCore.SqlServer" -o Models


-o ���Ŀ¼(-OutputDir)
-f ���������ļ�(-Force),���ݿ����ʱ���õ�
-t ָ������(-Tables)
����м��������ȷ����Ĺ����ļ����Ա���ɹ�����ʹ��-f�������������ļ�
 Scaffold-DbContext "Server=127.0.0.1;port=3306;Database=test;uid=root;pwd=123;Character Set=utf8;" Pomelo.EntityFrameworkCore.MySql -o Models
dotnet ef dbcontext scaffold "Server=127.0.0.1;port=3306;Database=test;uid=root;pwd=123;Character Set=utf8;" Pomelo.EntityFrameworkCore.MySql -o Models
