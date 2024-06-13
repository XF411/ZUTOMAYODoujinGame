一个ZUTOMAYO相关的独立游戏项目,
初步构想是平面的像素风格项目，
目前整体框架采用了ALEXTANGXIAO佬集成的GameFramework项目:
https://github.com/ALEXTANGXIAO/GameFramework-Next
以GameFramework为基础集成了
YooAsset
UniTask
luban
hybridclr等内容

虽然目前看起来是个纯单机项目应该用不到hybridclr，后续可能会移除此部分以精简项目。

git工作流:
main分支会作为最后的发布分支,
并用版本号创建tag，
主要开发工作会在develop分支进行，
重要分支开发会新开分支 feature/对应功能名称 开发完成经过测试后合并回dev