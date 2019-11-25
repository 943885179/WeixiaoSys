# 执行`dotnet tool install -g dotnetrsa` 下载dotmetrsa
# 生成密钥 `dotnetrsa gen -h` 查看帮助
-f 或者 --format：指需要生成的格式，可以为 xml、pkcs1、pkcs8

-s 或者 --size ：指需要生成的秘钥长度，默认为2048

--pem ：只是否带有pem格式，值为 true 或者 false，默认为 false

-o 或者 --output ：指秘钥保存的路径，默认保存在当前文件夹
> 生成密钥测试：`dotnetrsa gen -s 2048 -f xml`

#转换秘钥` dontetrsa convert -h` 查看帮助
KeyFilePath：指定被转换的秘钥的路径

-f 或者 --from ：该字段为必须指定，指被转换的秘钥的格式，值只能为 xml、pkcs1、pkcs8

-t 或者 --to：该字段为必须指定，指需要转换的目标格式，值只能为 xml、pkcs1、pkcs8

-k ：指被转换的秘钥的类型，公钥或者私钥，值只能为 pri 或者 pub ，分别代表公钥和私钥

-o ：转换的秘钥的输出路径，默认为当前目录

>将一个格式为xml的私钥转换为pkcs1，示例：`dotnetrsa convert F:\WeixiaoSys\xml_private.key -f xml -t pkcs1 -k pri` 