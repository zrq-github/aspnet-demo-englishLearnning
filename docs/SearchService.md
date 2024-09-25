# SearchService

搜索服务：
- 搜索当前音频中的文字

通过据库命令like（全表扫描）：
select * from t_episodeswhere Subtitle like '%hello%'   
也能进行搜索，性能低，耗时长。

SQLServer有全文检索

通过 Listening.Admin.WebAPI.EventHandlers 

## 项目基本结构

1. 搜搜服务器主要使用Elastic Search来完成音频原文的索引和搜索。这个服务没有划分领域层、基础设施层。只有SearchService.WebAPI.
2. Elastic Search是一个独立运行的服务器，对外提供HTTP接口。我们一般借助于封装好的Nuget爆NEST来调用Elastic Search的接口

## 监听事件

在英语听力后台的应用层，我们会在新建一个音频的时候发布一个 ListeningEpisode.Created ，编写一个监听这个继承事件的处理器 ListeningEpisodeUpsertEventHandler，然后把原文等数据写入Elatic Search。

