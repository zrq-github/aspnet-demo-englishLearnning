# ListeningServers

## Album

```CSharp
private readonly IMemoryCacheHelper _cacheHelper;
```

内存缓存，分布式缓存。  
分布式缓存，多个服务器公用一份缓冲。  

## Episode

Episode设计思路：
如果用户上传的音频不是M4A格式，程序则不会立即把这个音频插入数据库，而是把用户提交的数据先暂存到Redis中，然后在启动音频转码，再转码完成后，程序再把Redis中暂存的音频数据保存到数据库中。

Episode对象一直处于完整状态，也就是数据库中音频记录一定是可用的，代码就简单很多了。这就是DDD给系统设计带来的一个很大的好处。

## 事件推送

1. 为了能够让前端页面随时看到转码状态的变化，我们通过SignalR来把转码状态推送给前端。转码服务会在转码开始、转码失败、转码完成等事件出现的时候，发布名字分别为MediaEncoding.Started、MediaEncoding.Failed、MediaEncoding.Completed等集成事件，因此我们只要监听这些集成事件，然后把转码状态的变化推送到前端页面即可。
2. 我们新建音频的时候，还要通知搜索服务收录新建的音频的原文。我们在新建音频的时候，会发布领域事件EpisodeCreatedEvent，但是领域事件只能被微服务内的代码监听到，而搜索服务属于一个独立的微服务，因此我们需要监听EpisodeCreatedEvent这个领域事件，然后再发布一个集成事件。

